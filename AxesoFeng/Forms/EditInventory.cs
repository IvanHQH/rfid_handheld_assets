using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AxesoFeng;
using AxesoFeng.Classes;
using System.IO;
using Newtonsoft.Json;

namespace RfidInventory.Forms
{
    public partial class EditInventory : BaseFormReader
    {
        private String path;
        private bool _restart;
        public bool Restart
        {
            get { return _restart; }
        }

        public EditInventory(MenuForm form, String path)
        {
            InitializeComponent();
            menu = form;
            setColors(menu.configData);
            this.path = path;
        }

        private void pbSave_Click(object sender, EventArgs e)
        {
            try
            {
                menu.configInvent.version = int.Parse(cmbVersion.Text);
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    writer.Write(JsonConvert.SerializeObject(menu.configData));
                }
                _restart = true;
                Hide();
            }
            catch (Exception exc) { }
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void EditInventory_GotFocus(object sender, EventArgs e)
        {
            txtshow.Text = menu.configData.id_client.ToString();            
            _restart = false;
        }
    }
}