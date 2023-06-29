using System;

namespace MyDrivers
{
    public class Fuvar
    {
        private int fuvar_id;
        private Felhasznalo felhasznalo;
        private Jarmu jarmu;
        private DateTime datum;
        private string kezdes;
        private string befejezes;
        private int kerid;
        private int felvett_csom;
        private int sikeres_csom;

        public Fuvar(int fuvar_id, Felhasznalo felhasznalo, Jarmu jarmu, DateTime datum, string kezdes, string befejezes, int kerid, int felvett_csom, int sikeres_csom)
        {
            this.Fuvar_id = fuvar_id;
            this.Felhasznalo = felhasznalo;
            this.Jarmu = jarmu;
            this.Datum = datum;
            this.Kezdes = kezdes;
            this.Befejezes = befejezes;
            this.Kerid = kerid;
            this.Felvett_csom = felvett_csom;
            this.Sikeres_csom = sikeres_csom;
        }

        public string Fnev { get => felhasznalo.Fnev; }
        public string Jnev { get => jarmu.Gyarto + " " + jarmu.Modell + " " + jarmu.Rendszam; }
        public string Fdatum { get => datum.ToString("yyyy-MM-dd"); }
        public string Kerulet { get => Kisegito.Keruletek[kerid]; }
        public string Sikeres
        {
            get
            {
                if (sikeres_csom == -1)
                {
                    return "Nincs adat";
                }
                else
                {
                    return sikeres_csom.ToString();
                }
            }
        }
        public string Sikertelen
        {
            get
            {
                if (sikeres_csom == -1)
                {
                    return "Nincs adat";
                }
                else
                {
                    return (felvett_csom - sikeres_csom).ToString();
                }
            }
        }

        public int Fuvar_id { get => fuvar_id; set => fuvar_id = value; }
        public Felhasznalo Felhasznalo { get => felhasznalo; set => felhasznalo = value; }
        public Jarmu Jarmu { get => jarmu; set => jarmu = value; }
        public DateTime Datum { get => datum; set => datum = value; }
        public string Kezdes { get => kezdes; set => kezdes = value; }
        public string Befejezes { get => befejezes; set => befejezes = value; }
        public int Kerid { get => kerid; set => kerid = value; }
        public int Felvett_csom { get => felvett_csom; set => felvett_csom = value; }
        public int Sikeres_csom { get => sikeres_csom; set => sikeres_csom = value; }
    }
}
