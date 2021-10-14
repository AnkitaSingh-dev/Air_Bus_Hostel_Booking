using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FelicityTicketPrint.Models
{

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string TicketNo { get; set; }
        public string AirlinePnr { get; set; }
        public string CSRPnr { get; set; }
        public string TravelDate { get; set; }
        public string FlightNo { get; set; }
        public string FlightClass { get; set; }
        public string Origin { get; set; }
        public string DepTime { get; set; }
        public string Departure { get; set; }
        public string ArrTime { get; set; }
    }

    public class Ticket
    {
        public string TotalFare { get; set; }
        public string Tnc { get; set; }
        public string Support { get; set; }
        public string AgentName { get; set; }
        public string AgentAddress { get; set; }
        public string AgentContact { get; set; }
        public string AdditonalDetails { get; set; }
    }
}