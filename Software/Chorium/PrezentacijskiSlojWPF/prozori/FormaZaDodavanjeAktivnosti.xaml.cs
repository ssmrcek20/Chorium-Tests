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
    /// Interaction logic for FormaZaDodavanjeAktivnosti.xaml
    /// </summary>
    public partial class FormaZaDodavanjeAktivnosti : UserControl
    {

        private ContentControl _sadrzaj;
        public FormaZaDodavanjeAktivnosti(ContentControl sadrzaj)
        {
            InitializeComponent();
            _sadrzaj = sadrzaj;
        }

        private void btnDodajAktivnost_Click(object sender, RoutedEventArgs e)
        {
            if (ProvjeriPodatke())
            {
                AktivnostiRepozitorij aktivnostiRepozitorij = new AktivnostiRepozitorij();
                AktivnostServis servis = new AktivnostServis(aktivnostiRepozitorij);
                Korisnik korisnik = cmbClanKucanstva.SelectedItem as Korisnik;

                if (servis.DodajAktivnost(NapraviObjektAktivnosti(), korisnik)) MessageBox.Show("Aktivnost dodana.");

                _sadrzaj.Content = new FormaZaPrikazKalendara(_sadrzaj);
            }
            else
            {
                MessageBox.Show("Provjerite podatke!");
            } 
        }

        private Aktivnost NapraviObjektAktivnosti()
        {
            string naziv = txtNaziv.Text;
            DateTime datumPocetka = NapraviDatumPocetka();
            DateTime datumKraja = NapraviDatumKraja();
            Aktivnost aktivnost = new Aktivnost
            {
                Naziv = naziv,
                Datum_kraja = datumKraja,
                Datum_pocetka = datumPocetka,
            };
            return aktivnost;
        }

        private DateTime NapraviDatumKraja()
        {
            DateTime datum = (DateTime)dtpDatumZavrsetka.SelectedDate;
            string datumString = datum.ToString("yyyy-MM-dd");

            string sati = txtKrajH.Text;
            string minute = txtKrajM.Text;
            string sekunde = txtKrajS.Text;

            if (sati == "") sati = "00";
            if (minute == "") minute = "00";
            if (sekunde == "") sekunde = "00";

            string vrijeme = sati + ":" + minute + ":" + sekunde;
            string finalniDatum = datumString + " " + vrijeme;
            return DateTime.Parse(finalniDatum);
        }

        private DateTime NapraviDatumPocetka()
        {
            DateTime datum = (DateTime)dtpDatumPocetka.SelectedDate;
            string sati = txtPocetakH.Text;
            string minute = txtPocetakM.Text;
            string sekunde = txtPocetakS.Text;

            if (sati == "") sati = "00";
            if (minute == "") minute = "00";
            if (sekunde == "") sekunde = "00";

            string finalniDatum = datum.ToString("yyyy-MM-dd") + " " + sati + ":" + minute + ":" + sekunde;
            return DateTime.Parse(finalniDatum);
        }

        private bool ProvjeriPodatke()
        {
            bool ispravni = false;

            if (int.TryParse(txtKrajH.Text, out int krajH) && int.TryParse(txtKrajM.Text, out int krajM) && int.TryParse(txtKrajS.Text, out int krajS)
                && int.TryParse(txtPocetakH.Text, out int pocH) && int.TryParse(txtPocetakM.Text, out int pocM) && int.TryParse(txtPocetakS.Text, out int pocS))
            {
                if (txtNaziv.Text != "" && dtpDatumPocetka.SelectedDate != null
                && dtpDatumZavrsetka.SelectedDate != null && cmbClanKucanstva.SelectedItem != null
                && krajH <= 24 && krajH >= 0 && pocH <= 24 && pocH >= 0
                && krajM <= 59 && krajM >= 0 && pocM <= 59 && pocM >= 0
                && krajS <= 59 && krajS >= 0 && pocS <= 59 && pocS >= 0)
                {
                    ispravni = true;
                }
            }

            return ispravni;
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            _sadrzaj.Content = new FormaZaPrikazKalendara(_sadrzaj);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PopuniComboBox();
        }

        private void PopuniComboBox()
        {
            var repo = new KorisnikRepozitorij();
            KorisnikServis servis = new KorisnikServis(repo);
            cmbClanKucanstva.ItemsSource = servis.DohvatiKorisnike();
        }
    }
}
