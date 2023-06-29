namespace MyDrivers
{
    public class Szerviz
    {
        private int szid;
        private string cim;
        private string sznev;
        private string kontaktnev;
        private string kontaktemail;
        private string kontakttelszam;

        public Szerviz(int szid, string cim, string sznev, string kontaktnev, string kontaktemail, string kontakttelszam)
        {
            this.Szid = szid;
            this.Cim = cim;
            this.Sznev = sznev;
            this.Kontaktnev = kontaktnev;
            this.Kontaktemail = kontaktemail;
            this.Kontakttelszam = kontakttelszam;
        }

        public int Szid { get => szid; set => szid = value; }
        public string Cim { get => cim; set => cim = value; }
        public string Sznev { get => sznev; set => sznev = value; }
        public string Kontaktnev { get => kontaktnev; set => kontaktnev = value; }
        public string Kontaktemail { get => kontaktemail; set => kontaktemail = value; }
        public string Kontakttelszam { get => kontakttelszam; set => kontakttelszam = value; }

    }
}
