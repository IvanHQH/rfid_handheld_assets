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
    public class Inventory
    {
        private RestClient client;
        string bu;
        private String pathFolderName;

        public Inventory(String BaseUrl, String pathFolderName)
        {
            client = new RestClient(BaseUrl);
            bu = BaseUrl;
            this.pathFolderName = pathFolderName;
        }

        public List<RespInventory.Assets> GetProductsFile(string fileName)
        {
            List<RespInventory.Assets> productsFile = new List<RespInventory.Assets>();
            using (CsvFileReader reader = new CsvFileReader(fileName))
            {
                CsvRow rowcsv = new CsvRow();
                while (reader.ReadRow(rowcsv))
                {
                    productsFile.Add(new RespInventory.Assets(rowcsv[0], rowcsv[1], rowcsv[2], int.Parse(rowcsv[3])));
                    //table.addRow(rowcsv[0],rowcsv[1],rowcsv[2]);
                }
            }
            return productsFile;
        }

        public List<string> CompareTo(String path)
        {
            RespInventory respFolio;
            string folio = GetFolio(path);
            string fileName = Path.GetFileName(path);
            respFolio = GETInventoryFile();
            List<string> messages = new List<string>();
            if (respFolio != null)
            {
                List<RespInventory.Assets> productsFile = GetProductsFile(fileName);
                //messages = CompareTo(productsFile, respFolio);
            }
            return messages;
        }

        public List<string> CompareTo(List<RespInventory.Assets> productsRead, String valueWarehouse)
        {
            RespInventory respFolio;
            respFolio = GETInventoryFile();
            List<string> messages = new List<string>();
            if (respFolio != null)
            {
                messages = CompareTo(productsRead, respFolio,valueWarehouse);
            }
            return messages;
        }

        private List<string> CompareTo(List<RespInventory.Assets> productsRead, RespInventory respInv, String valueWarehouse)
        {
            //esta mal esta comparasion
            List<string> Inequalities = new List<string>();
            bool firstMessage = true;
            foreach (RespInventory.Assets prodRead in productsRead)
            {
                foreach (RespInventory.Assets prodInv in respInv.assets)
                {
                    if (prodRead.upc == prodInv.upc)
                    {
                        if (prodInv.place_id != int.Parse(valueWarehouse))
                        {
                            if (firstMessage)
                            {
                                Inequalities.Add("**Excedentes:");
                                firstMessage = false;
                            }
                            Inequalities.Add(" " + prodRead.name + " " + prodRead.upc);
                            break;
                        }
                    }
                }
            }
            bool find= false;
            firstMessage = true;
            foreach (RespInventory.Assets prodInv in respInv.assets)
            {
                if (prodInv.place_id == int.Parse(valueWarehouse))
                {
                    find = false;
                    foreach (RespInventory.Assets prodRead in productsRead)
                    {
                        if (prodInv.name == prodRead.name)
                        {
                            find = true; 
                            break;
                        }
                    }
                    if (find == false)
                    {
                        if (firstMessage)
                        {
                            Inequalities.Add("");
                            Inequalities.Add("**Faltantes:");
                            firstMessage = false;
                        }
                        Inequalities.Add(" " + prodInv.name + " " + prodInv.upc);
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

        public RespInventory GETInventoryFile()
        {
            try {
                System.IO.StreamReader objReader;
                objReader = File.OpenText(pathFolderName + "inventario.json");
                String text = objReader.ReadToEnd();
                RespInventory data = JsonConvert.DeserializeObject<RespInventory>(text);
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

        public static void DeleteFiles(String pathFolderName)
        {
            Cursor.Current = Cursors.WaitCursor;
            string[] filePaths = Directory.GetFiles(pathFolderName);

            foreach (String path in filePaths)
            {
                if (path.Contains("iupc") || path.Contains("oupc") || path.Contains("iepc")
                    || path.Contains("oepc") || path.Contains("message"))
                {
                    File.Delete(path);
                }
            }
            Cursor.Current = Cursors.Default;
        }

    }
}
