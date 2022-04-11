namespace CloudinaryMigration
{
    partial class frmMain
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
            this.btnUpload = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.dlgSelectFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.txtFolderSelected = new System.Windows.Forms.TextBox();
            this.chkUploadPath = new System.Windows.Forms.CheckBox();
            this.txtDestinationpath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtrowidfrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtrowidto = new System.Windows.Forms.TextBox();
            this.rchTextboxUnprocessedDtls = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(399, 296);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(275, 37);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "Upload to Cloudinary";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(84, 89);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(429, 26);
            this.textBox1.TabIndex = 1;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(528, 84);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(47, 37);
            this.btnSelectFolder.TabIndex = 2;
            this.btnSelectFolder.Text = "...";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // txtFolderSelected
            // 
            this.txtFolderSelected.Location = new System.Drawing.Point(84, 142);
            this.txtFolderSelected.Name = "txtFolderSelected";
            this.txtFolderSelected.ReadOnly = true;
            this.txtFolderSelected.Size = new System.Drawing.Size(202, 26);
            this.txtFolderSelected.TabIndex = 3;
            // 
            // chkUploadPath
            // 
            this.chkUploadPath.AutoSize = true;
            this.chkUploadPath.Location = new System.Drawing.Point(295, 144);
            this.chkUploadPath.Name = "chkUploadPath";
            this.chkUploadPath.Size = new System.Drawing.Size(270, 24);
            this.chkUploadPath.TabIndex = 4;
            this.chkUploadPath.Text = "Include this folder for Cloudinary?";
            this.chkUploadPath.UseVisualStyleBackColor = true;
            // 
            // txtDestinationpath
            // 
            this.txtDestinationpath.Location = new System.Drawing.Point(84, 186);
            this.txtDestinationpath.Name = "txtDestinationpath";
            this.txtDestinationpath.Size = new System.Drawing.Size(202, 26);
            this.txtDestinationpath.TabIndex = 5;
            this.txtDestinationpath.Text = "Ingest";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(292, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(270, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cloudninary Folder path ( use / slash)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(84, 296);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(275, 37);
            this.button1.TabIndex = 9;
            this.button1.Text = "Sync to DB";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtrowidfrom
            // 
            this.txtrowidfrom.Location = new System.Drawing.Point(194, 242);
            this.txtrowidfrom.Name = "txtrowidfrom";
            this.txtrowidfrom.Size = new System.Drawing.Size(202, 26);
            this.txtrowidfrom.TabIndex = 10;
            this.txtrowidfrom.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Row ID from";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(415, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Row ID To";
            // 
            // txtrowidto
            // 
            this.txtrowidto.Location = new System.Drawing.Point(541, 242);
            this.txtrowidto.Name = "txtrowidto";
            this.txtrowidto.Size = new System.Drawing.Size(202, 26);
            this.txtrowidto.TabIndex = 13;
            // 
            // rchTextboxUnprocessedDtls
            // 
            this.rchTextboxUnprocessedDtls.Location = new System.Drawing.Point(84, 511);
            this.rchTextboxUnprocessedDtls.Name = "rchTextboxUnprocessedDtls";
            this.rchTextboxUnprocessedDtls.Size = new System.Drawing.Size(614, 143);
            this.rchTextboxUnprocessedDtls.TabIndex = 15;
            this.rchTextboxUnprocessedDtls.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(81, 476);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(163, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "Uprocessed Records ";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1707, 873);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.rchTextboxUnprocessedDtls);
            this.Controls.Add(this.txtrowidto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtrowidfrom);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDestinationpath);
            this.Controls.Add(this.chkUploadPath);
            this.Controls.Add(this.txtFolderSelected);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnUpload);
            this.Name = "frmMain";
            this.Text = "Cloudinary Migration - CLI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.FolderBrowserDialog dlgSelectFolder;
        private System.Windows.Forms.TextBox txtFolderSelected;
        private System.Windows.Forms.CheckBox chkUploadPath;
        private System.Windows.Forms.TextBox txtDestinationpath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtrowidfrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtrowidto;
        private System.Windows.Forms.RichTextBox rchTextboxUnprocessedDtls;
        private System.Windows.Forms.Label label4;
    }
}