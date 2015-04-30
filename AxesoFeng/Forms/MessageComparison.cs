using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ReadWriteCsv;

namespace AxesoFeng.Forms
{
    public partial class MessageComparison : BaseForm
    {
        private MenuForm menu;
        private String valueWarehouse;
        public bool saveDiff;

        public MessageComparison(MenuForm form)
        {            
            InitializeComponent();
            menu = form;
            setColors(menu.configData);
        }

        public void fillMessages(List<String> messages,String valueWarehouse)
        {
            this.valueWarehouse = valueWarehouse;
            messagesListview.Items.Clear();
            foreach (String message in messages)
                messagesListview.Items.Add(new ListViewItem(message));
        }

        public void fillMessages(List<String> messages)
        {
            messagesListview.Items.Clear();
            foreach (String message in messages)
                messagesListview.Items.Add(new ListViewItem(message));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
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
            Save(valueWarehouse);
        }

        public void Save(String valueWarehouse)
        {
            this.valueWarehouse = valueWarehouse;
            menu.rrfid.stop();
            labelLog.Text = "Guardando";
            String folder = menu.pathFolderName;
            String path;
            DateTime timestamp = DateTime.Now;
            path = NameFile(TypeFile.epc, valueWarehouse,timestamp,false);
            menu.products.saveEPCs(menu.rrfid, folder, path);
            path = NameFile(TypeFile.upc, valueWarehouse,timestamp,false);
            menu.products.saveUPCs(menu.rrfid, folder, path);
            path = NameFile(TypeFile.upc, valueWarehouse, timestamp, true);
            SaveMessage(folder, path);
            menu.rrfid.clear();
            labelLog.Text = "";
            MessageBox.Show("Orden guardada");
            this.Hide();
        }

        private void SaveMessage(string folder, string path)
        {
            Directory.CreateDirectory(folder);
            using (CsvFileWriter writer = new CsvFileWriter(path))
            {
                if (messagesListview.Items.Count != 0)
                {
                    foreach (ListViewItem item in messagesListview.Items)
                        writer.WriteLine(item.Text + ",");
                    saveDiff = true;
                }
                else
                    writer.WriteLine("Comparasión exitosa");
            }
        }

        protected String NameFile(TypeFile type, String valueWarehouse,DateTime timestamp,Boolean message)
        {
            String dataName = menu.configData.id_client.ToString();//client id
            dataName += "_" + valueWarehouse;//(WarehouseBox.SelectedItem as ComboboxItem).Value.ToString();
            dataName += "_" + FormatDateTime(timestamp);
            String path;
            if (message)
                path = menu.pathFolderName + "message_" + dataName + ".csv";
            else
            {
                if (TypeFile.epc == type)
                    path = menu.pathFolderName + "iepcs_" + dataName + ".csv";
                else
                    path = menu.pathFolderName + "iupcs_" + dataName + ".csv";
            }
            return path;
        }

        private void MessageComparison_GotFocus(object sender, EventArgs e)
        {
            
        }

    }
}