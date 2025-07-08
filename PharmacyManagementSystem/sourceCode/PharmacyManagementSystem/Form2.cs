using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.OleDb;
namespace PharmacyManagementSystem
{
    public partial class DashBoardForm: Form
    {
        private Form1 form1;
        private string adminEmail;
        OleDbConnection con = new OleDbConnection();
        public DashBoardForm(Form1 form1, string adminEmail)
        {
            InitializeComponent();
            this.form1 = form1;
            this.adminEmail = adminEmail;
            MessageBox.Show("Welcome " +this.adminEmail + " !!");
            string dbPath = System.IO.Path.Combine(Application.StartupPath, "PharmacyDB.accdb");
            //con.ConnectionString= "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\syedh\\Documents\\PharmacyDB.accdb";
            con.ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}";
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            form1.Show();
            this.Close();
        }

        private void insertbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(medNametxt.Text) || string.IsNullOrWhiteSpace(categorytxt.Text) || string.IsNullOrWhiteSpace(quantitytxt.Text) || string.IsNullOrWhiteSpace(dateTimePicker1.Text) || string.IsNullOrWhiteSpace(pricetext.Text)
                || string.IsNullOrWhiteSpace(pillsInLeafTxt.Text) || string.IsNullOrWhiteSpace(leafPerBoxTxt.Text))
            {
                MessageBox.Show("Please complete all fields.");
                return;
            }

            try
            {
                OleDbCommand insrt = new OleDbCommand();
                insrt.Connection = con;
                insrt.CommandText = "insert into inventory(name,category,quantity,price,expiryDate,quantity_pills_per_leaf,numbers_leafs_per_box) values(@name,@category,@quantity,@price,@date,@pillsperleaf,@leafsPerBox)";
                insrt.Parameters.AddWithValue("@name", medNametxt.Text);
                insrt.Parameters.AddWithValue("@category", categorytxt.Text);
                insrt.Parameters.AddWithValue("@quantity", Convert.ToInt32(quantitytxt.Text));
                insrt.Parameters.AddWithValue("@price",Convert.ToInt32(pricetext.Text));
                insrt.Parameters.AddWithValue("@expiryDate",dateTimePicker1.Value);
                insrt.Parameters.AddWithValue("@quantity_pills_per_leaf", Convert.ToInt32(pillsInLeafTxt.Text));
                insrt.Parameters.AddWithValue("numbers_leafs_per_box", Convert.ToInt32(leafPerBoxTxt.Text));
                con.Open();
                int result= insrt.ExecuteNonQuery();
                if(result>0)
                {
                    MessageBox.Show("Data successfully inserted");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                medNametxt.Clear();
                categorytxt.Clear();
                quantitytxt.Clear();
                pricetext.Clear();
                pillsInLeafTxt.Clear();
                leafPerBoxTxt.Clear();
            }

        }

        private void viewBtn_Click(object sender, EventArgs e)
        {
            OleDbCommand viewcmd= new OleDbCommand();
            viewcmd.Connection = con;
            viewcmd.CommandText = "select * from inventory";
            con.Open();
            DataTable dt = new DataTable();
            dt.Load(viewcmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ScrollBars = ScrollBars.None;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(medNametxt.Text))
            {
                MessageBox.Show("Please Enter medicine name to delete");
                return;
            }
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "delete from inventory where name=@name";
                cmd.Parameters.AddWithValue("name", medNametxt.Text);
                con.Open();
               int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Record deleted successfully","Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch(Exception ex ) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(medNametxt.Text) || string.IsNullOrWhiteSpace(categorytxt.Text) || string.IsNullOrWhiteSpace(quantitytxt.Text) || string.IsNullOrWhiteSpace(dateTimePicker1.Text) || string.IsNullOrWhiteSpace(pricetext.Text)
               || string.IsNullOrWhiteSpace(pillsInLeafTxt.Text) || string.IsNullOrWhiteSpace(leafPerBoxTxt.Text))
            {
                MessageBox.Show("Please complete all fields.");
                return;
            }

            try
            {
                OleDbCommand insrt = new OleDbCommand();
                insrt.Connection = con;
                insrt.CommandText = "update inventory set name=@name,category=@category,quantity=@quantity,price=@price,expiryDate=@expiryDate,quantity_pills_per_leaf=@quantitypills,numbers_leafs_per_box=@numbers";
                insrt.Parameters.AddWithValue("@name", medNametxt.Text);
                insrt.Parameters.AddWithValue("@category", categorytxt.Text);
                insrt.Parameters.AddWithValue("@quantity", Convert.ToInt32(quantitytxt.Text));
                insrt.Parameters.AddWithValue("@price", Convert.ToInt32(pricetext.Text));
                insrt.Parameters.AddWithValue("@expiryDate", dateTimePicker1.Value);
                insrt.Parameters.AddWithValue("@quantitypills", Convert.ToInt32(pillsInLeafTxt.Text));
                insrt.Parameters.AddWithValue("@numbers", Convert.ToInt32(leafPerBoxTxt.Text));
                con.Open();
                int result = insrt.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("Data updated successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                medNametxt.Clear();
                categorytxt.Clear();
                quantitytxt.Clear();
                pricetext.Clear();
                pillsInLeafTxt.Clear();
                leafPerBoxTxt.Clear();
            }






        }
    }
}
