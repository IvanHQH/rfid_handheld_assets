using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AxesoFeng
{
    public class UpcInventory
    {
        public string upc;
        public string name;
        /// Oficialia
        public string place_name;
        public int place_id;
        /// 
        public int total;

        //public UpcInventory(String upc,String name)
        //{
        //    this.upc = upc;
        //    this.name = name;
        //    this.total = 0;
        //}

        /// Oficialia
        public UpcInventory(String upc, String name, string place_name, int place_id)
        {
            this.upc = upc;
            this.name = name;
            this.total = 0;
            this.place_name = place_name;
            this.place_id = place_id;
        }        
        /// 

    }
}
