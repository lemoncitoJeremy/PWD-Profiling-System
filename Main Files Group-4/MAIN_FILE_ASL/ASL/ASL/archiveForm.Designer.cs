namespace ASL
{
    partial class archiveForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.inactivearchiveTblBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.recordsofpwdDataSet4BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.records_of_pwdDataSet4 = new ASL.records_of_pwdDataSet4();
            this.inactivearchiveTblBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.inactive_archiveTblTableAdapter = new ASL.records_of_pwdDataSet4TableAdapters.inactive_archiveTblTableAdapter();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.records_of_pwdDataSet = new ASL.records_of_pwdDataSet();
            this.recordsofpwdDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.inactivearchiveTblBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.inactive_archiveTblTableAdapter1 = new ASL.records_of_pwdDataSetTableAdapters.inactive_archiveTblTableAdapter();
            this.iDNUMBERDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lASTNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fIRSTNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mIDDLENAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aGEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gENDERDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bIRTHDAYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dISABILITYIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAUSEOFDISABILITYIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gUARDIANIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cONTACTNODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hOUSENODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aDDRESSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vALIDITYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dATEISSUEDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iMAGEDIRECTORYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pWDSTATUSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cREATEDBYDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.printButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inactivearchiveTblBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordsofpwdDataSet4BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.records_of_pwdDataSet4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inactivearchiveTblBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.records_of_pwdDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordsofpwdDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inactivearchiveTblBindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDNUMBERDataGridViewTextBoxColumn,
            this.lASTNAMEDataGridViewTextBoxColumn,
            this.fIRSTNAMEDataGridViewTextBoxColumn,
            this.mIDDLENAMEDataGridViewTextBoxColumn,
            this.aGEDataGridViewTextBoxColumn,
            this.gENDERDataGridViewTextBoxColumn,
            this.bIRTHDAYDataGridViewTextBoxColumn,
            this.dISABILITYIDDataGridViewTextBoxColumn,
            this.cAUSEOFDISABILITYIDDataGridViewTextBoxColumn,
            this.gUARDIANIDDataGridViewTextBoxColumn,
            this.cONTACTNODataGridViewTextBoxColumn,
            this.hOUSENODataGridViewTextBoxColumn,
            this.aDDRESSDataGridViewTextBoxColumn,
            this.vALIDITYDataGridViewTextBoxColumn,
            this.dATEISSUEDDataGridViewTextBoxColumn,
            this.iMAGEDIRECTORYDataGridViewTextBoxColumn,
            this.pWDSTATUSDataGridViewTextBoxColumn,
            this.cREATEDBYDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.inactivearchiveTblBindingSource2;
            this.dataGridView1.Location = new System.Drawing.Point(-2, 83);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(894, 443);
            this.dataGridView1.TabIndex = 0;
            // 
            // inactivearchiveTblBindingSource1
            // 
            this.inactivearchiveTblBindingSource1.DataMember = "inactive_archiveTbl";
            this.inactivearchiveTblBindingSource1.DataSource = this.recordsofpwdDataSet4BindingSource;
            // 
            // recordsofpwdDataSet4BindingSource
            // 
            this.recordsofpwdDataSet4BindingSource.DataSource = this.records_of_pwdDataSet4;
            this.recordsofpwdDataSet4BindingSource.Position = 0;
            // 
            // records_of_pwdDataSet4
            // 
            this.records_of_pwdDataSet4.DataSetName = "records_of_pwdDataSet4";
            this.records_of_pwdDataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // inactivearchiveTblBindingSource
            // 
            this.inactivearchiveTblBindingSource.DataMember = "inactive_archiveTbl";
            this.inactivearchiveTblBindingSource.DataSource = this.records_of_pwdDataSet4;
            // 
            // inactive_archiveTblTableAdapter
            // 
            this.inactive_archiveTblTableAdapter.ClearBeforeFill = true;
            // 
            // searchBox
            // 
            this.searchBox.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBox.Location = new System.Drawing.Point(660, 30);
            this.searchBox.Multiline = true;
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(168, 25);
            this.searchBox.TabIndex = 200;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(571, 33);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 17);
            this.label16.TabIndex = 199;
            this.label16.Text = "Quick Search";
            // 
            // searchButton
            // 
            this.searchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchButton.ForeColor = System.Drawing.SystemColors.Window;
            this.searchButton.Image = global::ASL.Properties.Resources.background;
            this.searchButton.Location = new System.Drawing.Point(827, 30);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(53, 25);
            this.searchButton.TabIndex = 198;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = global::ASL.Properties.Resources._9ikiL1Aq_400x400_fotor_bg_remover_2023041521025;
            this.pictureBox4.Location = new System.Drawing.Point(-2, 5);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(95, 72);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 360;
            this.pictureBox4.TabStop = false;
            // 
            // records_of_pwdDataSet
            // 
            this.records_of_pwdDataSet.DataSetName = "records_of_pwdDataSet";
            this.records_of_pwdDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // recordsofpwdDataSetBindingSource
            // 
            this.recordsofpwdDataSetBindingSource.DataSource = this.records_of_pwdDataSet;
            this.recordsofpwdDataSetBindingSource.Position = 0;
            // 
            // inactivearchiveTblBindingSource2
            // 
            this.inactivearchiveTblBindingSource2.DataMember = "inactive_archiveTbl";
            this.inactivearchiveTblBindingSource2.DataSource = this.recordsofpwdDataSetBindingSource;
            // 
            // inactive_archiveTblTableAdapter1
            // 
            this.inactive_archiveTblTableAdapter1.ClearBeforeFill = true;
            // 
            // iDNUMBERDataGridViewTextBoxColumn
            // 
            this.iDNUMBERDataGridViewTextBoxColumn.DataPropertyName = "ID_NUMBER";
            this.iDNUMBERDataGridViewTextBoxColumn.HeaderText = "ID_NUMBER";
            this.iDNUMBERDataGridViewTextBoxColumn.Name = "iDNUMBERDataGridViewTextBoxColumn";
            this.iDNUMBERDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lASTNAMEDataGridViewTextBoxColumn
            // 
            this.lASTNAMEDataGridViewTextBoxColumn.DataPropertyName = "LAST_NAME";
            this.lASTNAMEDataGridViewTextBoxColumn.HeaderText = "LAST_NAME";
            this.lASTNAMEDataGridViewTextBoxColumn.Name = "lASTNAMEDataGridViewTextBoxColumn";
            this.lASTNAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fIRSTNAMEDataGridViewTextBoxColumn
            // 
            this.fIRSTNAMEDataGridViewTextBoxColumn.DataPropertyName = "FIRST_NAME";
            this.fIRSTNAMEDataGridViewTextBoxColumn.HeaderText = "FIRST_NAME";
            this.fIRSTNAMEDataGridViewTextBoxColumn.Name = "fIRSTNAMEDataGridViewTextBoxColumn";
            this.fIRSTNAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mIDDLENAMEDataGridViewTextBoxColumn
            // 
            this.mIDDLENAMEDataGridViewTextBoxColumn.DataPropertyName = "MIDDLE_NAME";
            this.mIDDLENAMEDataGridViewTextBoxColumn.HeaderText = "MIDDLE_NAME";
            this.mIDDLENAMEDataGridViewTextBoxColumn.Name = "mIDDLENAMEDataGridViewTextBoxColumn";
            this.mIDDLENAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // aGEDataGridViewTextBoxColumn
            // 
            this.aGEDataGridViewTextBoxColumn.DataPropertyName = "AGE";
            this.aGEDataGridViewTextBoxColumn.HeaderText = "AGE";
            this.aGEDataGridViewTextBoxColumn.Name = "aGEDataGridViewTextBoxColumn";
            this.aGEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gENDERDataGridViewTextBoxColumn
            // 
            this.gENDERDataGridViewTextBoxColumn.DataPropertyName = "GENDER";
            this.gENDERDataGridViewTextBoxColumn.HeaderText = "GENDER";
            this.gENDERDataGridViewTextBoxColumn.Name = "gENDERDataGridViewTextBoxColumn";
            this.gENDERDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bIRTHDAYDataGridViewTextBoxColumn
            // 
            this.bIRTHDAYDataGridViewTextBoxColumn.DataPropertyName = "BIRTHDAY";
            this.bIRTHDAYDataGridViewTextBoxColumn.HeaderText = "BIRTHDAY";
            this.bIRTHDAYDataGridViewTextBoxColumn.Name = "bIRTHDAYDataGridViewTextBoxColumn";
            this.bIRTHDAYDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dISABILITYIDDataGridViewTextBoxColumn
            // 
            this.dISABILITYIDDataGridViewTextBoxColumn.DataPropertyName = "DISABILITY_ID";
            this.dISABILITYIDDataGridViewTextBoxColumn.HeaderText = "DISABILITY_ID";
            this.dISABILITYIDDataGridViewTextBoxColumn.Name = "dISABILITYIDDataGridViewTextBoxColumn";
            this.dISABILITYIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cAUSEOFDISABILITYIDDataGridViewTextBoxColumn
            // 
            this.cAUSEOFDISABILITYIDDataGridViewTextBoxColumn.DataPropertyName = "CAUSE_OF_DISABILITY_ID";
            this.cAUSEOFDISABILITYIDDataGridViewTextBoxColumn.HeaderText = "CAUSE_OF_DISABILITY_ID";
            this.cAUSEOFDISABILITYIDDataGridViewTextBoxColumn.Name = "cAUSEOFDISABILITYIDDataGridViewTextBoxColumn";
            this.cAUSEOFDISABILITYIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // gUARDIANIDDataGridViewTextBoxColumn
            // 
            this.gUARDIANIDDataGridViewTextBoxColumn.DataPropertyName = "GUARDIAN_ID";
            this.gUARDIANIDDataGridViewTextBoxColumn.HeaderText = "GUARDIAN_ID";
            this.gUARDIANIDDataGridViewTextBoxColumn.Name = "gUARDIANIDDataGridViewTextBoxColumn";
            this.gUARDIANIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cONTACTNODataGridViewTextBoxColumn
            // 
            this.cONTACTNODataGridViewTextBoxColumn.DataPropertyName = "CONTACT_NO";
            this.cONTACTNODataGridViewTextBoxColumn.HeaderText = "CONTACT_NO";
            this.cONTACTNODataGridViewTextBoxColumn.Name = "cONTACTNODataGridViewTextBoxColumn";
            this.cONTACTNODataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hOUSENODataGridViewTextBoxColumn
            // 
            this.hOUSENODataGridViewTextBoxColumn.DataPropertyName = "HOUSE_NO";
            this.hOUSENODataGridViewTextBoxColumn.HeaderText = "HOUSE_NO";
            this.hOUSENODataGridViewTextBoxColumn.Name = "hOUSENODataGridViewTextBoxColumn";
            this.hOUSENODataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // aDDRESSDataGridViewTextBoxColumn
            // 
            this.aDDRESSDataGridViewTextBoxColumn.DataPropertyName = "ADDRESS";
            this.aDDRESSDataGridViewTextBoxColumn.HeaderText = "ADDRESS";
            this.aDDRESSDataGridViewTextBoxColumn.Name = "aDDRESSDataGridViewTextBoxColumn";
            this.aDDRESSDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vALIDITYDataGridViewTextBoxColumn
            // 
            this.vALIDITYDataGridViewTextBoxColumn.DataPropertyName = "VALIDITY";
            this.vALIDITYDataGridViewTextBoxColumn.HeaderText = "VALIDITY";
            this.vALIDITYDataGridViewTextBoxColumn.Name = "vALIDITYDataGridViewTextBoxColumn";
            this.vALIDITYDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dATEISSUEDDataGridViewTextBoxColumn
            // 
            this.dATEISSUEDDataGridViewTextBoxColumn.DataPropertyName = "DATE_ISSUED";
            this.dATEISSUEDDataGridViewTextBoxColumn.HeaderText = "DATE_ISSUED";
            this.dATEISSUEDDataGridViewTextBoxColumn.Name = "dATEISSUEDDataGridViewTextBoxColumn";
            this.dATEISSUEDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iMAGEDIRECTORYDataGridViewTextBoxColumn
            // 
            this.iMAGEDIRECTORYDataGridViewTextBoxColumn.DataPropertyName = "IMAGE_DIRECTORY";
            this.iMAGEDIRECTORYDataGridViewTextBoxColumn.HeaderText = "IMAGE_DIRECTORY";
            this.iMAGEDIRECTORYDataGridViewTextBoxColumn.Name = "iMAGEDIRECTORYDataGridViewTextBoxColumn";
            this.iMAGEDIRECTORYDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pWDSTATUSDataGridViewTextBoxColumn
            // 
            this.pWDSTATUSDataGridViewTextBoxColumn.DataPropertyName = "PWD_STATUS";
            this.pWDSTATUSDataGridViewTextBoxColumn.HeaderText = "PWD_STATUS";
            this.pWDSTATUSDataGridViewTextBoxColumn.Name = "pWDSTATUSDataGridViewTextBoxColumn";
            this.pWDSTATUSDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cREATEDBYDataGridViewTextBoxColumn
            // 
            this.cREATEDBYDataGridViewTextBoxColumn.DataPropertyName = "CREATED_BY";
            this.cREATEDBYDataGridViewTextBoxColumn.HeaderText = "CREATED_BY";
            this.cREATEDBYDataGridViewTextBoxColumn.Name = "cREATEDBYDataGridViewTextBoxColumn";
            this.cREATEDBYDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // printButton
            // 
            this.printButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.printButton.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printButton.ForeColor = System.Drawing.SystemColors.Window;
            this.printButton.Image = global::ASL.Properties.Resources.background;
            this.printButton.Location = new System.Drawing.Point(99, 23);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(82, 39);
            this.printButton.TabIndex = 361;
            this.printButton.Text = "Print";
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // archiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(891, 525);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pictureBox4);
            this.Name = "archiveForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "archiveForm";
            this.Load += new System.EventHandler(this.archiveForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inactivearchiveTblBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordsofpwdDataSet4BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.records_of_pwdDataSet4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inactivearchiveTblBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.records_of_pwdDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordsofpwdDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inactivearchiveTblBindingSource2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private records_of_pwdDataSet4 records_of_pwdDataSet4;
        private System.Windows.Forms.BindingSource inactivearchiveTblBindingSource;
        private records_of_pwdDataSet4TableAdapters.inactive_archiveTblTableAdapter inactive_archiveTblTableAdapter;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.BindingSource inactivearchiveTblBindingSource1;
        private System.Windows.Forms.BindingSource recordsofpwdDataSet4BindingSource;
        private System.Windows.Forms.BindingSource recordsofpwdDataSetBindingSource;
        private records_of_pwdDataSet records_of_pwdDataSet;
        private System.Windows.Forms.BindingSource inactivearchiveTblBindingSource2;
        private records_of_pwdDataSetTableAdapters.inactive_archiveTblTableAdapter inactive_archiveTblTableAdapter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDNUMBERDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lASTNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fIRSTNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mIDDLENAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aGEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gENDERDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bIRTHDAYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dISABILITYIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cAUSEOFDISABILITYIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gUARDIANIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cONTACTNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hOUSENODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aDDRESSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vALIDITYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dATEISSUEDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iMAGEDIRECTORYDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pWDSTATUSDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cREATEDBYDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button printButton;
    }
}