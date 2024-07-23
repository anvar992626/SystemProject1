using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteterna
{
    public class Konferenslokal
    {
       
        public KlokalTyp Typ { get; set; }

        private string priserPerVeckaLokal;
        private string priserPerDagLokal;
        private string priserPerTimLokal;

        public int storlek { get; set; }
        public string beskrivning { get; set; }
        public int konferensID { get; set; }
        public bool tillgänglig { get; set; }

        [NotMapped]
        public Dictionary<int, int> PrisPerVeckaLokal 
        {
            get => priserPerVeckaLokal == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(priserPerVeckaLokal);
            set => priserPerVeckaLokal = JsonConvert.SerializeObject(value);
        }

        [NotMapped]
        public Dictionary<int, int> PrisPerDygnLokal 
        {
            get => priserPerDagLokal == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(priserPerDagLokal);
            set => priserPerDagLokal = JsonConvert.SerializeObject(value);
        }

        [NotMapped]
        public Dictionary<int, int> PrisPerTimLokal 
        {
            get => priserPerTimLokal == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(priserPerTimLokal);
            set => priserPerTimLokal = JsonConvert.SerializeObject(value);
        }
        public enum KlokalTyp
        {
            Lokal1,
            Lokal2, 
        }
        //[NotMapped]
        //public List<DateRange> KonferensLokalDateRange { get; set; } = new List<DateRange>();

       



        public Konferenslokal() { }

        public ICollection<KonferensBokningsRad> BokningsRaderKonferens { get; set; }


    }
}
