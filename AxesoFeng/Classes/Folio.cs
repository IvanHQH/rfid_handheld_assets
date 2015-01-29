using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.IO;
using ReadWriteCsv;
using System.Windows.Forms;
using Newtonsoft.Json;
using AxesoFeng.Classes;

namespace AxesoFeng
{
    public class FolioOrder
    {
        private RestClient client;
        string bu;
        public FolioOrder(String BaseUrl)
        {
            client = new RestClient(BaseUrl);
            bu = BaseUrl;
        }

        public List<RespFolio.Assets> GetProductsFile(string fileName)
        {
            List<RespFolio.Assets> productsFile = new List<RespFolio.Assets>();
            using (CsvFileReader reader = new CsvFileReader(fileName))
            {
                CsvRow rowcsv = new CsvRow();
                while (reader.ReadRow(rowcsv))
                {
                    productsFile.Add(new RespFolio.Assets(rowcsv[0], rowcsv[1], rowcsv[2], int.Parse(rowcsv[3])));
                    //table.addRow(rowcsv[0],rowcsv[1],rowcsv[2]);
                }
            }
            return productsFile;
        }

        public List<string> CompareTo(String path)
        {
            RespFolio respFolio;
            string folio = GetFolio(path);
            string fileName = Path.GetFileName(path);
            respFolio = GETServer(fileName);
            List<string> messages = new List<string>();
            if (respFolio != null)
            {
                List<RespFolio.Assets> productsFile = GetProductsFile(fileName);
                //messages = CompareTo(productsFile, respFolio);
            }
            return messages;
        }

        public List<string> CompareTo(List<RespFolio.Assets> productsRead, String folio, String valueWarehouse)
        {
            RespFolio respFolio;
            respFolio = GETServer(folio);
            List<string> messages = new List<string>();
            if (respFolio != null)
            {
                messages = CompareTo(productsRead, respFolio,valueWarehouse);
            }
            return messages;
        }

        private List<string> CompareTo(List<RespFolio.Assets> productsRead, RespFolio respInv, String valueWarehouse)
        {
            List<string> Inequalities = new List<string>();
            bool firstMessage = true;
            foreach (RespFolio.Assets prodRead in productsRead)
            {
                if (prodRead.place_id != int.Parse(valueWarehouse)) {
                    if (firstMessage)
                    {
                        Inequalities.Add("**No deberian estar aqui:");
                        firstMessage = false;
                    }
                    Inequalities.Add(" "+prodRead.name);
                }
            }
            bool find= false;
            firstMessage = true;
            foreach (RespFolio.Assets prodInv in respInv.assets)
            {
                if (prodInv.place_id == int.Parse(valueWarehouse))
                {
                    find = false;
                    foreach (RespFolio.Assets prodRead in productsRead)
                    {
                        if (prodInv.name == prodRead.name)
                        {
                            if (prodInv.place_id == prodRead.place_id)
                            {
                                find = true; break;                                
                            }                           
                        }
                    }
                    if (find == false)
                    {
                        if (firstMessage)
                        {
                            Inequalities.Add("");
                            Inequalities.Add("**Deberian estar aqui:");
                            firstMessage = false;
                        }
                        Inequalities.Add(" "+prodInv.name);
                    }
                }

            }
            return Inequalities;
        }

        private string GetFolio(string fileName)
        {
            string folio; 
            string[] comp = fileName.Split(new Char[] { '_' });
            try{
                folio = comp[3];
            }
            catch (Exception exc) {
                folio = "";
            }
            return folio;
        }

        private RespFolio GETServer(string folio)
        {
            //var request = new RestRequest("test_get_folio", Method.POST);
            //request.AddParameter("folio", folio);
            //IRestResponse<RespFolio> response = client.Execute<RespFolio>(request);
            //RespFolio data = JsonConvert.DeserializeObject<RespFolio>(response.Content);

            //if (!requestError(response.StatusCode.ToString()))
            //    return null;

            //return data;
            try {
                System.IO.StreamReader objReader;
                objReader = File.OpenText(@"\rfiddata\inventario.json");
                String text = objReader.ReadToEnd();
                RespFolio data = JsonConvert.DeserializeObject<RespFolio>(text);
                return data;
            }
            catch (Exception exc) { }
            return null;
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

    }
}
