using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteterna
{
    public class SkidshopBokningsRadUtrustning
    {

        
        public int RadID { get; set; }
        public int UtrusningsID { get; set; }
        public int SkidshopBokningID { get; set; }


        
        public virtual SkidShopView BokningSkidshopView { get; set; }

        public virtual Utrustning Utrustning { get; set; }
        public DateTime startTid { get; set; }
        public DateTime slutTid { get; set; }

    }
}
