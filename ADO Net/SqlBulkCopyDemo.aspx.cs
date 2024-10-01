using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADO_Net
{
    public partial class SqlBulkCopyDemo : System.Web.UI.Page
    {
        String connectionS = ConfigurationManager.ConnectionStrings["csTOProduct"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Load data only on the initial page load
            {
                LoadSourceData();
            }

            if (Request.Form[Button1.UniqueID] != null) // Check if Button1 is clicked
            {
                PerformBulkCopy();
            }
        }

        private void LoadSourceData()
        {
            using (SqlConnection con = new SqlConnection(connectionS))
            {
                con.Open();
                SqlCommand sourceDataCommand = new SqlCommand("SELECT ProductID, ProductName, Price FROM ProductSource;", con);
                SqlDataReader sourceReader = sourceDataCommand.ExecuteReader();
                GridView1.DataSource = sourceReader;
                GridView1.DataBind();
                sourceReader.Close();
            }
        }

        private void PerformBulkCopy()
        {
            using (SqlConnection con = new SqlConnection(connectionS))
            {
                con.Open();

                // Read source data into a DataTable
                DataTable dataTable = new DataTable();
                using (SqlCommand sourceDataCommand = new SqlCommand("SELECT ProductID, ProductName, Price FROM ProductSource;", con))
                {
                    using (SqlDataReader sourceReader = sourceDataCommand.ExecuteReader())
                    {
                        dataTable.Load(sourceReader);
                    }
                }

                // Perform bulk copy to the destination table
                using (SqlConnection destinationConnection = new SqlConnection(connectionS))
                {
                    destinationConnection.Open();
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection))
                    {
                        bulkCopy.DestinationTableName = "dbo.ProductDestination";

                        try
                        {
                            // Write from the source DataTable to the destination.
                            bulkCopy.WriteToServer(dataTable);
                            Response.Write("Data copied to DestinationTable successfully.<br/>");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            Response.Write("Data not copied to DestinationTable successfully.<br/>");
                        }
                    }

                    // Check and display the data from the destination table
                    SqlCommand commandCheckData = new SqlCommand("SELECT ProductID, ProductName, Price FROM ProductDestination;", destinationConnection);
                    SqlDataReader destinationReader = commandCheckData.ExecuteReader();

                    GridView2.DataSource = destinationReader;
                    GridView2.DataBind();

                    destinationReader.Close();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Any additional logic for button click can be added here
        }
    }
}
