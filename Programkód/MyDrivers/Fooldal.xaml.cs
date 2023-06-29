using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyDrivers
{
    /// <summary>
    /// Interaction logic for Fooldal.xaml
    /// </summary>

    public partial class Fooldal : Window
    {
        private ABcsatlakozas ab;
        private Felhasznalo felhasznalo;
        private readonly string kijelentkezesTag = "Kijelentkezes";
        private readonly string szerkTag = "Szerkesztes";
        private readonly string[] nincsAdat = { "Nincs adat." };
        private Felhasznalo SzerkFelhasznalo;
        private Szervizeles SzerkSzervizeles;
        private Jarmu SzerkJarmu;
        private Szerviz SzerkSzerviz;
        private Fuvar SzerkFuvar;
        private Beosztas SzerkBeosztas;
        private string[] SzerkFuvarFhJarmuKer = new string[3];
        private bool cbBetoltott = false;
        private Dictionary<int, string> JarmuvekKevesAdat;
        private Dictionary<int, string> FelhasznalokKevesAdat;
        private Dictionary<int, string> SzervizekKevesAdat;
        private DateTime eppElsoNap;

        public Fooldal(Felhasznalo fh, ABcsatlakozas ab)
        {
            InitializeComponent();
            felhasznalo = fh;
            this.ab = ab;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TeljesKepernyoBeallitas();
            FelugroAblak.Tag = kijelentkezesTag;
            this.MaxHeight = SystemParameters.WorkArea.Height;
            this.MaxWidth = SystemParameters.WorkArea.Width;
            FHnev_Label.Content = felhasznalo.Fnev;
            if (Kisegito.Jogosultsagok[felhasznalo.Jogid] == "Felhasználó")
            {
                Fooldal_btn.Visibility = Visibility.Collapsed;
                BeosztasTervezes_btn.Visibility = Visibility.Collapsed;
                Fuvarok_btn.Visibility = Visibility.Collapsed;
                Felhasznalok_btn.Visibility = Visibility.Collapsed;
                Jarmuvek_btn.Visibility = Visibility.Collapsed;
                Szervizek_btn.Visibility = Visibility.Collapsed;
                Szervizeles_btn.Visibility = Visibility.Collapsed;

                SzervizekBezar();
                JarmuvekBezar();
                FelhasznalokBezar();
                SzervizelesBezar();
                FooldalBezar();
                FuvarokBezar();
                BeosztasBezar();
                SajatProfNyit();
            }
            else
            {
                SzervizekBezar();
                SajatProfBezar();
                JarmuvekBezar();
                FelhasznalokBezar();
                SzervizelesBezar();
                FuvarokBezar();
                BeosztasBezar();
                FooldalNyit();
            }
        }

        private void TeljesKepernyoBeallitas()
        {
            this.WindowState = WindowState.Maximized;
            Max_min_gomb.Source = new BitmapImage(new Uri(@"/minimize.png", UriKind.Relative));
        }

        private void Bezar_gomb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void Max_min_gomb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                Max_min_gomb.Source = new BitmapImage(new Uri(@"/maximize.png", UriKind.Relative));
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                Max_min_gomb.Source = new BitmapImage(new Uri(@"/minimize.png", UriKind.Relative));
            }
        }

        //menu gombok
        private void Fooldal_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((string)FelugroAblak.Tag == szerkTag)
            {
                FelugroAblak.VerticalAlignment = VerticalAlignment.Center;
                FelugroAblak.HorizontalAlignment = HorizontalAlignment.Center;
                Felugro_Label.Text = "Szerkesztés folyamatban, biztos félbehagyod a szerkesztést?";
                FelugroAblak.Visibility = Visibility.Visible;
            }
            else
            {
                SzervizekBezar();
                SajatProfBezar();
                JarmuvekBezar();
                FelhasznalokBezar();
                SzervizelesBezar();
                FuvarokBezar();
                BeosztasBezar();
                FooldalNyit();
            }
        }

        private void BeosztasTervezes_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((string)FelugroAblak.Tag == szerkTag)
            {
                FelugroAblak.VerticalAlignment = VerticalAlignment.Center;
                FelugroAblak.HorizontalAlignment = HorizontalAlignment.Center;
                Felugro_Label.Text = "Szerkesztés folyamatban, biztos félbehagyod a szerkesztést?";
                FelugroAblak.Visibility = Visibility.Visible;
            }
            else
            {
                SzervizekBezar();
                SajatProfBezar();
                JarmuvekBezar();
                FelhasznalokBezar();
                SzervizelesBezar();
                FuvarokBezar();
                FooldalBezar();
                BeosztasNyit();
            }
        }

        private void Felhasznalok_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((string)FelugroAblak.Tag == szerkTag)
            {
                FelugroAblak.VerticalAlignment = VerticalAlignment.Center;
                FelugroAblak.HorizontalAlignment = HorizontalAlignment.Center;
                Felugro_Label.Text = "Szerkesztés folyamatban, biztos félbehagyod a szerkesztést?";
                FelugroAblak.Visibility = Visibility.Visible;
            }
            else
            {
                FooldalBezar();
                SzervizekBezar();
                SajatProfBezar();
                JarmuvekBezar();
                SzervizelesBezar();
                FuvarokBezar();
                BeosztasBezar();
                FelhasznalokNyit();
            }
        }

        private void Jarmuvek_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((string)FelugroAblak.Tag == szerkTag)
            {
                FelugroAblak.VerticalAlignment = VerticalAlignment.Center;
                FelugroAblak.HorizontalAlignment = HorizontalAlignment.Center;
                Felugro_Label.Text = "Szerkesztés folyamatban, biztos félbehagyod a szerkesztést?";
                FelugroAblak.Visibility = Visibility.Visible;
            }
            else
            {
                FooldalBezar();
                SzervizekBezar();
                SajatProfBezar();
                FelhasznalokBezar();
                SzervizelesBezar();
                FuvarokBezar();
                BeosztasBezar();
                JarmuvekNyit();
            }
        }

        private void Szervizek_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((string)FelugroAblak.Tag == szerkTag)
            {
                FelugroAblak.VerticalAlignment = VerticalAlignment.Center;
                FelugroAblak.HorizontalAlignment = HorizontalAlignment.Center;
                Felugro_Label.Text = "Szerkesztés folyamatban, biztos félbehagyod a szerkesztést?";
                FelugroAblak.Visibility = Visibility.Visible;
            }
            else
            {
                SajatProfBezar();
                FooldalBezar();
                JarmuvekBezar();
                FelhasznalokBezar();
                SzervizelesBezar();
                FuvarokBezar();
                BeosztasBezar();
                SzervizekNyit();
            }
        }


        private void SajatProfil_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((string)FelugroAblak.Tag == szerkTag)
            {
                FelugroAblak.VerticalAlignment = VerticalAlignment.Center;
                FelugroAblak.HorizontalAlignment = HorizontalAlignment.Center;
                Felugro_Label.Text = "Szerkesztés folyamatban, biztos félbehagyod a szerkesztést?";
                FelugroAblak.Visibility = Visibility.Visible;
            }
            else
            {
                FooldalBezar();
                SzervizekBezar();
                JarmuvekBezar();
                FelhasznalokBezar();
                SzervizelesBezar();
                FuvarokBezar();
                BeosztasBezar();
                SajatProfNyit();
            }
        }

        private void Fuvarok_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((string)FelugroAblak.Tag == szerkTag)
            {
                FelugroAblak.VerticalAlignment = VerticalAlignment.Center;
                FelugroAblak.HorizontalAlignment = HorizontalAlignment.Center;
                Felugro_Label.Text = "Szerkesztés folyamatban, biztos félbehagyod a szerkesztést?";
                FelugroAblak.Visibility = Visibility.Visible;
            }
            else
            {
                FooldalBezar();
                SzervizekBezar();
                JarmuvekBezar();
                FelhasznalokBezar();
                SzervizelesBezar();
                SajatProfBezar();
                BeosztasBezar();
                FuvarokNyit();
            }
        }

        private void Szervizeles_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((string)FelugroAblak.Tag == szerkTag)
            {
                FelugroAblak.VerticalAlignment = VerticalAlignment.Center;
                FelugroAblak.HorizontalAlignment = HorizontalAlignment.Center;
                Felugro_Label.Text = "Szerkesztés folyamatban, biztos félbehagyod a szerkesztést?";
                FelugroAblak.Visibility = Visibility.Visible;
            }
            else
            {
                FooldalBezar();
                SzervizekBezar();
                JarmuvekBezar();
                FelhasznalokBezar();
                SajatProfBezar();
                FuvarokBezar();
                BeosztasBezar();
                SzervizelesNyit();
            }
        }


        private void Kijelentkezes_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((string)FelugroAblak.Tag == szerkTag)
            {
                FelugroAblak.VerticalAlignment = VerticalAlignment.Center;
                FelugroAblak.HorizontalAlignment = HorizontalAlignment.Center;
                Felugro_Label.Text = "Szerkesztés folyamatban, biztos félbehagyod a szerkesztést?";
                FelugroAblak.Visibility = Visibility.Visible;
            }
            else
            {
                FelugroAblak.VerticalAlignment = VerticalAlignment.Center;
                FelugroAblak.HorizontalAlignment = HorizontalAlignment.Center;
                Felugro_Label.Text = "Biztos ki szeretnél jelentkezni?";
                FelugroAblak.Visibility = Visibility.Visible;
            }
        }

        //Fooldal

        private void KeresoAlap()
        {
            dg.ItemsSource = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("Keresés");
            var sor = dt.NewRow();
            sor["Keresés"] = "A kereséshez irjon a kereső mezőbe és kattintson a keresés gombra.";
            dt.Rows.Add(sor);
            dg.ItemsSource = dt.DefaultView;
        }

        private void NincsTalalat()
        {
            dg.ItemsSource = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("Találat");
            var sor = dt.NewRow();
            sor["Találat"] = "Nincs találat a keresett kifejezésre.";
            dt.Rows.Add(sor);
            dg.ItemsSource = dt.DefaultView;
        }

        private void Kereses_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Ellenorzo.UresE(new string[] { Kereses_tb.Text }))
            {
                MessageBox.Show("A kereső mező nem maradhat üresen!", "Hiba!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string keresettszo = Kereses_tb.Text;
            string tabla = Kisegito.TablaNevek[KeresoTabla_cb.SelectedItem.ToString()];
            string mezo = Kisegito.MezoNevek[KeresoMezo_cb.SelectedItem.ToString()];

            DataTable dt = ab.Kereso(tabla, mezo, keresettszo, false);

            if (dt.Rows.Count == 0)
            {
                NincsTalalat();
            }
            else
            {
                dg.ItemsSource = dt.DefaultView;
            }
        }

        private void FooldalNormal()
        {
            KeresoTabla_cb.ItemsSource = Kisegito.KeresoCB_Megjelenites.Keys;
            KeresoTabla_cb.SelectedIndex = 0;
            KeresoMezo_cb.ItemsSource = Kisegito.KeresoCB_Megjelenites[KeresoTabla_cb.SelectedItem.ToString()];
            KeresoMezo_cb.SelectedIndex = 0;
            KeresoAlap();
            cbBetoltott = true;
            Kereses_tb.Text = "";
        }

        private void KeresoTabla_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBetoltott)
            {
                KeresoMezo_cb.ItemsSource = Kisegito.KeresoCB_Megjelenites[KeresoTabla_cb.SelectedItem.ToString()];
                KeresoMezo_cb.SelectedIndex = 0;
            }
        }

        //Beosztas

        private void SzerkBeosztasMentes_btn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(SzerkBeosztasOra_tb.Text) || String.IsNullOrWhiteSpace(SzerkBeosztasDatum_tb.Text))
            {
                MessageBox.Show("Ne hagyj üresen egy mezőt se!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.MegfeleloDatum(SzerkBeosztasDatum_tb.Text))
            {
                MessageBox.Show("Nem megfelelő dátum formátum! Pl.: 2022-01-01", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string datum = SzerkBeosztasDatum_tb.Text;
            string ora = SzerkBeosztasOra_tb.Text;
            string felhasznalo = SzerkBeosztasFh_cb.SelectedItem.ToString();
            string jarmu = SzerkBeosztasJarmuvek_cb.SelectedItem.ToString();
            bool muszak = SzerkBeosztasMuszak_cb.Text == "Délután" ? false : true;

            string[] regiElemek = { SzerkBeosztas.Datum.ToString("yyyy-MM-dd"), SzerkBeosztas.Ora.ToString(),
            SzerkBeosztas.Fsz.Fnev + " - " + Kisegito.Munkakorok[SzerkBeosztas.Fsz.Munkakorid], SzerkBeosztas.Jnev,
            SzerkBeosztas.Muszak.ToString()};

            string[] ujElemek = { datum, ora, felhasznalo, jarmu, muszak.ToString() };

            if (!Ellenorzo.VanEvaltozas(regiElemek, ujElemek))
            {
                BeosztasOldalNormal();
                return;
            }

            int fid = Kisegito.KulcsKereso(FelhasznalokKevesAdat, felhasznalo);
            int jarmuid = Kisegito.KulcsKereso(JarmuvekKevesAdat, jarmu);
            bool muszak1 = Convert.ToBoolean(SzerkBeosztasMuszak_cb.SelectedIndex);

            bool eredmeny = ab.BeosztasSzerk(SzerkBeosztas.Bid, fid, jarmuid, datum, muszak1, ora);

            if (!eredmeny)
            {
                MessageBox.Show("Sikertelen mentés!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                BeosztasOldalNormal();
                return;
            }

            MessageBox.Show("Sikeres mentés!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            BeosztasOldalNormal();
        }

        private void SzerkBeosztas_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Beosztasok_lb.SelectedIndex == -1)
            {
                MessageBox.Show("Nincs adat amit szerkeszteni tudnál!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SzerkBeosztas = (Beosztas)Beosztasok_lb.SelectedItem;

            FelugroAblak.Tag = szerkTag;

            SzerkBeosztasOra_tb.Text = SzerkBeosztas.Ora.ToString();

            KivalasztottBeosztas_label.Content = SzerkBeosztas.Fnev + " " + SzerkBeosztas.Datum.ToString("yyyy-MM-dd");

            Beosztasok_lb.Visibility = Visibility.Collapsed;
            UjBeosztas_Grid.Visibility = Visibility.Collapsed;

            SzerkBeosztas_Grid.Visibility = Visibility.Visible;
            BeosztasVissza_btn.Visibility = Visibility.Visible;


            FelhasznalokKevesAdat = ab.FelhasznalokKevesAdat();
            if (FelhasznalokKevesAdat.Count != 0)
            {
                string kereses = SzerkBeosztas.Fsz.Fnev + " - " + Kisegito.Munkakorok[SzerkBeosztas.Fsz.Munkakorid];
                SzerkBeosztasFh_cb.ItemsSource = FelhasznalokKevesAdat.Values;
                SzerkBeosztasFh_cb.SelectedIndex = Kisegito.ErtekIndexKereso(FelhasznalokKevesAdat.Values, kereses);
            }
            else
            {
                SzerkBeosztasFh_cb.ItemsSource = nincsAdat;
                SzerkBeosztasFh_cb.SelectedIndex = 0;
            }

            JarmuvekKevesAdat = ab.JarmuvekKevesAdat();
            if (JarmuvekKevesAdat.Count != 0)
            {
                string kereses = SzerkBeosztas.Jnev;
                SzerkBeosztasJarmuvek_cb.ItemsSource = JarmuvekKevesAdat.Values;
                SzerkBeosztasJarmuvek_cb.SelectedIndex = Kisegito.ErtekIndexKereso(JarmuvekKevesAdat.Values, kereses);
            }
            else
            {
                SzerkBeosztasJarmuvek_cb.ItemsSource = nincsAdat;
                SzerkBeosztasJarmuvek_cb.SelectedIndex = 0;
            }

            SzerkBeosztasMuszak_cb.SelectedIndex = SzerkBeosztas.Muszak == true ? 0 : 1;

            SzerkBeosztasDatum_tb.Text = SzerkBeosztas.Datum.ToString("yyyy-MM-dd");
        }

        private void UjBeosztasMentes_btn_Click(object sender, RoutedEventArgs e)
        {
            if (UjBeosztasFh_cb.SelectedItem.ToString() == nincsAdat[0])
            {
                MessageBox.Show("Nincs felhasználó akit ki tudnál választani!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (UjBeosztasJarmuvek_cb1.SelectedItem.ToString() == nincsAdat[0])
            {
                MessageBox.Show("Nincs jármű amit ki tudnál választani!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (String.IsNullOrWhiteSpace(UjBeosztasOra_tb.Text))
            {
                MessageBox.Show("Ne hagyj üresen egy mezőt se!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string ora = UjBeosztasOra_tb.Text;
            int fid = Kisegito.KulcsKereso(FelhasznalokKevesAdat, UjBeosztasFh_cb.SelectedItem.ToString());
            int jarmuid = Kisegito.KulcsKereso(JarmuvekKevesAdat, UjBeosztasJarmuvek_cb1.SelectedItem.ToString());
            string datum = UjBeosztasDatum_cb.SelectedItem.ToString();
            bool muszak = Convert.ToBoolean(UjBeosztasMuszak_cb.SelectedIndex);

            bool eredmeny = ab.UjBeosztas(fid, jarmuid, datum, muszak, ora);

            if (!eredmeny)
            {
                MessageBox.Show("Sikertelen mentés!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Sikeres mentés!", "Mentés!", MessageBoxButton.OK, MessageBoxImage.Information);
            BeosztasOldalNormal();
        }

        private void BeosztasVissza_btn_Click(object sender, RoutedEventArgs e)
        {
            BeosztasOldalNormal();
        }

        private void UjBeosztas_btn_Click(object sender, RoutedEventArgs e)
        {
            Beosztasok_lb.Visibility = Visibility.Collapsed;
            SzerkBeosztas_Grid.Visibility = Visibility.Collapsed;

            UjBeosztasOra_tb.Text = "";

            UjBeosztas_Grid.Visibility = Visibility.Visible;
            BeosztasVissza_btn.Visibility = Visibility.Visible;

            FelhasznalokKevesAdat = ab.FelhasznalokKevesAdat();
            if (FelhasznalokKevesAdat.Count != 0)
            {
                UjBeosztasFh_cb.ItemsSource = FelhasznalokKevesAdat.Values;
                UjBeosztasFh_cb.SelectedIndex = 0;
            }
            else
            {
                UjBeosztasFh_cb.ItemsSource = nincsAdat;
                UjBeosztasFh_cb.SelectedIndex = 0;
            }

            JarmuvekKevesAdat = ab.JarmuvekKevesAdat();
            if (JarmuvekKevesAdat.Count != 0)
            {
                UjBeosztasJarmuvek_cb1.ItemsSource = JarmuvekKevesAdat.Values;
                UjBeosztasJarmuvek_cb1.SelectedIndex = 0;
            }
            else
            {
                UjBeosztasJarmuvek_cb1.ItemsSource = nincsAdat;
                UjBeosztasJarmuvek_cb1.SelectedIndex = 0;
            }

            UjBeosztasDatum_cb.Items.Clear();
            for (int i = 0; i < 7; i++)
            {
                UjBeosztasDatum_cb.Items.Add(eppElsoNap.AddDays(i).ToString("yyyy-MM-dd"));
            }
            UjBeosztasDatum_cb.SelectedIndex = 0;

        }

        private void BeosztasOldalNormal()
        {
            BeosztasVissza_btn.Visibility = Visibility.Collapsed;
            UjBeosztas_Grid.Visibility = Visibility.Collapsed;
            SzerkBeosztas_Grid.Visibility = Visibility.Collapsed;

            Beosztasok_lb.Visibility = Visibility.Visible;

            DateTime ma = DateTime.Now;
            DateTime elsonap = ElsoNap(ma);
            DateTime utolsonap = UtolsoNap(ma);

            eppElsoNap = elsonap;

            BeosztasDatumHet_label.Content = elsonap.ToString("yyyy-MM-dd") + " <---> " + utolsonap.ToString("yyyy-MM-dd");

            List<Beosztas> beosztasok = ab.Beosztasok(elsonap.ToString("yyyy-MM-dd"), utolsonap.ToString("yyyy-MM-dd"));

            Beosztasok_lb.ItemsSource = null;
            Beosztasok_lb.SelectedIndex = -1;

            if (beosztasok.Count != 0)
            {
                Beosztasok_lb.ItemsSource = beosztasok;
                Beosztasok_lb.SelectedIndex = 0;
            }
        }

        private void BeosztasHetHatra_btn_Click(object sender, RoutedEventArgs e)
        {
            DateTime elsonap = ElsoNap(eppElsoNap.AddDays(-1));
            DateTime utolsonap = UtolsoNap(eppElsoNap.AddDays(-1));

            eppElsoNap = elsonap;

            BeosztasDatumHet_label.Content = elsonap.ToString("yyyy-MM-dd") + " <---> " + utolsonap.ToString("yyyy-MM-dd");

            List<Beosztas> beosztasok = ab.Beosztasok(elsonap.ToString("yyyy-MM-dd"), utolsonap.ToString("yyyy-MM-dd"));

            if (beosztasok.Count != 0)
            {
                Beosztasok_lb.ItemsSource = beosztasok;
                Beosztasok_lb.SelectedIndex = 0;
            }
            else
            {
                Beosztasok_lb.ItemsSource = null;
                Beosztasok_lb.SelectedIndex = -1;
            }

            UjBeosztasDatum_cb.Items.Clear();
            for (int i = 0; i < 7; i++)
            {
                UjBeosztasDatum_cb.Items.Add(eppElsoNap.AddDays(i).ToString("yyyy-MM-dd"));
            }
            UjBeosztasDatum_cb.SelectedIndex = 0;
        }

        private void BeosztasHetElore_btn_Click(object sender, RoutedEventArgs e)
        {
            DateTime elsonap = ElsoNap(eppElsoNap.AddDays(8));
            DateTime utolsonap = UtolsoNap(eppElsoNap.AddDays(8));

            eppElsoNap = elsonap;

            BeosztasDatumHet_label.Content = elsonap.ToString("yyyy-MM-dd") + " <---> " + utolsonap.ToString("yyyy-MM-dd");

            List<Beosztas> beosztasok = ab.Beosztasok(elsonap.ToString("yyyy-MM-dd"), utolsonap.ToString("yyyy-MM-dd"));

            if (beosztasok.Count != 0)
            {
                Beosztasok_lb.ItemsSource = beosztasok;
                Beosztasok_lb.SelectedIndex = 0;
            }
            else
            {
                Beosztasok_lb.ItemsSource = null;
                Beosztasok_lb.SelectedIndex = -1;
            }

            UjBeosztasDatum_cb.Items.Clear();
            for (int i = 0; i < 7; i++)
            {
                UjBeosztasDatum_cb.Items.Add(eppElsoNap.AddDays(i).ToString("yyyy-MM-dd"));
            }
            UjBeosztasDatum_cb.SelectedIndex = 0;
        }

        private DateTime ElsoNap(DateTime datum)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            int kulonbseg = datum.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;

            if (kulonbseg < 0)
            {
                kulonbseg += 7;
            }

            return datum.AddDays(-kulonbseg).Date;
        }

        public DateTime UtolsoNap(DateTime datum)
        {
            return ElsoNap(datum).AddDays(6);
        }

        //Fuvarok

        private void SzerkFuvarSikeresCsomi_cb_Click(object sender, RoutedEventArgs e)
        {
            if (SzerkFuvarSikeresCsomi_cb.IsChecked.Value)
            {
                SzerkFuvarSikeresCsomi_tb.IsEnabled = true;
                if (SzerkFuvar.Sikeres_csom == -1)
                {
                    SzerkFuvarSikeresCsomi_tb.Text = "";
                }
                else
                {
                    SzerkFuvarSikeresCsomi_tb.Text = SzerkFuvar.Sikeres_csom.ToString();
                }
            }
            else
            {
                if (SzerkFuvar.Sikeres_csom == -1)
                {
                    SzerkFuvarSikeresCsomi_tb.Text = "Nincs adat";
                    SzerkFuvarSikeresCsomi_tb.IsEnabled = false;
                }
                else
                {
                    SzerkFuvarSikeresCsomi_tb.Text = SzerkFuvar.Sikeres_csom.ToString();
                    SzerkFuvarSikeresCsomi_tb.IsEnabled = false;
                }
            }
        }

        private void SzerkFuvarBefejezes_cb_Click(object sender, RoutedEventArgs e)
        {
            if (SzerkFuvarBefejezes_cb.IsChecked.Value)
            {
                SzerkFuvarBefejezes_tb.IsEnabled = true;
                if (SzerkFuvar.Befejezes == "Nincs adat")
                {
                    SzerkFuvarBefejezes_tb.Text = "";
                }
                else
                {
                    SzerkFuvarBefejezes_tb.Text = SzerkFuvar.Befejezes;
                }
            }
            else
            {
                SzerkFuvarBefejezes_tb.Text = SzerkFuvar.Befejezes;
                SzerkFuvarBefejezes_tb.IsEnabled = false;
            }
        }

        private void FuvarokCsakAktivak_chb_Click(object sender, RoutedEventArgs e)
        {
            if (FuvarokCsakAktivak_chb.IsChecked.Value)
            {
                List<Fuvar> fuvarok = ab.Fuvarok(true);

                Fuvar_lb.ItemsSource = null;
                Fuvar_lb.SelectedIndex = -1;

                if (fuvarok.Count != 0)
                {
                    Fuvar_lb.ItemsSource = fuvarok;
                    Fuvar_lb.SelectedIndex = 0;
                    return;
                }
            }
            else
            {
                FuvarokOldalNormal();
            }
        }

        private void SzerkFuvarMentes_btn_Click(object sender, RoutedEventArgs e)
        {
            string datum = SzerkFuvarDatum_tb.Text;
            string kezdes = SzerkFuvarKezdes_tb.Text;
            string befejezes = SzerkFuvarBefejezes_tb.Text;
            string sikeres = SzerkFuvarSikeresCsomi_tb.Text;
            string felvett = SzerkFuvarFelvettCsomi_tb.Text;
            string felhasz = SzerkFuvarFelhasznalo_cb.SelectedItem.ToString();
            string jarmu = SzerkFuvarJarmuvek_cb.SelectedItem.ToString();
            string kerulet = SzerkFuvarKeruletek_cb.SelectedItem.ToString();

            string[] ujelemek = { datum, kezdes, befejezes, sikeres, felvett, felhasz, jarmu, kerulet };
            string[] regielemek = { SzerkFuvar.Datum.ToString("yyyy-MM-dd"), SzerkFuvar.Kezdes.ToString(), SzerkFuvar.Befejezes.ToString(),
                SzerkFuvar.Sikeres_csom.ToString(), SzerkFuvar.Felvett_csom.ToString(), SzerkFuvarFhJarmuKer[0],
            SzerkFuvarFhJarmuKer[1], SzerkFuvarFhJarmuKer[2]};

            if (!Ellenorzo.VanEvaltozas(regielemek, ujelemek))
            {
                FuvarokOldalNormal();
                return;
            }

            if (Ellenorzo.UresE(ujelemek))
            {
                MessageBox.Show("Nem maradhat üresen mező!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.MegfeleloDatum(datum))
            {
                MessageBox.Show("Nem megfelelő dátum formátum! Pl.: 2022-01-01", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Ellenorzo.RegiDatum(datum))
            {
                MessageBox.Show("A dátum nem lehet a mai dátumtól egy hétnél régebbi!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.MegfeleloIdo(kezdes))
            {
                MessageBox.Show("Nem megfelelő idő formátum! Pl.: 10:00", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.CsakSzam(felvett))
            {
                MessageBox.Show("A felvett csomagok mező csak számokat tartalmazhat!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SzerkFuvarBefejezes_cb.IsChecked.Value)
            {
                if (!Ellenorzo.MegfeleloIdo(befejezes))
                {
                    MessageBox.Show("Nem megfelelő idő formátum! Pl.: 10:00", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (SzerkFuvarSikeresCsomi_cb.IsChecked.Value != true)
                {
                    MessageBox.Show("Ha a befejezés mező be van jelölve a sikeres csomag mezőnek is bejelelölve kell lennie!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (SzerkFuvarSikeresCsomi_cb.IsChecked.Value)
            {
                if (!Ellenorzo.CsakSzam(sikeres))
                {
                    MessageBox.Show("A sikeres csomagok mező csak számokat tartalmazhat!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (SzerkFuvarBefejezes_cb.IsChecked.Value != true)
                {
                    MessageBox.Show("Ha a sikeres csomag mező be van jelölve a befejezés mezőnek is bejelelölve kell lennie!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Convert.ToInt32(sikeres) > Convert.ToInt32(felvett))
                {
                    MessageBox.Show("A sikeres csomagok száma nem lehet nagyobb a felvett csomagok számánál!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            int fid = Kisegito.KulcsKereso(FelhasznalokKevesAdat, SzerkFuvarFelhasznalo_cb.SelectedItem.ToString());
            int jarmuid = Kisegito.KulcsKereso(JarmuvekKevesAdat, SzerkFuvarJarmuvek_cb.SelectedItem.ToString());
            int kerid = Kisegito.KulcsKereso(Kisegito.Keruletek, SzerkFuvarKeruletek_cb.SelectedItem.ToString());

            bool eredmeny = ab.FuvarSzerk(SzerkFuvar.Fuvar_id, datum, kezdes, befejezes, sikeres, felvett, fid, jarmuid, kerid);

            FelugroAblak.Tag = kijelentkezesTag;

            if (!eredmeny)
            {
                MessageBox.Show("Sikertelen mentés!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                FuvarokOldalNormal();
                return;
            }

            MessageBox.Show("Sikeres mentés!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            FuvarokOldalNormal();
        }

        private void FuvarSzerk_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Fuvar_lb.SelectedIndex == -1)
            {
                MessageBox.Show("Nincs adat amit szerkeszteni tudnál!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                FuvarokOldalNormal();
                return;
            }

            SzerkFuvar = (Fuvar)Fuvar_lb.SelectedItem;

            FelugroAblak.Tag = szerkTag;

            SzerkFuvarDatum_tb.Text = SzerkFuvar.Datum.ToString("yyyy-MM-dd");
            SzerkFuvarKezdes_tb.Text = SzerkFuvar.Kezdes;
            SzerkFuvarBefejezes_tb.Text = SzerkFuvar.Befejezes;
            SzerkFuvarFelvettCsomi_tb.Text = SzerkFuvar.Felvett_csom.ToString();
            SzerkFuvarSikeresCsomi_tb.Text = SzerkFuvar.Sikeres_csom == -1 ? "Nincs adat." : SzerkFuvar.Sikeres_csom.ToString();

            KivalasztottFuvar_Label.Content = SzerkFuvar.Felhasznalo.Fnev + "; " + SzerkFuvar.Jarmu.Modell + " " + SzerkFuvar.Jarmu.Rendszam;

            UjFuvar_Grid.Visibility = Visibility.Collapsed;
            Fuvar_lb.Visibility = Visibility.Collapsed;
            FuvarCsakAktivakLabel.Visibility = Visibility.Collapsed;
            FuvarokCsakAktivak_chb.Visibility = Visibility.Collapsed;
            SzerkFuvar_Grid.Visibility = Visibility.Visible;
            FuvarVissza_btn.Visibility = Visibility.Visible;


            FelhasznalokKevesAdat = ab.FelhasznalokKevesAdat();
            if (FelhasznalokKevesAdat.Count != 0)
            {
                string kereses = SzerkFuvar.Felhasznalo.Fnev + " - " + Kisegito.Munkakorok[SzerkFuvar.Felhasznalo.Munkakorid];
                SzerkFuvarFelhasznalo_cb.ItemsSource = FelhasznalokKevesAdat.Values;
                SzerkFuvarFelhasznalo_cb.SelectedIndex = Kisegito.ErtekIndexKereso(FelhasznalokKevesAdat.Values, kereses);
                SzerkFuvarFhJarmuKer[0] = SzerkFuvarFelhasznalo_cb.SelectedItem.ToString();
            }
            else
            {
                SzerkFuvarFelhasznalo_cb.ItemsSource = nincsAdat;
                SzerkFuvarFelhasznalo_cb.SelectedIndex = 0;
                SzerkFuvarFhJarmuKer[0] = SzerkFuvarFelhasznalo_cb.SelectedItem.ToString();
            }

            JarmuvekKevesAdat = ab.JarmuvekKevesAdat();
            if (JarmuvekKevesAdat.Count != 0)
            {
                string kereses = SzerkFuvar.Jarmu.Gyarto + " " + SzerkFuvar.Jarmu.Modell + " " + SzerkFuvar.Jarmu.Rendszam;
                SzerkFuvarJarmuvek_cb.ItemsSource = JarmuvekKevesAdat.Values;
                SzerkFuvarJarmuvek_cb.SelectedIndex = Kisegito.ErtekIndexKereso(JarmuvekKevesAdat.Values, kereses);
                SzerkFuvarFhJarmuKer[1] = SzerkFuvarJarmuvek_cb.SelectedItem.ToString();
            }
            else
            {
                SzerkFuvarJarmuvek_cb.ItemsSource = nincsAdat;
                SzerkFuvarJarmuvek_cb.SelectedIndex = 0;
                SzerkFuvarFhJarmuKer[1] = SzerkFuvarJarmuvek_cb.SelectedItem.ToString();
            }

            SzerkFuvarKeruletek_cb.ItemsSource = Kisegito.Keruletek.Values;
            SzerkFuvarKeruletek_cb.SelectedIndex = Kisegito.ErtekIndexKereso(Kisegito.Keruletek.Values, Kisegito.Keruletek[SzerkFuvar.Kerid]);
            SzerkFuvarFhJarmuKer[2] = SzerkFuvarKeruletek_cb.SelectedItem.ToString();
        }

        private void UjFuvarMentes_btn_Click(object sender, RoutedEventArgs e)
        {
            string datum = UjFuvarDatum_tb.Text;
            string kezdes = UjFuvarKezdes_tb.Text;
            string felvettcsomi = UjFuvarFelvettCsomi_tb.Text;
            string befejezes = "";
            string sikeres = "";

            string[] elemek = { datum, kezdes, felvettcsomi };

            if (UjFuvarBefejezes_cb.IsChecked.Value)
            {
                befejezes = UjFuvarBefejezes_tb.Text;
                if (String.IsNullOrWhiteSpace(befejezes))
                {
                    MessageBox.Show("Nem maradhat üresen egy mező sem!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!Ellenorzo.MegfeleloIdo(befejezes))
                {
                    MessageBox.Show("Nem megfelelő idő formátum! Pl.: 11:11", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (UjFuvarSikeresCsomi_cb.IsChecked.Value != true)
                {
                    MessageBox.Show("Ha a befejezés mező be van jelölve a sikeres csomagok mezőnek is bejelölve kell lennie!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (UjFuvarSikeresCsomi_cb.IsChecked.Value)
            {
                sikeres = UjFuvarSikeresCsomi_tb.Text;
                if (String.IsNullOrWhiteSpace(sikeres))
                {
                    MessageBox.Show("Nem maradhat üresen egy mező sem!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!Ellenorzo.CsakSzam(sikeres))
                {
                    MessageBox.Show("A sikeres csomagok mezőben csak számok szerepelhetnek!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (UjFuvarBefejezes_cb.IsChecked.Value != true)
                {
                    MessageBox.Show("Ha a sikeres csomagok mező be van jelölve a befejezés mezőnek is bejelölve kell lennie!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Convert.ToInt32(sikeres) > Convert.ToInt32(felvettcsomi))
                {
                    MessageBox.Show("A sikeres csomagok száma nem lehet nagyobb a felvett csomagok számánál!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (Ellenorzo.UresE(elemek))
            {
                MessageBox.Show("Nem maradhat üresen egy mező sem!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.MegfeleloDatum(datum))
            {
                MessageBox.Show("Nem megfelelő dátum formátum! Pl.: 2022-01-01", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Ellenorzo.RegiDatum(datum))
            {
                MessageBox.Show("A dátum nem lehet a mai dátumtól egy hétnél régebbi!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.MegfeleloIdo(kezdes))
            {
                MessageBox.Show("Nem megfelelő idő formátum! Pl.: 11:11", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.CsakSzam(felvettcsomi))
            {
                MessageBox.Show("A felvett csomag mező csak számból állhat!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (UjFuvarFelhasznalo_cb.SelectedItem.ToString() == nincsAdat[0])
            {
                MessageBox.Show("Nincs felhasználó kiválasztva! Előbb vegyél fel felhasználót!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (UjFuvarJarmuvek_cb.SelectedItem.ToString() == nincsAdat[0])
            {
                MessageBox.Show("Nincs jármű kiválasztva! Előbb vegyél fel járművet!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (felvettcsomi[0] == '0')
            {
                MessageBox.Show("A felvett csomag száma nem kezdődht 0-val!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int fid = Kisegito.KulcsKereso(FelhasznalokKevesAdat, UjFuvarFelhasznalo_cb.SelectedItem.ToString());
            int jarmuid = Kisegito.KulcsKereso(JarmuvekKevesAdat, UjFuvarJarmuvek_cb.SelectedItem.ToString());
            int kerid = Kisegito.KulcsKereso(Kisegito.Keruletek, UjFuvarKeruletek_cb.SelectedItem.ToString());


            bool eredmeny = ab.UjFuvarFelvetele(fid, jarmuid, kerid, datum, kezdes, felvettcsomi, sikeres, befejezes);

            if (!eredmeny)
            {
                MessageBox.Show("Valami hiba történt!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                FuvarokOldalNormal();
                return;
            }

            MessageBox.Show("Sikeres mentés!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            FuvarokOldalNormal();
        }

        private void UjFuvarBefejezes_cb_Click(object sender, RoutedEventArgs e)
        {
            if (UjFuvarBefejezes_cb.IsChecked.Value)
            {
                UjFuvarBefejezes_tb.IsEnabled = true;
            }
            else
            {
                UjFuvarBefejezes_tb.Text = "";
                UjFuvarBefejezes_tb.IsEnabled = false;
            }
        }

        private void UjFuvarSikeresCsomi_cb_Click(object sender, RoutedEventArgs e)
        {
            if (UjFuvarSikeresCsomi_cb.IsChecked.Value)
            {
                UjFuvarSikeresCsomi_tb.IsEnabled = true;
            }
            else
            {
                UjFuvarSikeresCsomi_tb.Text = "";
                UjFuvarSikeresCsomi_tb.IsEnabled = false;
            }
        }

        private void UjFuvar_btn_Click(object sender, RoutedEventArgs e)
        {
            Fuvar_lb.Visibility = Visibility.Collapsed;
            SzerkFuvar_Grid.Visibility = Visibility.Collapsed;
            FuvarCsakAktivakLabel.Visibility = Visibility.Collapsed;
            FuvarokCsakAktivak_chb.Visibility = Visibility.Collapsed;

            UjFuvarDatum_tb.Text = "";
            UjFuvarKezdes_tb.Text = "";
            UjFuvarBefejezes_tb.Text = "";
            UjFuvarFelvettCsomi_tb.Text = "";
            UjFuvarSikeresCsomi_tb.Text = "";

            UjFuvar_Grid.Visibility = Visibility.Visible;
            FuvarVissza_btn.Visibility = Visibility.Visible;

            FelhasznalokKevesAdat = ab.FelhasznalokKevesAdat();
            if (FelhasznalokKevesAdat.Count != 0)
            {
                UjFuvarFelhasznalo_cb.ItemsSource = FelhasznalokKevesAdat.Values;
                UjFuvarFelhasznalo_cb.SelectedIndex = 0;
            }
            else
            {
                UjFuvarFelhasznalo_cb.ItemsSource = nincsAdat;
                UjFuvarFelhasznalo_cb.SelectedIndex = 0;
            }

            JarmuvekKevesAdat = ab.JarmuvekKevesAdat();
            if (JarmuvekKevesAdat.Count != 0)
            {
                UjFuvarJarmuvek_cb.ItemsSource = JarmuvekKevesAdat.Values;
                UjFuvarJarmuvek_cb.SelectedIndex = 0;
            }
            else
            {
                UjFuvarJarmuvek_cb.ItemsSource = nincsAdat;
                UjFuvarJarmuvek_cb.SelectedIndex = 0;
            }

            UjFuvarKeruletek_cb.ItemsSource = Kisegito.Keruletek.Values;
            UjFuvarKeruletek_cb.SelectedIndex = 0;
        }

        private void FuvarVissza_btn_Click(object sender, RoutedEventArgs e)
        {
            FuvarokOldalNormal();
        }

        private void FuvarokOldalNormal()
        {
            FuvarVissza_btn.Visibility = Visibility.Collapsed;
            UjFuvar_Grid.Visibility = Visibility.Collapsed;
            SzerkFuvar_Grid.Visibility = Visibility.Collapsed;

            FuvarCsakAktivakLabel.Visibility = Visibility.Visible;
            FuvarokCsakAktivak_chb.Visibility = Visibility.Visible;
            Fuvar_lb.Visibility = Visibility.Visible;

            List<Fuvar> fuvarok = ab.Fuvarok(false);

            Fuvar_lb.ItemsSource = null;
            Fuvar_lb.SelectedIndex = -1;
            FelugroAblak.Tag = kijelentkezesTag;

            if (fuvarok.Count != 0)
            {
                Fuvar_lb.ItemsSource = fuvarok;
                Fuvar_lb.SelectedIndex = 0;
                return;
            }
        }

        //Szervizeles

        private void SzerkSzervizelesMeddig_cb_Click(object sender, RoutedEventArgs e)
        {
            if (SzerkSzervizelesMeddig_cb.IsChecked.Value)
            {
                SzerkSzervizelesMeddig_tb.IsEnabled = true;
                SzerkSzervizelesMeddig_tb.Text = "";
            }
            else
            {
                SzerkSzervizelesMeddig_tb.IsEnabled = false;
                if (SzerkSzervizeles.Meddig.ToString("yyyy-MM-dd") == "0001-01-01")
                {
                    SzerkSzervizelesMeddig_tb.Text = "Nincs adat";
                }
                else
                {
                    SzerkSzervizelesMeddig_tb.Text = SzerkSzervizeles.Meddig.ToString("yyyy-MM-dd");
                }
            }
        }

        private void SzervizelesCsakAktivak_chb_Click(object sender, RoutedEventArgs e)
        {
            if (SzervizelesCsakAktivak_chb.IsChecked.Value)
            {
                List<Szervizeles> szervizelesek = ab.Szervizelesek(true);

                Szervizeles_lb.ItemsSource = null;
                Szervizeles_lb.SelectedIndex = -1;

                if (szervizelesek.Count != 0)
                {
                    Szervizeles_lb.ItemsSource = szervizelesek;
                    Szervizeles_lb.SelectedIndex = 0;
                    return;
                }
            }
            else
            {
                SzervizelesOldalNormal();
            }
        }

        private void SzerkSzervizelesMentes_btn_Click(object sender, RoutedEventArgs e)
        {
            string mettol = SzerkSzervizelesMettol_tb.Text;
            string indok = SzerkSzervizelesIndok_tb.Text;
            string meddig = SzerkSzervizelesMeddig_tb.Text;

            string[] ujElemek = { mettol, meddig, indok };
            string[] regiElemek = { SzerkSzervizeles.Mettol.ToString("yyyy-MM-dd"), SzerkSzervizeles.Meddig.ToString("yyyy-MM-dd"), SzerkSzervizeles.Indok };

            if (!Ellenorzo.VanEvaltozas(regiElemek, ujElemek))
            {
                SzervizelesOldalNormal();
                return;
            }

            if (Ellenorzo.UresE(ujElemek))
            {
                MessageBox.Show("Nem maradhat üresen mező!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.MegfeleloDatum(mettol))
            {
                MessageBox.Show("Nem megfelelő dátum formátum! Pl.: 2000-01-01", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (SzerkSzervizelesMeddig_cb.IsChecked.Value)
            {
                if (!Ellenorzo.MegfeleloDatum(meddig))
                {
                    MessageBox.Show("Nem megfelelő dátum formátum! Pl.: 2000-01-01", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (Ellenorzo.RegiDatum(mettol))
            {
                MessageBox.Show("A dátum nem lehet a mai dátumtól egy hétnél régebbi!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (SzerkSzervizeles.Mettol == Convert.ToDateTime(mettol) && SzerkSzervizeles.Meddig == Convert.ToDateTime(meddig) && SzerkSzervizeles.Indok == indok)
                {
                    SzervizelesOldalNormal();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Valami hiba történt!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                SzervizelesOldalNormal();
                Console.WriteLine(ex.Message);
                return;
            }

            bool eredmeny = ab.SzervizelesSzerk(SzerkSzervizeles.Szerid, mettol, meddig, indok);

            if (!eredmeny)
            {
                MessageBox.Show("A szervízelés mentése sikertelen volt!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                SzervizelesOldalNormal();
                return;
            }

            MessageBox.Show("A szervízelés mentése sikeres volt!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            SzervizelesOldalNormal();
        }

        private void SzervizelesVissza_btn_Click(object sender, RoutedEventArgs e)
        {
            SzervizelesOldalNormal();
        }

        private void SzervizelesSzerk_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Szervizeles_lb.SelectedIndex == -1)
            {
                MessageBox.Show("Nincs adat amit szerkeszteni tudnál!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                SzervizelesOldalNormal();
                return;
            }

            Szervizeles_lb.Visibility = Visibility.Collapsed;
            SzerkSzervizeles_Grid.Visibility = Visibility.Visible;
            SzervizelesVissza_btn.Visibility = Visibility.Visible;

            Szervizeles sz = (Szervizeles)Szervizeles_lb.SelectedItem;

            FelugroAblak.Tag = szerkTag;

            KivalasztottSzervizeles_Label.Content = sz.Jarmu.Gyarto + " " + sz.Jarmu.Modell + " " + sz.Jarmu.Rendszam + "; " + sz.Szerviz.Sznev;

            SzerkSzervizelesMettol_tb.Text = sz.Mettol.ToString("yyyy-MM-dd");
            if (sz.Meddig.ToString("yyyy-MM-dd") == "0001-01-01")
            {
                SzerkSzervizelesMeddig_tb.Text = "Nincs adat";
            }
            else
            {
                SzerkSzervizelesMeddig_tb.Text = sz.Meddig.ToString("yyyy-MM-dd");
            }
            SzerkSzervizelesIndok_tb.Text = sz.Indok;

            SzerkSzervizeles = sz;
        }

        private void SzervizelesOldalNormal()
        {
            SzervizelesSzerk_btn.Visibility = Visibility.Visible;
            Szervizeles_lb.Visibility = Visibility.Visible;

            SzervizelesVissza_btn.Visibility = Visibility.Collapsed;
            SzerkSzervizeles_Grid.Visibility = Visibility.Collapsed;

            List<Szervizeles> szervizelesek = ab.Szervizelesek(false);

            Szervizeles_lb.ItemsSource = null;
            Szervizeles_lb.SelectedIndex = -1;
            FelugroAblak.Tag = kijelentkezesTag;

            if (szervizelesek.Count != 0)
            {
                Szervizeles_lb.ItemsSource = szervizelesek;
                Szervizeles_lb.SelectedIndex = 0;
                return;
            }
        }


        //Felhasznalok

        private void FelhasznaloJelszoModosit_btn_Click_1(object sender, RoutedEventArgs e)
        {
            if (Felhasznalok_lb.SelectedIndex == -1)
            {
                MessageBox.Show("Nincs adat amit szerkeszteni tudnál.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Felhasznalo fh = (Felhasznalo)Felhasznalok_lb.SelectedItem;
            FelhasznaloJelszoKiut fhjk = new FelhasznaloJelszoKiut(fh, ab, this);
            fhjk.Show();
        }

        private void FelhasznaloSzabiraKuld_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Felhasznalok_lb.SelectedIndex == -1)
            {
                MessageBox.Show("Nincs felhasználó akit szabadságra tudnál küldeni!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Felhasznalo fh = (Felhasznalo)Felhasznalok_lb.SelectedItem;
            FelhasznaloSzabiraKuld sz = new FelhasznaloSzabiraKuld(ab, fh, this);
            sz.Show();
        }

        public void FelhasznalokFrissit()
        {
            FelhasznalokOldalNormal();
        }

        private void FelhasznaloTorles_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Felhasznalok_lb.SelectedIndex == -1)
            {
                MessageBox.Show("Nincs adat amit szerkeszteni tudnál!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                FelhasznalokOldalNormal();
                return;
            }

            SzerkFelhasznalo = (Felhasznalo)Felhasznalok_lb.SelectedItem;

            if (Kisegito.Jogosultsagok[SzerkFelhasznalo.Jogid] == "Admin")
            {
                MessageBox.Show("Admin profil-t nem lehet törölni!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                FelhasznalokOldalNormal();
                return;
            }

            MessageBoxResult eredmeny = MessageBox.Show("Biztos szeretnéd törölni a következő felhasználói profilt? " +
                "\n- " + SzerkFelhasznalo.Fnev + " -",
                "Biztos vagy benne?", MessageBoxButton.YesNo, MessageBoxImage.Stop, MessageBoxResult.No);
            if (eredmeny == MessageBoxResult.Yes)
            {
                MessageBoxResult eredmeny2 = MessageBox.Show("A profil törléséhez még egy megerősítés szükséges. " +
                    "Biztosan törlöd ezt a profilt? \n- " + SzerkFelhasznalo.Fnev + " -",
                    "Biztos?",
                    MessageBoxButton.YesNo, MessageBoxImage.Stop, MessageBoxResult.No);
                if (eredmeny2 == MessageBoxResult.Yes)
                {
                    ab.InaktivaTesz(SzerkFelhasznalo.Fid);
                    MessageBox.Show("A profil törlése sikeres volt.",
                         "Törölve!",
                         MessageBoxButton.OK, MessageBoxImage.Information);

                    FelhasznalokOldalNormal();
                }
            }
        }

        private void SzerkFhMentes_btn_Click(object sender, RoutedEventArgs e)
        {
            string munkakor = F_SzerkMunkakorok_cb.SelectedItem.ToString();
            string jogosultsag = F_SzerkJogosultsagok_cb1.SelectedItem.ToString();
            string fnev = FSzerkfnev_tb1.Text;
            string email = FSzerkemail_tb1.Text;
            string teljesnev = FSzerkteljesnev_tb1.Text;
            string telszam = FSzerktelefonszam_tb1.Text;
            string nem = FSzerknem_tb1.Text;
            string kivehetoszabi = FSzerkkivehetoSzabi_tb1.Text;
            string irsz = FSzerkirsz_tb.Text;
            string telepules = FSzerktelepules_tb.Text;
            string cim = FSzerkcim_tb.Text;
            string adoazon = FSzerkadoazon_tb1.Text;
            string bankszamla = FSzerkbankszamla_tb1.Text;

            string szemelyi = FSzerkszemelyi_tb1.Text;
            string tajszam = FSzerktajszam_tb1.Text;
            string alapber = FSzerkalapber_tb1.Text;
            string szulnap = FSzerkszulnap_tb1.Text;

            string[] ujElemek = { munkakor, jogosultsag, fnev, email, teljesnev, telszam, nem, kivehetoszabi, irsz,
                telepules, cim, adoazon, bankszamla, szemelyi, tajszam, alapber};

            string[] regiElemek = { Kisegito.Munkakorok[SzerkFelhasznalo.Munkakorid], Kisegito.Jogosultsagok[SzerkFelhasznalo.Jogid],
                SzerkFelhasznalo.Fnev, SzerkFelhasznalo.Email, SzerkFelhasznalo.Teljesnev, SzerkFelhasznalo.Telefonszam,
            SzerkFelhasznalo.Nem, SzerkFelhasznalo.Kivehetoszabi.ToString(), SzerkFelhasznalo.Lakcim.Split(',')[0],
                SzerkFelhasznalo.Lakcim.Split(',')[1],SzerkFelhasznalo.Lakcim.Split(',')[2],SzerkFelhasznalo.Adoazon,
            SzerkFelhasznalo.Bankszamlaszam, SzerkFelhasznalo.Szemelyi, SzerkFelhasznalo.Tajszam, SzerkFelhasznalo.Alapber.ToString()};

            if (!Ellenorzo.VanEvaltozas(regiElemek, ujElemek))
            {
                FelhasznalokOldalNormal();
                return;
            }

            if (Ellenorzo.UresE(ujElemek))
            {
                MessageBox.Show("Minden mezőt tölts ki!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<string> ellenorzes = UjFelhasznalo_AdatokEllenorzese(szemelyi, email, szulnap, telszam, irsz, adoazon, bankszamla, tajszam, alapber);

            if (ellenorzes.Count != 0)
            {
                foreach (string item in ellenorzes)
                {
                    MessageBox.Show(item, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return;
            }

            if (alapber[0] == '0')
            {
                MessageBox.Show("Az alapbér nem kezdődhet 0-val!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Convert.ToInt32(kivehetoszabi) < 20 || Convert.ToInt32(kivehetoszabi) > 70)
            {
                MessageBox.Show("A kivehető szabadságnak 20-nál többnek kell lennie!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int jogid = Kisegito.KulcsKereso(Kisegito.Jogosultsagok, F_SzerkJogosultsagok_cb1.SelectedItem.ToString());
            int munkakorid = Kisegito.KulcsKereso(Kisegito.Munkakorok, F_SzerkMunkakorok_cb.SelectedItem.ToString());
            string lakcim = irsz + ", " + telepules + "," + cim;

            bool eredmeny = ab.FelhasznaloSzerk(SzerkFelhasznalo.Fid, munkakorid, jogid, fnev, email, teljesnev, telszam, nem,
                kivehetoszabi, lakcim, adoazon, bankszamla.Replace("-", ""), szemelyi, tajszam, alapber, szulnap);

            if (!eredmeny)
            {
                MessageBox.Show("Az felhasználó mentése az adatbázisba sikertelen volt!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                FelhasznalokOldalNormal();
                return;
            }

            MessageBox.Show("Sikeres mentés!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            FelhasznalokOldalNormal();
        }

        private void FelhasznaloSzerk_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Felhasznalok_lb.SelectedIndex == -1)
            {
                MessageBox.Show("Nincs adat amit szerkeszteni tudnál!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                FelhasznalokOldalNormal();
                return;
            }

            FelugroAblak.Tag = szerkTag;

            FelhasznaloVissza_btn.Visibility = Visibility.Visible;
            FelhasznaloSzabiraKuld_btn.Visibility = Visibility.Collapsed;
            FelhasznaloJelszoModosit_btn.Visibility = Visibility.Collapsed;

            SzerkFelhasznalo = (Felhasznalo)Felhasznalok_lb.SelectedItem;

            FSzerkfnev_tb1.Text = SzerkFelhasznalo.Fnev;
            FSzerkemail_tb1.Text = SzerkFelhasznalo.Email;
            FSzerkteljesnev_tb1.Text = SzerkFelhasznalo.Teljesnev;
            FSzerkszulnap_tb1.Text = SzerkFelhasznalo.Szuletesnap.ToString("yyyy-MM-dd");
            FSzerktelefonszam_tb1.Text = SzerkFelhasznalo.Telefonszam;
            FSzerknem_tb1.Text = SzerkFelhasznalo.Nem;
            FSzerkkivehetoSzabi_tb1.Text = SzerkFelhasznalo.Kivehetoszabi.ToString();
            FSzerkirsz_tb.Text = SzerkFelhasznalo.Lakcim.Split(',')[0];
            FSzerktelepules_tb.Text = SzerkFelhasznalo.Lakcim.Split(',')[1];
            FSzerkcim_tb.Text = SzerkFelhasznalo.Lakcim.Split(',')[2];
            FSzerkadoazon_tb1.Text = SzerkFelhasznalo.Adoazon;
            if (SzerkFelhasznalo.Bankszamlaszam == "Nincs adat")
            {
                FSzerkbankszamla_tb1.Text = SzerkFelhasznalo.Bankszamlaszam;
            }
            else
            {
                string elso = "";
                string masodik = "";
                string harmadik = "";
                for (int i = 0; i < SzerkFelhasznalo.Bankszamlaszam.Length; i++)
                {
                    if (i < 8)
                    {
                        elso += SzerkFelhasznalo.Bankszamlaszam[i];
                    }
                    else if (i > 7 && i < 16)
                    {
                        masodik += SzerkFelhasznalo.Bankszamlaszam[i];
                    }
                    else if (i > 15 && i < 24)
                    {
                        harmadik += SzerkFelhasznalo.Bankszamlaszam[i];
                    }
                }
                FSzerkbankszamla_tb1.Text = elso + "-" + masodik + "-" + harmadik;
            }
            FSzerkszemelyi_tb1.Text = SzerkFelhasznalo.Szemelyi;
            FSzerktajszam_tb1.Text = SzerkFelhasznalo.Tajszam;
            FSzerkalapber_tb1.Text = SzerkFelhasznalo.Alapber.ToString();

            MunkakorJogFrissit(true);

            KivalasztottFh_Label.Content = SzerkFelhasznalo.Fnev;

            Felhasznalok_lb.Visibility = Visibility.Collapsed;
            UjFelhasznalo_Grid.Visibility = Visibility.Collapsed;
            SzerkFelhasznalo_Grid.Visibility = Visibility.Visible;
        }

        private void Fjelszo_Gen_btn_Click(object sender, RoutedEventArgs e)
        {
            string jelszo = Membership.GeneratePassword(8, 1);
            Fjelszo_tb.Text = jelszo;
        }

        private void UjFelhasznaloMentes_btn_Click(object sender, RoutedEventArgs e)
        {
            string munkakor = Munkakorok_cb.SelectedItem.ToString();
            string jogosultsag = Jogosultsagok_cb.SelectedItem.ToString();
            string fnev = Ffnev_tb.Text;
            string email = Femail_tb.Text;
            string teljesnev = Fteljesnev_tb.Text;
            string telszam = Ftelefonszam_tb.Text;
            string nem = Fnem_tb.Text;
            string kivehetoszabi = FkivehetoSzabi_tb.Text;
            string irsz = Firsz_tb.Text;
            string telepules = Ftelepules_tb.Text;
            string cim = Fcim_tb.Text;
            string adoazon = Fadoazon_tb.Text;
            string bankszamla = Fbankszamla_tb.Text;
            string szemelyi = Fszemelyi_tb.Text;
            string tajszam = Ftajszam_tb.Text;
            string alapber = Falapber_tb.Text;
            string szulnap = Fszulnap_tb.Text;
            DateTime csatlakozas = DateTime.Now;

            string[] elemek = { munkakor, jogosultsag, fnev, email, teljesnev, telszam, nem, kivehetoszabi, irsz,
                telepules, cim, adoazon, bankszamla, szemelyi, tajszam, alapber, Fjelszo_tb.Text};
            if (Ellenorzo.UresE(elemek))
            {
                MessageBox.Show("Minden mezőt tölts ki!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<string> ellenorzes = UjFelhasznalo_AdatokEllenorzese(szemelyi, email, szulnap, telszam, irsz, adoazon, bankszamla, tajszam, alapber);

            if (ellenorzes.Count != 0)
            {
                foreach (string item in ellenorzes)
                {
                    MessageBox.Show(item, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return;
            }

            if (alapber[0] == '0')
            {
                MessageBox.Show("Az alapbér nem kezdődhet 0-val!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int jogid = Kisegito.KulcsKereso(Kisegito.Jogosultsagok, Jogosultsagok_cb.SelectedItem.ToString());
            int munkakorid = Kisegito.KulcsKereso(Kisegito.Munkakorok, Munkakorok_cb.SelectedItem.ToString());

            string jelszo = Fjelszo_tb.Text;

            string lakcim = irsz + ", " + telepules + ", " + cim;

            bool eredmeny = ab.UjFelhasznalo(munkakorid, jogid, fnev, jelszo, email, teljesnev, telszam, nem,
                kivehetoszabi, lakcim, adoazon, bankszamla.Replace("-", ""), szemelyi, tajszam, alapber, szulnap, csatlakozas);

            if (!eredmeny)
            {
                MessageBox.Show("Az új felhasználó felvétele az adatbázisba sikertelen volt!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                FelhasznalokOldalNormal();
                return;
            }

            MessageBox.Show("Sikeres feltöltés!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            MessageBox.Show("A felvett felhasználó jelszava: " + jelszo, "Információ!", MessageBoxButton.OK, MessageBoxImage.Information);
            FelhasznalokOldalNormal();
        }

        private List<string> UjFelhasznalo_AdatokEllenorzese(string szemelyi, string email, string szuldatum, string telszam, string irsz, string adoszam, string bankszamlaszam, string tajszam, string alapber)
        {
            List<string> eredmenyek = new List<string>();

            if (!Ellenorzo.MegfeleloSzemelyi(szemelyi))
            {
                eredmenyek.Add("Nem megfelelő személyigazolvány szám formátum! Pl.: 123456XX");
            }

            if (!Ellenorzo.EmailCheck(email))
            {
                eredmenyek.Add("Nem megfelelő email formátum! Pl.: mydrivers@gmail.com");
            }

            if (!Ellenorzo.MegfeleloDatum(szuldatum))
            {
                eredmenyek.Add("Nem megfelelő dátum formátum! Pl.: 2022-01-01");
            }

            if (!Ellenorzo.MegfeleloTelszam(telszam))
            {
                eredmenyek.Add("Nem megfelelő telefonszám formátum! Pl.: 06301234567");
            }

            if (!Ellenorzo.CsakSzam(irsz))
            {
                eredmenyek.Add("Nem megfelelő irányítószám formátum! Az irányítószám csak számokat tartalmazhat!");
            }

            if (!Ellenorzo.CsakSzam(adoszam))
            {
                eredmenyek.Add("Nem megfelelő adóazonosító jel formátum! Az adóazonosító jel csak számokat tartalmazhat!");
            }

            if (bankszamlaszam.Replace("-", "").Length > 24)
            {
                eredmenyek.Add("A bankszámlaszám túl hosszú." +
                            " Maximum 24 db számból és két kötőjelből állhat!" +
                            " Jelenlegi szám mennyiség: " + bankszamlaszam.Replace("-", "").Length + "db");
            }

            if (bankszamlaszam.Replace("-", "").Length < 24)
            {
                eredmenyek.Add("A bankszámlaszám túl rövid." +
                            " Minimum 24 db számból és két kötőjelből állhat!" +
                            " Jelenlegi szám mennyiség: " + bankszamlaszam.Replace("-", "").Length + "db");
            }

            if (!Ellenorzo.CsakSzam(bankszamlaszam.Replace("-", "")))
            {
                eredmenyek.Add("Nem megfelelő bankszámlaszám formátum! A bankszámlaszám csak számokat tartalmazhat!");
            }

            if (!Ellenorzo.CsakSzam(tajszam))
            {
                eredmenyek.Add("Nem megfelelő tajszám formátum! A tajszám csak számokat tartalmazhat!");
            }

            if (!Ellenorzo.CsakSzam(alapber))
            {
                eredmenyek.Add("Nem megfelelő alap bér formátum! Az alap bér mező csak számokat tartalmazhat!");
            }

            return eredmenyek;
        }

        private void UjMunkakor_btn_Click(object sender, RoutedEventArgs e)
        {
            UjMunkakorAblak ujmkablak = new UjMunkakorAblak(ab, this);
            ujmkablak.Show();
            Hide();
        }

        private void UjFelhasznalo_btn_Click(object sender, RoutedEventArgs e)
        {
            UjFelhasznalo_Grid.Visibility = Visibility.Visible;
            Felhasznalok_lb.Visibility = Visibility.Collapsed;
            SzerkFelhasznalo_Grid.Visibility = Visibility.Collapsed;
            FelhasznaloSzabiraKuld_btn.Visibility = Visibility.Collapsed;
            FelhasznaloJelszoModosit_btn.Visibility = Visibility.Collapsed;
            MunkakorJogFrissit(false);
            FelhasznaloVissza_btn.Visibility = Visibility.Visible;

            Ffnev_tb.Text = "";
            Fjelszo_tb.Text = "";
            Femail_tb.Text = "";
            Fteljesnev_tb.Text = "";
            Fszulnap_tb.Text = "";
            Ftelefonszam_tb.Text = "";
            Fnem_tb.Text = "";
            FkivehetoSzabi_tb.Text = "";
            Firsz_tb.Text = "";
            Ftelepules_tb.Text = "";
            Fcim_tb.Text = "";
            Fadoazon_tb.Text = "";
            Fbankszamla_tb.Text = "";
            Fszemelyi_tb.Text = "";
            Ftajszam_tb.Text = "";
            Falapber_tb.Text = "";
        }

        public void MunkakorJogFrissit(bool szerkesztes)
        {
            if (!szerkesztes)
            {
                Kisegito.Munkakorok = ab.Munkakorok();
                Kisegito.Jogosultsagok = ab.Jogosultsagok();

                if (Kisegito.Munkakorok.Where(x => x.Value != "Admin").Count() == 0)
                {
                    Munkakorok_cb.ItemsSource = nincsAdat;
                    Munkakorok_cb.SelectedIndex = 0;
                }
                else
                {
                    Munkakorok_cb.ItemsSource = Kisegito.Munkakorok.Where(x => x.Value != "Admin").Select(x => x.Value);
                    Munkakorok_cb.SelectedIndex = 0;
                }
                Jogosultsagok_cb.ItemsSource = Kisegito.Jogosultsagok.Where(x => x.Value != "Admin").Select(x => x.Value);
                Jogosultsagok_cb.SelectedIndex = 0;
            }
            else
            {
                if (Kisegito.Munkakorok.Where(x => x.Value != "Admin").Count() == 0)
                {
                    F_SzerkMunkakorok_cb.ItemsSource = nincsAdat;
                    F_SzerkMunkakorok_cb.SelectedIndex = 0;
                }
                else
                {
                    F_SzerkMunkakorok_cb.ItemsSource = Kisegito.Munkakorok.Where(x => x.Value != "Admin").Select(x => x.Value);
                    F_SzerkMunkakorok_cb.SelectedIndex = 0;
                }

                F_SzerkJogosultsagok_cb1.ItemsSource = Kisegito.Jogosultsagok.Where(x => x.Value != "Admin").Select(x => x.Value);
                F_SzerkJogosultsagok_cb1.SelectedIndex = 0;
            }
        }

        private void FelhasznaloVissza_btn_Click(object sender, RoutedEventArgs e)
        {
            FelhasznalokOldalNormal();
        }

        private void FelhasznalokOldalNormal()
        {
            UjFelhasznalo_btn.Visibility = Visibility.Visible;
            FelhasznaloSzerk_btn.Visibility = Visibility.Visible;
            FelhasznaloTorles_btn.Visibility = Visibility.Visible;
            Felhasznalok_lb.Visibility = Visibility.Visible;
            FelhasznaloSzabiraKuld_btn.Visibility = Visibility.Visible;
            if (Kisegito.Jogosultsagok[felhasznalo.Jogid] == "Admin")
            {
                FelhasznaloJelszoModosit_btn.Visibility = Visibility.Visible;
            }

            FelhasznaloVissza_btn.Visibility = Visibility.Collapsed;
            UjFelhasznalo_Grid.Visibility = Visibility.Collapsed;
            SzerkFelhasznalo_Grid.Visibility = Visibility.Collapsed;

            List<Felhasznalo> felhasznalok = ab.Felhasznalok(false);

            Felhasznalok_lb.ItemsSource = null;
            Felhasznalok_lb.SelectedIndex = -1;
            FelugroAblak.Tag = kijelentkezesTag;

            if (felhasznalok.Count != 0)
            {
                Felhasznalok_lb.ItemsSource = felhasznalok;
                Felhasznalok_lb.SelectedIndex = 0;
                return;
            }
        }

        //Járművek

        private void JarmuSzervizeles_Meddig_cb_Click(object sender, RoutedEventArgs e)
        {
            if (JarmuSzervizeles_Meddig_cb.IsChecked.Value)
            {
                JarmuSzervizeles_Meddig_tb.IsEnabled = true;
            }
            else
            {
                JarmuSzervizeles_Meddig_tb.IsEnabled = false;
                JarmuSzervizeles_Meddig_tb.Text = "";
            }
        }

        private void JarmuSzervizelesMentes_btn_Click(object sender, RoutedEventArgs e)
        {
            string mettol = JarmuSzervizeles_Mettol_tb.Text;
            string meddig = JarmuSzervizeles_Meddig_tb.Text;
            string indok = JarmuSzervizeles_Indok_tb.Text;

            if (Ellenorzo.UresE(new string[] { mettol, indok }))
            {
                MessageBox.Show("Ne hagyjon üresen egy mezőt se.",
                    "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.MegfeleloDatum(mettol))
            {
                MessageBox.Show("A dátum formátuma nem megfelelő. Pl.: 2000-01-01",
                    "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (JarmuSzervizeles_Meddig_cb.IsChecked.Value)
            {
                if (!Ellenorzo.MegfeleloDatum(meddig))
                {
                    MessageBox.Show("A dátum formátuma nem megfelelő. Pl.: 2000-01-01",
                    "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (Ellenorzo.RegiDatum(mettol))
            {
                MessageBox.Show("A dátum nem lehet a mai naptól egy héttel régebbi!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (JarmuSzervizeles_Szerviz_cb.SelectedItem.ToString() == nincsAdat[0])
            {
                MessageBox.Show("Még nincs felvéve szervíz! Előbb vegyen fel egy szervízt.",
                    "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int szid = Kisegito.KulcsKereso(SzervizekKevesAdat, JarmuSzervizeles_Szerviz_cb.SelectedItem.ToString());

            bool eredmeny = ab.UjSzervizeles(SzerkJarmu.Jid, szid, mettol, meddig, indok);

            if (!eredmeny)
            {
                MessageBox.Show("A mentés sikertelen volt.",
                    "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                JarmuvekOldalNormal();
                return;
            }

            MessageBox.Show("A mentés sikeres volt.",
                    "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            JarmuvekOldalNormal();
        }

        private void JarmuSzervizeles_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Jarmuvek_lb.SelectedIndex == -1)
            {
                MessageBox.Show("Nincs adat amit szerkeszteni tudnál!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                JarmuvekOldalNormal();
                return;
            }

            Jarmuvek_lb.Visibility = Visibility.Collapsed;
            UjJarmu_Grid.Visibility = Visibility.Collapsed;
            SzerkJarmu_Grid.Visibility = Visibility.Collapsed;
            JarmuSzervizeles_btn.Visibility = Visibility.Collapsed;

            JarmuSzervizeles_Grid.Visibility = Visibility.Visible;
            JarmuVissza_btn.Visibility = Visibility.Visible;



            Jarmu jarmu = (Jarmu)Jarmuvek_lb.SelectedItem;

            KivalasztottJarmuLabel1.Content = jarmu.Gyarto + " " + jarmu.Modell + " " + jarmu.Rendszam;

            SzerkJarmu = jarmu;

            SzervizekKevesAdat = ab.SzervizekKevesAdat();
            if (SzervizekKevesAdat.Count() != 0)
            {
                JarmuSzervizeles_Szerviz_cb.ItemsSource = SzervizekKevesAdat.Values;
                JarmuSzervizeles_Szerviz_cb.SelectedIndex = 0;
                return;
            }
            JarmuSzervizeles_Szerviz_cb.ItemsSource = nincsAdat;
            JarmuSzervizeles_Szerviz_cb.SelectedIndex = 0;
        }

        private void SzerkJarmuMentes_btn_Click(object sender, RoutedEventArgs e)
        {
            string gyarto = JarmuGyarto_Szerk_tb.Text;
            string modell = JarmuModell_Szerk_tb.Text;
            string rendszam = JarmuRendszam_Szerk_tb.Text;
            string evjarat = JarmuEvjarat_Szerk_tb.Text;
            string kmora = JarmuKmora_Szerk_tb.Text;

            string[] ujElemek = { gyarto, modell, rendszam, evjarat, kmora };

            string[] regiElemek = { SzerkJarmu.Gyarto, SzerkJarmu.Modell, SzerkJarmu.Rendszam, SzerkJarmu.Evjarat,
            SzerkJarmu.Kmora.ToString()
            };

            if (!Ellenorzo.VanEvaltozas(regiElemek, ujElemek))
            {
                JarmuvekOldalNormal();
                return;
            }

            if (Ellenorzo.UresE(ujElemek))
            {
                MessageBox.Show("Ne hagyjon üresen egy mezőt se.",
                    "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.CsakSzam(kmora))
            {
                MessageBox.Show("A kilóméter óra állása mező csak számokat tartalmazhat.",
                    "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.MegfeleloRendszam(rendszam.ToUpper()))
            {
                MessageBox.Show("Nem megfelelő a rendszám formátuma. Pl.: ABC-123",
                       "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int jid = Convert.ToInt32(KivalasztottJarmuLabel.Content.ToString().Split(';')[0]);

            if (!ab.JarmuSzerkesztes(jid, gyarto, modell, rendszam.ToUpper(), evjarat, kmora))
            {
                MessageBox.Show("Hiba történt a jármű szerkesztése közben.",
                    "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                JarmuvekOldalNormal();
                return;
            }

            MessageBox.Show("Sikeres jármű szerkesztés!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            JarmuvekOldalNormal();
        }

        private void JarmuSzerk_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Jarmuvek_lb.SelectedIndex == -1)
            {
                MessageBox.Show("Nincs adat amit szerkeszteni tudnál!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                JarmuvekOldalNormal();
                return;
            }

            FelugroAblak.Tag = szerkTag;

            Jarmu kivalasztott = (Jarmu)Jarmuvek_lb.SelectedItem;

            KivalasztottJarmuLabel.Content = kivalasztott.Jid + "; " + kivalasztott.Gyarto + " " + kivalasztott.Modell + " " + kivalasztott.Rendszam;

            JarmuGyarto_Szerk_tb.Text = kivalasztott.Gyarto;
            JarmuModell_Szerk_tb.Text = kivalasztott.Modell;
            JarmuEvjarat_Szerk_tb.Text = kivalasztott.Evjarat;
            JarmuRendszam_Szerk_tb.Text = kivalasztott.Rendszam;
            JarmuKmora_Szerk_tb.Text = kivalasztott.Kmora.ToString();

            SzerkJarmu = kivalasztott;

            Jarmuvek_lb.Visibility = Visibility.Collapsed;
            UjJarmu_Grid.Visibility = Visibility.Collapsed;
            JarmuSzervizeles_Grid.Visibility = Visibility.Collapsed;
            JarmuSzervizeles_btn.Visibility = Visibility.Collapsed;

            SzerkJarmu_Grid.Visibility = Visibility.Visible;
            JarmuVissza_btn.Visibility = Visibility.Visible;
        }

        private void UjJarmuMentes_btn_Click(object sender, RoutedEventArgs e)
        {
            string gyarto = JarmuGyarto_tb.Text;
            string modell = JarmuModell_tb.Text;
            string rendszam = Jarmurendszam_tb.Text;
            string evjarat = JarmuEvjarat_tb.Text;
            string kmora = JarmuKmora_tb.Text;

            string[] elemek = { gyarto, modell, rendszam, evjarat, kmora };

            if (Ellenorzo.UresE(elemek))
            {
                MessageBox.Show("Ne hagyjon üresen egy mezőt se.",
                    "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.CsakSzam(kmora))
            {
                MessageBox.Show("A kilóméter óra állása mező csak számokat tartalmazhat.",
                    "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.MegfeleloRendszam(rendszam.ToUpper()))
            {
                MessageBox.Show("Nem megfelelő a rendszám formátuma. Pl.: ABC-123",
                   "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!ab.UjJarmuFelvetele(gyarto, modell, rendszam.ToUpper(), evjarat, kmora))
            {
                MessageBox.Show("Hiba történt a jármű felvétele közben.",
                    "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                JarmuvekOldalNormal();
                return;
            }

            MessageBox.Show("Sikeres jármű felvétel!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            JarmuvekOldalNormal();
        }

        private void UjJarmu_btn_Click(object sender, RoutedEventArgs e)
        {
            Jarmuvek_lb.Visibility = Visibility.Collapsed;
            SzerkJarmu_Grid.Visibility = Visibility.Collapsed;
            JarmuSzervizeles_Grid.Visibility = Visibility.Collapsed;
            JarmuSzervizeles_btn.Visibility = Visibility.Collapsed;

            JarmuGyarto_tb.Text = "";
            JarmuModell_tb.Text = "";
            Jarmurendszam_tb.Text = "";
            JarmuEvjarat_tb.Text = "";
            JarmuKmora_tb.Text = "";

            UjJarmu_Grid.Visibility = Visibility.Visible;
            JarmuVissza_btn.Visibility = Visibility.Visible;
        }

        private void JarmuVissza_btn_Click(object sender, RoutedEventArgs e)
        {
            JarmuvekOldalNormal();
            FelugroAblak.Tag = kijelentkezesTag;
        }

        private void JarmuvekOldalNormal()
        {
            SzerkJarmu_Grid.Visibility = Visibility.Collapsed;
            UjJarmu_Grid.Visibility = Visibility.Collapsed;
            JarmuVissza_btn.Visibility = Visibility.Collapsed;
            JarmuSzervizeles_Grid.Visibility = Visibility.Collapsed;

            UjJarmu_btn.Visibility = Visibility.Visible;
            JarmuSzerk_btn.Visibility = Visibility.Visible;
            Jarmuvek_lb.Visibility = Visibility.Visible;
            JarmuSzervizeles_btn.Visibility = Visibility.Visible;

            List<Jarmu> jarmuvek = ab.Jarmuvek();

            Jarmuvek_lb.ItemsSource = null;
            Jarmuvek_lb.SelectedIndex = -1;
            FelugroAblak.Tag = kijelentkezesTag;

            if (jarmuvek.Count != 0)
            {
                Jarmuvek_lb.ItemsSource = jarmuvek;
                Jarmuvek_lb.SelectedIndex = 0;
                return;
            }
        }

        //Szervizek

        private void SzerkSzervizMentes_Click(object sender, RoutedEventArgs e)
        {
            string ujkontaktNev = kontaktNev_Szerk.Text;
            string ujszervizNev = szervizNev_Szerk.Text;
            string ujkontaktEmail = KontaktEmail_Szerk1.Text;
            string ujkontaktTelszam = kontaktTelszam_Szerk.Text;
            string ujszervizIranyitoSzam = szervizIszam_Szerk.Text;
            string ujszervizTelepules = szervizTelepules_Szerk.Text;
            string ujszervizCim = szervizCim_Szerk.Text;

            string[] ujElemek = { ujkontaktNev, ujszervizNev, ujkontaktEmail, ujkontaktTelszam, ujszervizIranyitoSzam.Replace(" ", ""),
            ujszervizTelepules.Replace(" ", ""), ujszervizCim.Replace(" ", "")};

            string[] regiElemek = { SzerkSzerviz.Kontaktnev, SzerkSzerviz.Sznev, SzerkSzerviz.Kontaktemail,
                SzerkSzerviz.Kontakttelszam, SzerkSzerviz.Cim.Split(',')[0].Replace(" ", ""),SzerkSzerviz.Cim.Split(',')[1].Replace(" ", ""),
            SzerkSzerviz.Cim.Split(',')[2].Replace(" ", "")};

            if (!Ellenorzo.VanEvaltozas(regiElemek, ujElemek))
            {
                SzervizekOldalNormal();
                return;
            }

            if (Ellenorzo.UresE(ujElemek))
            {
                MessageBox.Show("Minden mezőt tölts ki!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Ellenorzo.EmailCheck(ujkontaktEmail))
            {
                MessageBox.Show("Nem megfelelő email formátum! Pl.: mydrivers@gmail.com", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Ellenorzo.MegfeleloTelszam(ujkontaktTelszam))
            {
                MessageBox.Show("Nem megfelelő telefonszám formátum! Pl.: 06301234567", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Ellenorzo.CsakSzam(ujszervizIranyitoSzam))
            {
                MessageBox.Show("Nem megfelelő irányítószám formátum! A mező csak számot tartalmazhat!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool eredmeny = ab.SzervizSzerkesztes(SzerkSzerviz.Szid, ujkontaktNev, ujszervizNev,
                ujkontaktEmail, ujkontaktTelszam, ujszervizIranyitoSzam,
                ujszervizTelepules, ujszervizCim);

            if (!eredmeny)
            {
                MessageBox.Show("Az mentés sikertelen volt!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                SzervizekOldalNormal();
                return;
            }

            MessageBox.Show("Sikeres mentés!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            SzervizekOldalNormal();
        }

        private void SzervizSzerk_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Szervizek_lb.SelectedIndex == -1)
            {
                MessageBox.Show("Nincs adat amit szerkeszteni tudnál!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                SzervizekOldalNormal();
                return;
            }

            FelugroAblak.Tag = szerkTag;

            Szerviz szerviz = (Szerviz)Szervizek_lb.SelectedItem;
            Szervizek_lb.Visibility = Visibility.Collapsed;
            UjSzerviz_Grid.Visibility = Visibility.Collapsed;
            SzerkSzerviz_Grid.Visibility = Visibility.Visible;
            SzervizVissza_btn.Visibility = Visibility;


            KivalasztottSzerviz.Content = szerviz.Szid + ";" + szerviz.Sznev;
            kontaktNev_Szerk.Text = szerviz.Kontaktnev;
            szervizNev_Szerk.Text = szerviz.Sznev;
            KontaktEmail_Szerk1.Text = szerviz.Kontaktemail;
            kontaktTelszam_Szerk.Text = szerviz.Kontakttelszam;
            szervizIszam_Szerk.Text = szerviz.Cim.Split(',')[0];
            szervizTelepules_Szerk.Text = szerviz.Cim.Split(',')[1];
            szervizCim_Szerk.Text = szerviz.Cim.Split(',')[2];

            SzerkSzerviz = szerviz;
        }

        private void UjSzervizMentes_btn_Click(object sender, RoutedEventArgs e)
        {
            string[] elemek = { kontaktNev_tb.Text, szervizNev_tb.Text, KontaktEmail_tb.Text, kontaktTelszam_tb.Text
            , szervizIszam_tb.Text, szervizTelepules_tb.Text, szervizCim_tb.Text};
            if (Ellenorzo.UresE(elemek))
            {
                MessageBox.Show("Minden mezőt tölts ki!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Ellenorzo.EmailCheck(KontaktEmail_tb.Text))
            {
                MessageBox.Show("Nem megfelelő email formátum! Pl.: mydrivers@gmail.com", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Ellenorzo.MegfeleloTelszam(kontaktTelszam_tb.Text))
            {
                MessageBox.Show("Nem megfelelő telefonszám formátum! Pl.: 06301234567", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Ellenorzo.CsakSzam(szervizIszam_tb.Text))
            {
                MessageBox.Show("Nem megfelelő irányítószám formátum! A mező csak számot tartalmazhat!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool eredmeny = ab.UjSzervizFelvetele(kontaktNev_tb.Text, szervizNev_tb.Text,
                KontaktEmail_tb.Text, kontaktTelszam_tb.Text, szervizIszam_tb.Text,
                szervizTelepules_tb.Text, szervizCim_tb.Text);

            if (!eredmeny)
            {
                MessageBox.Show("Az új adat felvétele az adatbázisba sikertelen volt!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                SzervizekOldalNormal();
                return;
            }

            MessageBox.Show("Sikeres feltöltés!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            SzervizekOldalNormal();
        }


        private void UjSzerviz_btn_Click(object sender, RoutedEventArgs e)
        {
            Szervizek_lb.Visibility = Visibility.Collapsed;
            UjSzerviz_Grid.Visibility = Visibility.Visible;
            SzerkSzerviz_Grid.Visibility = Visibility.Collapsed;
            SzervizVissza_btn.Visibility = Visibility;
            kontaktNev_tb.Text = "";
            szervizNev_tb.Text = "";
            KontaktEmail_tb.Text = "";
            kontaktTelszam_tb.Text = "";
            szervizIszam_tb.Text = "";
            szervizTelepules_tb.Text = "";
            szervizCim_tb.Text = "";
        }

        private void SzervizVissza_btn_Click(object sender, RoutedEventArgs e)
        {
            SzervizekOldalNormal();
            FelugroAblak.Tag = kijelentkezesTag;
        }

        private void SzervizekOldalNormal()
        {
            FelugroAblak.Tag = kijelentkezesTag;
            Szervizek_lb.Visibility = Visibility.Visible;
            SzervizekLBFetloltes();
            SzervizVissza_btn.Visibility = Visibility.Collapsed;
            UjSzerviz_Grid.Visibility = Visibility.Collapsed;
            SzerkSzerviz_Grid.Visibility = Visibility.Collapsed;
            FelugroAblak.Tag = kijelentkezesTag;
        }

        private void SzervizekLBFetloltes()
        {
            List<Szerviz> szervizek = ab.Szervizek();


            Szervizek_lb.ItemsSource = null;
            Szervizek_lb.SelectedIndex = -1;

            if (szervizek.Count != 0)
            {
                Szervizek_lb.ItemsSource = szervizek;
                Szervizek_lb.SelectedIndex = 0;
                return;
            }
        }

        //SajátProfil

        private void Sajatprof_jelszoModosit_Click(object sender, RoutedEventArgs e)
        {
            JelszoModosit j = new JelszoModosit(felhasznalo.Fid, ab, this);
            j.Show();
            this.Hide();
        }

        private void Sajatprof_szerkesztes_Click(object sender, RoutedEventArgs e)
        {
            if ((string)FelugroAblak.Tag != szerkTag)
            {
                FelugroAblak.Tag = szerkTag;
                SajatProf_Szerkesztes();
            }
        }

        private void SajatProf_Szerkesztes()
        {
            Sajatprof_szerkesztes.Visibility = Visibility.Collapsed;

            Sajatprof_SzerkMentes.Visibility = Visibility.Visible;

            sajatprof_fnev_tb.Text = sajatprof_fnev.Content.ToString();
            sajatprof_fnev.Visibility = Visibility.Collapsed;
            sajatprof_fnev_tb.Visibility = Visibility.Visible;

            sajatprof_email_tb.Text = sajatprof_email.Content.ToString();
            sajatprof_email.Visibility = Visibility.Collapsed;
            sajatprof_email_tb.Visibility = Visibility.Visible;

            sajatprof_szulnap_tb.Text = sajatprof_szulnap.Content.ToString();
            sajatprof_szulnap.Visibility = Visibility.Collapsed;
            sajatprof_szulnap_tb.Visibility = Visibility.Visible;

            sajatprof_nem_tb.Text = sajatprof_nem.Content.ToString();
            sajatprof_nem.Visibility = Visibility.Collapsed;
            sajatprof_nem_tb.Visibility = Visibility.Visible;

            sajatprof_teljesnev_tb.Text = sajatprof_teljesnev.Content.ToString();
            sajatprof_teljesnev.Tag = sajatprof_teljesnev.Content.ToString();
            sajatprof_teljesnev.Content = "Teljes név:";
            sajatprof_teljesnev_tb.Visibility = Visibility.Visible;

            sajatprof_telszam_tb.Text = sajatprof_telszam.Content.ToString();
            sajatprof_telszam.Visibility = Visibility.Collapsed;
            sajatprof_telszam_tb.Visibility = Visibility.Visible;

            sajatprof_lakcim_tb.Text = sajatprof_lakcim.Content.ToString();
            sajatprof_lakcim.Visibility = Visibility.Collapsed;
            sajatprof_lakcim_tb.Visibility = Visibility.Visible;

            sajatprof_adoszam_tb.Text = sajatprof_adoszam.Content.ToString();
            sajatprof_adoszam.Visibility = Visibility.Collapsed;
            sajatprof_adoszam_tb.Visibility = Visibility.Visible;

            sajatprof_bankszam_tb.Text = sajatprof_bankszam.Content.ToString();
            sajatprof_bankszam.Visibility = Visibility.Collapsed;
            sajatprof_bankszam_tb.Visibility = Visibility.Visible;

            sajatprof_szemelyi_tb.Text = sajatprof_szemelyi.Content.ToString();
            sajatprof_szemelyi.Visibility = Visibility.Collapsed;
            sajatprof_szemelyi_tb.Visibility = Visibility.Visible;

            sajatprof_tajszam_tb.Text = sajatprof_tajszam.Content.ToString();
            sajatprof_tajszam.Visibility = Visibility.Collapsed;
            sajatprof_tajszam_tb.Visibility = Visibility.Visible;
        }

        private void Sajatprof_SzerkMentes_Click(object sender, RoutedEventArgs e)
        {
            if ((string)FelugroAblak.Tag == szerkTag)
            {
                if (VanValtozas())
                {
                    string teljesnev = sajatprof_teljesnev_tb.Text;
                    string fhnev = sajatprof_fnev_tb.Text;
                    string nem = sajatprof_nem_tb.Text;
                    string szemelyi = sajatprof_szemelyi_tb.Text;
                    string email = sajatprof_email_tb.Text;
                    string szuldatum = sajatprof_szulnap_tb.Text;
                    string telszam = sajatprof_telszam_tb.Text;
                    string lakcim = sajatprof_lakcim_tb.Text;
                    string adoszam = sajatprof_adoszam_tb.Text;
                    string bankszamlaszam = sajatprof_bankszam_tb.Text.Replace("-", "");
                    if (bankszamlaszam.Length > 24)
                    {
                        MessageBox.Show("A bankszámlaszám túl hosszú." +
                            " Maximum 24 db számból és két kötőjelből állhat!" +
                            " Jelenlegi szám mennyiség: " + bankszamlaszam.Length + "db",
                            "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (bankszamlaszam.Length < 24)
                    {
                        MessageBox.Show("A bankszámlaszám túl rövid." +
                            " Minimum 24 db számból és két kötőjelből állhat!" +
                            " Jelenlegi szám mennyiség: " + bankszamlaszam.Length + "db",
                            "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    string tajszam = sajatprof_tajszam_tb.Text;

                    string[] elemek = { teljesnev, fhnev, nem, szemelyi, email, szuldatum, telszam, telszam, lakcim, adoszam, bankszamlaszam, tajszam };

                    if (Ellenorzo.UresE(elemek))
                    {
                        MessageBox.Show("Nem maradhat mező üresen! Minden mezőt tölts ki.",
                            "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    List<string> ellenorzes = SajatProf_AdatokEllenorzese(szemelyi, email, szuldatum, telszam, lakcim, adoszam, bankszamlaszam, tajszam);

                    if (ellenorzes.Count != 0)
                    {
                        foreach (string item in ellenorzes)
                        {
                            MessageBox.Show(item, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        return;
                    }

                    bool eredmeny = ab.SajatProfilSzerkesztes(felhasznalo.Fid, teljesnev, fhnev, nem, szemelyi, email, szuldatum, telszam, lakcim, adoszam, bankszamlaszam, tajszam);
                    if (!eredmeny)
                    {
                        MessageBox.Show("Hiba történt a szerkesztések elmentése közben.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                        SajatProf_Normal();
                        return;
                    }

                    MessageBox.Show("Sikeresen elmentette az adatokat.", "Kész!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Sajatprof_szerkesztes.Visibility = Visibility.Visible;
                    SajatProf_Normal();
                    SajatProfil_AdatokBetoltese();
                }
                else
                {
                    Sajatprof_szerkesztes.Visibility = Visibility.Visible;
                    SajatProf_Normal();
                }
            }
        }

        private void SajatProfil_AdatokBetoltese()
        {
            felhasznalo = ab.Felhasznalo_Adatok(felhasznalo.Fid, "", "");
            sajatprof_fnev.Content = felhasznalo.Fnev;
            sajatprof_email.Content = felhasznalo.Email;
            sajatprof_szulnap.Content = felhasznalo.Szuletesnap.ToString("yyyy-MM-dd");
            sajatprof_nem.Content = felhasznalo.Nem;
            sajatprof_teljesnev.Content = felhasznalo.Teljesnev;
            sajatprof_telszam.Content = felhasznalo.Telefonszam;
            sajatprof_lakcim.Content = felhasznalo.Lakcim;
            sajatprof_adoszam.Content = felhasznalo.Adoazon;
            if (felhasznalo.Bankszamlaszam == "Nincs adat")
            {
                sajatprof_bankszam.Content = felhasznalo.Bankszamlaszam;
            }
            else
            {
                string elso = "";
                string masodik = "";
                string harmadik = "";
                for (int i = 0; i < felhasznalo.Bankszamlaszam.Length; i++)
                {
                    if (i < 8)
                    {
                        elso += felhasznalo.Bankszamlaszam[i];
                    }
                    else if (i > 7 && i < 16)
                    {
                        masodik += felhasznalo.Bankszamlaszam[i];
                    }
                    else if (i > 15 && i < 24)
                    {
                        harmadik += felhasznalo.Bankszamlaszam[i];
                    }
                }
                sajatprof_bankszam.Content = elso + "-" + masodik + "-" + harmadik;
            }
            sajatprof_szemelyi.Content = felhasznalo.Szemelyi;
            sajatprof_tajszam.Content = felhasznalo.Tajszam;
            sajatprof_csatlakozas.Content = felhasznalo.Csatlakozas.ToString("yyyy-MM-dd");
            sajatprof_alapber.Content = felhasznalo.Alapber.ToString();
        }

        private List<string> SajatProf_AdatokEllenorzese(string szemelyi, string email, string szuldatum, string telszam, string lakcim, string adoszam, string bankszamlaszam, string tajszam)
        {
            List<string> eredmenyek = new List<string>();

            if (!Ellenorzo.MegfeleloSzemelyi(szemelyi))
            {
                eredmenyek.Add("Nem megfelelő személyigazolvány szám formátum! Pl.: 123456XX");
            }

            if (!Ellenorzo.EmailCheck(email))
            {
                eredmenyek.Add("Nem megfelelő email formátum! Pl.: mydrivers@gmail.com");
            }

            if (!Ellenorzo.MegfeleloDatum(szuldatum))
            {
                eredmenyek.Add("Nem megfelelő dátum formátum! Pl.: 2022-01-01");
            }

            if (!Ellenorzo.MegfeleloTelszam(telszam))
            {
                eredmenyek.Add("Nem megfelelő telefonszám formátum! Pl.: 06301234567");
            }

            if (!LakcimEllenorzes(lakcim))
            {
                eredmenyek.Add("Nem megfelelő lakcím formátum! Pl.: 1136, Budapest, Példa utca 1");
            }

            if (!Ellenorzo.CsakSzam(adoszam))
            {
                eredmenyek.Add("Nem megfelelő adóazonosító jel formátum! Az adóazonosító jel csak számokat tartalmazhat!");
            }

            if (!Ellenorzo.CsakSzam(bankszamlaszam))
            {
                eredmenyek.Add("Nem megfelelő bankszámlaszám formátum! A bankszámlaszám csak számokat tartalmazhat!");
            }

            if (!Ellenorzo.CsakSzam(tajszam))
            {
                eredmenyek.Add("Nem megfelelő tajszám formátum! A tajszám csak számokat tartalmazhat!");
            }

            return eredmenyek;
        }

        private bool LakcimEllenorzes(string lakcim)
        {
            try
            {
                string irsz = lakcim.Split(',')[0];
                if (!Ellenorzo.CsakSzam(irsz))
                {
                    return false;
                }
                string telepules = lakcim.Split(',')[1];
                string cim = lakcim.Split(',')[2];

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SajatProf_Normal()
        {
            Sajatprof_SzerkMentes.Visibility = Visibility.Collapsed;
            Sajatprof_szerkesztes.Visibility = Visibility.Visible;

            sajatprof_fnev.Visibility = Visibility.Visible;
            sajatprof_fnev_tb.Visibility = Visibility.Collapsed;

            sajatprof_email.Visibility = Visibility.Visible;
            sajatprof_email_tb.Visibility = Visibility.Collapsed;

            sajatprof_szulnap.Visibility = Visibility.Visible;
            sajatprof_szulnap_tb.Visibility = Visibility.Collapsed;

            sajatprof_nem.Visibility = Visibility.Visible;
            sajatprof_nem_tb.Visibility = Visibility.Collapsed;

            sajatprof_teljesnev.Content = sajatprof_teljesnev_tb.Text;
            sajatprof_teljesnev.Visibility = Visibility.Visible;
            sajatprof_teljesnev_tb.Visibility = Visibility.Collapsed;

            sajatprof_telszam.Visibility = Visibility.Visible;
            sajatprof_telszam_tb.Visibility = Visibility.Collapsed;

            sajatprof_lakcim.Visibility = Visibility.Visible;
            sajatprof_lakcim_tb.Visibility = Visibility.Collapsed;

            sajatprof_adoszam.Visibility = Visibility.Visible;
            sajatprof_adoszam_tb.Visibility = Visibility.Collapsed;

            sajatprof_bankszam.Visibility = Visibility.Visible;
            sajatprof_bankszam_tb.Visibility = Visibility.Collapsed;

            sajatprof_szemelyi.Visibility = Visibility.Visible;
            sajatprof_szemelyi_tb.Visibility = Visibility.Collapsed;

            sajatprof_tajszam.Visibility = Visibility.Visible;
            sajatprof_tajszam_tb.Visibility = Visibility.Collapsed;

            SajatProfil_AdatokBetoltese();
            FelugroAblak.Tag = kijelentkezesTag;
        }

        private bool VanValtozas()
        {
            if (sajatprof_fnev_tb.Text != (string)sajatprof_fnev.Content)
            {
                return true;
            }
            if (sajatprof_email_tb.Text != (string)sajatprof_email.Content)
            {
                return true;
            }
            if (sajatprof_szulnap_tb.Text != (string)sajatprof_szulnap.Content)
            {
                return true;
            }
            if (sajatprof_nem_tb.Text != (string)sajatprof_nem.Content)
            {
                return true;
            }
            if (sajatprof_teljesnev_tb.Text != (string)sajatprof_teljesnev.Tag)
            {
                return true;
            }
            if (sajatprof_telszam_tb.Text != (string)sajatprof_telszam.Content)
            {
                return true;
            }
            if (sajatprof_lakcim_tb.Text != (string)sajatprof_lakcim.Content)
            {
                return true;
            }
            if (sajatprof_adoszam_tb.Text != (string)sajatprof_adoszam.Content)
            {
                return true;
            }
            if (sajatprof_bankszam_tb.Text != (string)sajatprof_bankszam.Content)
            {
                return true;
            }
            if (sajatprof_szemelyi_tb.Text != (string)sajatprof_szemelyi.Content)
            {
                return true;
            }
            if (sajatprof_tajszam_tb.Text != (string)sajatprof_tajszam.Content)
            {
                return true;
            }
            return false;
        }

        //FelugroAblak

        private void FelugroIgen_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((string)FelugroAblak.Tag == szerkTag)
            {
                FelugroAblak.Visibility = Visibility.Collapsed;
                SajatProf_Normal();
                SajatProfil_AdatokBetoltese();
                FelugroAblak.Tag = kijelentkezesTag;
            }
            else
            {
                felhasznalo = null;
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        }

        private void FelugroMegse_btn_Click(object sender, RoutedEventArgs e)
        {
            if ((string)FelugroAblak.Tag == szerkTag)
            {
                FelugroAblak.Visibility = Visibility.Collapsed;
                FelugroAblak.Tag = szerkTag;
            }
            else
            {
                FelugroAblak.Visibility = Visibility.Collapsed;
                FelugroAblak.Tag = kijelentkezesTag;
            }
        }

        //Oldal Nyito-bezaro

        private void FooldalBezar()
        {
            Fooldal_Grid.Visibility = Visibility.Collapsed;
        }

        private void FooldalNyit()
        {
            Fooldal_Grid.Visibility = Visibility.Visible;
            FooldalNormal();
        }

        private void FelhasznalokBezar()
        {
            Felhasznalok_Grid.Visibility = Visibility.Collapsed;
        }

        private void FelhasznalokNyit()
        {
            Felhasznalok_Grid.Visibility = Visibility.Visible;
            FelhasznalokOldalNormal();
        }

        private void JarmuvekNyit()
        {
            Jarmuvek_Grid.Visibility = Visibility.Visible;
            JarmuvekOldalNormal();
        }

        private void JarmuvekBezar()
        {
            Jarmuvek_Grid.Visibility = Visibility.Collapsed;
        }

        private void SzervizekBezar()
        {
            Szervizek_Grid.Visibility = Visibility.Collapsed;
        }

        private void SzervizekNyit()
        {
            Szervizek_Grid.Visibility = Visibility.Visible;
            SzervizekOldalNormal();
        }

        private void SajatProfBezar()
        {
            SajatProfil_Grid.Visibility = Visibility.Collapsed;
        }

        private void SajatProfNyit()
        {
            SajatProfil_Grid.Visibility = Visibility.Visible;
            SajatProf_Normal();
        }

        private void SzervizelesBezar()
        {
            Szervizeles_Grid.Visibility = Visibility.Collapsed;
        }

        private void SzervizelesNyit()
        {
            Szervizeles_Grid.Visibility = Visibility.Visible;
            SzervizelesOldalNormal();
        }

        private void FuvarokBezar()
        {
            Fuvarok_Grid.Visibility = Visibility.Collapsed;
        }

        private void FuvarokNyit()
        {
            Fuvarok_Grid.Visibility = Visibility.Visible;
            FuvarokOldalNormal();
        }

        private void BeosztasBezar()
        {
            Beosztas_Grid.Visibility = Visibility.Collapsed;
        }

        private void BeosztasNyit()
        {
            Beosztas_Grid.Visibility = Visibility.Visible;
            BeosztasOldalNormal();
        }


        //számláló
        private Color piros = (Color)ColorConverter.ConvertFromString("#FFB33838");
        private Color alap = (Color)ColorConverter.ConvertFromString("#FF000000");

        private void Fbankszamla_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            FbankszamlaSzamlalo.Content = Fbankszamla_tb.Text.Replace("-", "").Length + "/24";
            if (Fbankszamla_tb.Text.Replace("-", "").Length != 24)
            {
                FbankszamlaSzamlalo.Foreground = new SolidColorBrush(piros);
            }
            else
            {
                FbankszamlaSzamlalo.Foreground = new SolidColorBrush(alap);
            }
        }

        private void FSzerkbankszamla_tb1_TextChanged(object sender, TextChangedEventArgs e)
        {
            FSzerkBszSzamlalo.Content = FSzerkbankszamla_tb1.Text.Replace("-", "").Length + "/24";
            if (FSzerkbankszamla_tb1.Text.Replace("-", "").Length != 24)
            {
                FSzerkBszSzamlalo.Foreground = new SolidColorBrush(piros);
            }
            else
            {
                FSzerkBszSzamlalo.Foreground = new SolidColorBrush(alap);
            }
        }


    }
}