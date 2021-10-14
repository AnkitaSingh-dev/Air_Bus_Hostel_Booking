using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FelicityTicketPrint.Models
{
    public class EmployeeDB
    {
        //declare connection string
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        //Return list of all Employees
        public List<Employee> ListAll()
        {
            List<Employee> lst = new List<Employee>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SelectEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new Employee
                    {
                        EmployeeID = Convert.ToInt32(rdr["EmployeeId"]),
                        Name = rdr["Name"].ToString(),
                        TicketNo = rdr["TicketNo"].ToString(),
                        AirlinePnr = rdr["AirlinePnr"].ToString(),
                        CSRPnr = rdr["CSRPnr"].ToString(),
                        TravelDate = rdr["TravelDate"].ToString(),
                        FlightNo = rdr["FlightNo"].ToString(),
                        FlightClass = rdr["FlightClass"].ToString(),
                        Origin = rdr["Origin"].ToString(),
                        DepTime = rdr["DepTime"].ToString(),
                        Departure = rdr["Departure"].ToString(),
                        ArrTime = rdr["ArrTime"].ToString()
                    });
                }
                return lst;
            }
        }
        //Method for Adding an Employee
        public int Add(Employee emp)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", emp.EmployeeID);
                com.Parameters.AddWithValue("@Name", emp.Name);
                com.Parameters.AddWithValue("@TicketNo", emp.TicketNo);
                com.Parameters.AddWithValue("@AirlinePnr", emp.AirlinePnr);
                com.Parameters.AddWithValue("@CSRPnr", emp.CSRPnr);
                com.Parameters.AddWithValue("@TravelDate", emp.TravelDate);
                com.Parameters.AddWithValue("@FlightClass", emp.FlightClass);
                com.Parameters.AddWithValue("@FlightNo", emp.FlightNo);
                com.Parameters.AddWithValue("@Origin", emp.Origin);
                com.Parameters.AddWithValue("@DepTime", emp.DepTime);
                com.Parameters.AddWithValue("@Departure", emp.Departure);
                com.Parameters.AddWithValue("@ArrTime", emp.ArrTime);
                com.Parameters.AddWithValue("@Action", "Insert");
                i = com.ExecuteNonQuery();
            }
            return i;
        }
        //Method for Updating Employee record
        public int Update(Employee emp)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("InsertUpdateEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", emp.EmployeeID);
                com.Parameters.AddWithValue("@Name", emp.Name);
                com.Parameters.AddWithValue("@TicketNo", emp.TicketNo);
                com.Parameters.AddWithValue("@AirlinePnr", emp.AirlinePnr);
                com.Parameters.AddWithValue("@CSRPnr", emp.CSRPnr);
                com.Parameters.AddWithValue("@TravelDate", emp.TravelDate);
                com.Parameters.AddWithValue("@FlightClass", emp.FlightClass);
                com.Parameters.AddWithValue("@FlightNo", emp.FlightNo);
                com.Parameters.AddWithValue("@Origin", emp.Origin);
                com.Parameters.AddWithValue("@DepTime", emp.DepTime);
                com.Parameters.AddWithValue("@Departure", emp.Departure);
                com.Parameters.AddWithValue("@ArrTime", emp.ArrTime);
                com.Parameters.AddWithValue("@Action", "Update");
                i = com.ExecuteNonQuery();
            }
            return i;
        }
        //Method for Deleting an Employee
        public int Delete(int ID)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteEmployee", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public void Delete()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("DeleteAll", con);
                com.CommandType = CommandType.StoredProcedure;
                com.ExecuteNonQuery();
            }
        }
    }
}