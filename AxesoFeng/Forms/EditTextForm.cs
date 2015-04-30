using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AxesoFeng.Classes;
using System.IO;
using Newtonsoft.Json;

namespace AxesoFeng
{
    public partial class EditTextForm : BaseFormReader
    {
        private String path;
        private String _pathVersion;
        public String PathVersion { get { return _pathVersion; } }
        private bool _restart;
        public bool Restart
        {
            get { return _restart; }
        }

        public EditTextForm(MenuForm form,String path, String pathVersion)
        {
            InitializeComponent();
            menu = form;
            setColors(menu.configData);
            this.path = path;
            this._pathVersion = pathVersion;
        }

        private void pbSave_Click(object sender, EventArgs e)
        {
            if (txtContraseña.Text.Equals("123"))
            {
                try
                {
                    menu.configData.url = txturl.Text;
                    menu.configData.id_client = int.Parse(txtIdClient.Text);
                    //menu.configData.version = int.Parse(txtversion.Text);
                    menu.configInvent.version = int.Parse(cmbVersion.Text);
                    using (StreamWriter writer = new StreamWriter(path, false))
                    {
                        writer.Write(JsonConvert.SerializeObject(menu.configData));
                    }
                    using (StreamWriter writer = new StreamWriter(PathVersion, false))
                    {
                        writer.Write(JsonConvert.SerializeObject(menu.configInvent));
                    }
                    _restart = true;
                    Hide();
                }
                catch (Exception exc) { }
            }
            else
            { 
                MessageBox.Show("Contraseña invalida", "Autenticación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1); 
            }
        }

        private void EditTextForm_GotFocus(object sender, EventArgs e)
        {
            txtIdClient.Text = menu.configData.id_client.ToString();
            txturl.Text = menu.configData.url;
            cmbVersion.Text = menu.configInvent.version.ToString();
            _restart = false;
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}