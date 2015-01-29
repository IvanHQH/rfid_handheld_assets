using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AxesoFeng.Classes
{
    public class RespFolio
    {
        public class Assets
        {
            public String name;
            public String upc;
            ///Oficilia///
            public String place_name;
            public int place_id;
            /////////////
            public Assets(String upc, String name,String place_name,int place_id)
            {
                this.name = name;
                this.upc = upc;
                this.place_id = place_id;
                this.place_name = place_name;
            }
        }
        public List<Assets> assets;
    }

}
