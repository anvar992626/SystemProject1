using Azure;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entiteterna
{
    public class Utrustning
    {
        private string priserPerDag;
        public UtrustningTyp Typ { get; set; }
        public enum UtrustningTyp
        {
            SkidorAlpint,PjäxorAlpint, StavarAlpint, Snowboard ,SnowboardSkor, LängdSkidor, SkidPjäxorLängd, StavarLängd, SnöSkotrar, PaketAlpint,
            PaketLängd, Pulka, PaketSnowbord, YamahaViking, Hjälm, Lynx50



        }
        public int utrustningID { get; set; }

        [NotMapped]
        public Dictionary<int, int> PriserPerDag
        {
            get => priserPerDag == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(priserPerDag);
            set => priserPerDag = JsonConvert.SerializeObject(value);
        }

  

        private string alpintPaket { get; set; }
        [NotMapped]
        public Dictionary<int, int> AlpintPaket
        {
            get => alpintPaket == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(alpintPaket);
            set => alpintPaket = JsonConvert.SerializeObject(value);
        }
        private string nilaPulka;
        [NotMapped]
        public Dictionary<int, int> NilaPulka
        {
            get => nilaPulka == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(nilaPulka);
            set => nilaPulka = JsonConvert.SerializeObject(value);
        }
        private string yamahaViking;
        [NotMapped]
        public Dictionary<int, int> YamahaViking
        {
            get => yamahaViking == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(yamahaViking);
            set => yamahaViking = JsonConvert.SerializeObject(value);
        }
        private string skoterLynx50;
        [NotMapped]
        public Dictionary<int, int> SkoterLynx50
        {
            get => skoterLynx50 == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(skoterLynx50);
            set => skoterLynx50 = JsonConvert.SerializeObject(value);
        }
        private string hjälm;
        [NotMapped]
        public Dictionary<int, int> Hjälm
        {
            get => hjälm == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(hjälm);
            set => hjälm = JsonConvert.SerializeObject(value);
        }
        private string skorSnowbord;
        [NotMapped]
        public Dictionary<int, int> SkorSnowbord
        {
            get => skorSnowbord == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(skorSnowbord);
            set => skorSnowbord = JsonConvert.SerializeObject(value);
        }
        private string snowbord;
        [NotMapped]
        public Dictionary<int, int> Snowbord
        {
            get => snowbord == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(snowbord);
            set => snowbord = JsonConvert.SerializeObject(value);
        }
        private string paketSnowbord;
        [NotMapped]
        public Dictionary<int, int> PaketSnowbord
        {
            get => paketSnowbord == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(paketSnowbord);
            set => paketSnowbord = JsonConvert.SerializeObject(value);
        }
        private string längdPaket;
        [NotMapped]
        public Dictionary<int, int> LängdPaket
        {
            get => längdPaket == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(längdPaket);
            set => längdPaket = JsonConvert.SerializeObject(value);
        }
        private string längdStavar;
        [NotMapped]
        public Dictionary<int, int> LängdStavar
        {
            get => längdStavar == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(längdStavar);
            set => längdStavar = JsonConvert.SerializeObject(value);
        }

        private string längdPjäxor;
        [NotMapped]
        public Dictionary<int, int> LängdPjäxor
        {
            get => längdPjäxor == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(längdPjäxor);
            set => längdPjäxor = JsonConvert.SerializeObject(value);
        }
        private string längdSkidor;
        [NotMapped]
        public Dictionary<int, int> LängdSkidor
        {
            get => längdSkidor == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(längdSkidor);
            set => längdSkidor = JsonConvert.SerializeObject(value);
        }
        private string alpintStavar;
        [NotMapped]
        public Dictionary<int, int> AlpintStavar
        {
            get => alpintStavar == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(alpintStavar);
            set => alpintStavar = JsonConvert.SerializeObject(value);
        }
        private string alpintPjäxor;

        [NotMapped]
        public Dictionary<int, int> AlpintPjäxor
        {
            get => alpintPjäxor == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(alpintPjäxor);
            set => alpintPjäxor = JsonConvert.SerializeObject(value);
        }
        private string alpintSkidor;

        [NotMapped]
        public Dictionary<int, int> AlpintSkidor
        {
            get => alpintSkidor == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(alpintSkidor);
            set => alpintSkidor = JsonConvert.SerializeObject(value);
        }


        public string benämning { get; set; }

        public override string ToString()
        {
            return $"Utrustning ID: {utrustningID}    Benämning: {benämning}  Typ:  {Typ} ";
        }

        public Utrustning() { }
        public ICollection<BokningsRadUtrustning> BokningsRader { get; set; }
        public ICollection<SkidshopBokningsRadUtrustning> SkidshopBokningsRader { get; set; }

    }
}