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

        public BaseFormReader()
        {}

        protected void RefreshGrid(ref DataGrid reportGrid)
        {
            Config config = Config.getConfig(@"\rfiddata\config.json");
            ProductTable table = new ProductTable();
            Sync sync = new Sync(config.url,menu.idClient);
            sync.UpdatedDataBase(menu.rrfid.m_TagTable, menu.products.items);
            foreach (UpcInventory item in menu.rrfid.fillUPCsInventory(menu.products))
            {
                ///Oficialia
                //table.addRow(item.upc, item.name, item.total.ToString());
                table.addRow(item.upc, item.name);
                ///
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

        protected List<RespFolio.Assets> ProductsRead()
        {
            List<RespFolio.Assets> assets = new List<RespFolio.Assets>();
            try
            {
                foreach (DataRow row in dataView.Table.Rows)
                {
                    //Oficialia
                    //products.Add(new RespFolio.Assets(row.ItemArray[(int)IndexDataView.name].ToString(),
                    //    int.Parse(row.ItemArray[(int)IndexDataView.quantity].ToString())));
                    assets.Add(new RespFolio.Assets(row.ItemArray[(int)IndexDataView.upc].ToString(),
                        row.ItemArray[(int)IndexDataView.name].ToString(),
                        "",1));
                    ////
                }
            }
            catch (Exception exc) { }
            return assets;
        }

        protected void CompareTo(String valueWarehouse)
        {
            FolioOrder folio = new FolioOrder(menu.configData.url);
            List<string> messages = new List<string>();
            List<RespFolio.Assets> productsRead = ProductsRead();
            messages = folio.CompareTo(productsRead,valueWarehouse);
            if (messages.Count == 0)
            {
                if (MessageBox.Show("¿Desea guardar la lectura?", "OK", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    messageForm.Save(valueWarehouse);
            }
            else
                ShowMessages(messages, valueWarehouse);
        }

    }
}