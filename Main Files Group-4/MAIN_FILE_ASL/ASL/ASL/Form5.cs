using Microsoft.Win32;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ASL
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
       
        public Form PreviousForm { get; set; }
        
        //STORED DIRECTORY FOR NEW UPLOAD
        string imgLoc = "";
        // DIRECTORY FROM DATABASE
        string databaseImageDirectory = "";

        //GUARDIAN CODE FROM DATABASE 
        int guardianNameCODE = 0;

        

        SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True");
        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void getGuardianName(string guardianCODE) 
        
        {

            string guardian = "";

            using (SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True"))
            {
                // Open the connection
                conn.Open();

                // Create a new SqlCommand object with the query and connection

                using (SqlCommand command = new SqlCommand("SELECT GUARDIAN_NAME FROM guardian_namesTbl WHERE GUARDIAN_ID = '" + guardianCODE + "'", conn))
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
            conn.Close();
            guardianBox.Text = guardian;


        }

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
                genderCbox.Text = selectedRow.Cells[5].Value.ToString();
                birthdayBox.Text = selectedRow.Cells[6].Value.ToString();
                disabilityBox.Text = selectedRow.Cells[7].Value.ToString();
                string guardianCODE = selectedRow.Cells[8].Value.ToString();
                contactnumberBox.Text = selectedRow.Cells[9].Value.ToString();
                housenumBox.Text = selectedRow.Cells[10].Value.ToString();
                addressBox.Text = selectedRow.Cells[11].Value.ToString();
                validityBox.Text = selectedRow.Cells[12].Value.ToString();
                date_issued.Text = selectedRow.Cells[13].Value.ToString();
                string stats = selectedRow.Cells[16].Value.ToString();
                causeBox.Text = selectedRow.Cells[15].Value.ToString();

                //SEND THE GUARDIAN CODE TO DISPLAY THE GUARDIAN NAME
                getGuardianName(guardianCODE);
                guardianNameCODE = int.Parse(guardianCODE);
                guardianID.Text = guardianNameCODE.ToString();  

                // Load the image directory path from the database
                string imagePath = selectedRow.Cells[14].Value.ToString();

                databaseImageDirectory = imagePath;

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
                    conn.Close();
                }
                conn.Close();

                statuschecker(stats);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error "+ex.Message +"");
            }     
        }

        private void statuschecker(string stats)

        {
            if (stats == "ACTIVE")
            {
                pwdStatusButton.Text = "ACTIVE";
                pwdStatusButton.BackColor = Color.SpringGreen;
            }
            else if (stats == "INACTIVE")
            {
                pwdStatusButton.Text = "INACTIVE";
                pwdStatusButton.BackColor = Color.Red;
            }

        }

        private void updateGuardianNameTbl() 
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True"))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE guardian_namesTbl SET GUARDIAN_NAME = @guardianName WHERE GUARDIAN_ID = '" + guardianID.Text + "'", conn))
                {
                    cmd.Parameters.AddWithValue("@guardianName", guardianBox.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        
           

        private void updateRecordsButton_Click(object sender, EventArgs e)
        {
           
            try
            {
                updateGuardianNameTbl();


                //ORIGINAL IMAGE FROM DATABASE
                string imagePath = databaseImageDirectory;

                bool isImageChanged = false;

                //NEW IMAGE DIRECTORY
                string newImagePath = imgLoc;
                string origDirectory = "";
                // Check if the image file path is available in the database
                conn.Open();
                SqlCommand selectCmd = new SqlCommand("SELECT IMAGE_DIRECTORY FROM pwd_recordsTbl WHERE ID_NUMBER='" + IdNumberlabel.Text + "'", conn);
                SqlDataReader reader = selectCmd.ExecuteReader();

                if (reader.Read())
                {
                    // Get the original image directory path from the database
                    string originalImagePath = reader["IMAGE_DIRECTORY"].ToString();
                    origDirectory = originalImagePath;
                    // Check if the image directory path is different from the original
                    if (imagePath == originalImagePath)
                    {
                        isImageChanged = true;
                    }
                    else 
                    {
                        isImageChanged = false;
                    }  
                }
                conn.Close();
                reader.Close();
                conn.Close();



                if (!isImageChanged)
                {

                    
                    
                    // If the image directory path has not changed, perform the update without modifying the image
                    conn.Open();
                    
                    SqlCommand cmd = new SqlCommand("UPDATE pwd_recordsTbl SET LAST_NAME=@lastname, FIRST_NAME=@firstname, MIDDLE_NAME=@middlename, AGE=@age, GENDER=@gender, BIRTHDAY=@birthday, DISABILITY_ID=@disability_id, GUARDIAN_ID=@guardian_id, CONTACT_NO=@contactnumber, HOUSE_NO=@housenum, ADDRESS=@address, VALIDITY=@validity,IMAGE_DIRECTORY=@imageDirectory, CAUSE_OF_DISABILITY_ID=@cause,PWD_STATUS =@status WHERE ID_NUMBER='" + IdNumberlabel.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@lastname", lastnameBox.Text);
                    cmd.Parameters.AddWithValue("@firstname", firstnameBox.Text);
                    cmd.Parameters.AddWithValue("@middlename", middlenameBox.Text);
                    cmd.Parameters.AddWithValue("@age", ageBox.Text);
                    cmd.Parameters.AddWithValue("@gender", genderCbox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@birthday", birthdayBox.Text);
                    cmd.Parameters.AddWithValue("@disability_id", disabilityBox.Text);
                    cmd.Parameters.AddWithValue("@guardian_id", guardianID.Text);
                    cmd.Parameters.AddWithValue("@contactnumber", contactnumberBox.Text);
                    cmd.Parameters.AddWithValue("@housenum", housenumBox.Text);
                    cmd.Parameters.AddWithValue("@address", addressBox.Text);
                    cmd.Parameters.AddWithValue("@validity", validityBox.Text);
                    cmd.Parameters.AddWithValue("@imageDirectory", origDirectory);
                    cmd.Parameters.AddWithValue("@cause", causeBox.Text);
                    cmd.Parameters.AddWithValue("@status", pwdStatusButton.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully");
                    conn.Close();
                }
                else
                {
                   
                    
                    // If the image directory path has changed, update the image and perform the update
                    conn.Open();
                
                    SqlCommand cmd = new SqlCommand("UPDATE pwd_recordsTbl SET LAST_NAME=@lastname, FIRST_NAME=@firstname, MIDDLE_NAME=@middlename, AGE=@age, GENDER=@gender, BIRTHDAY=@birthday, DISABILITY_ID=@disability_id, GUARDIAN_ID=@guardian_id, CONTACT_NO=@contactnumber, HOUSE_NO=@housenum, ADDRESS=@address, VALIDITY=@validity, IMAGE_DIRECTORY=@imageDirectory,CAUSE_OF_DISABILITY_ID=@cause,PWD_STATUS =@status WHERE ID_NUMBER='" + IdNumberlabel.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@lastname", lastnameBox.Text);
                    cmd.Parameters.AddWithValue("@firstname", firstnameBox.Text);
                    cmd.Parameters.AddWithValue("@middlename", middlenameBox.Text);
                    cmd.Parameters.AddWithValue("@age", ageBox.Text);
                    cmd.Parameters.AddWithValue("@gender", genderCbox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@birthday", birthdayBox.Text);
                    cmd.Parameters.AddWithValue("@disability_id", disabilityBox.Text);
                    cmd.Parameters.AddWithValue("@guardian_id", guardianID.Text);
                    cmd.Parameters.AddWithValue("@contactnumber", contactnumberBox.Text);
                    cmd.Parameters.AddWithValue("@housenum", housenumBox.Text);
                    cmd.Parameters.AddWithValue("@address", addressBox.Text);
                    cmd.Parameters.AddWithValue("@validity", validityBox.Text);
                    cmd.Parameters.AddWithValue("@imageDirectory", newImagePath);
                    cmd.Parameters.AddWithValue("@cause", causeBox.Text);
                    cmd.Parameters.AddWithValue("@status", pwdStatusButton.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record:"+ ex.Message);
            
                
            }
            conn.Close();
        }

        private void RefreshDataGridView()
        {


            // set the connection string
            string connectionString = "Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            // retrieve the latest data from the database
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM pwd_recordsTbl ORDER BY ID_NUMBER ASC", conn);
            da.Fill(dt);

            // update the DataGridView control
            dataGridView1.DataSource = dt;
            conn.Close();
            rowCounter();


            
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {

            RefreshDataGridView();
         
            lastnameBox.Text = "";
            firstnameBox.Text = "";
            middlenameBox.Text = "";
            ageBox.Text = "";
            genderCbox.Text = "";
            birthdayBox.Text = "";
            disabilityBox.Text = "";
            guardianBox.Text = "";
            contactnumberBox.Text = "";
            housenumBox.Text = "";
            addressBox.Text = "";
            validityBox.Text = "";
            IdNumberlabel.Text = "1375040080000VC";
            pictureBox1.Image = null;
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            //UPLOAD BUTTON FOR UPDATING PICTURE

            OpenFileDialog Dialog = new OpenFileDialog();
            Dialog.Filter = "Image Files(*.PNG;*.BMP;*.JPG;*.GIF)|*.PNG;*.BMP;*.JPG;*.GIF";
            if (Dialog.ShowDialog() == DialogResult.OK)
            {
                string imgLocation = Dialog.FileName.ToString();
                pictureBox1.ImageLocation = imgLocation;
                imgLoc = imgLocation;
                
            }

        }

        private void displayTable()
        {
            conn.Close();
            conn.Open();
            

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

        private void searchButton_Click(object sender, EventArgs e)
        {
            displayTable();
            int rowCount = dataGridView1.Rows.Count;
            resNumlabel.Text = rowCount.ToString();
            rowCounter();
        }
        private void rowCounter()
        {
            int rowCount = dataGridView1.Rows.Count;
            
            resNumlabel.Text = rowCount.ToString();

        }

      

        private void showAllrecords_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM pwd_recordsTbl ORDER BY last_name ASC;", conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
            rowCounter();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            PreviousForm.Show();
            this.Hide();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
           
               
           
            try
            {

                //transfer the selected data first to archiveTbl
                insertToArchive();

                // DELETING RECORDS
                HashSet<int> selectedRows = new HashSet<int>();

                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    if (!selectedRows.Contains(cell.RowIndex))
                    {
                        selectedRows.Add(cell.RowIndex);
                    }
                }

                string connectionString = "Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "UPDATE pwd_recordsTbl SET LAST_NAME = NULL, FIRST_NAME = NULL, MIDDLE_NAME = NULL, AGE = NULL, GENDER = NULL, BIRTHDAY = NULL, DISABILITY_ID = NULL, CAUSE_OF_DISABILITY_ID = NULL, GUARDIAN_ID = NULL, CONTACT_NO = NULL, HOUSE_NO = NULL, ADDRESS = NULL, VALIDITY = NULL,DATE_ISSUED = NULL, IMAGE_DIRECTORY = NULL, PWD_STATUS= NULL WHERE ID_NUMBER IN (";

                    // Add parameters dynamically based on the number of selected rows
                    for (int i = 0; i < selectedRows.Count; i++)
                    {
                        sql += "@id" + i;
                        if (i != selectedRows.Count - 1)
                        {
                            sql += ",";
                        }
                    }

                    sql += ")";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // Assign parameter values based on the selected rows
                        int parameterIndex = 0;
                        foreach (int rowIndex in selectedRows)
                        {
                            cmd.Parameters.AddWithValue("@id" + parameterIndex, dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());
                            parameterIndex++;
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

                conn.Close();
                // Clear the selected cells
                dataGridView1.ClearSelection();
                rowCounter();
                MessageBox.Show("Deleted Successfully");
            }
            catch  
            { 
                MessageBox.Show("No Record(s) Found");
            } 
            
        }

        private void pwdStatusButton_Click(object sender, EventArgs e)
        {
            if (pwdStatusButton.Text == "ACTIVE")
            {
                pwdStatusButton.Text = "INACTIVE";
                pwdStatusButton.BackColor = Color.Red;
            }
            else if (pwdStatusButton.Text == "INACTIVE")
            {
                pwdStatusButton.Text = "ACTIVE";
                pwdStatusButton.BackColor = Color.SpringGreen;
            }
        }



        private void insertToArchive() 
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            // Get the selected row
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            // Populate the TextBox controls with the data from the selected row
            string idnumber = selectedRow.Cells[0].Value.ToString();
            string lastname= selectedRow.Cells[1].Value.ToString();
            string firstname = selectedRow.Cells[2].Value.ToString();
            string middlename = selectedRow.Cells[3].Value.ToString();
            string age = selectedRow.Cells[4].Value.ToString();
            string gender = selectedRow.Cells[5].Value.ToString();
            string birthday = selectedRow.Cells[6].Value.ToString();
            string disability_id = selectedRow.Cells[7].Value.ToString();
            string guardian = selectedRow.Cells[8].Value.ToString();
            string contactnumber = selectedRow.Cells[9].Value.ToString();
            string housenum = selectedRow.Cells[10].Value.ToString();
            string address = selectedRow.Cells[11].Value.ToString();
            string validity = selectedRow.Cells[12].Value.ToString();
            string dateissued = selectedRow.Cells[13].Value.ToString();
            string imagePath = selectedRow.Cells[14].Value.ToString();
            string causeofdis = selectedRow.Cells[15].Value.ToString();
            string stats = selectedRow.Cells[16].Value.ToString();

            conn.Open();
            SqlCommand cmd1 = new SqlCommand(@"INSERT INTO [dbo].[inactive_archiveTbl]
                       ([ID_NUMBER]
                       ,[LAST_NAME]
                       ,[FIRST_NAME]
                       ,[MIDDLE_NAME]
                       ,[AGE]
                       ,[GENDER]
                       ,[BIRTHDAY]
                       ,[DISABILITY_ID]
                       ,[CAUSE_OF_DISABILITY_ID]
                       ,[GUARDIAN_ID]
                       ,[CONTACT_NO]
                       ,[HOUSE_NO]
                       ,[ADDRESS]
                       ,[VALIDITY]
                       ,[DATE_ISSUED],[IMAGE_DIRECTORY],[PWD_STATUS])
                        VALUES
                        ('" + idnumber + "','" + lastname+ "','" + firstname + "','" + middlename + "','" + age + "', '" + gender +
            "','" + birthday + "','" + disability_id + "', '" + causeofdis + "','" + guardian +
            "','" + contactnumber + "','" + housenum + "','" + address + "','" + validity + "','" + dateissued + "','" + imagePath + "','" + stats + "')", conn);


            cmd1.ExecuteNonQuery();
           
            MessageBox.Show("Transferred To Archive");
            conn.Close();

        }

        private void disabilityFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void genderFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ageCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void multipleDis_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }



        private void ApplyFilters()
        {
            conn.Close();

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
                MessageBox.Show("Error" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ageCategory.Text = null;
            genderFilter.Text = null;
            disabilityFilter.Text = null;
            multipleDis.Text = null;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
