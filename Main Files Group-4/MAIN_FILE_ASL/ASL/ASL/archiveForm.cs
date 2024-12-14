using iTextSharp.text.pdf;
using iTextSharp.text;
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

namespace ASL
{
    public partial class archiveForm : Form
    {
        public archiveForm()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=PC-1;Initial Catalog=records_of_pwd;Integrated Security=True");
        private void archiveForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'records_of_pwdDataSet.inactive_archiveTbl' table. You can move, or remove it, as needed.
            this.inactive_archiveTblTableAdapter1.Fill(this.records_of_pwdDataSet.inactive_archiveTbl);


        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            displayTable();
        }
        private void displayTable()
        {


            conn.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM inactive_archiveTbl WHERE last_name='" + searchBox.Text + "' ORDER BY ID_NUMBER ASC", conn);

            SqlCommand cmd = new SqlCommand("SELECT * FROM inactive_archiveTbl WHERE last_name = '" + searchBox.Text + "' UNION ALL SELECT * FROM inactive_archiveTbl WHERE First_name = '" + searchBox.Text + "' UNION ALL " +
           "SELECT * FROM inactive_archiveTbl WHERE CONCAT(LAST_NAME, ', ', FIRST_NAME) = '" + searchBox.Text + "' UNION ALL SELECT * FROM inactive_archiveTbl WHERE ID_NUMBER = '" + searchBox.Text + "' UNION ALL " +
           "SELECT * FROM inactive_archiveTbl WHERE AGE = '" + searchBox.Text + "' UNION ALL SELECT * FROM inactive_archiveTbl WHERE DISABILITY_ID = (SELECT DISABILITY_ID FROM disabilityTbl WHERE DISABILITY = '" + searchBox.Text + "') UNION ALL " +
           "SELECT * FROM inactive_archiveTbl WHERE DISABILITY_ID = (SELECT DISABILITY_ID FROM multiple_disabilityTbl WHERE MULTIPLE_DISABILITY = '" + searchBox.Text + "') UNION ALL SELECT * FROM inactive_archiveTbl WHERE PWD_STATUS = '" + searchBox.Text + "'", conn);

            SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            conn.Close();


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
                MessageBox.Show("Error Occured While Reading FIle", ex.Message);
            }
        }
    }
}
