using System;
using System.Windows;

namespace MyDrivers
{
    /// <summary>
    /// Interaction logic for UjMunkakorAblak.xaml
    /// </summary>
    public partial class UjMunkakorAblak : Window
    {
        private ABcsatlakozas ab;
        private Fooldal fooldal;

        public UjMunkakorAblak(ABcsatlakozas ab, Fooldal fooldal)
        {
            InitializeComponent();
            this.ab = ab;
            this.fooldal = fooldal;
        }

        private void UjMkMentes_btn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(UjMk_tb.Text))
            {
                MessageBox.Show("A mező nem lehet üres!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool letezik = ab.LetezoMunkakor(UjMk_tb.Text);

            if (letezik)
            {
                MessageBox.Show("Ez a munkakör már létezik!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool eredmeny = ab.UjMunkakor(UjMk_tb.Text);

            if (!eredmeny)
            {
                MessageBox.Show("Hiba történt a munkakör mentése közben!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Sikeres mentés!", "Siker!", MessageBoxButton.OK, MessageBoxImage.Information);
            fooldal.Show();
            fooldal.MunkakorJogFrissit(false);
            Close();
        }

        private void UjMkVissza_btn_Click(object sender, RoutedEventArgs e)
        {
            fooldal.Show();
            Close();
        }
    }
}
