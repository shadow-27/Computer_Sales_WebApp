using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using Computers_Sales_WebApplication.Models;

namespace Computers_Sales_WebApplication.Controllers
{
    public class ComputersController : Controller
    {
        // GET: Computers
        public ActionResult Index(DateTime? start, DateTime? end)
        {
            if (start > end)
            {
                Response.Write("Start Date is greater than Release");
            }

            string mainConn = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainConn))
            {

                sqlconn.Open();
                //   var query = "Select * from COMPUTERS where Release_Date Between '"+start+ "' and '" +end+ "' ";
                var query = "Select * from COMPUTERS where Release_Date Between @value1 and @value2";

                SqlCommand cmd = new SqlCommand(query, sqlconn);

                cmd.Parameters.AddWithValue("@value1", start ?? (object) DBNull.Value);
                cmd.Parameters.AddWithValue("@value2", end ?? (object) DBNull.Value);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                List<ComputersClass> lc = new List<ComputersClass>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lc.Add(new ComputersClass
                    {
                        Producent = dr["Producent"].ToString(),
                        Model = dr["Model"].ToString(),
                        ReleaseDate = Convert.ToDateTime(dr["Release_Date"]),
                        Price = Convert.ToInt32(dr["Price"])

                    });
                }

                sqlconn.Close();
                ModelState.Clear();
                return View(lc);

            }
        }
       // [HttpPost]
        public ActionResult Filter_Producents(string key, int? from, int? to)
        {

            string mainConn = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainConn))
            {

                sqlconn.Open();

                //   var query = "Select * from COMPUTERS where Release_Date Between '"+start+ "' and '" +end+ "' ";
                /*var query =
                    "Select Producent from COMPUTERS where Producent like %@key% and Price between @from and @to";*/
                string whereCondition = $"where Producent like '%{key}%' and Price>=@from and Price<=@to";
                string query = "SELECT Producent from COMPUTERS " + whereCondition;

                SqlCommand cmd = new SqlCommand(query, sqlconn);

              //  cmd.Parameters.AddWithValue("@key", key  (object) DBNull.Value);
                cmd.Parameters.AddWithValue("@from", from ?? (object) DBNull.Value);
                cmd.Parameters.AddWithValue("@to", to ?? (object) DBNull.Value);
                

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                List<ComputersClass> lc = new List<ComputersClass>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lc.Add(new ComputersClass
                    {
                        Producent = dr["Producent"].ToString()
                    });
                }

                sqlconn.Close();
                ModelState.Clear();
                return View(lc);
            }

        }
    }
}