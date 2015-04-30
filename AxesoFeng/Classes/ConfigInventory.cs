using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace RfidInventory.Classes
{
    public class ConfigInventory
    {
        public int version{ get; set; }

        public static ConfigInventory getConfigInventory(String path)
        {
            StreamReader sreader = new StreamReader(path);
            String json = sreader.ReadToEnd();
            sreader.Close();
            return JsonConvert.DeserializeObject<ConfigInventory>(json);
        }
    }
}
