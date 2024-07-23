using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entiteterna
{
    public class Logial
    {
       
       
     
        [NotMapped]
        public Dictionary<int, int> PriserPerVecka
        {
            get => _priserPerVecka == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(_priserPerVecka);
            set => _priserPerVecka = JsonConvert.SerializeObject(value);
        }
        [NotMapped]
        public Dictionary<int, int> Sönfre
        {
            get => sönfre == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(sönfre);
            set => sönfre = JsonConvert.SerializeObject(value);
        }
        [NotMapped]
        public Dictionary<int, int> Fresön
        {
            get => fresön == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(fresön);
            set => fresön = JsonConvert.SerializeObject(value);
        }

        [NotMapped]
        public Dictionary<int, int> Dag
        {
            get => dag == null ? null : JsonConvert.DeserializeObject<Dictionary<int, int>>(dag);
            set => dag = JsonConvert.SerializeObject(value);
        }

        private string dag;
        private string _priserPerVecka;
        private string sönfre;
        private string fresön;
        public int logiID { get; set; }
        public string beskrivning { get; set; }
        public int pris { get; set; }
        public bool tillgänglig { get; set; }
        public int storlek { get; set; }
        public ApartmentType Typ { get; set; }
        public enum ApartmentType
        {
            LagenheterI,
            LagenheterII,
            Camp
        }


        public override string ToString()
        {
            return $"Logial ID: {logiID}    Benämning: {beskrivning}  Storlek: {storlek}  Typ:  {Typ} ";
        }

        public Logial() { }
       
        public virtual ICollection<BokningsRadLogial> BookingRader { get; set; }
        
    }
}