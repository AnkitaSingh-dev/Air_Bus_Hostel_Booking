using FelicityTicketPrint.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using TuesPechkin;

namespace FelicityTicketPrint.Controllers
{
    public class PrintController : ApiController
    {
        EmployeeDB empDB = new EmployeeDB();
        // GET: api/Print
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Print/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Print
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Print/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Print/5
        public void Delete(int id)
        {
        }

        public bool BookingDetails(Ticket ticket, bool includeFare)
        {
            var htmlBody = string.Empty;

            ticket.Tnc = string.IsNullOrEmpty(ticket.Tnc) ? ticket.Tnc : ticket.Tnc.Replace("\n", "<br />");
            ticket.Support = string.IsNullOrEmpty(ticket.Support) ? ticket.Support : ticket.Support.Replace("\n", "<br />");
            ticket.AgentName = string.IsNullOrEmpty(ticket.AgentName) ? "FELICITY TOURS & TRAVELS LLP" : ticket.AgentName.ToUpper();
            ticket.AgentAddress = string.IsNullOrEmpty(ticket.AgentAddress) ? "306, Powai Plaza, Hiranandani Gardens,<br />Central Avenue, Powai, Mumbai., Mumbai(400076).<br />State: Maharashtra(State Code: 27), Country: India.<br />" : ticket.AgentAddress.Replace("\n", "<br />");
            ticket.AgentContact = string.IsNullOrEmpty(ticket.AgentContact) ? "(P) +91 22 2570 1618 <br /> (PAN) AAGFF6802K" : ticket.AgentContact.Replace("\n", "<br />");
            ticket.AdditonalDetails = string.IsNullOrEmpty(ticket.AdditonalDetails) ? " <p>(w)www.felicitytravels.in  <br />(e)accounts@felicitytravels.in</p><br /><br /><p> (GSTIN)27AAGFF6802K1ZA </p>": ticket.AdditonalDetails.Replace("\n", "<br />");

            if (includeFare)
            {
                var ticketfilePath = System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data/Ticket.html");
                htmlBody = File.ReadAllText(ticketfilePath, Encoding.UTF8);

                htmlBody = htmlBody.Replace("@TermsAndConditions", ticket.Tnc);
                htmlBody = htmlBody.Replace("@Support", ticket.Support);
                htmlBody = htmlBody.Replace("@TotalFare", ticket.TotalFare);
                htmlBody = htmlBody.Replace("@AgentsName", ticket.AgentName);
                htmlBody = htmlBody.Replace("@AgentAddress", ticket.AgentAddress);
                htmlBody = htmlBody.Replace("@AgentContact", ticket.AgentContact);
                htmlBody = htmlBody.Replace("@AdditionalDetails", ticket.AdditonalDetails);

                var Passenger = empDB.ListAll().ToList();

                if (Passenger != null && Passenger.Count > 0)
                {
                    string passengerListPNRTkt = "";
                    foreach (var x in Passenger)
                    {
                        passengerListPNRTkt += "<tr align='center'><td>" + x.Name + "</td ><td>" + x.Origin + " " + x.Departure + " " + x.FlightClass + "</td >" + "<td>" + x.FlightNo + "</td><td> " + x.TravelDate + " </ td ><td>" + x.DepTime + "</td ><td>" + x.ArrTime + "</td ><td>" + x.TicketNo + "</td ><td>" + x.AirlinePnr + "</td ><td>" + x.CSRPnr + "</td ></tr>";
                    }
                    htmlBody = htmlBody.Replace("@PassengerListPNRTicket", passengerListPNRTkt);
                }
            }
            else
            {
                var ticketfilePath = System.Web.HttpContext.Current.Server.MapPath(@"~/App_Data/TicketWoFare.html");
                htmlBody = File.ReadAllText(ticketfilePath, Encoding.UTF8);

                htmlBody = htmlBody.Replace("@TermsAndConditions", ticket.Tnc);
                htmlBody = htmlBody.Replace("@Support", ticket.Support);

                var Passenger = empDB.ListAll().ToList();

                if (Passenger != null && Passenger.Count > 0)
                {
                    string passengerListPNRTkt = "";
                    foreach (var x in Passenger)
                    {
                        passengerListPNRTkt += "<tr align='center'><td>" + x.Name + "</td ><td>" + x.Origin + " " + x.Departure + " " + x.FlightClass + "</td >" + "<td>" + x.FlightNo + "</td><td> " + x.TravelDate + " </ td ><td>" + x.DepTime + "</td ><td>" + x.ArrTime + "</td ><td>" + x.TicketNo + "</td ><td>" + x.AirlinePnr + "</td ><td>" + x.CSRPnr + "</td ></tr>";
                    }
                    htmlBody = htmlBody.Replace("@PassengerListPNRTicket", passengerListPNRTkt);

                }
            }
           return GeneratePDF(htmlBody);
        }

        public bool GeneratePDF(string HtmlBody)
        {
            var document = new HtmlToPdfDocument
            {
                GlobalSettings =
                {
                    ProduceOutline = true,
                    DocumentTitle = "Ticket",
                    PaperSize = PaperKind.A4, // Implicit conversion to PechkinPaperSize
                    Margins =
                            {
                                 All = 1.375,
                                 Unit = Unit.Centimeters
                            }
                },
                Objects = { new ObjectSettings { HtmlText = HtmlBody } }
            };

            var tempFolderDeployment = new TempFolderDeployment();
            var winAnyCPUEmbeddedDeployment = new WinAnyCPUEmbeddedDeployment(tempFolderDeployment);
            var remotingToolset = new RemotingToolset<PdfToolset>(winAnyCPUEmbeddedDeployment);

            var converter = new ThreadSafeConverter(remotingToolset);

            byte[] pdfBuf = converter.Convert(document);
            remotingToolset.Unload();

            try
            {
                string fn = string.Format("{0}.pdf", Path.GetTempFileName());

                FileStream fs = new FileStream(fn, FileMode.Create);
                fs.Write(pdfBuf, 0, pdfBuf.Length);
                fs.Close();

                Process myProcess = new Process();
                myProcess.StartInfo.FileName = fn;
                myProcess.Start();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
