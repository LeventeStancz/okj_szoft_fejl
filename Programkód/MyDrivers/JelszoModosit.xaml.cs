using System;
using System.Windows;
using System.Windows.Input;

namespace MyDrivers
{
    /// <summary>
    /// Interaction logic for JelszoModosit.xaml
    /// </summary>
    public partial class JelszoModosit : Window
    {
        private ABcsatlakozas ab;
        private int fid;
        Fooldal f;
        public JelszoModosit(int fid, ABcsatlakozas ab, Fooldal f)
        {
            InitializeComponent();
            this.fid = fid;
            this.f = f;
            this.ab = ab;
        }

        private void AblakBezar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            f.Show();
            this.Close();
        }

        private void JelszoModosit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (UresMezok(RegiJelszo_pb.Password, UjJelszo_pb.Password, UjJelszo_pb_2.Password))
            {
                HibaLabel.Content = "Ne hagyjon üresen egy mezőt sem!";
                return;
            }
            if (UjJelszo_pb.Password != UjJelszo_pb_2.Password)
            {
                HibaLabel.Content = "Az jelszavaknak egyeznie kell az `új jelszó` mezőkben!";
                return;
            }
            if (!ab.JelszoEgyezes(fid, RegiJelszo_pb.Password))
            {
                HibaLabel.Content = "A jelenlegi jelszó nem egyezik!";
                return;
            }

            bool eredmeny = ab.JelszoModosit(fid, UjJelszo_pb.Password);
            if (!eredmeny)
            {
                MessageBox.Show("Sikertelen jelszó módosítás.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Sikeres jelszó módosítás." +
                "A rendszer most kijelentkezteti önt.", "Sikeres!",
                MessageBoxButton.OK, MessageBoxImage.Information);
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private bool UresMezok(string password1, string password2, string password3)
        {
            if (String.IsNullOrWhiteSpace(password1) || String.IsNullOrWhiteSpace(password2) || String.IsNullOrWhiteSpace(password3))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RegiJelszo_pb.Password = "";
            UjJelszo_pb.Password = "";
            UjJelszo_pb_2.Password = "";
        }
    }
}
