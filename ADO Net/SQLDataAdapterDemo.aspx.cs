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
    public partial class SQLDataAdapterDemo : System.Web.UI.Page
    {
        String connectionS = ConfigurationManager.ConnectionStrings["csTOProduct"].ConnectionString;
        DataSet ds;
        static int flag=1;
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();

        }
        public void getData()
        {
            using (SqlConnection con = new SqlConnection(connectionS))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM PRODUCTS_NEW", con);
                SqlDataAdapter da1 = new SqlDataAdapter("SELECT * FROM STUDENTS", con);

                ds = new DataSet();
                ds.Tables.Add(new DataTable("Products_New"));
                ds.Tables.Add(new DataTable("Students"));

                da.Fill(ds.Tables["Products_New"]);
                da1.Fill(ds.Tables["Students"]);
                GridView1.DataSource = ds.Tables["Products_New"];
                GridView1.DataBind();

                Response.Write(ds.Tables.Count);
                //da.SelectCommand = new SqlCommand("SELECT * FROM PRODUCTS_NEW");

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (flag==1)
            {
                GridView1.DataSource = ds.Tables["Students"];
                GridView1.DataBind();
                flag=0;
            }
            else
            {
                GridView1.DataSource = ds.Tables["Products_New"];
                GridView1.DataBind();
                flag = 1;

            }
        }
    }
}