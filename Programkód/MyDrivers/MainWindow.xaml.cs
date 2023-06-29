using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace MyDrivers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ABcsatlakozas ab;
        public MainWindow()
        {
            InitializeComponent();
            bej_fnev.Focus();
        }

        private void Kilep_gomb(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void bejelentkez_gomb_Click(object sender, RoutedEventArgs e)
        {

            if (ab.Inaktiv(bej_fnev.Text, bej_jelszo.Password.ToString()) == "True")
            {
                if (ab.hiba)
                {
                    hiba_label.Content = "Az adatbázishoz való csatlakozás sikertelen volt!";
                    return;
                }
                hiba_label.Content = "Ez a felhasználó jelenleg inaktív.";
                return;
            }

            string jelszo = string.Empty;
            if (JelszoMutat_cb.IsChecked.Value)
            {
                jelszo = JelszoMutat_tb.Text;
            }
            else
            {
                jelszo = bej_jelszo.Password.ToString();
            }

            Felhasznalo fh = ab.Felhasznalo_Adatok(-1, bej_fnev.Text, jelszo);
            if (fh == null)
            {
                if (ab.hiba)
                {
                    hiba_label.Content = "Az adatbázishoz való csatlakozás sikertelen volt!";
                    return;
                }
                hiba_label.Content = "Nem megfelelő jelszó vagy felhasználó név.";
                return;
            }

            Storyboard sb1 = this.Resources["Storyboard1"] as Storyboard;
            sb1.Completed += (sender2, e2) => FooldalNyit(sender2, e2, fh);
            bej_jelszo.Visibility = Visibility.Visible;
            sb1.Begin();
        }

        private void FooldalNyit(object sender, EventArgs e, Felhasznalo fh)
        {
            Fooldal f = new Fooldal(fh, ab);
            f.Show();
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                bejelentkez_gomb_Click(sender, e);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ab = new ABcsatlakozas();
            Kisegito.Jogosultsagok = ab.Jogosultsagok();
            Kisegito.Munkakorok = ab.Munkakorok();
            Kisegito.Keruletek = ab.Keruletek();
            if (ab.hiba)
            {
                hiba_label.Content = "Az adatbázishoz való csatlakozás sikertelen volt!";
            }
        }

        private void JelszoMutat_cb_Click(object sender, RoutedEventArgs e)
        {
            if (JelszoMutat_cb.IsChecked.Value)
            {
                JelszoMutat_tb.Text = bej_jelszo.Password.ToString();
                bej_jelszo.Visibility = Visibility.Collapsed;
                JelszoMutat_tb.Visibility = Visibility.Visible;
            }
            else
            {
                bej_jelszo.Password = JelszoMutat_tb.Text;
                JelszoMutat_tb.Visibility = Visibility.Collapsed;
                bej_jelszo.Visibility = Visibility.Visible;
            }
        }
    }
}
