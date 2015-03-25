﻿namespace AxesoFeng
{
    partial class SearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
            this.productBox = new System.Windows.Forms.ComboBox();
            this.EPCBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbBuscar = new System.Windows.Forms.PictureBox();
            this.pbBack = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // productBox
            // 
            this.productBox.Location = new System.Drawing.Point(4, 23);
            this.productBox.Name = "productBox";
            this.productBox.Size = new System.Drawing.Size(233, 22);
            this.productBox.TabIndex = 0;
            this.productBox.SelectedIndexChanged += new System.EventHandler(this.productBox_SelectedIndexChanged);
            // 
            // EPCBox
            // 
            this.EPCBox.Location = new System.Drawing.Point(4, 80);
            this.EPCBox.Name = "EPCBox";
            this.EPCBox.Size = new System.Drawing.Size(233, 21);
            this.EPCBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Por producto:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.Text = "Por EPC:";
            // 
            // pbBuscar
            // 
            this.pbBuscar.Image = ((System.Drawing.Image)(resources.GetObject("pbBuscar.Image")));
            this.pbBuscar.Location = new System.Drawing.Point(202, 190);
            this.pbBuscar.Name = "pbBuscar";
            this.pbBuscar.Size = new System.Drawing.Size(35, 25);
            this.pbBuscar.Click += new System.EventHandler(this.SearchEPCButton_Click);
            // 
            // pbBack
            // 
            this.pbBack.Image = ((System.Drawing.Image)(resources.GetObject("pbBack.Image")));
            this.pbBack.Location = new System.Drawing.Point(161, 190);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new System.Drawing.Size(35, 25);
            this.pbBack.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.pbBuscar);
            this.Controls.Add(this.pbBack);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EPCBox);
            this.Controls.Add(this.productBox);
            this.Name = "SearchForm";
            this.Text = "Buscar";
            this.GotFocus += new System.EventHandler(this.SearchForm_GotFocus);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox productBox;
        private System.Windows.Forms.TextBox EPCBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbBuscar;
        private System.Windows.Forms.PictureBox pbBack;
    }
}