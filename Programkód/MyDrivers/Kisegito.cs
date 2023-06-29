using System.Collections.Generic;
using System.Linq;

namespace MyDrivers
{
    public class Kisegito
    {
        public static Dictionary<int, string> Jogosultsagok;
        public static Dictionary<int, string> Munkakorok;
        public static Dictionary<int, string> Keruletek;

        public static readonly Dictionary<string, string[]> KeresoCB_Megjelenites = new Dictionary<string, string[]>()
        {
            ["Felhasználók"] = new string[] { "Felhasznalónév", "Email cím", "Teljes név", "Személyi szám", "Tajkártya szám" },
            ["Beosztások"] = new string[] { "Dátum" },
            ["Fuvarok"] = new string[] { "Dátum", "Kerület" },
            ["Járművek"] = new string[] { "Rendszám", "Gyártó", "Modell" },
            ["Szervízek"] = new string[] { "Szervíz név", "Kontakt név", "Kontakt email cím" }
        };

        public static readonly Dictionary<string, string> TablaNevek = new Dictionary<string, string>()
        {
            ["Felhasználók"] = "felhasznalo",
            ["Beosztások"] = "beosztas",
            ["Fuvarok"] = "fuvar",
            ["Járművek"] = "jarmu",
            ["Szervízek"] = "szerviz"
        };

        public static readonly Dictionary<string, string> MezoNevek = new Dictionary<string, string>()
        {
            ["Felhasznalónév"] = "fnev",
            ["Email cím"] = "email",
            ["Teljes név"] = "teljesnev",
            ["Személyi szám"] = "szemelyi",
            ["Tajkártya szám"] = "tajszam",
            ["Dátum"] = "datum",
            ["Kerület"] = "kernev",
            ["Rendszám"] = "rendszam",
            ["Gyártó"] = "gyarto",
            ["Modell"] = "modell",
            ["Szervíz név"] = "sznev",
            ["Kontakt név"] = "kontaktnev",
            ["Kontakt email cím"] = "kontaktemail"
        };

        public static int ErtekIndexKereso(Dictionary<int, string>.ValueCollection ertekek, string megkotes)
        {
            int i = 0;
            while (i < ertekek.Count && ertekek.ElementAt(i) != megkotes)
            {
                i++;
            }
            if (i < ertekek.Count)
            {
                return i;
            }
            else
            {
                return 0;
            }
        }

        public static int KulcsKereso(Dictionary<int, string> szotar, string megkotes)
        {
            int eredmeny = -1;
            foreach (var item in szotar)
            {
                if (item.Value == megkotes)
                {
                    eredmeny = item.Key;
                }
            }
            return eredmeny;
        }
    }
}
