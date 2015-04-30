using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AxesoFeng.Forms;
using AxesoFeng.Classes;

namespace AxesoFeng
{
    public partial class ListAssetsForm : BaseFormReader
    {
        public ListAssetsForm(MenuForm form)
        {
            InitializeComponent();
            menu = form;
            setColors(menu.configData);
        }

        private void reportGrid_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void ListAssetsForm_GotFocus(object sender, EventArgs e)
        {
            ProductTable table = new ProductTable();
            table.SetInventory();
            Inventory folio = new Inventory(menu.configData.url,menu.pathFolderName);
            RespInventory respFolio = folio.GETInventoryFile();
            foreach (RespInventory.Assets asset in respFolio.assets)
            {
                table.addRow(asset.upc, asset.name,asset.place_name);
            }
            DataView view = new DataView(table);
            reportGrid.DataSource = view;
            reportGrid.TableStyles.Clear();
            reportGrid.TableStyles.Add(table.getStyle());  
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {

        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}