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
using System.IO;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Org.BouncyCastle.Asn1.Cmp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Security.Principal;

namespace ASL
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            timer1.Start();
          
        }
        public string TextBoxValue { get; set; }
        public Form PreviousForm { get; set; }
        // CONNECTION STRING
        SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True");
       
        //GLOBAL VARIABLE FOR ID_NUMBER AND PREFIX
        double val = 1375040080000;
        string prefix = "VC";
        string imgLocation = "";
        int agecheck = 0;
        int userID = 0;
        string userName = "";

        private void registrationButton_Click(object sender, EventArgs e)
        {
            try
            {
                IF_txtboxblank();
                //EVERY NEW REGISTRATION IS SET ACTIVE
                string pwdStatus ="ACTIVE";
               
                //VARIABLES USED FOR VALIDATING THE DATA IF ITS RECORDED IN THE DATABASE
                int disabilityCode = disabilitiesCode() ;
                int guardianCode = guardian_nameCode();
                int otherCauseDisabilityCode = cause_of_disabilityCode();
                int othersDisabilityspecifiedCode = disabilitiesCode();
                int needguardian = agecheck;


               // IF THE NAME OF THE GUARDIAN IS NOT IN THE DATABASE IT WILL BE ADDED TO THE DATABASE FOR GUARDIANS
                if (guardianCode == 0)
                {
                    conn.Open();
                    SqlCommand cmd0 = new SqlCommand(@"INSERT INTO [dbo].[guardian_namesTbl]
                        ([GUARDIAN_NAME])
                         VALUES
                         ('" + GlastnameBox.Text + "," + GfirstnameBox.Text + "," + GmiddlenameBox.Text + "')", conn);
                    
                    cmd0.ExecuteNonQuery();
                    conn.Close();
                }


                //IF WALA YUNG CAUSE NG DISABILITY AND INISPECIFY SA TEXTBOX ILALAGAY SIYA SA CAUSE_OF_DISABILITYTBL SA DATABASE
                if (otherCauseDisabilityCode == 0)
                {
                    conn.Open();
                    SqlCommand cmd0 = new SqlCommand(@"INSERT INTO [dbo].[cause_of_disabilityTbl]
                        ([CAUSE_OF_DISABILITY])
                         VALUES
                         ('" + cause_of_disabilityBox.Text+ "')", conn);

                    cmd0.ExecuteNonQuery();
                    conn.Close();
                }

                // iF WALA NMAN YUNG DISABILITY AND INISPECIFY SA TEXTBOX ILLAAGAY SA disabilityTbl SA DATABASE BEFORE IEXCUTE YUNG RECORDING TO pwd_recordsTbl
                if (othersDisabilityspecifiedCode == 0)
                {
                    conn.Open();
                    SqlCommand cmd0 = new SqlCommand(@"INSERT INTO [dbo].[disabilityTbl]
                        ([DISABILITY])
                         VALUES
                         ('" + disabilityBox.Text + "')", conn);

                    cmd0.ExecuteNonQuery();
                    conn.Close();
                }



                //IF OVER OR EQUAL TO 18
                if (needguardian >= 18)
                {
                    // VALIDITY DATE VARIABLES

                    //CHECK IF THE PICTUREBOX IS NOT NULL TO EXECUTE METHODS,  IF NULL A MESSAGEBOX WILL BE SHOWN
                    if (!string.IsNullOrEmpty(imgLocation))
                    {

                        int guardianNameCode = guardian_nameCode();
                        int causeofDisCode = cause_of_disabilityCode();
                        int disCode = disabilitiesCode();

                        string imageDirectory = imgLocation;
                        string username = userlogin().ToString();

                        // SQL COMMAND INSERTING DATA TO DATABASE
                        conn.Open();
                        SqlCommand cmd1 = new SqlCommand(@"INSERT INTO [dbo].[pwd_recordsTbl]
                       ([ID_NUMBER]
                       ,[LAST_NAME]
                       ,[FIRST_NAME]
                       ,[MIDDLE_NAME]
                       ,[AGE]
                       ,[GENDER]
                       ,[BIRTHDAY]
                       ,[DISABILITY_ID]
                       ,[GUARDIAN_ID]
                       ,[CONTACT_NO]
                       ,[HOUSE_NO]
                       ,[ADDRESS]
                       ,[VALIDITY]
                       ,[DATE_ISSUED],[IMAGE_DIRECTORY],[CAUSE_OF_DISABILITY_ID],[PWD_STATUS],[CREATED_BY])
                        VALUES
                        ('" + regisLabel.Text.ToString() + "','" + lastnameBox.Text.ToString() + "','" + firstnameBox.Text.ToString() + "','" + middlenameBox.Text.ToString() + "','" + agetxtBox.Text.ToString() + "', '" + genderCbox.Text.ToString() +
                        "','" + dateTimePicker1.Text.ToString() + "','" + disCode + "', '" + guardianNameCode + "','" + contactnumBox.Text.ToString() +
                        "','" + housenumBox.Text.ToString() + "','" + addressBox.Text.ToString() + "','" + validitydateTimePicker2.Text.ToString() + "','" + timer.Text.ToString() + "','" + imageDirectory + "','" + causeofDisCode + "','" + pwdStatus + "','"+username+"')", conn);


                        cmd1.ExecuteNonQuery();
                        conn.Close();
                        AUTO_ID();
                        MessageBox.Show("Registered Successfully");
                        txtBoxClear();
                        conn.Close();


                    }
                    else
                    {
                        MessageBox.Show("Please Upload an Image");
                    }

                }
                // IF WLANG LAMAN YUNG TEXTBOXES PAG MINORITY ANG NIREREGISTER ITO LALABAS
                else if (string.IsNullOrWhiteSpace(GlastnameBox.Text) || string.IsNullOrWhiteSpace(GfirstnameBox.Text) || string.IsNullOrWhiteSpace(GmiddlenameBox.Text))
                {
                    MessageBox.Show("Guardian's Fullname is Required for Minorities");
                }


                // IF NALAGYAN NA NG INPUT ANG MGA TEXTBOXES DURING MINOR REGISTRATION
                else if (!string.IsNullOrWhiteSpace(GlastnameBox.Text) || !string.IsNullOrWhiteSpace(GfirstnameBox.Text) || !string.IsNullOrWhiteSpace(GmiddlenameBox.Text))
                {
                    //PAG WALA YUNG RECORD NG GUARDIAN SA DATABASE, IRERECORD TO SA DATABASE
                    if (guardianCode == 0)
                    {
                        conn.Open();
                        SqlCommand cmd0 = new SqlCommand(@"INSERT INTO [dbo].[guardian_namesTbl]
                        ([GUARDIAN_NAME])
                         VALUES
                         ('" + GlastnameBox.Text.ToString() + "," + GfirstnameBox.Text.ToString() + "," + GmiddlenameBox.Text.ToString() + "')", conn);

                        cmd0.ExecuteNonQuery();
                        conn.Close();
                    }

                   
                    //IF WALA YUNG DISABILITY AND INISPECIFY SA TEXTBOX ILALAGAY SIYA SA CAUSE_OF_DISABILITYTBL SA DATABASE
                    if (otherCauseDisabilityCode == 0)
                    {
                        conn.Open();
                        SqlCommand cmd0 = new SqlCommand(@"INSERT INTO [dbo].[cause_of_disabilityTbl]
                        ([CAUSE_OF_DISABILITY])
                         VALUES
                         ('" + cause_of_disabilityBox.Text + "')", conn);

                        cmd0.ExecuteNonQuery();
                        conn.Close();
                    }
                    // PANG ADD NG SPECIFIED NA DISABILITY SA DATABASE

                    if (othersDisabilityspecifiedCode == 0)
                    {
                        conn.Open();
                        SqlCommand cmd0 = new SqlCommand(@"INSERT INTO [dbo].[disabilityTbl]
                        ([DISABILITY])
                         VALUES
                         ('" + disabilityBox.Text + "')", conn);

                        cmd0.ExecuteNonQuery();
                        conn.Close();
                    }

                    // NAULIT LANG ITONG CODE KASI DI KO ALAM AYUSIN YUNG ISANG BESES LANG ICCALL
                    //CHECK IF THE PICTUREBOX IS NOT NULL TO EXECUTE METHODS,  IF NULL A MESSAGEBOX WILL BE SHOWN
                    if (!string.IsNullOrEmpty(imgLocation))
                        {

                            int guardianNameCode = guardian_nameCode();
                            int causeofDisCode = cause_of_disabilityCode();

                            string imageDirectory = imgLocation;
                            string username = userlogin().ToString();

                            // SQL COMMAND INSERTING DATA TO DATABASE
                            conn.Open();
                            SqlCommand cmd1 = new SqlCommand(@"INSERT INTO [dbo].[pwd_recordsTbl]
                           ([ID_NUMBER]
                           ,[LAST_NAME]
                           ,[FIRST_NAME]
                           ,[MIDDLE_NAME]
                           ,[AGE]
                           ,[GENDER]
                           ,[BIRTHDAY]
                           ,[DISABILITY_ID]
                           ,[GUARDIAN_ID]
                           ,[CONTACT_NO] 
                           ,[HOUSE_NO]
                           ,[ADDRESS]
                           ,[VALIDITY]
                           ,[DATE_ISSUED],[IMAGE_DIRECTORY],[CAUSE_OF_DISABILITY_ID],[PWD_STATUS],[CREATED_BY])
                            VALUES
                             ('" + regisLabel.Text.ToString() + "','" + lastnameBox.Text.ToString() + "','" + firstnameBox.Text.ToString() + "','" + middlenameBox.Text.ToString() + "','" + agetxtBox.Text.ToString() + "', '" + genderCbox.Text.ToString() +
                            "','" + dateTimePicker1.Text.ToString() + "','" + disabilityCode + "', '" + guardianNameCode + "','" + contactnumBox.Text.ToString() +
                            "','" + housenumBox.Text.ToString() + "','" + addressBox.Text.ToString() + "','" + validitydateTimePicker2.Text.ToString() + "','" + timer.Text.ToString() + "','" + imageDirectory + "','" + causeofDisCode + "','" + pwdStatus +"','"+username+"')", conn);


                            cmd1.ExecuteNonQuery();
                            conn.Close();
                            AUTO_ID();
                            MessageBox.Show("Registered Successfully");
                            txtBoxClear();
                            conn.Close();
                        }
                        else
                        {
                            MessageBox.Show("Please Upload an Image");
                        }  
                }

            }
            catch 
            {
                //HOPEFULLY WALANG ERROR
                MessageBox.Show("Data Recorded");
            }

        }

        //FOR GETTING THE CODE EQUIVALENT FOR RECORDED VARYING DISABILITIES
        private int disabilitiesCode()
        {
            int disabilityId = 0;

            using (SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True"))
            {
                // Open the connection
                conn.Open();

                // Create a new SqlCommand object with the query and connection
                
                using (SqlCommand command = new SqlCommand("SELECT DISABILITY_ID FROM disabilityTbl WHERE DISABILITY = '"+disabilityBox.Text+"' UNION SELECT DISABILITY_ID FROM multiple_disabilityTbl WHERE MULTIPLE_DISABILITY = '"+disabilityBox.Text+"'", conn))
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
                                disabilityId = reader.GetInt32(0);
                            }
                        }
                        else
                        {
                            // No rows returned, handle this case accordingly
                        }
                    }
                }
            }

            return disabilityId;
        }
        private int userlogin()
        {
            int userId = 0;

            using (SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True"))
            {
                // Open the connection
                conn.Open();

                // Create a new SqlCommand object with the query and connection

                using (SqlCommand command = new SqlCommand("SELECT TOP 1(userID) FROM Login_Logs ORDER BY Date_Login DESC ", conn))
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
                                userId = reader.GetInt32(0);
                            }
                        }
                        else
                        {
                            // No rows returned, handle this case accordingly
                        }
                    }
                }
            }

            return userId;
        }






        //FOR GETTING THE GUARDIAN_ID 
        private int guardian_nameCode()
        {
            int guardianId = 0;
           
            using (SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True"))
            {
                // Open the connection
                conn.Open();

                // Create a new SqlCommand object with the query and connection

                using (SqlCommand command = new SqlCommand("SELECT GUARDIAN_ID FROM guardian_namesTbl WHERE GUARDIAN_NAME ='" + GlastnameBox.Text + "," + GfirstnameBox.Text + "," + GmiddlenameBox.Text + "'", conn))
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
                                guardianId = reader.GetInt32(0);
                            }
                        }
                        else
                        {
                            // No rows returned, handle this case accordingly
                        }
                    }
                }
            }

            return guardianId;
        }


        //FOR GETTING THE ID OF THE CAUSE OF DISABILITY
        private int cause_of_disabilityCode()
        {
            int guardianId = 0;

            using (SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True"))
            {
                // Open the connection
                conn.Open();

                // Create a new SqlCommand object with the query and connection

                using (SqlCommand command = new SqlCommand("SELECT CAUSE_OF_DISABILITY_ID FROM cause_of_disabilityTbl WHERE CAUSE_OF_DISABILITY ='" + cause_of_disabilityBox.Text + "'UNION SELECT CAUSE_OF_DISABILITY_ID FROM multiple_causesTbl WHERE MULTIPLE_CAUSES = '" + cause_of_disabilityBox.Text+"'", conn))
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
                                guardianId = reader.GetInt32(0);
                            }
                        }
                        else
                        {
                            // No rows returned, handle this case accordingly
                        }
                    }
                }
            }

            return guardianId;
        }






        private void AUTO_ID()
        {
            //AUTO GENERATING INCREMENTING ID_NUMBER ON EVERY REGISTRATION
            conn.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT (count(ID_NUMBER)) FROM [pwd_recordsTbl]", conn);
            int i = Convert.ToInt32(cmd2.ExecuteScalar());
            conn.Close();
            i++;
            regisLabel.Text = Convert.ToString(val + i) + prefix;

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            //PRELOADING AUTO GENERATING ID_NUMBER
            AUTO_ID();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy";
            validitydateTimePicker2.Format = DateTimePickerFormat.Custom;
            validitydateTimePicker2.CustomFormat = "MM/dd/yyyy";
            label25.Visible = false;
            label26.Visible = false;

            label27.Text = TextBoxValue;
        }
        private void IF_txtboxblank()
        {
            //VALIDATION IF USER MISSED A TEXTBOX
            if (string.IsNullOrWhiteSpace(lastnameBox.Text))
            { MessageBox.Show("lastName is required"); return; }
            if (string.IsNullOrWhiteSpace(firstnameBox.Text))
            { MessageBox.Show("firstName is required"); return; }
            if (string.IsNullOrWhiteSpace(middlenameBox.Text))
            { MessageBox.Show("middleName is required"); return; }
            if (string.IsNullOrWhiteSpace(agetxtBox.Text))
            { MessageBox.Show("Age is required"); return; }
            if (string.IsNullOrWhiteSpace(genderCbox.Text))
            { MessageBox.Show("Gender is required"); return; }
            if (string.IsNullOrWhiteSpace(disabilityBox.Text))
            { MessageBox.Show("Disability is required"); return; }
            if (string.IsNullOrWhiteSpace(cause_of_disabilityBox.Text))
            { MessageBox.Show("Cause of disability is required"); return; }
            if (string.IsNullOrWhiteSpace(contactnumBox.Text))
            { MessageBox.Show("contactNumber is required"); return; }
            if (string.IsNullOrWhiteSpace(housenumBox.Text))
            { MessageBox.Show("houseNumber is required"); return; }
            if (string.IsNullOrWhiteSpace(addressBox.Text))
            { MessageBox.Show("address is required"); return; }
            if (pictureBox1.Image == null)
            { MessageBox.Show("Please Upload a Picture"); return; }
        }
       
        // CLEARING ALL COMPONENTS AFTER REGISTER
        private void txtBoxClear()
        {
            lastnameBox.Text = "";
            firstnameBox.Text = "";
            middlenameBox.Text = "";
            agetxtBox.Text = "";
            genderCbox.Text = "";
            disabilityBox.Text = " ";
            cause_of_disabilityBox.Text = " ";
            GlastnameBox.Text = "";
            GfirstnameBox.Text = "";
            GmiddlenameBox.Text = "";
            contactnumBox.Text = "";
            housenumBox.Text = "";
            addressBox.Text = "";
            hearingCB.Checked = false;
            mentalCB.Checked = false;
            speechCB.Checked = false;
            learningCB.Checked = false;
            physicalCB.Checked = false;
            visualCB.Checked = false;
            orthopedicCB.Checked = false;
            psychoCB.Checked = false;
            pictureBox1.Image = null;   

            acquiredCB.Checked = false;
            cancerCB.Checked = false;   
            chronicCB.Checked = false;
            inbornCB.Checked = false;
            injuryCB.Checked = false;
            rarediseaseCB.Checked = false;
            autismCB.Checked = false;
            othersChbox.Checked = false;
            cause_othersCB.Checked = false; 
            


        }


        private void ClearButton_Click(object sender, EventArgs e)
        {
            txtBoxClear();
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            //UPLOAD BUTTON

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)|*.*)";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imgLocation;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void sign_outButton_Click(object sender, EventArgs e)
        {
            //Back BUTTON
            dashboard dashboard = new dashboard();
            dashboard.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.PreviousForm = this;
            form5.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer.Text = DateTime.Now.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Assuming you have a DateTimePicker named dateTimePicker1

            // Get the selected year from the DateTimePicker
            int selectedYear = dateTimePicker1.Value.Year;

            // Get the current year
            int currentYear = DateTime.Now.Year;

            // Calculate the age
            int age = currentYear - selectedYear;

            // Set the calculated age to a TextBox control named textBoxAge
            agetxtBox.Text = age.ToString();
        }



        private void checkboxdisabilities()
        {
            int num_checked = 0;
            string output = "";

            if (hearingCB.Checked)
            {
                output += "HEARING";
                num_checked++;
            }
            if (mentalCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "MENTAL";
                num_checked++;
            }
            if (speechCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "SPEECH";
                num_checked++;
            }
            if (learningCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "LEARNING";
                num_checked++;
            }
            if (physicalCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "PHYSICAL";
                num_checked++;
            }
            if (visualCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "VISUAL";
                num_checked++;
            }
            if (orthopedicCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "ORTHOPEDIC";
                num_checked++;
            }
            if (psychoCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "PSYCHOSOCIAL";
                num_checked++;
            }

            

            if (num_checked == 0)
            {
                output = " ";
            }
            else if (num_checked >= 3)
            {
                output = "MULTIPLE DISABILITIES";
            }

            disabilityBox.Text = output;
        }

        private void hearingCB_CheckedChanged(object sender, EventArgs e)
        {
            checkboxdisabilities();
        }

        private void mentalCB_CheckedChanged(object sender, EventArgs e)
        {
            checkboxdisabilities();
        }

        private void speechCB_CheckedChanged(object sender, EventArgs e)
        {
            checkboxdisabilities();
        }

        private void learningCB_CheckedChanged(object sender, EventArgs e)
        {
            checkboxdisabilities();
        }

        private void chronicCB_CheckedChanged(object sender, EventArgs e)
        {
            checkboxdisabilities();
        }

        private void visualCB_CheckedChanged(object sender, EventArgs e)
        {
            checkboxdisabilities();
        }

        private void orthopedicCB_CheckedChanged(object sender, EventArgs e)
        {
            checkboxdisabilities();
        }

        private void psychoCB_CheckedChanged(object sender, EventArgs e)
        {
            checkboxdisabilities();
        }

        private void othersChbox_CheckedChanged(object sender, EventArgs e)
        {
            disabilityBox.Text = disabilityBox.Text.ToUpper();
            disabilityBox.ReadOnly = !othersChbox.Checked;
           
            if (othersChbox.Checked)
            {
                label25.Visible = true;
            }
            else
            {
                label25.Visible = false;
            }
        }

        private void causecheckBoxCheck()
        {

            int num_checked = 0;
            string output = "";

            if (acquiredCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "ACQUIRED";
                num_checked++;
            }
            if (cancerCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "CANCER";
                num_checked++;
            }
            if (chronicCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "CHRONIC ILLNESS";
                num_checked++;
            }
            if (inbornCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "INBORN";
                num_checked++;
            }
            if (injuryCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "INJURY";
                num_checked++;
            }
            if (rarediseaseCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "RARE DISEASE";
                num_checked++;
            }
            if (autismCB.Checked)
            {
                if (num_checked > 0)
                {
                    output += "/";
                }
                output += "AUTISM";
                num_checked++;
            }

            if (num_checked == 0)
            {
                output = "  ";
            }
            else if (num_checked >= 3)
            {
                output = "MULTIPLE CAUSE";
            }

            cause_of_disabilityBox.Text = output;

        }

        private void acquiredCB_CheckedChanged(object sender, EventArgs e)
        {
            causecheckBoxCheck();
        }

        private void cancerCB_CheckedChanged(object sender, EventArgs e)
        {
            causecheckBoxCheck();
        }

        private void chronicCB_CheckedChanged_1(object sender, EventArgs e)
        {
            causecheckBoxCheck();
        }

        private void inbornCB_CheckedChanged(object sender, EventArgs e)
        {
            causecheckBoxCheck();
        }

        private void injuryCB_CheckedChanged(object sender, EventArgs e)
        {
            causecheckBoxCheck();
        }

        private void rarediseaseCB_CheckedChanged(object sender, EventArgs e)
        {
            causecheckBoxCheck();
        }

        private void autismCB_CheckedChanged(object sender, EventArgs e)
        {
            causecheckBoxCheck();
        }

        private void cause_othersCB_CheckedChanged(object sender, EventArgs e)
        {
            cause_of_disabilityBox.Text = cause_of_disabilityBox.Text.ToUpper();
            cause_of_disabilityBox.ReadOnly = !cause_othersCB.Checked;

            if (cause_othersCB.Checked)
            {
                label26.Visible = true;
            }
            else
            {
                label26.Visible = false;
            }

        }
        
        private void agetxtBox_TextChanged(object sender, EventArgs e)
        {

            //IF AGE IS LESS THAN 18 GUARDIAN NAME IS REQUIRED 
            int agechecker = Convert.ToInt32(agetxtBox.Text);
            if (agechecker < 18)
            {
                asteriskFirstname.Text = "*";
                asteriskLastname.Text = "*";
                asteriskmiddlename.Text = "*";
            }//IF THE AGE IS GREATER THAN OR EQUAL TO 18 GUARDIAN NAME IS OPTIONAL
            else if (agechecker >= 18) 
            {
                asteriskFirstname.Text = " ";
                asteriskLastname.Text = " ";
                asteriskmiddlename.Text = " ";

            }
            agecheck = agechecker;
        }
    }
}
