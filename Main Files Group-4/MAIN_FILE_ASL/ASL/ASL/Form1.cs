using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace ASL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();

        }
        SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            


           
            login();
            Form2 secondForm = new Form2();
            secondForm.TextBoxValue = textBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private int getUserID()
        {
            int userID = 0;

            using (SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True"))
            {


                // Open the connection
                conn.Open();

                // Create a new SqlCommand object with the query and connection

                using (SqlCommand command = new SqlCommand("SELECT userID FROM userAcc_Tbl WHERE username = '"+textBox1.Text+"'", conn))
                {
                    // Execute the query and retrieve the results
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if there are any rows returned
                        if (reader.HasRows)
                        {
                            // Retrieve the value of the DISABILITY_ID column
                            if (reader.Read())
                            {
                                userID = reader.GetInt32(0);
                            }
                        }
                        else
                        {
                            // No rows returned, handle this case accordingly
                        }
                    }
                }
            }

            return userID;
        }
        private void login() 
        {

           
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("username is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("password is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            String username, password;
            username = textBox1.Text;
            password = textBox2.Text;


          


            try
            {

                login_Logs();

                String querry = "SELECT * FROM userAcc_Tbl WHERE username= '" + textBox1.Text + "' AND password = '" + textBox2.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(querry, conn);
                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if (dtable.Rows.Count > 0)
                {
                    username = textBox1.Text;
                    password = textBox2.Text;

                    //page to load

                    dashboard dashboard = new dashboard();
                    dashboard.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Invalid login credentials \nCheck username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();

                    //focus username
                    textBox1.Focus();
                }

            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                conn.Close();
            }

           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            date.Text = DateTime.Now.ToString();
        }


        private void login_Logs() 
        {
            conn.Open();
            SqlCommand cmd0 = new SqlCommand(@"INSERT INTO [dbo].[login_Logs]
                        ([userID],[Date_Login])
                         VALUES
                         ('" + getUserID() + "','" + date.Text + "')", conn);

            cmd0.ExecuteNonQuery();
            conn.Close();

        }
    }
}
