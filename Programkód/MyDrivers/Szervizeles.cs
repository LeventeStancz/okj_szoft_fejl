using System;

namespace MyDrivers
{
    public class Szervizeles
    {
        private int szerid;
        private Jarmu jarmu;
        private Szerviz szerviz;
        private DateTime mettol;
        private DateTime meddig;
        private string indok;

        public Szervizeles(int szerid, Jarmu jarmu, Szerviz szerviz, DateTime mettol, DateTime meddig, string indok)
        {
            this.Szerid = szerid;
            this.Jarmu = jarmu;
            this.Szerviz = szerviz;
            this.Mettol = mettol;
            this.Meddig = meddig;
            this.Indok = indok;
        }

        public string Jnev
        {
            get
            {
                return jarmu.Gyarto + " " + jarmu.Modell;
            }
        }
        public string Sznev
        {
            get
            {
                return szerviz.Sznev;
            }
        }
        public string Szmettol
        {
            get
            {
                return mettol.ToString("yyyy-MM-dd");
            }
        }
        public string Szmeddig
        {
            get
            {
                if (meddig == DateTime.MinValue)
                {
                    return "Nincs adat";
                }
                else
                {
                    return meddig.ToString("yyyy-MM-dd");
                }
            }
        }

        public int Szerid { get => szerid; set => szerid = value; }
        public Jarmu Jarmu { get => jarmu; set => jarmu = value; }
        public Szerviz Szerviz { get => szerviz; set => szerviz = value; }
        public DateTime Mettol { get => mettol; set => mettol = value; }
        public DateTime Meddig { get => meddig; set => meddig = value; }
        public string Indok { get => indok; set => indok = value; }
    }
}