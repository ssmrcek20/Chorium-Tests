using SlojPoslovneLogike.Servisi;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrezentacijskiSlojWPF.prozori
{
    /// <summary>
    /// Interaction logic for FormaZaPrijavu.xaml
    /// </summary>
    public partial class FormaZaPrijavu : UserControl
    {
        ContentControl contentControl;
        MainWindow _main;
        public FormaZaPrijavu(ContentControl sadrzaj, MainWindow main)
        {
            InitializeComponent();
            contentControl = sadrzaj;
            _main = main;
        }

        private void BtnPrijava_Click(object sender, RoutedEventArgs e)
        {
            if (ProvjeriPostojanjePodataka())
            {
                var repo = new KorisnikRepozitorij();
                KorisnikServis servis = new KorisnikServis(repo);
                var lozinkaServis = new LozinkaServis();
                string lozinka = lozinkaServis.HashirajLozinku(TxtLozinka.Password, TxtKorime.Text);
                if (servis.ProvjeriIspravnostPodataka(TxtKorime.Text, lozinka))
                {
                    UspjesanLogin();
                }else MessageBox.Show("Neispravno korisničko ime ili lozinka");

            }
            else
            {
                MessageBox.Show("Unesite korisničko ime i lozinku");
            }
        }

        private bool ProvjeriPostojanjePodataka()
        {
            if (TxtKorime.Text == "" || TxtLozinka.Password == "" || TxtLozinka.Password == " ")
            {
                return false;
            }
            else return true;
        }

        private void UspjesanLogin()
        {
            contentControl.Content = new FormaZaPrikazPoslova(contentControl);
            MainWindow main = new MainWindow();
            var repo = new KorisnikRepozitorij();
            KorisnikServis servis = new KorisnikServis(repo);
            if (servis.ProvjeriKorisnika()) _main.btnAdminPanel.IsEnabled = true;
        }

        private void BtnFaceLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var repo = new KorisnikRepozitorij();
                var servis = new KorisnikServis(repo);
                if (servis.TrenirajLica()) UspjesanLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska!");
            }
        }
    }
}
