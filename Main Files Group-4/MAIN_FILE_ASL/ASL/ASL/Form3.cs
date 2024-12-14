using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Image = System.Drawing.Image;

namespace ASL
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True");
        public Form PreviousForm { get; set; }
        private void Form3_Load(object sender, EventArgs e)
        {
         

        }


        //BACK BUTTON TO FORM2
        private void button2_Click(object sender, EventArgs e)
        {
            dashboard dashboard = new dashboard();
            dashboard.Show();
            this.Close();
        }


        //DISPLAY TABLE FUNCTION
        private void displayTable() 
        {
               
               
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM pwd_recordsTbl WHERE last_name='" + searchBox.Text + "' ORDER BY ID_NUMBER ASC", conn);

            SqlCommand cmd = new SqlCommand("SELECT * FROM pwd_recordsTbl WHERE last_name = '" + searchBox.Text + "' UNION ALL SELECT * FROM pwd_recordsTbl WHERE First_name = '" + searchBox.Text + "' UNION ALL " +
           "SELECT * FROM pwd_recordsTbl WHERE CONCAT(LAST_NAME, ', ', FIRST_NAME) = '" + searchBox.Text + "' UNION ALL SELECT * FROM pwd_recordsTbl WHERE ID_NUMBER = '" + searchBox.Text + "' UNION ALL " +
           "SELECT * FROM pwd_recordsTbl WHERE AGE = '" + searchBox.Text + "' UNION ALL SELECT * FROM pwd_recordsTbl WHERE DISABILITY_ID = (SELECT DISABILITY_ID FROM disabilityTbl WHERE DISABILITY = '" + searchBox.Text + "') UNION ALL " +
           "SELECT * FROM pwd_recordsTbl WHERE DISABILITY_ID = (SELECT DISABILITY_ID FROM multiple_disabilityTbl WHERE MULTIPLE_DISABILITY = '" + searchBox.Text + "') UNION ALL SELECT * FROM pwd_recordsTbl WHERE PWD_STATUS = '" + searchBox.Text + "'", conn);

            SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    dataGridView1.DataSource = dt1;
                    conn.Close();

           
        }
        //DISPLAY THE RELEVANT RESULTS
        private void searchButton_Click(object sender, EventArgs e)
        {
            displayTable();
            int rowCount = dataGridView1.Rows.Count;
            resNumlabel.Text = rowCount.ToString();
            rowCounter();
        }


        // WHEN ROW IS SELECTED, DATA WILL AUTOMATICALLY BE PLACED IN THE CORRESPONDING TEXTBOXES
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    return;
                }



                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Populate the TextBox controls with the data from the selected row
                IdNumberlabel.Text = selectedRow.Cells[0].Value.ToString();
                lastnameBox.Text = selectedRow.Cells[1].Value.ToString();
                firstnameBox.Text = selectedRow.Cells[2].Value.ToString();
                middlenameBox.Text = selectedRow.Cells[3].Value.ToString();
                ageBox.Text = selectedRow.Cells[4].Value.ToString();
                genderBox.Text = selectedRow.Cells[5].Value.ToString();
                birthdayBox.Text = selectedRow.Cells[6].Value.ToString();
               string disability  = selectedRow.Cells[7].Value.ToString();
                string guardian  = selectedRow.Cells[8].Value.ToString();
                contactnumberBox.Text = selectedRow.Cells[9].Value.ToString();
                housenumBox.Text = selectedRow.Cells[10].Value.ToString();
                addressBox.Text = selectedRow.Cells[11].Value.ToString();
                validityDatelabel.Text = selectedRow.Cells[12].Value.ToString();
                date_issued.Text = selectedRow.Cells[13].Value.ToString();

                string imagePath = selectedRow.Cells[14].Value.ToString();
               string cause  = selectedRow.Cells[15].Value.ToString();

                string stats = selectedRow.Cells[16].Value.ToString();
                

                if (!string.IsNullOrEmpty(imagePath))
                {
                    // Check if the image file exists
                    if (File.Exists(imagePath))
                    {
                        // Load the image from the file
                        pictureBox1.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        // Handle the case when the image file does not exist
                        pictureBox1.Image = Image.FromFile(imagePath);
                    }
                }
                

                retrieveDisability(disability);
                Retrieveguardianname(guardian);
                statuschecker(stats);

                retrievecauseofdisname(cause);


            }
            catch  (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            } 
        }

        private void statuschecker(string stats)

        {
            if (stats == "ACTIVE")
            {
                statusLabel.Text = "ACTIVE";
                statusLabel.BackColor = Color.SpringGreen;
            }
            else if (stats == "INACTIVE")
            {
                statusLabel.Text = "INACTIVE";
                statusLabel.BackColor = Color.Red;
            }

        }


        // MINOR, ADULT, SENIOR ORGANIZER BUTTON
        private void retrieveDisability(string disabilityname)
        {
            string disability = "";

            using (SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True"))
            {
                // Open the connection
                conn.Open();

                // Create a new SqlCommand object with the query and connection

                using (SqlCommand command = new SqlCommand("SELECT DISABILITY FROM disabilityTbl WHERE DISABILITY_ID = '" + disabilityname + "' UNION SELECT MULTIPLE_DISABILITY FROM multiple_disabilityTbl WHERE DISABILITY_ID = '" + disabilityname + "'", conn))
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
                                 disability = reader.GetString(0);
                            }
                        }
                        else
                        {
                            // No rows returned, handle this case accordingly
                        }
                    }
                }
            }
            disabilityBox.Text = disability;
    
        }

        private void retrievecauseofdisname(string causename)
        {
            string cause = "";


            using (SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True"))
            {
                // Open the connection
                conn.Open();

                // Create a new SqlCommand object with the query and connection

                using (SqlCommand command = new SqlCommand("SELECT CAUSE_OF_DISABILITY FROM cause_of_disabilityTbl WHERE CAUSE_OF_DISABILITY_ID = '" + causename + "'", conn))
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
                                cause = reader.GetString(0);
                            }
                        }
                        else
                        {
                            // No rows returned, handle this case accordingly
                        }
                    }
                }
            }
            causeBox.Text = cause;

        }

        private void Retrieveguardianname(string guardianname)
        {
            string guardian = "";

            using (SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True"))
            {
                // Open the connection
                conn.Open();

                // Create a new SqlCommand object with the query and connection

                using (SqlCommand command = new SqlCommand("SELECT GUARDIAN_NAME FROM guardian_namesTbl WHERE GUARDIAN_ID = '" + guardianname + "'", conn))
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
                                guardian = reader.GetString(0);
                            }
                        }
                        else
                        {
                            // No rows returned, handle this case accordingly
                        }
                    }
                }
            }
            guardianBox.Text = guardian;

        }











       

        private void showAllrecords_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM pwd_recordsTbl ORDER BY last_name ASC ", conn);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
            rowCounter();

            disabilityFilter.Text = "";
            ageCategory.Text = "";
            genderFilter.Text = "";
        }

        private void rowCounter() 
        {
            int rowCount = dataGridView1.Rows.Count;
            
            resNumlabel.Text = rowCount.ToString();

        }
        private void RefreshDataGridView()
        {
            // retrieve the latest data from the database
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM pwd_recordsTbl ORDER BY ID_NUMBER ASC", conn);
            da.Fill(dt);

            // update the DataGridView control
            dataGridView1.DataSource = dt;
            rowCounter();
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
            rowCounter();
            searchBox.Text = "";
           
            lastnameBox.Text = "";
            firstnameBox.Text = "";
            middlenameBox.Text = "";
            ageBox.Text = "";
            genderBox.Text = "";
            birthdayBox.Text = "";
            disabilityBox.Text = "";
            guardianBox.Text = "";
            contactnumberBox.Text = "";
            housenumBox.Text = "";
            addressBox.Text = "";
            validityDatelabel.Text = "";
            IdNumberlabel.Text = "1375040080000VC";
            pictureBox1.Image = null;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.PreviousForm = this;
            form5.Show();
            this.Hide();
        }

        private void printButton_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    SaveFileDialog save = new SaveFileDialog();
                    save.Filter = "PDF (*.pdf)|*.pdf";
                    save.FileName = "Pwd_Records.pdf";
                    bool ErrorMessage = false;

                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(save.FileName))
                        {
                            try
                            {
                                File.Delete(save.FileName);
                            }
                            catch (Exception ex)
                            {
                                ErrorMessage = true;
                                MessageBox.Show("Unable to write data to disk" + ex.Message);
                            }
                        }

                        if (!ErrorMessage)
                        {
                            try
                            {
                                PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                                pTable.DefaultCell.Padding = 1;
                                pTable.WidthPercentage = 100;
                                pTable.HorizontalAlignment = Element.ALIGN_LEFT;

                                //COLUMN
                                foreach (DataGridViewColumn col in dataGridView1.Columns)
                                {
                                    PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f)));
                                    pTable.AddCell(pCell);
                                }

                                foreach (DataGridViewRow viewRow in dataGridView1.Rows)
                                {
                                    foreach (DataGridViewCell dcell in viewRow.Cells)
                                    {
                                        PdfPCell dataCell = new PdfPCell(new Phrase(dcell.Value.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f)));
                                        pTable.AddCell(dataCell);
                                    }
                                }

                                using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                                {
                                    Document document = new Document(PageSize.A4.Rotate(), 8f, 16f, 16f, 8f);
                                    PdfWriter.GetInstance(document, fileStream);
                                    document.Open();
                                    document.Add(pTable);
                                    document.Close();
                                    fileStream.Close();
                                }

                                MessageBox.Show("Data Exported Successfully", "Info");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error while exporting Data: " + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Record(s) Found", "Info");
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error Occured While Reading FIle",ex.Message);
            }
        }

        

        private void ageCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           ApplyFilters();
        }

        private void genderFilter_SelectedIndexChanged(object sender, EventArgs e)
        {


            ApplyFilters();
        }

      

        private void disabilityFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            ApplyFilters();



            
        }

        private void ApplyFilters()
        {

            try
            {
                string age = null;
                string gender = null;
                string disability = null;
                string multiple_dis = null;
                // Get selected values from combo boxes
                if (ageCategory.SelectedItem != null)
                {
                    age = ageCategory.SelectedItem.ToString();
                }

                if (genderFilter.SelectedItem != null)
                {
                    gender = genderFilter.SelectedItem.ToString();
                }

                if (disabilityFilter.SelectedItem != null)
                {
                    disability = disabilityFilter.SelectedItem.ToString();
                }
                if (multipleDis.SelectedItem != null)
                {
                    multiple_dis = multipleDis.SelectedItem.ToString();
                }

                // Build the SQL query based on selected filters
                string query = "SELECT * FROM pwd_recordsTbl WHERE ";

                if (!string.IsNullOrEmpty(age))
                {
                    if (age == "Minors")
                    {
                        query += "AGE < 18";
                    }
                    else if (age == "Adults")
                    {
                        query += "AGE >= 18 AND AGE < 60";
                    }
                    else
                    {
                        query += "AGE >= 60";
                    }
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    if (!string.IsNullOrEmpty(age))
                    {
                        query += " AND ";
                    }

                    query += $"GENDER = '{gender}'";
                }

                if (!string.IsNullOrEmpty(disability))
                {
                    if (!string.IsNullOrEmpty(age) || !string.IsNullOrEmpty(gender))
                    {
                        query += " AND ";
                    }

                    if (disability == "Hearing")
                    {
                        query += "DISABILITY_ID = (SELECT DISABILITY_ID FROM disabilityTbl WHERE DISABILITY = '" + disabilityFilter.SelectedItem.ToString() + "')";
                    }
                    else if (disability == "Mental")
                    {
                        query += "DISABILITY_ID = (SELECT DISABILITY_ID FROM disabilityTbl WHERE DISABILITY = '" + disabilityFilter.SelectedItem.ToString() + "')";
                    }
                    else if (disability == "Speech")
                    {
                        query += "DISABILITY_ID = (SELECT DISABILITY_ID FROM disabilityTbl WHERE DISABILITY = '" + disabilityFilter.SelectedItem.ToString() + "')";
                    }
                    else if (disability == "Learning")
                    {
                        query += "DISABILITY_ID = (SELECT DISABILITY_ID FROM disabilityTbl WHERE DISABILITY = '" + disabilityFilter.SelectedItem.ToString() + "')";
                    }
                    else if (disability == "Visual")
                    {
                        query += "DISABILITY_ID = (SELECT DISABILITY_ID FROM disabilityTbl WHERE DISABILITY = '" + disabilityFilter.SelectedItem.ToString() + "')";
                    }
                    else if (disability == "Physical")
                    {
                        query += "DISABILITY_ID = (SELECT DISABILITY_ID FROM disabilityTbl WHERE DISABILITY = '" + disabilityFilter.SelectedItem.ToString() + "')";
                    }
                    else if (disability == "Orthopedic")
                    {
                        query += "DISABILITY_ID = (SELECT DISABILITY_ID FROM disabilityTbl WHERE DISABILITY = '" + disabilityFilter.SelectedItem.ToString() + "')";
                    }
                    else if (disability == "Psychosocial")
                    {
                        query += "DISABILITY_ID = (SELECT DISABILITY_ID FROM disabilityTbl WHERE DISABILITY = '" + disabilityFilter.SelectedItem.ToString() + "')";
                    }
                }

                if (!string.IsNullOrEmpty(multiple_dis))
                {
                    if (!string.IsNullOrEmpty(age) || !string.IsNullOrEmpty(gender))
                    {
                        query += " AND ";
                    }

                    if (multiple_dis == "HEARING/MENTAL" || multiple_dis == "HEARING/SPEECH" || multiple_dis == "HEARING/LEARNING" ||
                     multiple_dis == "HEARING/VISUAL" || multiple_dis == "HEARING/PHYSICAL" || multiple_dis == "HEARING/ORTHOPEDIC" ||
                     multiple_dis == "HEARING/PSYCHOSOCIAL" || multiple_dis == "MENTAL/SPEECH" || multiple_dis == "MENTAL/LEARNING" ||
                     multiple_dis == "MENTAL/VISUAL" || multiple_dis == "MENTAL/PHYSICAL" || multiple_dis == "MENTAL/ORTHOPEDIC" ||
                     multiple_dis == "MENTAL/PSYCHOSOCIAL" || multiple_dis == "SPEECH/LEARNING" || multiple_dis == "SPEECH/VISUAL" ||
                     multiple_dis == "SPEECH/PHYSICAL" || multiple_dis == "SPEECH/ORTHOPEDIC" || multiple_dis == "SPEECH/PSYCHOSOCIAL" ||
                     multiple_dis == "LEARNING/VISUAL" || multiple_dis == "LEARNING/PHYSICAL" || multiple_dis == "LEARNING/ORTHOPEDIC" ||
                     multiple_dis == "LEARNING/PSYCHOSOCIAL" || multiple_dis == "VISUAL/PHYSICAL" || multiple_dis == "VISUAL/ORTHOPEDIC" ||
                     multiple_dis == "VISUAL/PSYCHOSOCIAL" || multiple_dis == "PHYSICAL/ORTHOPEDIC" ||
                     multiple_dis == "PHYSICAL/PSYCHOSOCIAL" || multiple_dis == "ORTHOPEDIC/PSYCHOSOCIAL")
                    {
                        query += "DISABILITY_ID = (SELECT DISABILITY_ID FROM multiple_disabilityTbl WHERE MULTIPLE_DISABILITY = '" + multipleDis.SelectedItem.ToString() + "')";
                    }
                }


                query += " ORDER BY ID_NUMBER ASC";

                // Execute the query and update the DataGridView
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                conn.Close();
                rowCounter();
                conn.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Warning: " + ex.Message);
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ageCategory.Text = null;
            genderFilter.Text = null;
            disabilityFilter.Text = null;
            multipleDis.Text = null;
        }

        private void multipleDis_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void archiveButton_Click(object sender, EventArgs e)
        {
            archiveForm archive = new archiveForm();
            archive.Show();
        }
    }
}
