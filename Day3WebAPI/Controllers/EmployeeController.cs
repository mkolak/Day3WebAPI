using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Day3WebAPI.Models;

namespace Day3WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        private SqlConnection connection;

        [Route("api/employee/")]
        [HttpGet]
        public HttpResponseMessage Get() {

            List<Employee> retVal = new List<Employee>();
            connection = new SqlConnection("Data Source=DESKTOP-0NGMPOG;Initial Catalog=CompanyDB;Trusted_Connection=True;");

            using (connection) {
                SqlCommand command = new SqlCommand("SELECT * FROM Employees;", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        retVal.Add(new Employee() { id = reader.GetInt32(0), 
                            firstName = reader.GetString(1),
                            lastName = reader.GetString(2), 
                            department = reader.GetString(3) });
                    }
                }
                reader.Close();
            }

            if (!retVal.Any()) return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");

            return Request.CreateResponse(HttpStatusCode.OK, retVal);
        }

        [Route("api/employee/")]
        [HttpGet]
        public HttpResponseMessage GetEmployeesByPosition(string position) {
            List<Employee> retVal = new List<Employee>();
            connection = new SqlConnection("Data Source=DESKTOP-0NGMPOG;Initial Catalog=CompanyDB;Trusted_Connection=True;");
            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Employees WHERE Department='" + position + "';", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        retVal.Add(new Employee() { id = reader.GetInt32(0), firstName = reader.GetString(1), lastName = reader.GetString(2), department = reader.GetString(3) });
                    }
                }
                reader.Close();
            }

            if (!retVal.Any()) return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");

            return Request.CreateResponse(HttpStatusCode.OK, retVal);
        }

        [Route("api/employee/")]
        [HttpPost]
        public HttpResponseMessage Post(Employee emp) {
            connection = new SqlConnection("Data Source=DESKTOP-0NGMPOG;Initial Catalog=CompanyDB;Trusted_Connection=True;");
            string dataStr = "(" + emp.id + ", '" + emp.firstName + "', '" + emp.lastName + "', '" + emp.department + "')";
            using (connection) {
                SqlCommand insertComm = new SqlCommand("INSERT INTO Employees VALUES " + dataStr + ";", connection);
                connection.Open();
                insertComm.ExecuteNonQuery();
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }

        [Route("api/employee/")]
        [HttpDelete]
        public HttpResponseMessage Delete([FromBody] int id) {
            connection = new SqlConnection("Data Source=DESKTOP-0NGMPOG;Initial Catalog=CompanyDB;Trusted_Connection=True;");
            using (connection)
            {
                SqlCommand selectComm = new SqlCommand("SELECT * FROM Employees WHERE EmployeeID = " + id + ";", connection);
                connection.Open();

                SqlDataReader reader = selectComm.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    SqlCommand deleteComm = new SqlCommand("DELETE FROM Employees WHERE EmployeeID = " + id + ";", connection);
                    deleteComm.ExecuteNonQuery();
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else {
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No content to delete");
                }    
            }
        }

    }
}
