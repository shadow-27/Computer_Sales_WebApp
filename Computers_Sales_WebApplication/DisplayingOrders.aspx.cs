using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Computers_Sales_WebApplication
{
    public partial class DisplayingOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string mainConnection = ConfigurationManager.ConnectionStrings["DatabaseConn"].ConnectionString;
            string sql_query = "SELECT * from ORDERS";
            using (SqlConnection sqlConn = new SqlConnection(mainConnection))
            {
                try
                {
                    sqlConn.Open();
                    SqlCommand sqlCommand = new SqlCommand(sql_query, sqlConn);
                    SqlDataAdapter sdr = new SqlDataAdapter(sqlCommand);
                    DataTable dt = new DataTable();
                    sdr.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    sqlConn.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
    }
}