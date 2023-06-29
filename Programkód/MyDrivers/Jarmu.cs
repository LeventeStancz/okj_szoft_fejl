namespace MyDrivers
{
    public class Jarmu
    {
        private int jid;
        private string rendszam;
        private string evjarat;
        private string gyarto;
        private string modell;
        private int kmora;

        public Jarmu(int jid, string rendszam, string evjarat, string gyarto, string modell, int kmora)
        {
            this.Jid = jid;
            this.Rendszam = rendszam;
            this.Evjarat = evjarat;
            this.Gyarto = gyarto;
            this.Modell = modell;
            this.Kmora = kmora;
        }

        public Jarmu(int jid, string gyarto, string modell, string rendszam)
        {
            this.Jid = jid;
            this.Gyarto = gyarto;
            this.Modell = modell;
            this.Rendszam = rendszam;
        }

        public int Jid { get => jid; set => jid = value; }
        public string Rendszam { get => rendszam; set => rendszam = value; }
        public string Evjarat { get => evjarat; set => evjarat = value; }
        public string Gyarto { get => gyarto; set => gyarto = value; }
        public string Modell { get => modell; set => modell = value; }
        public int Kmora { get => kmora; set => kmora = value; }
    }
}
