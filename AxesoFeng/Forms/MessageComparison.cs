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
        private List<string> assetsNamesDelete;
        private int indexInitExced;

        public MessageComparison(MenuForm form)
        {            
            InitializeComponent();
            menu = form;
            setColors(menu.configData);
            assetsNamesDelete = new List<string>();
        }

        public void fillMessages(List<String> messages,String valueWarehouse)
        {
            this.valueWarehouse = valueWarehouse;
            messagesListview.Items.Clear();
            int i = 0;
            foreach (String message in messages)
            {
                if (message.Equals("**Faltantes:"))
                    indexInitExced = i;
                messagesListview.Items.Add(new ListViewItem(message));
                i++;
            }
            assetsNamesDelete = new List<string>();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void messagesListview_GotFocus(object sender, EventArgs e)
        {

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
            if(menu.configInvent.version == 2)
                menu.products.saveEPCs(menu.rrfid, folder, path,assetsNamesDelete);
            else if (menu.configInvent.version == 3)
                menu.products.saveEPCs(menu.rrfid, folder, path);
            path = NameFile(TypeFile.upc, valueWarehouse,timestamp,false);
            if (menu.configInvent.version == 2)
                menu.products.saveUPCs(menu.rrfid, folder, path, assetsNamesDelete);
            else if (menu.configInvent.version == 3)
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

        private void pbDelete_Click(object sender, EventArgs e)
        {
            if(messagesListview.SelectedIndices.Count>0)
            {
                int index = messagesListview.SelectedIndices[0];
                string asset =  messagesListview.Items[index].Text;
                string[] words; 
                if (index < indexInitExced - 1 && asset.Length > 0)
                {
                    if (MessageBox.Show("¿Está seguro de eliminar " + asset +
                        " de la lectura?", "Confirmación", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        messagesListview.Items.RemoveAt(index);
                    words = asset.Split(' ');
                    assetsNamesDelete.Add(words[words.Length - 1]);
                }
                else
                    MessageBox.Show("No se permite eliminar los faltantes","Aviso",
                        MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
            }
        }

        private void MessageComparison_GotFocus(object sender, EventArgs e)
        {
            
        }

    }
}