using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Computers_Sales_WebApplication.Models;

namespace Computers_Sales_WebApplication.Controllers
{
    public class OrdersController : Controller
    {
       
        public ActionResult Index()
        {
            string mainConn = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainConn))
            {

                sqlconn.Open();
                //   var query = "Select * from COMPUTERS where Release_Date Between '"+start+ "' and '" +end+ "' ";
                var query = " SELECT * FROM ORDERS WHERE Order_ID > (SELECT MAX(Order_ID) - 5 FROM ORDERS)";

                SqlCommand cmd = new SqlCommand(query, sqlconn);



                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                List<OrdersModel> lc = new List<OrdersModel>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    lc.Add(new OrdersModel()
                    {
                        Order_ID = Convert.ToInt32(dr["Order_ID"]),
                        Order_Date = Convert.ToDateTime(dr["Order_Date"]),
                        Computer_ID = Convert.ToInt32(dr["Computer_ID"]),
                        Order_Value = Convert.ToInt32(dr["Order_Value"]),
                        Discount = Convert.ToDecimal(dr["Discount"]),
                        Amount = Convert.ToDouble(dr["Amount"])

                    });
                }

                sqlconn.Close();
                ModelState.Clear();
                return View(lc);
            }
        }



        // GET: Orders/Create
        public ActionResult Create()
        {

            return View(new OrdersModel());
        }

        // POST: Orders/Create
        [HttpPost]
        public ActionResult Create(OrdersModel order)
        {
            
                // TODO: Add insert logic here

                string mainConn = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
                using (SqlConnection sqlconn = new SqlConnection(mainConn))
                {
                    Int32 Price = 0;
                    Int32 Year = 2018;
                    decimal Discount = 0;


                    sqlconn.Open();
                    //   var query = "Select * from COMPUTERS where Release_Date Between '"+start+ "' and '" +end+ "' ";
                    var query1 =
                        " Insert into ORDERS Values(@Order_Date,@Computer_ID,@Order_Value,@Discount,@Amount)";

                    SqlCommand cmd = new SqlCommand(query1, sqlconn);
                    cmd.Parameters.AddWithValue("@Order_Date", order.Order_Date);
                    cmd.Parameters.AddWithValue("@Computer_ID", order.Computer_ID);
                    var query2 =
                        $"Select Price,year(Release_Date) from Computers where Computers.Computer_ID={order.Computer_ID}";
                    SqlCommand cmd2 = new SqlCommand(query2, sqlconn);
                    SqlDataReader reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {
                        Price = Convert.ToInt32(reader[0]);
                        Year = Convert.ToInt32(reader[1]);
                    }
                    reader.Close();
                    

                    cmd.Parameters.AddWithValue("@Order_Value", Price);
                    if ((order.Order_Date.Year - Year) > 2)
                    {
                        Discount = 0.2m;
                        cmd.Parameters.AddWithValue("@Discount", 0.2);
                    }

                    cmd.Parameters.AddWithValue("@Discount", 0);
                    var Amount = Price - Price * Discount;
                    cmd.Parameters.AddWithValue("@Amount", Amount);
                    cmd.ExecuteNonQuery();


                }

                return RedirectToAction("Index");
           
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            OrdersModel order = new OrdersModel();
            DataTable dt = new DataTable();
            string mainConn = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainConn))
            {

                sqlconn.Open();
                //   var query = "Select * from COMPUTERS where Release_Date Between '"+start+ "' and '" +end+ "' ";
                var query = " SELECT * FROM ORDERS WHERE Order_ID=@Order_ID";

               
                SqlDataAdapter sda = new SqlDataAdapter(query, sqlconn);
                sda.SelectCommand.Parameters.AddWithValue("@Order_ID", id);
                sda.Fill(dt);
            }

           if(dt.Rows.Count==1)
           {
               order.Order_ID = Convert.ToInt32(dt.Rows[0][0]);
               order.Order_Date = Convert.ToDateTime(dt.Rows[0][1]);
               order.Computer_ID = Convert.ToInt32(dt.Rows[0][2]);
               order.Order_Value = Convert.ToInt32(dt.Rows[0][3]);
               order.Discount = Convert.ToDecimal(dt.Rows[0][4]);
               order.Amount = Convert.ToDouble(dt.Rows[0][5]);
               return View(order);
            }



           return RedirectToAction("Index");
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(OrdersModel order)
        {
            string mainConn = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainConn))
            {
                sqlconn.Open();
                string query =
                    "Update Orders Set Order_Date=@Order_Date,Computer_ID=@Computer_ID,Order_Value=@Order_Value,Discount=@Discount,Amount=@Amount where Order_ID=@Order_ID";
                SqlCommand cmd = new SqlCommand(query, sqlconn);
                cmd.Parameters.AddWithValue("@Order_ID", order.Order_ID);
                cmd.Parameters.AddWithValue("@Order_Date", order.Order_Date);
                cmd.Parameters.AddWithValue("@Computer_ID", order.Computer_ID);
                cmd.Parameters.AddWithValue("@Order_Value", order.Order_Value);
                cmd.Parameters.AddWithValue("@Discount", order.Discount);
                cmd.Parameters.AddWithValue("@Amount", order.Amount);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            string mainConn = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
            using (SqlConnection sqlconn = new SqlConnection(mainConn))
            {
                sqlconn.Open();
                string query =
                    "Delete from Orders  where Order_ID=@Order_ID";
                SqlCommand cmd = new SqlCommand(query, sqlconn);
                cmd.Parameters.AddWithValue("@Order_ID",id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // POST: Orders/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
