﻿namespace AxesoFeng
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.ReaderPicture = new System.Windows.Forms.PictureBox();
            this.ReportPicture = new System.Windows.Forms.PictureBox();
            this.SyncPicture = new System.Windows.Forms.PictureBox();
            this.SearchPicture = new System.Windows.Forms.PictureBox();
            this.LogoPicture = new System.Windows.Forms.PictureBox();
            this.ExitPicture = new System.Windows.Forms.PictureBox();
            this.pbClear = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // ReaderPicture
            // 
            this.ReaderPicture.Location = new System.Drawing.Point(9, 88);
            this.ReaderPicture.Name = "ReaderPicture";
            this.ReaderPicture.Size = new System.Drawing.Size(96, 60);
            this.ReaderPicture.Click += new System.EventHandler(this.ReaderPicture_Click);
            // 
            // ReportPicture
            // 
            this.ReportPicture.Location = new System.Drawing.Point(111, 88);
            this.ReportPicture.Name = "ReportPicture";
            this.ReportPicture.Size = new System.Drawing.Size(96, 60);
            this.ReportPicture.Click += new System.EventHandler(this.ReportPicture_Click);
            // 
            // SyncPicture
            // 
            this.SyncPicture.Location = new System.Drawing.Point(9, 163);
            this.SyncPicture.Name = "SyncPicture";
            this.SyncPicture.Size = new System.Drawing.Size(96, 60);
            this.SyncPicture.Click += new System.EventHandler(this.SyncPicture_Click);
            // 
            // SearchPicture
            // 
            this.SearchPicture.Location = new System.Drawing.Point(111, 163);
            this.SearchPicture.Name = "SearchPicture";
            this.SearchPicture.Size = new System.Drawing.Size(96, 60);
            this.SearchPicture.Click += new System.EventHandler(this.SearchPicture_Click);
            // 
            // LogoPicture
            // 
            this.LogoPicture.Location = new System.Drawing.Point(9, 18);
            this.LogoPicture.Name = "LogoPicture";
            this.LogoPicture.Size = new System.Drawing.Size(120, 60);
            // 
            // ExitPicture
            // 
            this.ExitPicture.Location = new System.Drawing.Point(292, 18);
            this.ExitPicture.Name = "ExitPicture";
            this.ExitPicture.Size = new System.Drawing.Size(24, 24);
            this.ExitPicture.Click += new System.EventHandler(this.ExitPicture_Click);
            // 
            // pbClear
            // 
            this.pbClear.Image = ((System.Drawing.Image)(resources.GetObject("pbClear.Image")));
            this.pbClear.Location = new System.Drawing.Point(281, 198);
            this.pbClear.Name = "pbClear";
            this.pbClear.Size = new System.Drawing.Size(35, 25);
            this.pbClear.Click += new System.EventHandler(this.pbClear_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pbClear);
            this.Controls.Add(this.ExitPicture);
            this.Controls.Add(this.LogoPicture);
            this.Controls.Add(this.SyncPicture);
            this.Controls.Add(this.SearchPicture);
            this.Controls.Add(this.ReportPicture);
            this.Controls.Add(this.ReaderPicture);
            this.Menu = this.mainMenu1;
            this.Name = "MenuForm";
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ReaderPicture;
        private System.Windows.Forms.PictureBox ReportPicture;
        private System.Windows.Forms.PictureBox SyncPicture;
        private System.Windows.Forms.PictureBox SearchPicture;
        private System.Windows.Forms.PictureBox LogoPicture;
        private System.Windows.Forms.PictureBox ExitPicture;
        private System.Windows.Forms.PictureBox pbClear;
    }
}