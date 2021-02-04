using Day3WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Day3WebAPI.Controllers
{
    public class DepartmentController : ApiController
    {

        static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-0NGMPOG;Initial Catalog=CompanyDB;Trusted_Connection=True;");

        public HttpResponseMessage Get() {
            List<Department> departments = new List<Department>();

            using (connection) {
                SqlCommand selectComm = new SqlCommand("SELECT * FROM Departments", connection);
                connection.Open();

                SqlDataReader reader = selectComm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            id = reader.GetInt32(0),
                            departmentName = reader.GetString(1),
                            salary = reader.GetInt32(2)
                        });
                    }
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, departments);
                }
                else { 
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.NoContent, "No Content");
                }
            }
        }

    }
}
