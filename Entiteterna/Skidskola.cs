using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entiteterna
{
    public class Skidskola
    {
        public LektionTyp Typ { get; set; }
        public enum LektionTyp
        {
            Grön,
            Blå,
            Röd,
            Svart,
            PrivatLektion


        }
       
        private string _priserPerVecka; 
        public int skolaID { get; set; }
        public int LärareID { get; set; }
       public string dagar { get; set; }
        public override string ToString()
        {
            return $"Lektion ID: {skolaID}    Lärare ID: {LärareID}  Dagar: {dagar}  Typ:  {Typ} ";
        }


        [NotMapped]
        public Dictionary<string, int> PriserPerVecka
        {
            get => _priserPerVecka == null ? null : JsonConvert.DeserializeObject<Dictionary<string, int>>(_priserPerVecka);
            set => _priserPerVecka = JsonConvert.SerializeObject(value);
        }
        public bool tillgänglig { get; set; }
        public int pris { get; set; }

        private string lektionPrivat { get; set; }
        [NotMapped]
        public Dictionary<int, int> LektionPrivat
        {
            get => lektionPrivat == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(lektionPrivat);
            set => lektionPrivat = JsonConvert.SerializeObject(value);
        }

        public ICollection<BokningsRadSkidskola> SkidshopBokningsRader { get; set; }
        public ICollection<SkidshopBokningsRadSkidskola> SkidshopRader { get; set; }

        public Skidskola ()
        { }


    }
}