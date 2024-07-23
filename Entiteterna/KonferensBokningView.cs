using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteterna
{
    public class KonferensBokningView
    {
        public int KonferensBokningID { get; set; }
        public DateTime UtlämningsDatum { get; set; }
        public DateTime ÅterlämningsDatum { get; set; }
        public FöretagKund FöretagKundNr { get; set; }
        public List<Konferenslokal> Lokal { get; set; }
        public bool Aktiv { get; set; }
        public virtual ICollection<KonferensBokningsRad> KonferensBokingRader { get; set; }
        public KonferensBokningView(DateTime UtlämningsDatum, DateTime ÅterlämningsDatum, FöretagKund FöretagKundNr, List<Konferenslokal> Lokal, bool Aktiv)
        {
            this.UtlämningsDatum = UtlämningsDatum;
            this.ÅterlämningsDatum = ÅterlämningsDatum;
            this.FöretagKundNr = FöretagKundNr;
            this.Aktiv = Aktiv;
            this.Lokal = Lokal;
        }
        public KonferensBokningView() { }

        

    }

}
