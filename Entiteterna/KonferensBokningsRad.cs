using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteterna
{
    public class KonferensBokningsRad
    {

        public int KonferensBokningRadID { get; set; }
        public int KonferensBokningID { get; set; }
        public int KonferenslokalID { get; set; }
        public DateTime startTid { get; set; }
        public DateTime slutTid { get; set; }

        public virtual KonferensBokningView KonferensBokningView { get; set; }
        public virtual Konferenslokal Lokal { get; set; }
    }
}
