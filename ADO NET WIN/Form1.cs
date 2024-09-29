using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_NET_WIN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            String connectionS = "Data Source=THANUSHA\\SQLEXPRESS08;Database=Products;Integrated Security=True;Connect Timeout=30;";
            using (SqlConnection con = new SqlConnection(connectionS))
            {
                SqlCommand cmd = new SqlCommand("SELECT TOP(10) * FROM PRODUCTS", con);
                con.Open();
                dataGridView2.DataSource = cmd.ExecuteReader();

            }
        }
    }
}
