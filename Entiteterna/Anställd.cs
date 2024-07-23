namespace Model
{

    public class Anställd
    {
        public int anställdID { get; set; }
        public string namn { get; set; }
        public string Lösenord { get; set; }

        // Private parameterless constructor for Entity Framework
        public Anställd()
        {
        }

        public Anställd(string namn, string Lösenord)
        {
            this.namn = namn;
            this.Lösenord = Lösenord;
        }

        public bool VerifieraLösenord(string l)
        {
            return Lösenord == l;
        }
    }
}
