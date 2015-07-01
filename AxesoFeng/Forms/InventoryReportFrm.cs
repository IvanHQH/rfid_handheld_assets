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

namespace AxesoFeng
{
    public partial class InventoryReportFrm : BaseForm
    {
        private MenuForm menu;
        List<string> upcFiles;

        public InventoryReportFrm(MenuForm form)
        {
            InitializeComponent();
            menu = form;
            setColors(menu.configData);
        }

        private void Report_GotFocus(object sender, EventArgs e)
        {
            reportBox.Items.Clear();
            string[] filePaths = Directory.GetFiles(menu.pathFolderName);
            upcFiles = new List<string>();

            foreach (String path in filePaths){
                if (path.StartsWith(menu.pathFolderName + "iupc"))
                    upcFiles.Add(path);
            }
            string[] comp;            
            foreach (String path1 in upcFiles)                
            {
                comp = path1.Split(new Char[] { '_' });
                try
                {
                    //Date only whit tens 
                    //public enum index
                    //{
                    //    client_id = 1,
                    //    warehouse_id = 2,
                    //    date_time = 3,
                    //}
                    if (menu.configInvent.version == 2)
                    {
                        reportBox.Items.Add(getNameWarehouse(int.Parse(comp[(int)Sync.SyncOrdenEsM.index.warehouse_id]),
                            menu.warehouses.collection) + " " +
                        Sync.FormatDateTime(comp[(int)Sync.SyncOrdenEsM.index.date_time]).Substring(2, 
                            comp[(int)Sync.SyncOrdenEsM.index.date_time].Length - 2));
                    }
                    else {
                        reportBox.Items.Add(Sync.FormatDateTime(comp[(int)Sync.SyncOrdenEsM.index.date_time]).Substring(2, 
                            comp[(int)Sync.SyncOrdenEsM.index.date_time].Length - 2));
                    }
                }
                catch (Exception exc) {
                    MessageBox.Show("Nombre del archivo sin formato correcto", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    this.Hide();
                    break;
                }                
            }
        }

        private void NamePlace(int idPlace)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void reportBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductTable table = new ProductTable(false,false);
            if (menu.configInvent.version == 2)
                table = new ProductTable(false, true);
            else if (menu.configInvent.version == 3)
                table = new ProductTable(true, false); 
            //String path=@"\rfiddata\iupc"+DateTime.Parse(reportBox.Items[reportBox.SelectedIndex].ToString()).ToFileTime().ToString()+".csv";
            if (reportBox.SelectedIndex < 0)
                return;
            String path = upcFiles[reportBox.SelectedIndex];

            using (CsvFileReader reader = new CsvFileReader(path))
            {
                CsvRow rowcsv = new CsvRow();
                while (reader.ReadRow(rowcsv))
                {
                    if (menu.configInvent.version == 2)
                        table.addRow(rowcsv[0], rowcsv[1], 
                            getNameWarehouse(int.Parse(rowcsv[2]), menu.warehouses.collection));
                    else if (menu.configInvent.version == 3)
                        table.addRow(rowcsv[0], rowcsv[1], int.Parse(rowcsv[2]));                    
                }
            }
            DataView view = new DataView(table);            
            reportGrid.DataSource = view;
            reportGrid.TableStyles.Clear();
            reportGrid.TableStyles.Add(table.getStyle());
            
        }

        private void reportGrid_MouseDown(object sender, MouseEventArgs e)
        {
            ProductTable.sortGrid(sender, e);
        }

    }
}