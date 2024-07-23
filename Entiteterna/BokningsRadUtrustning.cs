using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteterna
{
    public class BokningsRadUtrustning
    {

        public int UtrustningsRadID { get; set; }
        public int BokningID { get; set; }
        public int UtrustningID { get; set; }
        

        public virtual TellefonMottagareView BokningMottagareView { get; set; }
        

        public virtual Utrustning Utrustning { get; set; }
        public DateTime startTid { get; set; }
        public DateTime slutTid { get; set; }
    }
}
