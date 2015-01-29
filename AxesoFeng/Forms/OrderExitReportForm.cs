﻿using System;
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
    public partial class OrderExitReportForm : BaseForm
    {
        private MenuForm menu;

        public OrderExitReportForm(MenuForm form)
        {
            InitializeComponent();
            menu = form;
            setColors(menu.configData);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void OrderExitReport_Load(object sender, EventArgs e)
        {
            ReportBox.Items.Clear();
            string[] filePaths = Directory.GetFiles(@"\rfiddata");
            List<string> upcFiles = new List<string>();

            foreach (String path in filePaths)
            {
                if (path.StartsWith("\\rfiddata\\oupc"))
                    upcFiles.Add(path);
            }
            ComboboxItem item;
            string[] comp;
            foreach (String path1 in upcFiles)
            {
                //warehousesId = Convert.ToInt32(path1.Substring(0, path1.IndexOf("upcs")).Replace(@"\rfiddata\wh", ""));
                //item = new ComboboxItem();
                //item.Text = menu.warehouses.getById(warehousesId).name + "-" + DateTime.FromFileTime(Convert.ToInt64(path1.Substring(path1.IndexOf("upcs") + 4).Replace(@".csv", ""))).ToString("dd/MM/yy HH:mm");
                //item.Value = path1;
                //ReportBox.Items.Add(item);
                comp = path1.Split(new Char[] { '_' });
                item = new ComboboxItem();
                try
                {
                    item.Text = comp[3] + " " +
                            //Date only whit tens 
                          Sync.FormatDateTime(comp[4]).Substring(2, comp[4].Length - 2);
                    item.Value = path1;
                    ReportBox.Items.Add(item);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Nombre del archivo sin formato correcto", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    break;
                }    
            }
        }

        private void ReportBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductTable table = new ProductTable();

            String path = (ReportBox.SelectedItem as ComboboxItem).Value.ToString();

            using (CsvFileReader reader = new CsvFileReader(path))
            {
                CsvRow rowcsv = new CsvRow();
                while (reader.ReadRow(rowcsv))
                {
                    ///Oficialia
                    //table.addRow(rowcsv[0],rowcsv[1],rowcsv[2]);
                    table.addRow(rowcsv[0], rowcsv[1], rowcsv[2], rowcsv[3]);
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

        private void OrderExitReportForm_GotFocus(object sender, EventArgs e)
        {

        }
    }
}