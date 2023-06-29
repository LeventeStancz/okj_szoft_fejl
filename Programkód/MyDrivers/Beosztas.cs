using System;

namespace MyDrivers
{
    public class Beosztas
    {
        private int bid;
        private Felhasznalo fsz;
        private Jarmu jarmu;
        private DateTime datum;
        private bool muszak;
        private int ora;

        public Beosztas(int bid, Felhasznalo fsz, Jarmu jarmu, DateTime datum, bool muszak, int ora)
        {
            this.Bid = bid;
            this.Fsz = fsz;
            this.Jarmu = jarmu;
            this.Datum = datum;
            this.Muszak = muszak;
            this.Ora = ora;
        }

        public string Fnev { get => fsz.Fnev; }
        public string Jnev { get => jarmu.Gyarto + " " + jarmu.Modell + " " + jarmu.Rendszam; }
        public string Bdatum { get => datum.ToString("yyyy-MM-dd"); }
        public string Bmuszak
        {
            get
            {
                if (muszak)
                {
                    return "Délelőtt";
                }
                return "Délután";
            }
        }

        public int Bid { get => bid; set => bid = value; }
        public Felhasznalo Fsz { get => fsz; set => fsz = value; }
        public Jarmu Jarmu { get => jarmu; set => jarmu = value; }
        public DateTime Datum { get => datum; set => datum = value; }
        public bool Muszak { get => muszak; set => muszak = value; }
        public int Ora { get => ora; set => ora = value; }
    }
}
