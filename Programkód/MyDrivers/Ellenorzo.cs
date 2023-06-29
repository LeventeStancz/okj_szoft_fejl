using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace MyDrivers
{
    public class Ellenorzo
    {
        public static bool EmailCheck(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool MegfeleloDatum(string datum)
        {
            Regex regex = new Regex(@"^\d{4}-(02-(0[1-9]|[12][0-9])|(0[469]|11)-(0[1-9]|[12][0-9]|30)|(0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))$");
            return regex.IsMatch(datum);
        }

        public static bool RegiDatum(string datum)
        {
            DateTime bekertdatum = Convert.ToDateTime(datum);
            DateTime mai = DateTime.Now;
            DateTime egyhet = mai.AddDays(-7);
            if (bekertdatum.Date < egyhet.Date)
            {
                return true;
            }
            return false;
        }

        public static bool MegfeleloIdo(string ido)
        {
            Regex regex = new Regex(@"^(2[0-3]|[01]?[0-9]):([0-5]?[0-9])$");
            return regex.IsMatch(ido);
        }

        public static bool CsakSzam(string szamsor)
        {
            Regex regex = new Regex(@"^[0-9]*$");
            return regex.IsMatch(szamsor);
        }

        public static bool MegfeleloSzemelyi(string szemelyi)
        {
            Regex regex = new Regex(@"^[0-9]{6}[a-zA-Z]{2}$");
            return regex.IsMatch(szemelyi);
        }

        public static bool MegfeleloTelszam(string telszam)
        {
            Regex regex = new Regex(@"^(0|3){1}6\d{8,9}$");
            return regex.IsMatch(telszam);
        }

        public static bool MegfeleloRendszam(string rendszam)
        {
            Regex regex = new Regex(@"^[A-Za-z]{3}-\d{3}$");
            return regex.IsMatch(rendszam);
        }

        public static bool UresE(string[] elemek)
        {
            foreach (string item in elemek)
            {
                if (String.IsNullOrWhiteSpace(item))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool VanEvaltozas(string[] regiElemek, string[] ujElemek)
        {
            for (int i = 0; i < regiElemek.Length; i++)
            {
                if (regiElemek[i] != ujElemek[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}
