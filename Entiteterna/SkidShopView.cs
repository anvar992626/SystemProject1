using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteterna
{
    public class SkidShopView
    {
        public int SkidshopBokningID { get; set; }
        
        public DateTime StartTid { get; set; }
        public DateTime Sluttid { get; set; }
        public Kund KundNr { get; set; }
        public bool Aktiv { get; set; }
        public int TotalPris { get; set; }



        public virtual ICollection<SkidshopBokningsRadUtrustning> SkidshopBokningsraderUtrustningar { get; set; }
        
        public virtual ICollection<SkidshopBokningsRadSkidskola> SkidshopBokningsRaderSkidskola { get; set; }
        

        public SkidShopView() { }

       
    }
}
