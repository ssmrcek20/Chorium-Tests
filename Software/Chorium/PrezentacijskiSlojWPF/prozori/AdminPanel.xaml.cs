using SlojEntiteta.Entiteti;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrezentacijskiSlojWPF.prozori
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : UserControl
    {
        private ContentControl _sadrzaj;
        public AdminPanel(ContentControl sadrzaj)
        {
            InitializeComponent();
            _sadrzaj = sadrzaj;
        }

        private void btnPromjeniKorime_Click(object sender, RoutedEventArgs e)
        {
            AdminServis adminServis = new AdminServis();
            Korisnik korisnik = dgvKorisnici.SelectedItem as Korisnik;

            if (adminServis.CheckEnteredUsername(txtOdabraniKorisnik.Text) && korisnik != null)
            {
                korisnik.Korisnicko_ime = txtOdabraniKorisnik.Text;
                adminServis.UpdateUsername(korisnik);
            }
            UcitajKorisnike();
        }

        private void dgvKorisnici_Loaded(object sender, RoutedEventArgs e)
        {
            UcitajKorisnike();
        }

        private void UcitajKorisnike()
        {
            KorisnikRepozitorij korisnikRepozitorij = new KorisnikRepozitorij();
            KorisnikServis korisnikServis = new KorisnikServis(korisnikRepozitorij);
            dgvKorisnici.ItemsSource = korisnikServis.DohvatiKorisnike();
            FormatirajDataGrid();
        }

        private void FormatirajDataGrid()
        {
            dgvKorisnici.Columns[0].Visibility = Visibility.Hidden;
            for (int i = 5; i <= 16; i++) dgvKorisnici.Columns[i].Visibility = Visibility.Hidden;
        }

        private void dgvKorisnici_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgvKorisnici.SelectedItem as Korisnik != null) txtOdabraniKorisnik.Text = (dgvKorisnici.SelectedItem as Korisnik).Korisnicko_ime;
        }
    }
}
