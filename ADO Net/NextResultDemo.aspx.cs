using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADO_Net
{
    public partial class NextResultDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String connectionS = ConfigurationManager.ConnectionStrings["csTOProduct"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionS))
            {
                SqlCommand cmd = new SqlCommand("Select * from Students; select * from Customers;", con);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    StudnetsGridView.DataSource = rdr;
                    StudnetsGridView.DataBind();
                    while (rdr.NextResult()) { 
                    CustomersGridView.DataSource = rdr;
                    CustomersGridView.DataBind();
                    }
                }
            }
        }
    }
}