﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using ReadWriteCsv;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using MobileEPC;
using System.Collections;

namespace AxesoFeng
{
    public class Sync
    {

        private RestClient client;

        //private 

        private class GetObject {
            public List<SyncProduct> products { get; set; }
            public List<SyncWarehouse> warehouses { get; set; }
        }

        public class SyncProduct {
            public int id { get; set; }
            public String upc { get; set; }
            public String name { get; set; }
            public String description { get; set; }
        }

        private class SyncWarehouse
        {
            public int id { get; set; }
            public String name { get; set; }
            public String description { get; set; }
            public int customer_id { get; set; }
        }

        private class SyncOrdenEsM
        {
            public int customer_id;
            public string date_time;
            public string folio;
            public int type;
            public JsonArray epcs = new JsonArray();
            public int warehouse_id;
            
            public enum index
            {
                customer_id = 1,
                warehouse_id = 2,
                folio = 3,
                date_time = 4,
            }

            public SyncOrdenEsM(int customer_id,string date_time,string folio, int type,int warehouse_id)
            {
                this.customer_id = customer_id;
                this.date_time = date_time;
                this.folio = folio;
                this.type = type;
                this.warehouse_id = warehouse_id;
            }

        }

        public class SyncOrdenEsD
        {
            public int id;
            public int orden_es_m_id;
            public String epc;
            public int quantity;
            public String created_at;
            public String updated_at;
            public String upc;

            public SyncOrdenEsD(String epc, int quantity)
            {
                String datetime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); 
                this.orden_es_m_id = 0;
                this.epc = epc;
                this.quantity = quantity;
                this.updated_at = datetime;
                this.created_at = datetime;
                this.upc = EpcTools.getUpc(epc.ToString());
            }
        }

        public Sync(String BaseUrl)
        {
            client = new RestClient(BaseUrl);
        }

        public bool GET()
        {
            var request = new RestRequest("sync", Method.GET);
            IRestResponse<GetObject> response = client.Execute<GetObject>(request);
            GetObject data = response.Data;

            if (!requestError(response.StatusCode.ToString()))
                return false;

            Directory.CreateDirectory(@"\rfiddata");

            using (CsvFileWriter writer = new CsvFileWriter(@"\rfiddata\productsrfid.csv"))
            {                
                foreach (SyncProduct item in data.products)
                {
                    CsvRow row = new CsvRow();
                    row.Add(item.upc);
                    row.Add(item.name);
                    writer.WriteRow(row);
                }
            }

            using (CsvFileWriter writer = new CsvFileWriter(@"\rfiddata\warehouses.csv"))
            {
                foreach (SyncWarehouse item in data.warehouses)
                {
                    CsvRow row = new CsvRow();
                    row.Add(item.id.ToString());
                    row.Add(item.name);
                    writer.WriteRow(row);
                }
            }            
            return true;
        }

        public bool POST(SyncForm sync)
        {
            Directory.CreateDirectory(@"\rfiddataold");
            string[] filePaths = Directory.GetFiles(@"\rfiddata");
            List<string> upcFiles = new List<string>();
            List<string> upcFilesAll = new List<string>();
            List<string> epcFiles = new List<string>();

            //fill list file names 
            foreach (String path in filePaths)
            {
                if (path.Contains("epc") )
                    epcFiles.Add(path);
                if (path.Contains("epc") || path.Contains("upc"))
                    upcFilesAll.Add(path);
            }

            sync.updateInventory(upcFiles.Count.ToString() + " Inventarios");
            sync.updateOrder(epcFiles.Count.ToString() + " Salidas");
            Application.DoEvents();

            int numInventories = upcFiles.Count;
            int numOrders = epcFiles.Count;

            foreach (String path1 in epcFiles)
            {
                var request = new RestRequest("ordenesmhd", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(OrderMToJson(deserealizeNameFile(path1)));
                IRestResponse response = client.Execute(request);
                if (!requestError(response.StatusCode.ToString()))
                    return false;
                if (response.Content.Equals("yes save"))
                {
                    List<SyncOrdenEsD> orden_es_ds = new List<SyncOrdenEsD>();
                    using (CsvFileReader reader = new CsvFileReader(path1))
                    {
                        String epc;
                        CsvRow rowcsv = new CsvRow();
                        while (reader.ReadRow(rowcsv))
                            orden_es_ds.Add(new SyncOrdenEsD(rowcsv[0],1));
                    }
                    foreach (SyncOrdenEsD order in orden_es_ds)
                    {
                        request = new RestRequest("ordenesd", Method.POST);
                        request.RequestFormat = DataFormat.Json;
                        request.AddBody(OrderDToJson(order));
                        response = client.Execute(request);
                        if (!requestError(response.StatusCode.ToString()))
                            return false;
                    }                    
                }
                numInventories--;
                sync.updateInventory(numInventories + " Inventarios");
                Application.DoEvents();
                File.Move(path1, path1.Replace("rfiddata", "rfiddataold"));
                File.Move(path1.Replace("epc", "upc"), path1.Replace("rfiddata", "rfiddataold").Replace("epc", "upc"));
            }
            return true;
        }

        private bool requestError(String StatusCode)
        {            
            switch (StatusCode)
            {
                case "0":
                case "NotFound":
                    MessageBox.Show("Error de Red. No se pudieron sincronizar los datos con el servidor. Verifique que tiene su wifi encendida, que tiene acceso a la red y al servidor.", "Error");
                    return false;
                case "Forbidden":
                case "InternalServerError":
                    MessageBox.Show("Error contacte a su administrador.", "Error");
                    return false;
                case "OK":
                    return true;
                default:
                    MessageBox.Show(StatusCode, "Error");
                    return false;
            }
        }

        /// <summary>
        /// deserealize the name of a file for create an object OrdenEsM
        /// </summary>
        /// <param name="path"></param>
        /// 1(idCustomer)_1(type)_12345(folio)_1405301435(datetime)</param>
        /// <returns></returns>
        /// 
        private SyncOrdenEsM deserealizeNameFile(string path)
        {
            //1_1_1234124412_14-11-29-013045
            string[] comp = path.Split(new Char[] { '_'});
            SyncOrdenEsM orden = null;
            try
            {
                orden = new SyncOrdenEsM(int.Parse(comp[(int)SyncOrdenEsM.index.customer_id]),
                    FormatDateTime(comp[(int)SyncOrdenEsM.index.date_time].Replace(".csv","")),
                    comp[(int)SyncOrdenEsM.index.folio],
                    Type(comp[0].Replace("\\rfiddata\\","")[0]),
                    int.Parse(comp[(int)SyncOrdenEsM.index.warehouse_id]));
            }
            catch (Exception exc) { }
            return orden;
        }

        private int Type(char cType)
        {
            int iType = -1; 
            if(cType == 'i')
                iType = 1;
            else if(cType == 'o')
                iType = 0;
            return iType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime">format example 1405301435</param>
        /// <returns>string format 2014-12-31 20:30:35</returns>
        public static string FormatDateTime(String dateTime)
        {
            string[] dt = dateTime.Split(new Char[] { '-'});
            string dateTimeF = "20" + dt[0] + "-" + dt[1] + "-" + dt[2] + " " + 
                dt[3].Substring(0, 2) + ":" + dt[3].Substring(2, 2) + ":" + dt[3].Substring(4, 2);
            return dateTimeF;
        }

        private JsonObject OrderMToJson(SyncOrdenEsM OrderM)
        {
            JsonObject json = new JsonObject();
            json.Add("customer_id",OrderM.customer_id);
            json.Add("created_at",OrderM.date_time);
            json.Add("updated_at",OrderM.date_time);
            json.Add("folio",OrderM.folio);
            json.Add("type",OrderM.type);
            json.Add("warehouse_id", OrderM.warehouse_id);
            json.Add("pending",0);
            json.Add("handheld", 1);
            return json;
        }

        private JsonObject OrderDToJson(SyncOrdenEsD OrderD)
        {
            JsonObject json = new JsonObject();
            json.Add("id", 0);
            json.Add("epc", OrderD.epc);
            json.Add("updated_at", OrderD.updated_at);
            json.Add("created_at", OrderD.created_at);
            json.Add("quantity", OrderD.quantity);
            json.Add("upc", OrderD.upc);
            json.Add("orden_es_m_id", 0);
            return json;
        }


        internal void UpdatedDataBase(Hashtable productsRead, List<Asset> productlist)
        {
            List<String> productsUnre = ListUPCUnrecognized(productsRead,productlist);
            SyncProduct prodResp;
            foreach (String product in productsUnre)
            {
                prodResp = GETProduct(product);
                AddProductDataBase(prodResp);
            }
        }

        private bool AddProductDataBase(SyncProduct product)
        {
            using (CsvFileWriter writer = new CsvFileWriter(@"\rfiddata\productsrfid.csv"))
            {
                CsvRow row = new CsvRow();
                row.Add(product.upc);
                row.Add(product.name);
                writer.WriteRow(row);
            }
            var request = new RestRequest("add_product", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(product);
            IRestResponse response = client.Execute(request);
            if (!requestError(response.StatusCode.ToString()))
                return false;
            if (!response.Content.Equals("yes save"))
                return false;
            return true;
        }

        private List<String> ListUPCUnrecognized(Hashtable productsRead, List<Asset> productlist) 
        {
            Boolean find;
            List<String> productsUnr = new List<String>();
            String epc;
            foreach (DictionaryEntry productr in productsRead)
            {
                find = false;
                //product.Key, product.Value
                foreach (Asset productl in productlist)
                {
                    epc = EpcTools.getUpc(productr.Key.ToString());
                    if (productl.upc == epc)
                    { find = true;  break;}
                }
                if (find == false)
                    productsUnr.Add(productr.Key.ToString());
            }
            return productsUnr;
        }

        public SyncProduct GETProduct(String epc)
        {
            var request = new RestRequest("test_get_product", Method.POST);
            IRestResponse<SyncProduct> response = client.Execute<SyncProduct>(request);
            SyncProduct product = response.Data;

            if (!requestError(response.StatusCode.ToString()))
                return null;

            return product;
        }
    }
}
