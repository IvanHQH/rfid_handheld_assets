using System;
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
        private int pclient_id;
        private String pathFolderName;

        private class GetObject
        {
            public List<SyncProduct> products { get; set; }
            public List<SyncWarehouse> warehouses { get; set; }
        }

        public class SyncProduct
        {
            public int id { get; set; }
            public int pclient_id { get; set; }
            public int warehouse_id { get; set; }
            public String upc { get; set; }
            public String name { get; set; }
            public String description { get; set; }
        }

        private class SyncWarehouse
        {
            public int id { get; set; }
            public String name { get; set; }
            public String description { get; set; }
            //public int customer_id { get; set; }
        }

        public class SyncOrdenEsM
        {
            public int client_id;
            public string date_time;
            public JsonArray epcs = new JsonArray();
            public int warehouse_id;

            public enum index
            {
                client_id = 1,
                warehouse_id = 2,
                date_time = 3,
            }

            public SyncOrdenEsM(int client_id, int warehouse_id, string date_time)
            {
                this.client_id = client_id;
                this.date_time = date_time;
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

        private class SyncLog
        {
            public int event_id;
            public int user_id;
            public String description;
            public String date_time;
            public int client_id;
        }

        public Sync(String BaseUrl, int idClient, String pathFolderName)
        {
            client = new RestClient(BaseUrl);
            this.pclient_id = idClient;
            this.pathFolderName = pathFolderName;
        }

        public bool GET()
        {
            var request = new RestRequest("sync_data", Method.POST);
            JsonObject json = new JsonObject();

            request.RequestFormat = DataFormat.Json;
            json.Add("pclient_id", pclient_id.ToString());
            request.AddBody(json);
            IRestResponse<GetObject> response = client.Execute<GetObject>(request);
            GetObject data = response.Data;
            if (!requestError(response.StatusCode.ToString()))
                return false;
            Directory.CreateDirectory(pathFolderName);
            using (CsvFileWriter writer = new CsvFileWriter(pathFolderName + "products.csv"))
            {
                foreach (SyncProduct item in data.products)
                {
                    CsvRow row = new CsvRow();
                    row.Add(item.upc);
                    row.Add(item.name);
                    try { row.Add(item.warehouse_id.ToString()); }
                    catch (Exception exc) { }
                    writer.WriteRow(row);
                }
            }
            using (CsvFileWriter writer = new CsvFileWriter(pathFolderName + "warehouses.csv"))
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

        public bool GET_Test()
        {
            var request = new RestRequest("test_conection", Method.GET);
            IRestResponse<GetObject> response = client.Execute<GetObject>(request);
            if (response.Content == "ok")
                return true;
            return false;
        }

        //public bool Login_Forced(int idUser, string pwd, int idClient)
        //{
        //    var request = new RestRequest("login_forced", Method.POST);
        //    request.RequestFormat = DataFormat.Json;
        //    JsonObject json = new JsonObject();
        //    json.Add("id", idUser);
        //    json.Add("pwd", pwd);
        //    json.Add("idClient", idClient);
        //    request.AddBody(json);
        //    IRestResponse response = client.Execute(request);
        //    if (response.Content == "ok")
        //        return true;
        //    return false;
        //}

        //public bool Logout()
        //{
        //    var request = new RestRequest("logout", Method.GET);
        //    IRestResponse response = client.Execute(request);
        //    return true;
        //}

        public bool POSTTrans(SyncForm sync, int idUser, string pwd, int idClient)
        {
            Directory.CreateDirectory(pathFolderName);
            string[] filePaths = Directory.GetFiles(pathFolderName);
            List<string> upcFiles = new List<string>();
            List<string> epcFiles = new List<string>();
            List<string> messages = new List<string>();

            foreach (String path in filePaths)
            {
                if (path.Contains("epc"))
                    epcFiles.Add(path);
                if (path.Contains("message"))
                    messages.Add(path);
            }

            sync.updateReads(upcFiles.Count.ToString() + " Inventarios");
            Application.DoEvents();

            int numInventories = upcFiles.Count;
            foreach (String path1 in epcFiles)
            {
                var request = new RestRequest("sync", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(buildPOSTRequest(path1, ""));
                IRestResponse response = client.Execute(request);
                //MessageBox.Show(response.Content.ToString());
                if (!requestError(response.StatusCode.ToString()))
                    return false;
                numInventories--;
                sync.updateReads(numInventories + " Lecturas");
                Application.DoEvents();
                try
                {
                    String nameFileMessage = path1.Replace("iepcs", "message").Replace("oepcs", "message");
                    File.Move(nameFileMessage, nameFileMessage.Replace("rfiddata", "rfiddataold")); 
                }
                catch (Exception exc){}
                
                File.Move(path1, path1.Replace("rfiddata", "rfiddataold"));
                File.Move(path1.Replace("epc", "upc"), path1.Replace("rfiddata", "rfiddataold").Replace("epc", "upc"));
            }
            return true;
        }

        private JsonObject buildPOSTRequest(String path, String path1)
        {
            var inventories = new JsonArray();
            //var messages = new JsonArray();

            if (path != "")
            {
                inventories.Add(buildInventory(path));
            //    messages.Add(buildMessages(path));
            }
            
            var json = new JsonObject();
            json.Add("inventories", inventories);
            //json.Add("messages", messages);

            return json;
        }

        private JsonObject buildInventory(String path)
        {
            //var messages = new JsonArray();
            string messages;
            var inventory = new JsonObject();
            var epcs = new JsonArray();
            SyncOrdenEsM OrderM = deserealizeNameFile(path);
            using (CsvFileReader reader = new CsvFileReader(path))
            {
                CsvRow rowcsv = new CsvRow();
                while (reader.ReadRow(rowcsv))
                {
                    epcs.Add(rowcsv[0]);
                }
            }
            messages = buildMessages(path);
            //messages = buildMessages(path);
            inventory.Add("user_id", 1);
            inventory.Add("client_id", pclient_id);
            inventory.Add("created_at", OrderM.date_time);
            inventory.Add("updated_at", OrderM.date_time);
            inventory.Add("warehouse_id", OrderM.warehouse_id);
            inventory.Add("epcs", epcs);
            inventory.Add("messages", messages);
            //inventory.Add("messages", messages);
            return inventory;
        }

        private string buildMessages(String path)
        {
            string messages = "";
            String nameFileMessage = path.Replace("iepcs", "message").Replace("oepcs", "message");
            using (CsvFileReader reader = new CsvFileReader(nameFileMessage))
            {
                CsvRow rowcsv = new CsvRow();
                while (reader.ReadRow(rowcsv))
                {
                    messages = messages+","+rowcsv[0];
                }
            }
            return messages;
        }

        //private JsonArray buildMessages(String path)
        //{
        //    var messages = new JsonArray();
        //    String nameFileMessage = path.Replace("iepcs", "message").Replace("oepcs", "message");
        //    using (CsvFileReader reader = new CsvFileReader(nameFileMessage))
        //    {
        //        CsvRow rowcsv = new CsvRow();
        //        while (reader.ReadRow(rowcsv))
        //        {
        //            messages.Add(rowcsv[0]);
        //        }
        //    }
        //    return messages;
        //}

        //private JsonObject LogToJson(SyncLog log)
        //{
        //    JsonObject json = new JsonObject();
        //    json.Add("client_id", log.client_id);
        //    json.Add("created_at", log.date_time);
        //    json.Add("updated_at", log.date_time);
        //    json.Add("user_id", log.user_id);
        //    json.Add("description", log.description);
        //    return json;
        //}

        private SyncLog deserealizeNameFileLog(string path)
        {
            //1_1_1234124412_14-11-29-013045
            string[] comp = path.Split(new Char[] { '_' });
            SyncLog log = null;
            try
            {
                log = new SyncLog();
                log.client_id = pclient_id;
                log.date_time = FormatDateTime(comp[(int)SyncOrdenEsM.index.date_time].Replace(".csv", ""));
                log.user_id = 1;
                log.description = ContentFile(path);
            }
            catch (Exception exc) { }
            return log;
        }

        private String ContentFile(string path)
        {
            string[] comp = path.Split(new Char[] { '_' });
            String content = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    content = sr.ReadToEnd();
                }
            }
            catch (Exception exc)
            { }
            return content;
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
            string[] comp = path.Split(new Char[] { '_' });
            SyncOrdenEsM orden = null;
            try
            {
                orden = new SyncOrdenEsM(int.Parse(comp[(int)SyncOrdenEsM.index.client_id]),
                    int.Parse(comp[(int)SyncOrdenEsM.index.warehouse_id]),
                    FormatDateTime(comp[(int)SyncOrdenEsM.index.date_time].Replace(".csv", ""))
                    );
            }
            catch (Exception exc) { }
            return orden;
        }

        private int Type(char cType)
        {
            int iType = -1;
            if (cType == 'i')
                iType = 1;
            else if (cType == 'o')
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
            string[] dt = dateTime.Split(new Char[] { '-' });
            string dateTimeF = "20" + dt[0] + "-" + dt[1] + "-" + dt[2] + " " +
                dt[3].Substring(0, 2) + ":" + dt[3].Substring(2, 2) + ":" + dt[3].Substring(4, 2);
            return dateTimeF;
        }

        private JsonObject OrderMToJson(SyncOrdenEsM OrderM)
        {
            JsonObject json = new JsonObject();
            json.Add("client_id", pclient_id);
            json.Add("created_at", OrderM.date_time);
            json.Add("updated_at", OrderM.date_time);
            json.Add("warehouse_id", OrderM.warehouse_id);
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
            json.Add("client_id", pclient_id);
            return json;
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
