namespace Entiteterna
{
    public class Kund
    {
      

        public string namn { get; set; }
        public float kreditGräns { get; set; }
        public float rabatt { get; set; }
        public int kundID { get; set; }
        public int faktureringsAddress { get; set; }
        //public List<BokningMottagareView> Bokningar { get; set; }



        public Kund(string namn, float kreditGräns, float rabatt)
        {
            //Bokningar = new List<BokningMottagareView>();
            this.namn = namn;
            this.kreditGräns = kreditGräns > 12000 ? 12000 : kreditGräns;
            this.rabatt = rabatt;
            
        }

        //public ICollection<BokningMottagareView> Bokningar { get; set; }
    }
}