using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteterna
{
    public class BokningsRadSkidskola
    {

        public int SkidskolaRadID { get; set; }
        public int BokningID { get; set; }
        public int SKidskolaID { get; set; }
       


        public virtual TellefonMottagareView BokningMottagareView { get; set; }
       

        public virtual Skidskola Skidskola { get; set; }
        public DateTime startTid { get; set; }
        public DateTime slutTid { get; set; }

     

     
    }
}
