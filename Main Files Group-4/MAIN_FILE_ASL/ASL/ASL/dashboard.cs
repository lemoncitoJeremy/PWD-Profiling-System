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
using System.Windows.Forms.DataVisualization.Charting;

namespace ASL
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
            timer1.Start();
        }
        private string conn = "Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=Truey"; // Replace with your actual connection string
        public Form PreviousForm { get; set; }
        private void dashboard_Load(object sender, EventArgs e)
        {
            // Start a timer to update the label every second
            Timer timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;
            timer.Start();
            chartDisplay();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            //MINORS
            
          
                int minorityCount = GetMinorityCount();
                minorCount.Text = minorityCount.ToString("D3");

                int adultsCount = GetAdultCount();
                adultCount.Text = adultsCount.ToString("D3");

                int srCount = GetseniorCount();
                seniorCount.Text = srCount.ToString("D3");

                int malecount = GetMaleCount();
                malecountlabel.Text = malecount.ToString("D3");

                int femalecount = GetFemaleCount();
                FemaleCount.Text = femalecount.ToString("D3");

                int allAccounts = getallCount();
                allLabel.Text = allAccounts.ToString("D3");

        }

        private int GetMinorityCount()
        {
            
            int count = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // Execute the SQL query
                    string query = "SELECT Count(AGE) As total FROM pwd_recordsTbl WHERE Age < 18;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get the count value from the result
                                count = Convert.ToInt32(reader["total"]);
                            }
                        }
                    }
                    connection.Close();
                }

                return count;
            }
            catch 
            {
                return count;
            }

        }
        private int GetAdultCount()
        {
            int count = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // Execute the SQL query
                    string query = "SELECT COUNT(*) AS age_count FROM pwd_recordsTbl WHERE Age >= 18 AND Age < 60;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get the count value from the result
                                count = Convert.ToInt32(reader["age_count"]);
                            }
                        }
                    }
                    connection.Close();
                }
                return count;
            }
            catch 
            {
                return count;
            }
        }
        private int getallCount()
        {
            int count = 0;
            try
            {
              
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // Execute the SQL query
                    string query = "SELECT COUNT(*) AS all_acc FROM pwd_recordsTbl ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get the count value from the result
                                count = Convert.ToInt32(reader["all_acc"]);
                            }
                        }
                    }
                    connection.Close();
                }
                return count;
            }
            catch 
            {
                return count;
            }
        }

        private int GetseniorCount()
        {
            int count = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // Execute the SQL query
                    string query = "SELECT COUNT(*) AS age_count FROM pwd_recordsTbl WHERE Age >= 60;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get the count value from the result
                                count = Convert.ToInt32(reader["age_count"]);
                            }
                        }
                    }
                    connection.Close();
                }
                return count;
            }
            catch 
            {
                return count;
            }
        }

        private int GetMaleCount()
        {
            int count = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // Execute the SQL query
                    string query = "SELECT COUNT(*) AS male_count FROM pwd_recordsTbl WHERE GENDER ='MALE';";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get the count value from the result
                                count = Convert.ToInt32(reader["male_count"]);
                            }
                        }
                    }
                    connection.Close();
                }
                return count;
            }
            catch 
            {
                return count;
            }
        }

        private int GetFemaleCount()
        {
            int count = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // Execute the SQL query
                    string query = "SELECT COUNT(*) AS female_count FROM pwd_recordsTbl WHERE GENDER ='FEMALE';";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get the count value from the result
                                count = Convert.ToInt32(reader["female_count"]);
                            }
                        }
                    }
                    connection.Close();
                }
                return count;
            }
            catch 
            {
                return count;

            }
        }
        private double FemalePercentage() 
        {
            double percentage = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // Execute the SQL query
                    string query = "SELECT (COUNT(*) * 100 / (SELECT COUNT(*) FROM pwd_recordsTbl)) AS female FROM pwd_recordsTbl WHERE GENDER = 'FEMALE';";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get the count value from the result
                                percentage = Convert.ToInt32(reader["female"]);
                            }
                        }
                    }
                    connection.Close();
                }
                return percentage;
            }
            catch 
            {
                return percentage;
            
            }
        }
        private double MalePercentage()
        {
            double percentage = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // Execute the SQL query
                    string query = "SELECT (COUNT(*) * 100 / (SELECT COUNT(*) FROM pwd_recordsTbl)) AS male FROM pwd_recordsTbl WHERE GENDER = 'MALE';";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get the count value from the result
                                percentage = Convert.ToInt32(reader["male"]);
                            }
                        }
                    }
                    connection.Close();
                }
                return percentage;
            }
            catch 
            {
                return percentage;

            }
        }

        private void chartDisplay() 
        {
            chart2.Series["Age_category"].Points.AddXY("MINORS: " + Minorpercentage().ToString() + "%", Minorpercentage()) ;
            
            chart2.Series["Age_category"].Points.AddXY("ADULTS: " + AdultsPercentage().ToString() + "%", AdultsPercentage());
            chart2.Series["Age_category"].Points.AddXY("SENIORS: " + SeniorsPercentage().ToString() + "%", SeniorsPercentage());

            chart1.Series["gender"].Points.AddXY("MALE: " + MalePercentage().ToString() + "%", MalePercentage());
            chart1.Series["gender"].Points.AddXY("FEMALE: " + FemalePercentage().ToString() + "%", FemalePercentage()) ;

            
            

        }

        private double Minorpercentage() 
        {
            double getPercent = 0.0;

            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // Execute the SQL query
                    string query = "SELECT (COUNT(*) * 100 / (SELECT COUNT(*) FROM pwd_recordsTbl)) AS minors FROM pwd_recordsTbl WHERE AGE < 18;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get the count value from the result
                                getPercent = Convert.ToInt32(reader["minors"]);
                            }
                        }
                    }
                    connection.Close();
                }
                return getPercent;
            }
            catch
            {
                return getPercent;
            }

        }
        private double AdultsPercentage()
        {
            double getPercent = 0.0;
            try
            {
                
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // Execute the SQL query
                    string query = "SELECT (COUNT(*) * 100 / (SELECT COUNT(*) FROM pwd_recordsTbl)) AS adults FROM pwd_recordsTbl WHERE AGE >= 18 AND AGE < 60;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get the count value from the result
                                getPercent = Convert.ToInt32(reader["adults"]);
                            }
                        }
                    }
                    connection.Close();
                }
                return getPercent;
            }
            catch
            {
                return getPercent;
            }
        }
        private double SeniorsPercentage()
        {
            double getPercent = 0.0;
            try
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();

                    // Execute the SQL query
                    string query = "SELECT (COUNT(*) * 100 / (SELECT COUNT(*) FROM pwd_recordsTbl)) AS seniors FROM pwd_recordsTbl WHERE AGE >= 60;";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Get the count value from the result
                                getPercent = Convert.ToInt32(reader["seniors"]);
                            }
                        }
                    }
                    connection.Close();
                }
                return getPercent;
            }
            catch 
            {
                return getPercent;
            }

        }

        private void sign_outButton_Click(object sender, EventArgs e)
        {
            //LOG OUT BUTTON
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.PreviousForm = this;
            form5.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            date.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
            this.Hide();
        }
    }
}
