using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteterna
{
    public class FöretagKund
    {
        
        public string namn { get; set; }
        public float kreditGräns { get; set; }
        public float rabatt { get; set; }
        public int företagkundID { get; set; }

        public FöretagKund(string namn, float kreditGräns, float rabatt)
        {

            this.namn = namn;
            this.kreditGräns = kreditGräns;
            this.rabatt = rabatt;
            
        }
        public ICollection<KonferensBokningView> KonferensBokningar { get; set; } = new List<KonferensBokningView>();
    }
}
