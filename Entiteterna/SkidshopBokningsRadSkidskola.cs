using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteterna
{
    public class SkidshopBokningsRadSkidskola
    {
     
        public int RadID { get; set; }
        public int SKidskolaID { get; set; }
        public int SkidshopBokningID { get; set; }


        
        public virtual SkidShopView BokningSkidshopView { get; set; }

        public virtual Skidskola Skidskola { get; set; }
        public DateTime startTid { get; set; }
        public DateTime slutTid { get; set; }
    }
}
