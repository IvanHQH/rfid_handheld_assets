using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AxesoFeng
{
    public class Asset
    {
        public int id { get; set; }
        public String upc { get; set; }
        public string name { get; set; }

        ///Oficilia///
        public int place_id { get; set; }
        public String place_name { get; set; }
        /////////////
    }
}
