using System;
using System.Web.Security;
using System.Windows;
using System.Windows.Input;

namespace MyDrivers
{
    /// <summary>
    /// Interaction logic for FelhasznaloJelszoKiut.xaml
    /// </summary>
    public partial class FelhasznaloJelszoKiut : Window
    {
        private Felhasznalo fh;
        private ABcsatlakozas ab;
        private Fooldal f;

        public FelhasznaloJelszoKiut(Felhasznalo fh, ABcsatlakozas ab, Fooldal f)
        {
            InitializeComponent();
            this.fh = fh;
            this.ab = ab;
            this.f = f;
        }

        private void BezarGomb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            kivalasztottFelhasznalo.Content = "";
            kivalasztottFelhasznalo.Content = fh.Fnev;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(jelszo_tb.Text))
            {
                MessageBox.Show("Nem maradhat üresen a mező!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool eredmeny = ab.JelszoModosit(fh.Fid, jelszo_tb.Text);

            if (!eredmeny)
            {
                MessageBox.Show("Sikertelen mentés!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Sikeres mentés!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            f.FelhasznalokFrissit();
            this.Close();
        }

        private void Jelszogeneral_Click(object sender, RoutedEventArgs e)
        {
            string jelszo = Membership.GeneratePassword(8, 1);
            jelszo_tb.Text = jelszo;
        }


    }
}
