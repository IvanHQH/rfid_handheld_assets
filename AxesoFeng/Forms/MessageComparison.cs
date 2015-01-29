using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AxesoFeng.Forms
{
    public partial class MessageComparison : BaseForm
    {
        private MenuForm menu;
        //private String nameFile;
        private String folio;
        private String valueWarehouse;

        public MessageComparison(MenuForm form)
        {            
            InitializeComponent();
            menu = form;
            setColors(menu.configData);
        }

        public void fillMessages(List<String> messages,String valueWarehouse, String folio)
        {
            //this.nameFile = nameFile;
            this.valueWarehouse = valueWarehouse;
            this.folio = folio;
            messagesListview.Items.Clear();
            foreach (String message in messages)
                messagesListview.Items.Add(new ListViewItem(message));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            menu.showCaptureFolio = false;
            this.Hide();
        }

        private void messagesListview_GotFocus(object sender, EventArgs e)
        {

        }

        private void messagesListview_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in messagesListview.Items)
            {
                if (item.Selected)
                {
                    MessageBox.Show(item.Text);
                    break;
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        { 
            Save(valueWarehouse,folio);
        }

        public void Save(String valueWarehouse,String folio)
        {
            this.valueWarehouse = valueWarehouse;
            this.folio = folio;

            menu.rrfid.stop();
            labelLog.Text = "Guardando";
            String folder = "\\rfiddata";
            String path;
            DateTime timestamp = DateTime.Now;
            path = NameFile(TypeFile.epc, valueWarehouse,timestamp);
            menu.products.saveEPCs(menu.rrfid, folder, path);
            path = NameFile(TypeFile.upc, valueWarehouse,timestamp);
            menu.products.saveUPCs(menu.rrfid, folder, path);
            menu.rrfid.clear();
            labelLog.Text = "";
            MessageBox.Show("Orden guardada");
            menu.showCaptureFolio = false;
            this.Hide();
        }

        protected String NameFile(TypeFile type, String valueWarehouse,DateTime timestamp)
        {
            
            String dataName = menu.idCustomer.ToString();
            dataName += "_" + valueWarehouse;//(WarehouseBox.SelectedItem as ComboboxItem).Value.ToString();
            dataName += "_" + folio;
            dataName += "_" + FormatDateTime(timestamp);
            String path;
            if (TypeFile.epc == type)
                path = "\\rfiddata\\iepcs_" + dataName + ".csv";
            else
                path = "\\rfiddata\\iupcs_" + dataName + ".csv";
            return path;
        }
    }
}