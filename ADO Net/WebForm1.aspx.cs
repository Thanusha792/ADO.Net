using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADO_Net
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String connectionS = ConfigurationManager.ConnectionStrings["csTOProduct"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionS))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP(10) * FROM PRODUCTS_NEW", con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable("PRODUCTS_NEW");
                    dt.Columns.Add("ProductID");
                    dt.Columns.Add("ProductName");
                    dt.Columns.Add("Price");
                    dt.Columns.Add("QuantityInStock");
                    dt.Columns.Add("Category");
                    while (reader.Read()) {
                        DataRow row = dt.NewRow();
                        row["ProductID"] = reader["ProductID"];
                        row["ProductName"] = reader["ProductName"];
                        row["Price"] = reader["Price"];
                        row["QuantityInStock"] = reader["QuantityInStock"];
                        row["Category"] = reader["Category"];
                        dt.Rows.Add(row);
                    }
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                };
                #region
                // Reads the top 5 rows
                //for (int i = 0; i < 5; i++)
                //{
                //    Label1.Text +=reader.Read().ToString()+"";
                //}
                //while (reader.Read()) {
                //    Label1.Text += "read";
                //}
                // reads the remianing 5 rows
                //GridView1.DataSource = reader;
                //GridView1.DataBind();
                #endregion
            }

        }
       
    }
}