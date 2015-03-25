using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RestSharp;
using System.IO;
using ReadWriteCsv;
using Newtonsoft.Json;
using System.Reflection;
using AxesoFeng.Forms;


namespace AxesoFeng
{
    public partial class MenuForm : BaseForm
    {
        public int idClient;
        //Child Forms
        public InventoryReportFrm reports;
        public SearchForm search;
        public UPCSearchForm upcsearch;
        public LocateForm locate;
        private InventoryForm frmInventory;
        public SyncForm frmsync;
        private EditTextForm frmEditText;
        private ListAssetsForm frmListAsset;

        //RFID Reader
        public SimpleRFID rrfid;

        //Catalogs
        public AssetsList products;
        public Warehouses warehouses;

        //Synchronization
        public Sync sync;

        //Configuration
        public Config configData;
        public string myResDir;

        public MenuForm()
        {
            InitializeComponent();
            //Set Config Data
            configData = Config.getConfig(@"\rfiddata\config.json");
            idClient = configData.id_client;
            //Set Synchronization
            sync = new Sync(configData.url,idClient);
            sync.GET_Test();
            sync.GET();
            //Set Reader
            rrfid = new SimpleRFID();
            //Set Catalogs
            products = new AssetsList(@"\rfiddata\products.csv");
            warehouses = new Warehouses(@"\rfiddata\warehouses.csv");

            //Set Current Event Data
            frmInventory = new InventoryForm(this);
            reports = new InventoryReportFrm(this);
            search = new SearchForm(this);
            upcsearch = new UPCSearchForm(this);
            locate = new LocateForm(this);
            frmsync = new SyncForm(this);
            frmListAsset = new ListAssetsForm(this);
            frmEditText = new EditTextForm(this, @"\rfiddata\config.json");
            this.setColors(configData);
            /*
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"\rfiddata\img\logo.bmp");
            Image image = new Bitmap(stream);
            LogoPicture.Image = image;
              */

            string myDir = Path.GetDirectoryName(Assembly.GetCallingAssembly().GetName().CodeBase);
            myResDir = Path.Combine(myDir, @"\rfiddata\img");
            Image image;
            
            image = new Bitmap(Path.Combine(myResDir, "logo_hqh_med.bmp"));
            LogoPicture.Image = image;
            image = new Bitmap(Path.Combine(myResDir, "menu1.bmp"));
            ReaderPicture.Image = image;
            image = new Bitmap(Path.Combine(myResDir, "menu2.bmp"));
            ReportPicture.Image = image;
            image = new Bitmap(Path.Combine(myResDir, "menu3.bmp"));
            SearchPicture.Image = image;
            image = new Bitmap(Path.Combine(myResDir, "menu4.bmp"));
            SyncPicture.Image = image;
            image = new Bitmap(Path.Combine(myResDir, "InventTotal.png"));
            InventoryPicture.Image = image;
            //InventTotal
            //image = new Bitmap(Path.Combine(myResDir, "menu5_export.bmp"));
            //ExitPicture.Image = image;
        }

        private void ExitPicture_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ReaderPicture_Click(object sender, EventArgs e)
        {
            frmInventory.Show();
        }

        private void ReportPicture_Click(object sender, EventArgs e)
        {
            reports.Show();
        }

        private void SearchPicture_Click(object sender, EventArgs e)
        {
            search.Show();
        }

        private void SyncPicture_Click(object sender, EventArgs e)
        {            
            frmsync.Show();            
            products = new AssetsList(@"\rfiddata\products.csv");
            //products_bar = new ProductsList(@"\rfiddata\products_bar.csv");
            warehouses = new Warehouses(@"\rfiddata\warehouses.csv");
            if (sync.POST(frmsync,configData.id_user,configData.pwd,configData.id_client))
                MessageBox.Show("Sincronización exitosa", "Sincronización");
            frmsync.Hide();
        }

        private void pbClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de eliminar todas las lecturas?", "Confirmación", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                Inventory.DeleteFiles();
        }

        private void pbEdit_Click(object sender, EventArgs e)
        {
            frmEditText.Show();
        }

        private void MenuForm_GotFocus(object sender, EventArgs e)
        {
            try {
                if (frmEditText.Restart)
                    Application.Exit();
            }
            catch (Exception exc) { }
        }

        private void InventoryPicture_Click(object sender, EventArgs e)
        {
            frmListAsset.Show();
        }

    }
}