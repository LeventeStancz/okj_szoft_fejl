using System;

namespace MyDrivers
{
    public class Felhasznalo
    {
        private int fid;
        private int munkakorid;
        private int jogid;
        private string email;
        private string fnev;
        private DateTime szuletesnap;
        private int kivehetoszabi;
        private int kivettszabi;
        private string nem;
        private string teljesnev;
        private string telefonszam;
        private string lakcim;
        private string adoazon;
        private string bankszamlaszam;
        private string szemelyi;
        private string tajszam;
        private DateTime csatlakozas;
        private int alapber;

        public Felhasznalo(int fid)
        {
            this.Fid = fid;
        }

        public Felhasznalo(int fid, int munkakorid, int jogid, string fnev)
        {
            this.Fid = fid;
            this.Munkakorid = munkakorid;
            this.Jogid = jogid;
            this.Fnev = fnev;
        }

        public Felhasznalo(int fid, int munkakorid, string email, string fnev, DateTime szuletesnap, int kivehetoszabi, int kivettszabi, string nem, string teljesnev, string telefonszam, string lakcim, string adoazon, string bankszamlaszam, string szemelyi, string tajszam, DateTime csatlakozas, int alapber, int jogid)
        {
            this.Fid = fid;
            this.Munkakorid = munkakorid;
            this.Email = email;
            this.Fnev = fnev;
            this.Szuletesnap = szuletesnap;
            this.Kivehetoszabi = kivehetoszabi;
            this.Kivettszabi = kivettszabi;
            this.Nem = nem;
            this.Teljesnev = teljesnev;
            this.Telefonszam = telefonszam;
            this.Lakcim = lakcim;
            this.Adoazon = adoazon;
            this.Bankszamlaszam = bankszamlaszam;
            this.Szemelyi = szemelyi;
            this.Tajszam = tajszam;
            this.Csatlakozas = csatlakozas;
            this.Alapber = alapber;
            this.Jogid = jogid;
        }

        public string Munkakor { get => Kisegito.Munkakorok[munkakorid]; }
        public string Jogosultsag { get => Kisegito.Jogosultsagok[jogid]; }
        public string Szulnap
        {
            get
            {
                if (szuletesnap.ToString("yyyy-MM-dd") == "0001-01-01")
                {
                    return "Nincs adat";
                }
                else
                {
                    return szuletesnap.ToString("yyyy-MM-dd");
                }
            }
        }
        public string Csatl { get => csatlakozas.ToString("yyyy-MM-dd"); }
        public string Bankszamla
        {
            get
            {
                if (Bankszamlaszam == "Nincs adat")
                {
                    return bankszamlaszam;
                }
                else
                {
                    string elso = "";
                    string masodik = "";
                    string harmadik = "";
                    for (int i = 0; i < bankszamlaszam.Length; i++)
                    {
                        if (i < 8)
                        {
                            elso += bankszamlaszam[i];
                        }
                        else if (i > 7 && i < 16)
                        {
                            masodik += bankszamlaszam[i];
                        }
                        else if (i > 15 && i < 24)
                        {
                            harmadik += bankszamlaszam[i];
                        }
                    }
                    return elso + "-" + masodik + "-" + harmadik;
                }
            }
        }

        public int Fid { get => fid; set => fid = value; }
        public int Munkakorid { get => munkakorid; set => munkakorid = value; }
        public string Email { get => email; set => email = value; }
        public string Fnev { get => fnev; set => fnev = value; }
        public DateTime Szuletesnap { get => szuletesnap; set => szuletesnap = value; }
        public int Kivehetoszabi { get => kivehetoszabi; set => kivehetoszabi = value; }
        public int Kivettszabi { get => kivettszabi; set => kivettszabi = value; }
        public string Nem { get => nem; set => nem = value; }
        public string Teljesnev { get => teljesnev; set => teljesnev = value; }
        public string Telefonszam { get => telefonszam; set => telefonszam = value; }
        public string Lakcim { get => lakcim; set => lakcim = value; }
        public string Adoazon { get => adoazon; set => adoazon = value; }
        public string Bankszamlaszam { get => bankszamlaszam; set => bankszamlaszam = value; }
        public string Szemelyi { get => szemelyi; set => szemelyi = value; }
        public string Tajszam { get => tajszam; set => tajszam = value; }
        public DateTime Csatlakozas { get => csatlakozas; set => csatlakozas = value; }
        public int Alapber { get => alapber; set => alapber = value; }
        public int Jogid { get => jogid; set => jogid = value; }
    }
}
