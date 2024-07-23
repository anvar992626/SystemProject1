using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteterna
{
    public class BokningsRadLogial
    {
      
        public int BokningRadLogialID { get; set; }
        public int BokningID { get; set; }
        public int LogialID { get; set; }
        
        public virtual TellefonMottagareView Bokning { get; set; }
        public virtual Logial Logial { get; set; }
        public DateTime startTid { get; set; }
        public DateTime slutTid { get; set; }

     
    }

}

