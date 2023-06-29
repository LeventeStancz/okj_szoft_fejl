using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyDrivers
{
    public class ABcsatlakozas
    {
        private const string eleres = "localhost";
        private const string abnev = "mydrivers";
        private const string felhasznalo = "root";
        private const string jelszo = "";
        private MySqlConnection csatlakozas;
        public bool hiba = false;

        public ABcsatlakozas()
        {
            csatlakozas = new MySqlConnection("Database = " + abnev + "; Data source = " + eleres + "; User id = " + felhasznalo + "; Password = " + jelszo + "; convert zero datetime=True;");
        }

        private bool Csatlakozott()
        {
            try
            {
                csatlakozas.Open();
                return true;
            }
            catch
            {
                hiba = true;
                return false;
            }
        }

        private bool Csatl_Bezar()
        {
            try
            {
                csatlakozas.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable Kereso(string tabla, string mezo, string megkotes, bool kerulet)
        {
            DataTable dt = new DataTable();
            string sql;
            MySqlCommand cmd;
            MySqlDataAdapter adapter;

            if (mezo == "kernev")
            {
                kerulet = true;
            }

            switch (tabla)
            {
                case "felhasznalo":
                    sql = "SELECT fid AS 'Id',fnev AS 'Felhasználó',munkakor.mknev AS 'Munkakör',jogosultsag.jognev AS 'Jogosultság',IFNULL(teljesnev, 'Nincs adat.') AS 'Teljes név',IFNULL(email, 'Nincs adat.') AS 'Email cím'," +
                        "IFNULL(szemelyi,'Nincs adat.') AS 'Személy igazolvány száma',CONCAT(kivettszabi, '/', kivehetoszabi) AS 'Szabadság',IFNULL(DATE_FORMAT(szuletesnap, '%Y-%m-%d'), 'Nincs adat.') AS 'Születési dátum'," +
                        "IFNULL(nem, 'Nincs adat') AS 'Nemi identitás',IFNULL(telefonszam, 'Nincs adat.') AS 'Telefonszám',IFNULL(lakcim, 'Nincs adat.') AS 'Lakcím'," +
                        "IFNULL(adoazon, 'Nincs adat.') AS 'Adóazonosító száma',IFNULL(bankszamlaszam, 'Nincs adat.') AS 'Bankszámla száma',IFNULL(tajszam, 'Nincs adat.') AS 'Tajkártya száma'," +
                        "IFNULL(DATE_FORMAT(csatlakozas, '%Y-%m-%d'), 'Nincs adat.') AS 'Csatlakozás dátuma',IFNULL(alapber, 'Nincs adat.') AS 'Alap bér' FROM felhasznalo INNER JOIN jogosultsag ON" +
                        " jogosultsag.jogid = felhasznalo.jogid INNER JOIN munkakor ON munkakor.mkid = felhasznalo.munkakorid WHERE " + mezo + " REGEXP @megkotes;";
                    cmd = new MySqlCommand(sql, csatlakozas);
                    cmd.Parameters.AddWithValue("@megkotes", megkotes);
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    return dt;
                case "beosztas":
                    sql = "SELECT bid AS 'Id', felhasznalo.fnev AS 'Felhasználó', jarmu.gyarto AS 'Jámrű gyártó', jarmu.modell AS 'Jármű modell'," +
                        "jarmu.rendszam AS 'Jármű rendszám',DATE_FORMAT(datum, '%Y-%m-%d') AS 'Dátum', IF(muszak, 'Délután', 'Délelőtt') AS 'Műszak'," +
                        "ora AS 'Munka óra száma' FROM beosztas INNER JOIN felhasznalo ON felhasznalo.fid = beosztas.fid INNER JOIN " +
                        "jarmu ON jarmu.jid = beosztas.jid WHERE " + mezo + " REGEXP @megkotes;";
                    cmd = new MySqlCommand(sql, csatlakozas);
                    cmd.Parameters.AddWithValue("@megkotes", megkotes);
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    return dt;
                case "fuvar":
                    if (kerulet)
                    {
                        try
                        {
                            int ker = Convert.ToInt32(megkotes);
                            sql = "SELECT fuvar_id AS 'Id',felhasznalo.fnev AS 'Felhasználó', jarmu.gyarto AS 'Jámrű gyártó', jarmu.modell AS 'Jármű modell'," +
                                "jarmu.rendszam AS 'Jármű rendszám', keruletek.kernev AS 'Kerület', DATE_FORMAT(datum, '%Y-%m-%d') AS 'Dátum'," +
                                "kezdes AS 'Fuvar kezdete', IFNULL(befejezes, 'Nincs adat.') AS 'Fuvar vége', felvett_csom AS 'Felvett csomagszám'," +
                                "IF(sikeres_csom=-1, 'Nincs adat', sikeres_csom) AS 'Sikeres csomagszám', " +
                                "CONCAT(IF(sikeres_csom=-1, 'Nincs adat.', (felvett_csom-sikeres_csom))) AS 'Sikertelen csomagszám' FROM fuvar " +
                                "INNER JOIN felhasznalo ON felhasznalo.fid = fuvar.fid INNER JOIN jarmu ON jarmu.jid = fuvar.jid INNER JOIN keruletek ON " +
                                "keruletek.kerid = fuvar.kerid WHERE keruletek.kerid = @megkotes;";
                            cmd = new MySqlCommand(sql, csatlakozas);
                            cmd.Parameters.AddWithValue("@megkotes", megkotes);
                            adapter = new MySqlDataAdapter(cmd);
                            adapter.Fill(dt);
                            return dt;
                        }
                        catch
                        {
                            sql = "SELECT fuvar_id AS 'Id',felhasznalo.fnev AS 'Felhasználó', jarmu.gyarto AS 'Jámrű gyártó', jarmu.modell AS 'Jármű modell'," +
                                "jarmu.rendszam AS 'Jármű rendszám', keruletek.kernev AS 'Kerület', DATE_FORMAT(datum, '%Y-%m-%d') AS 'Dátum'," +
                                "kezdes AS 'Fuvar kezdete', IFNULL(befejezes, 'Nincs adat.') AS 'Fuvar vége', felvett_csom AS 'Felvett csomagszám'," +
                                "IF(sikeres_csom=-1, 'Nincs adat', sikeres_csom) AS 'Sikeres csomagszám', " +
                                "CONCAT(IF(sikeres_csom=-1, 'Nincs adat.', (felvett_csom-sikeres_csom))) AS 'Sikertelen csomagszám' FROM fuvar " +
                                "INNER JOIN felhasznalo ON felhasznalo.fid = fuvar.fid INNER JOIN jarmu ON jarmu.jid = fuvar.jid INNER JOIN keruletek ON " +
                                "keruletek.kerid = fuvar.kerid WHERE keruletek.kernev REGEXP @megkotes;";
                            cmd = new MySqlCommand(sql, csatlakozas);
                            cmd.Parameters.AddWithValue("@megkotes", megkotes);
                            adapter = new MySqlDataAdapter(cmd);
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                    else
                    {
                        sql = "SELECT fuvar_id AS 'Id',felhasznalo.fnev AS 'Felhasználó', jarmu.gyarto AS 'Jámrű gyártó'," +
                             "jarmu.modell AS 'Jármű modell', jarmu.rendszam AS 'Jármű rendszám', keruletek.kernev AS 'Kerület'," +
                             "DATE_FORMAT(datum, '%Y-%m-%d') AS 'Dátum', kezdes AS 'Fuvar kezdete', IFNULL(befejezes, 'Nincs adat.') AS 'Fuvar vége'," +
                             "felvett_csom AS 'Felvett csomagszám', IF(sikeres_csom=-1, 'Nincs adat', sikeres_csom) AS 'Sikeres csomagszám'," +
                             "CONCAT(IF(sikeres_csom=-1, 'Nincs adat.', (felvett_csom-sikeres_csom))) AS 'Sikertelen csomagszám' FROM fuvar " +
                             "INNER JOIN felhasznalo ON felhasznalo.fid = fuvar.fid INNER JOIN jarmu ON jarmu.jid = fuvar.jid INNER JOIN keruletek " +
                             "ON keruletek.kerid = fuvar.kerid WHERE " + mezo + " REGEXP @megkotes;";
                        cmd = new MySqlCommand(sql, csatlakozas);
                        cmd.Parameters.AddWithValue("@megkotes", megkotes);
                        adapter = new MySqlDataAdapter(cmd);
                        adapter.Fill(dt);
                        return dt;
                    }
                case "jarmu":
                    sql = "SELECT jid AS 'Id',gyarto AS 'Gyártó', modell AS 'Modell', rendszam AS 'Rendszám', evjarat AS 'Évjárat', kmora AS 'Kilóméter óra'" +
                        " FROM jarmu WHERE " + mezo + " REGEXP @megkotes;";
                    cmd = new MySqlCommand(sql, csatlakozas);
                    cmd.Parameters.AddWithValue("@megkotes", megkotes);
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    return dt;
                case "szerviz":
                    sql = "SELECT szid AS 'Id', sznev AS 'Szervíz neve', kontaktnev AS 'Kontakt neve', kontaktemail AS 'Kontakt email címe'," +
                        "kontakttelszam AS 'Kontakt telefonszáma', cim AS 'Szervíz címe' FROM szerviz WHERE " + mezo + " REGEXP @megkotes;";
                    cmd = new MySqlCommand(sql, csatlakozas);
                    cmd.Parameters.AddWithValue("@megkotes", megkotes);
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    return dt;
                default:
                    sql = "SELECT CONCAT('Nincs a keresett szóra találat.') AS 'Nincs találat';";
                    cmd = new MySqlCommand(sql, csatlakozas);
                    adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dt);
                    return dt;
            }
        }

        public bool UjMunkakor(string mknev)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "INSERT INTO munkakor(mknev) " +
                        "VALUES(@mknev);";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@mknev", mknev);
                    int temp = cmd.ExecuteNonQuery();

                    eredmeny = temp > 0;
                    Csatl_Bezar();
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public bool SzabadsagModosit(int fid, int szabi)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "UPDATE felhasznalo SET kivettszabi = @szabi WHERE fid = @fid;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@szabi", szabi);
                    cmd.Parameters.AddWithValue("@fid", fid);
                    int temp = cmd.ExecuteNonQuery();

                    eredmeny = temp > 0;
                    Csatl_Bezar();
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public bool LetezoMunkakor(string mknev)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "SELECT COUNT(mkid) FROM munkakor WHERE mknev LIKE @mknev;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@mknev", mknev);

                    int talalat = (int)(long)cmd.ExecuteScalar();

                    if (talalat > 0)
                    {
                        eredmeny = true;
                    }

                    Csatl_Bezar();
                    return eredmeny;
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public Dictionary<int, string> Keruletek()
        {
            Dictionary<int, string> eredmeny = new Dictionary<int, string>();
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "SELECT kerid,kernev FROM keruletek;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    MySqlDataReader olvaso = cmd.ExecuteReader();
                    while (olvaso.Read())
                    {
                        eredmeny.Add(olvaso.GetInt32(0), olvaso.GetString(1));
                    }
                    Csatl_Bezar();
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public bool SzervizSzerkesztes(int szid, string kNev, string szNev, string kEmail, string kTelszam, string irsz, string telepules, string alCim)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    string cim = irsz + "," + telepules + "," + alCim;
                    string lekerdezes = "UPDATE szerviz SET kontaktnev = @kNev, kontaktemail = @kEmail, " +
                        "kontakttelszam = @kTelszam, sznev = @szNev, cim = @cim WHERE szid = @szid;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@kNev", kNev);
                    cmd.Parameters.AddWithValue("@kEmail", kEmail);
                    cmd.Parameters.AddWithValue("@kTelszam", kTelszam);
                    cmd.Parameters.AddWithValue("@szNev", szNev);
                    cmd.Parameters.AddWithValue("@cim", cim);
                    cmd.Parameters.AddWithValue("@szid", szid);
                    int temp = cmd.ExecuteNonQuery();

                    eredmeny = temp > 0;
                    Csatl_Bezar();
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public bool UjSzervizFelvetele(string kNev, string szNev, string kEmail, string kTelszam, string irsz, string telepules, string alCim)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    string cim = irsz + "," + telepules + "," + alCim;
                    string lekerdezes = "INSERT INTO szerviz(kontaktnev, kontaktemail, kontakttelszam, sznev, cim) " +
                        "VALUES(@kNev, @kEmail, @kTelszam, @szNev, @cim);";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@kNev", kNev);
                    cmd.Parameters.AddWithValue("@kEmail", kEmail);
                    cmd.Parameters.AddWithValue("@kTelszam", kTelszam);
                    cmd.Parameters.AddWithValue("@szNev", szNev);
                    cmd.Parameters.AddWithValue("@cim", cim);
                    int temp = cmd.ExecuteNonQuery();

                    eredmeny = temp > 0;
                    Csatl_Bezar();
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }


        public bool JelszoModosit(int fid, string password)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "UPDATE felhasznalo SET jelszo = SHA2(@jelszo, 256) WHERE fid = @fid;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@jelszo", password);
                    cmd.Parameters.AddWithValue("@fid", fid);

                    int temp = cmd.ExecuteNonQuery();

                    eredmeny = temp > 0;
                    Csatl_Bezar();
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public bool JelszoEgyezes(int fid, string password)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "SELECT COUNT(fid) FROM felhasznalo WHERE fid = @fid AND jelszo LIKE SHA2(@jelszo, 256) LIMIT 1;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@fid", fid);
                    cmd.Parameters.AddWithValue("@jelszo", password);

                    int talalat = (int)(long)cmd.ExecuteScalar();

                    if (talalat > 0)
                    {
                        eredmeny = true;
                    }

                    Csatl_Bezar();
                    return eredmeny;
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public Felhasznalo Felhasznalo_Adatok(int fhid, string fnev, string jelszo)
        {
            Felhasznalo fh = null;
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes;
                    MySqlCommand cmd;

                    if (fhid != -1)
                    {
                        lekerdezes = "SELECT fid, munkakorid, IFNULL(email, 'Nincs adat'), fnev, IFNULL(szuletesnap, '0000-00-00 00:00:00')," +
                        " IFNULL(kivehetoszabi, 0), IFNULL(kivettszabi, 0), IFNULL(nem, 'Nincs adat'), IFNULL(teljesnev, 'Nincs adat')," +
                        " IFNULL(telefonszam, 'Nincs adat'), IFNULL(lakcim, 'Nincs adat'), IFNULL(adoazon, 'Nincs adat'), IFNULL(bankszamlaszam, 'Nincs adat')," +
                        " IFNULL(szemelyi, 'Nincs adat'), IFNULL(tajszam, 'Nincs adat'), IFNULL(csatlakozas, '0000-00-00 00:00:00'), IFNULL(alapber, 0), jogid " +
                        " FROM felhasznalo WHERE fid = @fhid AND inaktiv = 0";
                        cmd = new MySqlCommand(lekerdezes, csatlakozas);
                        cmd.Parameters.AddWithValue("@fhid", fhid);
                    }
                    else
                    {
                        lekerdezes = "SELECT fid, munkakorid, IFNULL(email, 'Nincs adat'), fnev, IFNULL(szuletesnap, '0000-00-00 00:00:00')," +
                        " IFNULL(kivehetoszabi, 0), IFNULL(kivettszabi, 0), IFNULL(nem, 'Nincs adat'), IFNULL(teljesnev, 'Nincs adat')," +
                        " IFNULL(telefonszam, 'Nincs adat'), IFNULL(lakcim, 'Nincs adat'), IFNULL(adoazon, 'Nincs adat'), IFNULL(bankszamlaszam, 'Nincs adat')," +
                        " IFNULL(szemelyi, 'Nincs adat'), IFNULL(tajszam, 'Nincs adat'), IFNULL(csatlakozas, '0000-00-00 00:00:00'), IFNULL(alapber, 0), jogid " +
                        " FROM felhasznalo WHERE fnev LIKE @fnev AND jelszo LIKE SHA2(@jelszo, 256) AND inaktiv = 0";
                        cmd = new MySqlCommand(lekerdezes, csatlakozas);
                        cmd.Parameters.AddWithValue("@fnev", fnev);
                        cmd.Parameters.AddWithValue("@jelszo", jelszo);
                    }

                    MySqlDataReader olvaso = cmd.ExecuteReader();

                    while (olvaso.Read())
                    {
                        fh = new Felhasznalo(olvaso.GetInt32(0), olvaso.GetInt32(1),
                            olvaso.GetString(2), olvaso.GetString(3), olvaso.GetDateTime(4),
                            olvaso.GetInt32(5), olvaso.GetInt32(6), olvaso.GetString(7),
                            olvaso.GetString(8), olvaso.GetString(9), olvaso.GetString(10),
                            olvaso.GetString(11), olvaso.GetString(12), olvaso.GetString(13),
                            olvaso.GetString(14), olvaso.GetDateTime(15), olvaso.GetInt32(16),
                            olvaso.GetInt32(17));
                    }
                    Csatl_Bezar();
                }
                return fh;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return fh;
            }
        }

        public bool FelhasznaloSzerk(int fid, int munkakorid, int jogid, string fnev, string email, string teljesnev, string telszam, string nem, string kivehetoszabi, string lakcim, string adoazon, string bankszamla, string szemelyi, string tajszam, string alapber, string szulnap)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {

                    string lekerdezes = "UPDATE felhasznalo SET munkakorid = @munkakorid, jogid = @jogid, fnev = @fnev, email = @email, teljesnev = @teljesnev, " +
                        "telefonszam = @telszam, nem = @nem, kivehetoszabi = @kivehetoszabi, lakcim = @lakcim, " +
                        "adoazon = @adoazon, bankszamlaszam = @bankszamla, szemelyi = @szemelyi, tajszam = @tajszam, " +
                        "alapber = @alapber, szuletesnap = @szulnap WHERE fid = @fid;";

                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@fid", fid);
                    cmd.Parameters.AddWithValue("@munkakorid", munkakorid);
                    cmd.Parameters.AddWithValue("@jogid", jogid);
                    cmd.Parameters.AddWithValue("@fnev", fnev);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@teljesnev", teljesnev);
                    cmd.Parameters.AddWithValue("@telszam", telszam);
                    cmd.Parameters.AddWithValue("@nem", nem);
                    cmd.Parameters.AddWithValue("@kivehetoszabi", kivehetoszabi);
                    cmd.Parameters.AddWithValue("@lakcim", lakcim);
                    cmd.Parameters.AddWithValue("@adoazon", adoazon);
                    cmd.Parameters.AddWithValue("@bankszamla", bankszamla);
                    cmd.Parameters.AddWithValue("@szemelyi", szemelyi);
                    cmd.Parameters.AddWithValue("@tajszam", tajszam);
                    cmd.Parameters.AddWithValue("@alapber", alapber);
                    cmd.Parameters.AddWithValue("@szulnap", szulnap);
                    int temp = cmd.ExecuteNonQuery();
                    Csatl_Bezar();
                    return temp > 0;
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public List<Szervizeles> Szervizelesek(bool aktivak)
        {
            List<Szervizeles> szervizelesek = new List<Szervizeles>();
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "SELECT szerid, jarmu.jid, jarmu.rendszam, jarmu.evjarat, jarmu.gyarto, jarmu.modell, " +
                        "jarmu.kmora, szerviz.szid, szerviz.cim, szerviz.sznev, szerviz.kontaktnev, szerviz.kontaktemail, " +
                        "szerviz.kontakttelszam, mettol, meddig, indok FROM szervizeles INNER JOIN jarmu ON szervizeles.jid = " +
                        "jarmu.jid INNER JOIN szerviz ON szervizeles.szid = szerviz.szid ";
                    MySqlCommand cmd;
                    if (aktivak)
                    {
                        lekerdezes += "WHERE szervizeles.meddig LIKE '0001-01-01' OR szervizeles.meddig > '" + DateTime.Now.ToString("yyyy-MM-dd") + "';";
                        cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    }
                    else
                    {
                        cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    }
                    MySqlDataReader olvaso = cmd.ExecuteReader();
                    while (olvaso.Read())
                    {
                        szervizelesek.Add(new Szervizeles(olvaso.GetInt32("szerid"),
                            new Jarmu(
                                olvaso.GetInt32("jid"), olvaso.GetString("rendszam"), olvaso.GetString("evjarat"), olvaso.GetString("gyarto"),
                                olvaso.GetString("modell"), olvaso.GetInt32("kmora")
                                ),
                            new Szerviz(
                                olvaso.GetInt32("szid"), olvaso.GetString("cim"), olvaso.GetString("sznev"), olvaso.GetString("kontaktnev"),
                                olvaso.GetString("kontaktemail"), olvaso.GetString("kontakttelszam")
                                ),
                            olvaso.GetDateTime("mettol"), olvaso.GetDateTime("meddig"), olvaso.GetString("indok")
                            ));
                    }
                    Csatl_Bezar();
                }
                return szervizelesek;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return szervizelesek;
            }
        }

        public List<Fuvar> Fuvarok(bool aktivak)
        {
            List<Fuvar> fuvarok = new List<Fuvar>();
            try
            {
                if (Csatlakozott())
                {

                    string lekerdezes = "SELECT fuvar.fuvar_id, felhasznalo.fid, munkakorid, jogid, fnev, jarmu.jid, gyarto, modell, rendszam, " +
                        "datum, kezdes, IFNULL(befejezes, 'Nincs adat') as 'befejezes', fuvar.kerid, felvett_csom, IFNULL(sikeres_csom, '-1') as 'sikeres_csom' FROM fuvar " +
                        "INNER JOIN felhasznalo ON fuvar.fid = felhasznalo.fid INNER JOIN jarmu ON fuvar.jid = jarmu.jid " +
                        "INNER JOIN keruletek ON fuvar.kerid = keruletek.kerid ";
                    MySqlCommand cmd;
                    if (aktivak)
                    {
                        lekerdezes += "WHERE fuvar.befejezes IS NULL;";
                        cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    }
                    else
                    {
                        cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    }
                    MySqlDataReader olvaso = cmd.ExecuteReader();
                    while (olvaso.Read())
                    {
                        fuvarok.Add(new Fuvar(olvaso.GetInt32("fuvar_id"),
                            new Felhasznalo(olvaso.GetInt32("fid"), olvaso.GetInt32("munkakorid"), olvaso.GetInt32("jogid"), olvaso.GetString("fnev")),
                            new Jarmu(olvaso.GetInt32("jid"), olvaso.GetString("gyarto"), olvaso.GetString("modell"), olvaso.GetString("rendszam")),
                            olvaso.GetDateTime("datum"), olvaso.GetString("kezdes"), olvaso.GetString("befejezes"), olvaso.GetInt32("kerid"),
                            olvaso.GetInt32("felvett_csom"), olvaso.GetInt32("sikeres_csom")
                            )
                            );
                    }
                    Csatl_Bezar();
                }
                return fuvarok;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return fuvarok;
            }
        }

        public bool BeosztasSzerk(int bid, int fid, int jarmuid, string datum, bool muszak, string ora)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "UPDATE beosztas SET fid = @fid, jid = @jarmuid, datum = @datum, muszak = @muszak, " +
                        "ora = @ora WHERE bid = @bid;";

                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@fid", fid);
                    cmd.Parameters.AddWithValue("@jarmuid", jarmuid);
                    cmd.Parameters.AddWithValue("@datum", datum);
                    cmd.Parameters.AddWithValue("@muszak", muszak ? 0 : 1);
                    cmd.Parameters.AddWithValue("@ora", ora);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    int temp = cmd.ExecuteNonQuery();
                    Csatl_Bezar();
                    return temp > 0;
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public bool UjBeosztas(int fid, int jarmuid, string datum, bool muszak, string ora)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    MySqlCommand cmd;
                    string lekerdezes = "INSERT INTO beosztas(fid, jid, datum, muszak, ora) VALUES(@fid, @jid, @datum, @muszak, @ora);";
                    cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@fid", fid);
                    cmd.Parameters.AddWithValue("@jid", jarmuid);
                    cmd.Parameters.AddWithValue("@datum", datum);
                    cmd.Parameters.AddWithValue("@muszak", muszak);
                    cmd.Parameters.AddWithValue("@ora", ora);
                    int temp = cmd.ExecuteNonQuery();

                    eredmeny = temp > 0;
                    Csatl_Bezar();
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public bool UjFuvarFelvetele(int fid, int jarmuid, int kerid, string datum, string kezdes, string felvettcsomi, string sikeres, string befejezes)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    MySqlCommand cmd;
                    if (sikeres != "" && befejezes != "")
                    {
                        string lekerdezes = "INSERT INTO fuvar(fid, jid, kerid, datum, kezdes, felvett_csom, befejezes, sikeres_csom)" +
                        " VALUES(@fid, @jid, @kerid, @datum, @kezdes, @felvettcsomi, @sikeres, @befejezes);";
                        cmd = new MySqlCommand(lekerdezes, csatlakozas);
                        cmd.Parameters.AddWithValue("@fid", fid);
                        cmd.Parameters.AddWithValue("@jid", jarmuid);
                        cmd.Parameters.AddWithValue("@kerid", kerid);
                        cmd.Parameters.AddWithValue("@datum", datum);
                        cmd.Parameters.AddWithValue("@kezdes", kezdes);
                        cmd.Parameters.AddWithValue("@felvettcsomi", felvettcsomi);
                        cmd.Parameters.AddWithValue("@sikeres", sikeres);
                        cmd.Parameters.AddWithValue("@befejezes", befejezes);
                    }
                    else
                    {
                        string lekerdezes = "INSERT INTO fuvar(fid, jid, kerid, datum, kezdes, felvett_csom)" +
                        " VALUES(@fid, @jid, @kerid, @datum, @kezdes, @felvettcsomi);";
                        cmd = new MySqlCommand(lekerdezes, csatlakozas);
                        cmd.Parameters.AddWithValue("@fid", fid);
                        cmd.Parameters.AddWithValue("@jid", jarmuid);
                        cmd.Parameters.AddWithValue("@kerid", kerid);
                        cmd.Parameters.AddWithValue("@datum", datum);
                        cmd.Parameters.AddWithValue("@kezdes", kezdes);
                        cmd.Parameters.AddWithValue("@felvettcsomi", felvettcsomi);
                    }
                    int temp = cmd.ExecuteNonQuery();

                    eredmeny = temp > 0;
                    Csatl_Bezar();
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public bool SzervizelesSzerk(int szerid, string mettol, string meddig, string indok)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {

                    string lekerdezes = "UPDATE szervizeles SET mettol = @mettol, meddig = @meddig, indok = @indok WHERE szerid = @szerid;";

                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@szerid", szerid);
                    cmd.Parameters.AddWithValue("@mettol", mettol);
                    cmd.Parameters.AddWithValue("@meddig", meddig);
                    cmd.Parameters.AddWithValue("@indok", indok);
                    int temp = cmd.ExecuteNonQuery();
                    Csatl_Bezar();
                    return temp > 0;
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public bool FuvarSzerk(int fuvar_id, string datum, string kezdes, string befejezes, string sikeres, string felvett, int fid, int jarmuid, int kerid)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    MySqlCommand cmd;
                    string lekerdezes = "UPDATE fuvar SET fid = @fid, jid = @jid, kerid = @kerid, datum = @datum, kezdes = @kezdes, befejezes = @befejezes, " +
                        "felvett_csom = @felvettcsomi, sikeres_csom = @sikeres WHERE fuvar_id = @fuvar_id;";
                    cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@fid", fid);
                    cmd.Parameters.AddWithValue("@jid", jarmuid);
                    cmd.Parameters.AddWithValue("@kerid", kerid);
                    cmd.Parameters.AddWithValue("@datum", datum);
                    cmd.Parameters.AddWithValue("@kezdes", kezdes);
                    cmd.Parameters.AddWithValue("@felvettcsomi", felvett);
                    cmd.Parameters.AddWithValue("@sikeres", sikeres);
                    cmd.Parameters.AddWithValue("@befejezes", befejezes);
                    cmd.Parameters.AddWithValue("@fuvar_id", fuvar_id);

                    int temp = cmd.ExecuteNonQuery();

                    eredmeny = temp > 0;
                    Csatl_Bezar();
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public bool UjFelhasznalo(int munkakorid, int jogid, string fnev, string jelszo, string email, string teljesnev, string telszam, string nem, string kivehetoszabi, string lakcim, string adoazon, string bankszamla, string szemelyi, string tajszam, string alapber, string szulnap, DateTime csatlakozasDatum)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "INSERT INTO felhasznalo(munkakorid, jogid, email, fnev, jelszo, szuletesnap, kivehetoszabi, kivettszabi, nem, teljesnev, telefonszam, lakcim, adoazon, bankszamlaszam, szemelyi, tajszam, csatlakozas, alapber, inaktiv)" +
                        " VALUES(@munkakorid, @jogid, @email, @fnev, SHA2(@jelszo, 256), @szuletesnap, @kivehetoszabi, 0, @nem, @teljesnev, @telefonszam, @lakcim, @adoazon, @bankszamlaszam, @szemelyi, @tajszam, @csatlakozas, @alapber, 0);";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@munkakorid", munkakorid);
                    cmd.Parameters.AddWithValue("@jogid", jogid);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@fnev", fnev);
                    cmd.Parameters.AddWithValue("@jelszo", jelszo);
                    cmd.Parameters.AddWithValue("@szuletesnap", szulnap);
                    cmd.Parameters.AddWithValue("@kivehetoszabi", kivehetoszabi);
                    cmd.Parameters.AddWithValue("@nem", nem);
                    cmd.Parameters.AddWithValue("@teljesnev", teljesnev);
                    cmd.Parameters.AddWithValue("@telefonszam", telszam);
                    cmd.Parameters.AddWithValue("@lakcim", lakcim);
                    cmd.Parameters.AddWithValue("@adoazon", adoazon);
                    cmd.Parameters.AddWithValue("@bankszamlaszam", bankszamla);
                    cmd.Parameters.AddWithValue("@szemelyi", szemelyi);
                    cmd.Parameters.AddWithValue("@tajszam", tajszam);
                    cmd.Parameters.AddWithValue("@csatlakozas", csatlakozasDatum);
                    cmd.Parameters.AddWithValue("@alapber", alapber);

                    int temp = cmd.ExecuteNonQuery();

                    eredmeny = temp > 0;
                    Csatl_Bezar();
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public List<Felhasznalo> Felhasznalok(bool adminis)
        {
            List<Felhasznalo> felhasznalok = new List<Felhasznalo>();
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes;
                    if (adminis)
                    {
                        lekerdezes = "SELECT fid, munkakorid, IFNULL(email, 'Nincs adat'), fnev, IFNULL(szuletesnap, '0000-00-00 00:00:00')," +
                        " IFNULL(kivehetoszabi, 0), IFNULL(kivettszabi, 0), IFNULL(nem, 'Nincs adat'), IFNULL(teljesnev, 'Nincs adat')," +
                        " IFNULL(telefonszam, 'Nincs adat'), IFNULL(lakcim, 'Nincs adat'), IFNULL(adoazon, 'Nincs adat'), IFNULL(bankszamlaszam, 'Nincs adat')," +
                        " IFNULL(szemelyi, 'Nincs adat'), IFNULL(tajszam, 'Nincs adat'), IFNULL(csatlakozas, '0000-00-00 00:00:00'), IFNULL(alapber, 0), jogid " +
                        " FROM felhasznalo WHERE inaktiv = 0";
                    }
                    else
                    {
                        lekerdezes = "SELECT fid, munkakorid, IFNULL(email, 'Nincs adat'), fnev, IFNULL(szuletesnap, '0000-00-00 00:00:00')," +
                        " IFNULL(kivehetoszabi, 0), IFNULL(kivettszabi, 0), IFNULL(nem, 'Nincs adat'), IFNULL(teljesnev, 'Nincs adat')," +
                        " IFNULL(telefonszam, 'Nincs adat'), IFNULL(lakcim, 'Nincs adat'), IFNULL(adoazon, 'Nincs adat'), IFNULL(bankszamlaszam, 'Nincs adat')," +
                        " IFNULL(szemelyi, 'Nincs adat'), IFNULL(tajszam, 'Nincs adat'), IFNULL(csatlakozas, '0000-00-00 00:00:00'), IFNULL(alapber, 0), jogid " +
                        " FROM felhasznalo WHERE inaktiv = 0 AND jogid != (SELECT jogid FROM jogosultsag WHERE jognev LIKE 'Admin') ";
                    }
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    MySqlDataReader olvaso = cmd.ExecuteReader();
                    while (olvaso.Read())
                    {
                        felhasznalok.Add(new Felhasznalo(olvaso.GetInt32(0), olvaso.GetInt32(1),
                            olvaso.GetString(2), olvaso.GetString(3), olvaso.GetDateTime(4),
                            olvaso.GetInt32(5), olvaso.GetInt32(6), olvaso.GetString(7),
                            olvaso.GetString(8), olvaso.GetString(9), olvaso.GetString(10),
                            olvaso.GetString(11), olvaso.GetString(12), olvaso.GetString(13),
                            olvaso.GetString(14), olvaso.GetDateTime(15), olvaso.GetInt32(16),
                            olvaso.GetInt32(17)));
                    }
                    Csatl_Bezar();
                }
                return felhasznalok;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return felhasznalok;
            }
        }

        public Dictionary<int, string> FelhasznalokKevesAdat()
        {
            Dictionary<int, string> felhasznalok = new Dictionary<int, string>();
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "SELECT fid, munkakorid, jogid, fnev FROM felhasznalo WHERE jogid != (SELECT jogid FROM jogosultsag WHERE jognev LIKE 'Admin') AND inaktiv = 0;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    MySqlDataReader olvaso = cmd.ExecuteReader();
                    while (olvaso.Read())
                    {
                        int id = olvaso.GetInt32(0);
                        int munkakorid = olvaso.GetInt32(1);
                        int jogid = olvaso.GetInt32(2);
                        string fnev = olvaso.GetString(3);
                        felhasznalok.Add(id, fnev + " - " + Kisegito.Munkakorok[munkakorid]);
                    }
                    Csatl_Bezar();
                }
                return felhasznalok;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return felhasznalok;
            }
        }

        public Dictionary<int, string> JarmuvekKevesAdat()
        {
            Dictionary<int, string> jarmuvek = new Dictionary<int, string>();
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "SELECT jid, gyarto, modell, rendszam FROM jarmu;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    MySqlDataReader olvaso = cmd.ExecuteReader();
                    while (olvaso.Read())
                    {
                        jarmuvek.Add(
                            olvaso.GetInt32(0),
                            olvaso.GetString(1) + " " + olvaso.GetString(2) + " " + olvaso.GetString(3)
                            );
                    }
                    Csatl_Bezar();
                }
                return jarmuvek;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return jarmuvek;
            }
        }

        public Dictionary<int, string> Jogosultsagok()
        {
            Dictionary<int, string> jogosultsagok = new Dictionary<int, string>();
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "SELECT jogid, jognev FROM Jogosultsag ORDER BY jogid;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    MySqlDataReader olvaso = cmd.ExecuteReader();

                    while (olvaso.Read())
                    {
                        jogosultsagok.Add(olvaso.GetInt32(0), olvaso.GetString(1));
                    }
                    Csatl_Bezar();
                }
                return jogosultsagok;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return jogosultsagok;
            }
        }

        public bool JarmuSzerkesztes(int jid, string gyarto, string modell, string rendszam, string evjarat, string kmora)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {

                    string lekerdezes = "UPDATE jarmu SET gyarto = @gyarto, modell = @modell, rendszam = @rendszam, evjarat = @evjarat, kmora = @kmora WHERE jid = @jid;";

                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@jid", jid);
                    cmd.Parameters.AddWithValue("@gyarto", gyarto);
                    cmd.Parameters.AddWithValue("@modell", modell);
                    cmd.Parameters.AddWithValue("@rendszam", rendszam);
                    cmd.Parameters.AddWithValue("@evjarat", evjarat);
                    cmd.Parameters.AddWithValue("@kmora", kmora);
                    int temp = cmd.ExecuteNonQuery();
                    Csatl_Bezar();
                    return temp > 0;
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public bool UjJarmuFelvetele(string gyarto, string modell, string rendszam, string evjarat, string kmora)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "INSERT INTO jarmu(gyarto, modell, rendszam, evjarat, kmora) " +
                        "VALUES(@gyarto, @modell, @rendszam, @evjarat, @kmora);";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@gyarto", gyarto);
                    cmd.Parameters.AddWithValue("@modell", modell);
                    cmd.Parameters.AddWithValue("@rendszam", rendszam);
                    cmd.Parameters.AddWithValue("@evjarat", evjarat);
                    cmd.Parameters.AddWithValue("@kmora", kmora);
                    int temp = cmd.ExecuteNonQuery();

                    eredmeny = temp > 0;
                    Csatl_Bezar();
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public List<Jarmu> Jarmuvek()
        {
            List<Jarmu> jarmuvek = new List<Jarmu>();
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "SELECT jid, rendszam, evjarat, gyarto, modell, kmora FROM jarmu ";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas); ;
                    MySqlDataReader olvaso = cmd.ExecuteReader();

                    while (olvaso.Read())
                    {
                        jarmuvek.Add(new Jarmu(olvaso.GetInt32(0), olvaso.GetString(1),
                            olvaso.GetString(2), olvaso.GetString(3), olvaso.GetString(4),
                            olvaso.GetInt32(5)));
                    }
                    Csatl_Bezar();
                }
                return jarmuvek;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return jarmuvek;
            }
        }

        public void InaktivaTesz(int fid)
        {
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "UPDATE felhasznalo SET inaktiv = 1 WHERE fid = @fid;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@fid", fid);
                    int temp = cmd.ExecuteNonQuery();
                    Csatl_Bezar();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string Inaktiv(string fhnev, string jelszo)
        {
            string eredmeny = "hiba";
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "SELECT inaktiv FROM felhasznalo WHERE fnev LIKE @fhnev AND jelszo LIKE SHA2(@jelszo, 256);";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@fhnev", fhnev);
                    cmd.Parameters.AddWithValue("@jelszo", jelszo);
                    MySqlDataReader sr = cmd.ExecuteReader();

                    bool temp = false;

                    while (sr.Read())
                    {
                        temp = sr.GetBoolean(0);
                    }

                    Csatl_Bezar();
                    return temp.ToString();
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }


        public Dictionary<int, string> Munkakorok()
        {
            Dictionary<int, string> munkakorok = new Dictionary<int, string>();
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "SELECT mkid, mknev FROM munkakor ORDER BY mkid;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    MySqlDataReader olvaso = cmd.ExecuteReader();

                    while (olvaso.Read())
                    {
                        munkakorok.Add(olvaso.GetInt32(0), olvaso.GetString(1));
                    }
                    Csatl_Bezar();
                }
                return munkakorok;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return munkakorok;
            }
        }

        public List<Szerviz> Szervizek()
        {
            List<Szerviz> szervizek = new List<Szerviz>();
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "SELECT szid, cim, sznev, kontaktnev, kontaktemail, kontakttelszam FROM szerviz ";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas); ;
                    MySqlDataReader olvaso = cmd.ExecuteReader();
                    while (olvaso.Read())
                    {
                        szervizek.Add(new Szerviz(olvaso.GetInt32(0),
                            olvaso.GetString(1), olvaso.GetString(2),
                            olvaso.GetString(3), olvaso.GetString(4),
                            olvaso.GetString(5)));
                    }
                    Csatl_Bezar();
                }
                return szervizek;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return szervizek;
            }
        }

        public List<Beosztas> Beosztasok(string kezdo, string vege)
        {
            List<Beosztas> beosztasok = new List<Beosztas>();

            try
            {
                if (Csatlakozott())
                {

                    string lekerdezes = "SELECT bid, felhasznalo.fid, felhasznalo.munkakorid, felhasznalo.jogid, "
                        + "felhasznalo.fnev, jarmu.jid, jarmu.gyarto, jarmu.modell, jarmu.rendszam, datum, muszak, ora " +
                        "FROM beosztas INNER JOIN felhasznalo ON beosztas.fid = felhasznalo.fid INNER JOIN " +
                        "jarmu ON beosztas.jid = jarmu.jid ";
                    MySqlCommand cmd;
                    if (kezdo != "" && vege != "")
                    {
                        lekerdezes += "WHERE (datum BETWEEN @kezdo AND @vege) ORDER BY datum ASC;";
                        cmd = new MySqlCommand(lekerdezes, csatlakozas);
                        cmd.Parameters.AddWithValue("@kezdo", kezdo);
                        cmd.Parameters.AddWithValue("@vege", vege);
                    }
                    else
                    {
                        lekerdezes += "ORDER BY datum ASC;";
                        cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    }
                    MySqlDataReader olvaso = cmd.ExecuteReader();

                    while (olvaso.Read())
                    {
                        beosztasok.Add(new Beosztas(olvaso.GetInt32("bid"),
                            new Felhasznalo(olvaso.GetInt32("fid"), olvaso.GetInt32("munkakorid"),
                            olvaso.GetInt32("jogid"), olvaso.GetString("fnev")),
                            new Jarmu(olvaso.GetInt32("jid"), olvaso.GetString("gyarto"),
                            olvaso.GetString("modell"), olvaso.GetString("rendszam")),
                            olvaso.GetDateTime("datum"), olvaso.GetBoolean("muszak"),
                            olvaso.GetInt32("ora"))
                            );
                    }
                    Csatl_Bezar();
                }
                return beosztasok;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return beosztasok;
            }
        }

        public bool SajatProfilSzerkesztes(int fid, string teljesnev, string fhnev, string nem, string szemelyi, string email, string szuldatum, string telszam, string lakcim, string adoszam, string bankszamlaszam, string tajszam)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {

                    string lekerdezes = "UPDATE felhasznalo SET teljesnev = @teljesnev, fnev = @fhnev, nem = @nem, szemelyi = @szemelyi, email = @email, szuletesnap = @szuldatum, telefonszam = @telszam, lakcim = @lakcim, adoazon = @adoszam, bankszamlaszam = @bankszamlaszam, tajszam = @tajszam WHERE fid = @fid;";

                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@fid", fid);
                    cmd.Parameters.AddWithValue("@teljesnev", teljesnev);
                    cmd.Parameters.AddWithValue("@fhnev", fhnev);
                    cmd.Parameters.AddWithValue("@nem", nem);
                    cmd.Parameters.AddWithValue("@szemelyi", szemelyi);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@szuldatum", szuldatum);
                    cmd.Parameters.AddWithValue("@telszam", telszam);
                    cmd.Parameters.AddWithValue("@lakcim", lakcim);
                    cmd.Parameters.AddWithValue("@adoszam", adoszam);
                    cmd.Parameters.AddWithValue("@bankszamlaszam", bankszamlaszam);
                    cmd.Parameters.AddWithValue("@tajszam", tajszam);
                    int temp = cmd.ExecuteNonQuery();
                    Csatl_Bezar();
                    return temp > 0;
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public bool UjSzervizeles(int jid, int szid, string mettol, string meddig, string indok)
        {
            bool eredmeny = false;
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "INSERT INTO szervizeles(jid, szid, mettol, meddig, indok) " +
                        "VALUES(@jid, @szid, @mettol, @meddig, @indok);";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@jid", jid);
                    cmd.Parameters.AddWithValue("@szid", szid);
                    cmd.Parameters.AddWithValue("@mettol", mettol);
                    if (meddig == "")
                    {
                        cmd.Parameters.AddWithValue("@meddig", DateTime.MinValue);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@meddig", meddig);
                    }
                    cmd.Parameters.AddWithValue("@indok", indok);
                    int temp = cmd.ExecuteNonQuery();

                    eredmeny = temp > 0;
                    Csatl_Bezar();
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public bool ProfilTorlese(int fid)
        {
            bool eredmeny = false;
            try
            {
                List<int> eredmenyek = new List<int>();
                eredmenyek.Add(BeosztasbolFelhasznaloTorles(fid));
                eredmenyek.Add(FuvarbolFelhasznaloTorles(fid));
                eredmenyek.Add(FelhasznalobolTorles(fid));

                return !eredmenyek.All(c => c == 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        private int BeosztasbolFelhasznaloTorles(int fid)
        {
            int eredmeny = 0;
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "DELETE FROM beosztas WHERE beosztas.fid = @fid;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@fid", fid);
                    int temp = cmd.ExecuteNonQuery();
                    Csatl_Bezar();
                    return temp;
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        private int FuvarbolFelhasznaloTorles(int fid)
        {
            int eredmeny = 0;
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "DELETE FROM fuvar WHERE fuvar.fid = @fid;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@fid", fid);
                    int temp = cmd.ExecuteNonQuery();
                    Csatl_Bezar();
                    return temp;
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        private int FelhasznalobolTorles(int fid)
        {
            int eredmeny = 0;
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "DELETE FROM felhasznalo WHERE felhasznalo.fid = @fid;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    cmd.Parameters.AddWithValue("@fid", fid);
                    int temp = cmd.ExecuteNonQuery();
                    Csatl_Bezar();
                    return temp;
                }
                return eredmeny;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return eredmeny;
            }
        }

        public Dictionary<int, string> SzervizekKevesAdat()
        {
            Dictionary<int, string> szervizek = new Dictionary<int, string>();
            try
            {
                if (Csatlakozott())
                {
                    string lekerdezes = "SELECT szid, sznev FROM szerviz ORDER BY szid;";
                    MySqlCommand cmd = new MySqlCommand(lekerdezes, csatlakozas);
                    MySqlDataReader olvaso = cmd.ExecuteReader();

                    while (olvaso.Read())
                    {
                        szervizek.Add(olvaso.GetInt32(0), olvaso.GetString(1));
                    }
                    Csatl_Bezar();
                }
                return szervizek;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return szervizek;
            }
        }
    }
}
