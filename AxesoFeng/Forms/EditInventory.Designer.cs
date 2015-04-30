namespace RfidInventory.Forms
{
    partial class EditInventory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditInventory));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.pbSave = new System.Windows.Forms.PictureBox();
            this.pbBack = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblversion = new System.Windows.Forms.Label();
            this.txtshow = new System.Windows.Forms.TextBox();
            this.lblShowConfig = new System.Windows.Forms.Label();
            this.cmbVersion = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // pbSave
            // 
            this.pbSave.Image = ((System.Drawing.Image)(resources.GetObject("pbSave.Image")));
            this.pbSave.Location = new System.Drawing.Point(188, 119);
            this.pbSave.Name = "pbSave";
            this.pbSave.Size = new System.Drawing.Size(35, 25);
            this.pbSave.Click += new System.EventHandler(this.pbSave_Click);
            // 
            // pbBack
            // 
            this.pbBack.Image = ((System.Drawing.Image)(resources.GetObject("pbBack.Image")));
            this.pbBack.Location = new System.Drawing.Point(147, 119);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new System.Drawing.Size(35, 25);
            this.pbBack.Click += new System.EventHandler(this.pbBack_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(122, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "3 Inventario Simple";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(122, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "2 Inventario con zona";
            // 
            // lblversion
            // 
            this.lblversion.Location = new System.Drawing.Point(11, 58);
            this.lblversion.Name = "lblversion";
            this.lblversion.Size = new System.Drawing.Size(108, 20);
            this.lblversion.Text = "versión";
            // 
            // txtshow
            // 
            this.txtshow.Location = new System.Drawing.Point(124, 30);
            this.txtshow.Name = "txtshow";
            this.txtshow.Size = new System.Drawing.Size(98, 21);
            this.txtshow.TabIndex = 14;
            // 
            // lblShowConfig
            // 
            this.lblShowConfig.Location = new System.Drawing.Point(10, 25);
            this.lblShowConfig.Name = "lblShowConfig";
            this.lblShowConfig.Size = new System.Drawing.Size(108, 40);
            this.lblShowConfig.Text = "Mostrar en proxima ejecución";
            // 
            // cmbVersion
            // 
            this.cmbVersion.Items.Add("2");
            this.cmbVersion.Items.Add("3");
            this.cmbVersion.Location = new System.Drawing.Point(124, 56);
            this.cmbVersion.Name = "cmbVersion";
            this.cmbVersion.Size = new System.Drawing.Size(97, 22);
            this.cmbVersion.TabIndex = 21;
            // 
            // EditInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.cmbVersion);
            this.Controls.Add(this.pbSave);
            this.Controls.Add(this.pbBack);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblversion);
            this.Controls.Add(this.txtshow);
            this.Controls.Add(this.lblShowConfig);
            this.Menu = this.mainMenu1;
            this.Name = "EditInventory";
            this.Text = "Edit Inventory";
            this.GotFocus += new System.EventHandler(this.EditInventory_GotFocus);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.PictureBox pbBack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblversion;
        private System.Windows.Forms.TextBox txtshow;
        private System.Windows.Forms.Label lblShowConfig;
        private System.Windows.Forms.ComboBox cmbVersion;
    }
}