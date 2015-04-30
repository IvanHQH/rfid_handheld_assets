using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AxesoFeng.Forms;

namespace AxesoFeng.Classes
{
    public partial class BaseFormReader : BaseForm
    {
        protected enum IndexDataView { upc = 0,name = 1, place_name = 2,place_id = 3 }
        protected DataView dataView;
        protected MessageComparison messageForm;
        protected MenuForm menu;
        private bool comparisonSuccesfull;
        protected bool CompSuccesfull
        {
            get { return comparisonSuccesfull; }
        }

        public BaseFormReader()
        {}

        protected void RefreshGrid(ref DataGrid reportGrid)
        {
            Config config = Config.getConfig(menu.pathFolderName + "config.json");
            ProductTable table = new ProductTable();
            Sync sync = new Sync(config.url,menu.idClient,menu.pathFolderName);
            //sync.UpdatedDataBase(menu.rrfid.m_TagTable, menu.products.items);
            foreach (UpcInventory item in menu.rrfid.fillUPCsInventory(menu.products))
            {
                table.addRow(item.upc, item.name);
            }
            dataView = new DataView(table);
            reportGrid.DataSource = dataView;
            reportGrid.TableStyles.Clear();
            reportGrid.TableStyles.Add(table.getStyle());
        }

        protected void ShowMessages(List<string> messages, String valueWarehouse)
        {
            if (messageForm != null)
            {
                messageForm.fillMessages(messages,valueWarehouse);
                messageForm.Show();
            }
        }

        protected List<RespInventory.Assets> ProductsRead()
        {
            List<RespInventory.Assets> assets = new List<RespInventory.Assets>();
            try
            {
                foreach (DataRow row in dataView.Table.Rows)
                {
                    assets.Add(new RespInventory.Assets(row.ItemArray[(int)IndexDataView.upc].ToString(),
                        row.ItemArray[(int)IndexDataView.name].ToString(),
                        "",1));
                }
            }
            catch (Exception exc) { }
            return assets;
        }

        protected void CompareTo(String valueWarehouse)
        {
            Inventory folio = new Inventory(menu.configData.url,menu.pathFolderName);
            List<string> messages = new List<string>();
            List<RespInventory.Assets> productsRead = ProductsRead();
            messages = folio.CompareTo(productsRead,valueWarehouse);
            comparisonSuccesfull = false;
            if (messages.Count == 0)
            {
                if (MessageBox.Show("¿Desea guardar la lectura?", "OK", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    messageForm.Save(valueWarehouse);
                comparisonSuccesfull = true;
            }
            else
                ShowMessages(messages, valueWarehouse);
        }

    }
}