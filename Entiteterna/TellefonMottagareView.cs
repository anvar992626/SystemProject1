namespace Entiteterna
{
    public class TellefonMottagareView
    {
        public int BokningID { get; set; }
     
        public DateTime UtlämningsDatum { get; set; }
        public DateTime ÅterlämningsDatum { get; set; }
        public int TotalPris { get; set; }
        public Kund KundNr { get; set; }


        public bool Aktiv { get; set; }
        public virtual ICollection<BokningsRadLogial> BookingRaderLogialer { get; set; }
        public virtual ICollection<BokningsRadUtrustning> BookingRaderUtrustningar { get; set; }
        public virtual ICollection<BokningsRadSkidskola> BookingRaderLektioner { get; set; }
     

      
        public TellefonMottagareView() { }
        


    }
}

