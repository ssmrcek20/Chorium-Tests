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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrezentacijskiSlojWPF.prozori
{
    /// <summary>
    /// Interaction logic for FormaZaDodavanjePosla.xaml
    /// </summary>
    public partial class FormaZaDodavanjePosla : System.Windows.Controls.UserControl
    {
        private ContentControl _sadrzaj;
        public FormaZaDodavanjePosla(ContentControl sadrzaj)
        {
            InitializeComponent();
            _sadrzaj = sadrzaj;
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            _sadrzaj.Content = new FormaZaPrikazPoslova(_sadrzaj);
        }

        private void btnDodajPosao_Click(object sender, RoutedEventArgs e)
        {
            if (ProvjeriPodatke())
            {
                string nazivPosla = txtNazivPosla.Text;
                DateTime datumRoka = NapraviDatumRoka();
                Kategorija kategorija = cmbKategorija.SelectedItem as Kategorija;

                var repo = new KorisnikRepozitorij();
                KorisnikServis korisnikServis = new KorisnikServis(repo);
                Korisnik trenutniKorisnik = korisnikServis.DohvatiTrenutnogKorisnika();

                Status status = new Status
                {
                    ID = 0,
                    Naziv = "nedovrsen"
                };

                List<Korisnik> zaduzeniKorisnici = new List<Korisnik>();
                foreach (var kor in lvZaduzeniClanovi.SelectedItems)
                {
                    zaduzeniKorisnici.Add(kor as Korisnik);
                }

                if (korisnikServis.ProvjeriDobneGranice(zaduzeniKorisnici, kategorija))
                {
                    Kucanski_posao noviPosao = new Kucanski_posao
                    {
                        Naziv = nazivPosla,
                        Datum_pocetka = DateTime.Now,
                        Datum_kraja = datumRoka,
                        Status = status,
                        Korisnik = trenutniKorisnik,
                        Kategorija = kategorija,
                        Korisnik1 = zaduzeniKorisnici,
                    };

                    var posaoRepo = new KucanskiPosaoRepozitorij();
                    KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);

                    bool uspjesnoDodan = kucanskiPosaoServis.DodajKucanskiPosao(noviPosao);
                    if (uspjesnoDodan)
                    {
                        _sadrzaj.Content = new FormaZaPrikazPoslova(_sadrzaj);
                    }

                }
                else
                {
                    System.Windows.MessageBox.Show("Dobna granica kategorije je previsoka za neke od odabranih korisnika.");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Provjerite podatke.");
            }
        }

        private bool ProvjeriPodatke()
        {
            bool ispravni = false;

            if (int.TryParse(txtRokH.Text, out int rokH) && int.TryParse(txtRokM.Text, out int rokM) && int.TryParse(txtRokS.Text, out int rokS))
            {
                if (txtNazivPosla.Text != "" && dtpDatumRoka.SelectedDate != null
                && cmbKategorija.SelectedItem != null && lvZaduzeniClanovi.SelectedItems != null
                && rokH <= 24 && rokH >= 0 && rokM <= 59 && rokM >= 0 && rokS <= 59 && rokS >= 0)
                {
                    ispravni = true;
                }
            }

            return ispravni;
        }

        

        private DateTime NapraviDatumRoka()
        {
            DateTime datum = (DateTime)dtpDatumRoka.SelectedDate;
            string sati = txtRokH.Text;
            string minute = txtRokM.Text;
            string sekunde = txtRokS.Text;

            if (sati == "") sati = "00";
            if (minute == "") minute = "00";
            if (sekunde == "") sekunde = "00";

            string finalniDatum = datum.ToString("yyyy-MM-dd") + " " + sati + ":" + minute + ":" + sekunde;
            return DateTime.Parse(finalniDatum);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PopuniComboBox();
            PopuniListBox();
        }

        private void PopuniListBox()
        {
            var repo = new KorisnikRepozitorij();
            KorisnikServis korisnikServis = new KorisnikServis(repo);
            lvZaduzeniClanovi.ItemsSource = korisnikServis.DohvatiKorisnike();
        }

        private void PopuniComboBox()
        {
            KategorijaRepozitorij kategorijaRepozitorij = new KategorijaRepozitorij();
            KategorijaServis kategorijaServis = new KategorijaServis(kategorijaRepozitorij);
            cmbKategorija.ItemsSource = kategorijaServis.DohvatiKategorije();
        }

        private void cmbKategorija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Kategorija odabranaKategorija = cmbKategorija.SelectedItem as Kategorija;
            txtDobnaGranica.Text = odabranaKategorija.Dobna_granica.ToString();
        }

        private OpenFileDialog stvoriObjektZaCitanjeDatoteke() {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            return openFileDialog;
        }

        private void btnUveziCSV_Click(object sender, RoutedEventArgs e) {

            OpenFileDialog openFileDialog = stvoriObjektZaCitanjeDatoteke();

            DialogResult rezultat = openFileDialog.ShowDialog();

            if (rezultat == DialogResult.OK) {
                string putDoOdabraneDatoteke = openFileDialog.FileName;
                var posaoRepo = new KucanskiPosaoRepozitorij();
                KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);
                kucanskiPosaoServis.UpisiPodatke(putDoOdabraneDatoteke);
            }
        }
    }
}
