using System;
using System.Windows;
using System.Windows.Input;

namespace MyDrivers
{
    /// <summary>
    /// Interaction logic for FelhasznaloSzabiraKuld.xaml
    /// </summary>
    public partial class FelhasznaloSzabiraKuld : Window
    {
        private ABcsatlakozas ab;
        private Felhasznalo fh;
        private Fooldal f;

        public FelhasznaloSzabiraKuld(ABcsatlakozas ab, Felhasznalo fh, Fooldal f)
        {
            InitializeComponent();
            this.ab = ab;
            this.fh = fh;
            this.f = f;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(MegadottSzabi_tb.Text))
            {
                MessageBox.Show("Ne hagyja üresen a mezőt!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Ellenorzo.CsakSzam(MegadottSzabi_tb.Text))
            {
                MessageBox.Show("A mező csak számokat tartalmazhat!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int szabi = Convert.ToInt32(MegadottSzabi_tb.Text);

            if ((szabi + fh.Kivettszabi) > fh.Kivehetoszabi)
            {
                MessageBox.Show("A felhasználó a megadott szabadsággal már átlépné a maximálisan kivehető szabadságát!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool eredmeny = ab.SzabadsagModosit(fh.Fid, szabi);

            if (!eredmeny)
            {
                MessageBox.Show("A mentés sikertelen volt.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Sikeres mentés!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            f.FelhasznalokFrissit();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MaxSzabi_Label.Content = fh.Kivettszabi + "/" + fh.Kivehetoszabi;
            KivalasztottFelhasznalo.Content = fh.Fnev;
            MegadottSzabi_tb.Text = "";
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
