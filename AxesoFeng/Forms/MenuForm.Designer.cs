namespace AxesoFeng
{
    partial class MenuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuForm));
            this.ReaderPicture = new System.Windows.Forms.PictureBox();
            this.ReportPicture = new System.Windows.Forms.PictureBox();
            this.SyncPicture = new System.Windows.Forms.PictureBox();
            this.SearchPicture = new System.Windows.Forms.PictureBox();
            this.LogoPicture = new System.Windows.Forms.PictureBox();
            this.ExitPicture = new System.Windows.Forms.PictureBox();
            this.pbClear = new System.Windows.Forms.PictureBox();
            this.pbEdit = new System.Windows.Forms.PictureBox();
            this.InventoryPicture = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // ReaderPicture
            // 
            this.ReaderPicture.Location = new System.Drawing.Point(15, 86);
            this.ReaderPicture.Name = "ReaderPicture";
            this.ReaderPicture.Size = new System.Drawing.Size(96, 60);
            this.ReaderPicture.Click += new System.EventHandler(this.ReaderPicture_Click);
            // 
            // ReportPicture
            // 
            this.ReportPicture.Location = new System.Drawing.Point(117, 86);
            this.ReportPicture.Name = "ReportPicture";
            this.ReportPicture.Size = new System.Drawing.Size(96, 60);
            this.ReportPicture.Click += new System.EventHandler(this.ReportPicture_Click);
            // 
            // SyncPicture
            // 
            this.SyncPicture.Location = new System.Drawing.Point(15, 161);
            this.SyncPicture.Name = "SyncPicture";
            this.SyncPicture.Size = new System.Drawing.Size(96, 60);
            this.SyncPicture.Click += new System.EventHandler(this.SyncPicture_Click);
            // 
            // SearchPicture
            // 
            this.SearchPicture.Location = new System.Drawing.Point(117, 161);
            this.SearchPicture.Name = "SearchPicture";
            this.SearchPicture.Size = new System.Drawing.Size(96, 60);
            this.SearchPicture.Click += new System.EventHandler(this.SearchPicture_Click);
            // 
            // LogoPicture
            // 
            this.LogoPicture.Image = ((System.Drawing.Image)(resources.GetObject("LogoPicture.Image")));
            this.LogoPicture.Location = new System.Drawing.Point(0, 0);
            this.LogoPicture.Name = "LogoPicture";
            this.LogoPicture.Size = new System.Drawing.Size(120, 60);
            // 
            // ExitPicture
            // 
            this.ExitPicture.Image = ((System.Drawing.Image)(resources.GetObject("ExitPicture.Image")));
            this.ExitPicture.Location = new System.Drawing.Point(278, 9);
            this.ExitPicture.Name = "ExitPicture";
            this.ExitPicture.Size = new System.Drawing.Size(35, 24);
            this.ExitPicture.Click += new System.EventHandler(this.ExitPicture_Click);
            // 
            // pbClear
            // 
            this.pbClear.Image = ((System.Drawing.Image)(resources.GetObject("pbClear.Image")));
            this.pbClear.Location = new System.Drawing.Point(281, 266);
            this.pbClear.Name = "pbClear";
            this.pbClear.Size = new System.Drawing.Size(35, 25);
            this.pbClear.Click += new System.EventHandler(this.pbClear_Click);
            // 
            // pbEdit
            // 
            this.pbEdit.Image = ((System.Drawing.Image)(resources.GetObject("pbEdit.Image")));
            this.pbEdit.Location = new System.Drawing.Point(240, 266);
            this.pbEdit.Name = "pbEdit";
            this.pbEdit.Size = new System.Drawing.Size(35, 25);
            this.pbEdit.Click += new System.EventHandler(this.pbEdit_Click);
            // 
            // InventoryPicture
            // 
            this.InventoryPicture.Location = new System.Drawing.Point(220, 86);
            this.InventoryPicture.Name = "InventoryPicture";
            this.InventoryPicture.Size = new System.Drawing.Size(96, 60);
            this.InventoryPicture.Click += new System.EventHandler(this.InventoryPicture_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 307);
            this.Controls.Add(this.InventoryPicture);
            this.Controls.Add(this.pbEdit);
            this.Controls.Add(this.pbClear);
            this.Controls.Add(this.ExitPicture);
            this.Controls.Add(this.LogoPicture);
            this.Controls.Add(this.SyncPicture);
            this.Controls.Add(this.SearchPicture);
            this.Controls.Add(this.ReportPicture);
            this.Controls.Add(this.ReaderPicture);
            this.Name = "MenuForm";
            this.Text = "Menu";
            this.GotFocus += new System.EventHandler(this.MenuForm_GotFocus);
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
        private System.Windows.Forms.PictureBox pbEdit;
        private System.Windows.Forms.PictureBox InventoryPicture;
    }
}