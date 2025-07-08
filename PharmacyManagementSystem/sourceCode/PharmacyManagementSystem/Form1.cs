using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.CodeDom;
namespace PharmacyManagementSystem
{
    public partial class Form1: Form
    {
        OleDbConnection con = new OleDbConnection();
        public Form1()
        {
            
            InitializeComponent();
            string dbPath = System.IO.Path.Combine(Application.StartupPath, "PharmacyDB.accdb");
            //con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\syedh\\Documents\\PharmacyDB.accdb";
            con.ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailtxt.Text) || string.IsNullOrWhiteSpace(passwordtxt.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please complete all fields.");
                return;
            }
            
            try
            {
                
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = con;
                cmd.CommandText = "select email,password,status from [users] where email=@email";
                cmd.Parameters.AddWithValue("@email", emailtxt.Text);
                con.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    MessageBox.Show("Email not found.");
                }
                else
                {

                    string statusFromDb = reader["status"].ToString();
                    string passwordDb=reader["password"].ToString();
                    if(passwordtxt.Text==passwordDb)
                    {
                        if(comboBox1.SelectedItem.ToString()!=statusFromDb)
                        {
                            MessageBox.Show("please select correct role");
                        }
                        else
                        {
                            if(statusFromDb=="Admin")
                            {
                                DashBoardForm dashBoardForm = new DashBoardForm(this, emailtxt.Text);
                                dashBoardForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                UserDashBoard userDashBoard = new UserDashBoard(this, emailtxt.Text);
                                userDashBoard.Show();
                                this.Hide();
                            }
                            
                            
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong Email or Password");
                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
                emailtxt.Clear();
                passwordtxt.Clear();   
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailtxt.Text) || string.IsNullOrWhiteSpace(passwordtxt.Text) || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please complete all fields.");
                return;
            }
            try
            {
                if (comboBox1.SelectedItem.ToString() == "Admin")
                {
                    MessageBox.Show("You can't register as Admin", "role error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OleDbCommand insrtcmd = new OleDbCommand();
                    insrtcmd.Connection = con;
                    insrtcmd.CommandText = "insert into [users] ([email],[password],[status]) values (@email,@password,@status);";
                    insrtcmd.Parameters.AddWithValue("@email", emailtxt.Text);
                    insrtcmd.Parameters.AddWithValue("@password", passwordtxt.Text);
                    insrtcmd.Parameters.AddWithValue("@status", comboBox1.SelectedItem.ToString());
                    con.Open();
                    int result = insrtcmd.ExecuteNonQuery();
                    if(result>0)
                    {
                        MessageBox.Show("Pharmacist registered successfully!!");
                    }

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { 
                con.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                passwordtxt.PasswordChar = '\0';
                checkBox1.Text = "Hide";
            }
               
            else
            {
                passwordtxt.PasswordChar = '*';
                checkBox1.Text = "Show";
            }
                
        }

     
    }
}
