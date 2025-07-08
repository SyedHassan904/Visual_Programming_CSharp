using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagementSystem
{
    public partial class UserDashBoard: Form
    {
        OleDbConnection con = new OleDbConnection();
        private Form1 form1;
        private string userEmail;
        public UserDashBoard(Form1 form1, string userEmail)
        {
            InitializeComponent();
            this.form1 = form1;
            this.userEmail = userEmail;
            MessageBox.Show("Welcome " + this.userEmail + " !!");
            string dbPath = System.IO.Path.Combine(Application.StartupPath, "PharmacyDB.accdb");
            //con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\syedh\\Documents\\PharmacyDB.accdb";
            con.ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}";
        }

        private void UserDashBoard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchtxt.Text))
            {
                MessageBox.Show("Please Enter the Medicine or Category.");
                return;
            }
            OleDbCommand viewcmd = new OleDbCommand();
            viewcmd.Connection = con;
            viewcmd.CommandText = "select * from inventory where name=@medName or category=@categoryName";
            viewcmd.Parameters.AddWithValue("@medName", searchtxt.Text);
            viewcmd.Parameters.AddWithValue("@categoryName", searchtxt.Text);
            con.Open();
            DataTable dt = new DataTable();
            dt.Load(viewcmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.Columns["numbers_leafs_per_box"].DefaultCellStyle.Format = "F2";
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchtxt.Text) || string.IsNullOrWhiteSpace(selltxt.Text))
                {
                    MessageBox.Show("Please Enter the Medicine name and sell quantity ");
                    return;
                }

                OleDbCommand selectCmd = new OleDbCommand();
                selectCmd.Connection = con;
                selectCmd.CommandText = "select quantity, quantity_pills_per_leaf, numbers_leafs_per_box FROM inventory WHERE name =@medName";
                selectCmd.Parameters.AddWithValue("@medName", searchtxt.Text);
                con.Open();
                OleDbDataReader reader = selectCmd.ExecuteReader();
                if (reader.Read())
                {
                    int currentQuantity = reader.GetInt32(0);
                    int currentPillsPerLeaf = reader.GetInt32(1);
                    double currentLeafsPerBox = reader.GetFloat(2);
                    reader.Close();
                    int pillsSold = Convert.ToInt32(selltxt.Text);
                    if(pillsSold<0)
                    {
                        MessageBox.Show("Please enter postive number");
                        return;
                    }
                    if (pillsSold > currentQuantity)
                    {
                        MessageBox.Show("Not enough stock available.");
                        con.Close();
                        return;
                    }
                    double leafsSold = Math.Round((double)pillsSold / currentPillsPerLeaf, 2);

                    OleDbCommand sellCmd = new OleDbCommand();
                    sellCmd.Connection = con;
                    sellCmd.CommandText = @"UPDATE inventory SET quantity = quantity - @quantity, numbers_leafs_per_box = numbers_leafs_per_box - @leafs WHERE name = @medName";
                    sellCmd.Parameters.AddWithValue("@quantity", Convert.ToInt32(selltxt.Text));
                    sellCmd.Parameters.AddWithValue("@leafs", leafsSold);
                    sellCmd.Parameters.AddWithValue("@medName", searchtxt.Text);
                    int result = sellCmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Medicine sells scuccessfully");
                        con.Close();
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText = "select * from inventory where name=@name";
                        cmd.Parameters.AddWithValue("@name", searchtxt.Text);
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        dataGridView1.DataSource = dt;
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.ScrollBars = ScrollBars.None;
                        dataGridView1.Columns["numbers_leafs_per_box"].DefaultCellStyle.Format = "F2";
                        con.Close();
                        selltxt.Clear();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Medicine not found in inventory.");
               
                }




            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);   
            }
            finally
            {
                con.Close() ;
            }


        }
    }
}
