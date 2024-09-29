using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADO_Net
{
    public partial class DemoConnection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // String connectionS = "Data Source=THANUSHA\\SQLEXPRESS08;Database=Products;Integrated Security=True;Connect Timeout=30;";
            String connectionS = ConfigurationManager.ConnectionStrings["csTOProduct"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionS))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP(10) * FROM PRODUCTS_NEW", con);
                con.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["csTOProduct"].ConnectionString;
            //int productid = 23;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spAddProduct1", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // Define the output parameter for ProductID
                cmd.Parameters.AddWithValue("@ProductName", "Iphone10");
                cmd.Parameters.AddWithValue("@Price", 100.51);
                cmd.Parameters.AddWithValue("@QuantityInStock", 30);
                cmd.Parameters.AddWithValue("@Category", "Electronic");
                SqlParameter outParam = new SqlParameter();
                outParam.ParameterName = "@ProductID"; // Match the parameter name in the stored procedure
                outParam.SqlDbType = System.Data.SqlDbType.Int;
                outParam.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                // Add input parameters

                // Open the connection and execute the command
                con.Open();
                int i = cmd.ExecuteNonQuery(); // Execute the stored procedure

                // Retrieve the output value for ProductID
                String product_id =outParam.Value.ToString();

                // Display the result in a label
                Label1.Text = "Inserted Product ID: " + product_id + "  Rows affected: " + i;
                Console.Out.WriteLine(Label1.Text);


            }
        }
    }
}