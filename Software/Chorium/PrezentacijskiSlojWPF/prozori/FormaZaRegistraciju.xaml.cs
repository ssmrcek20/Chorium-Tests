﻿using Emgu.CV;
using SlojEntiteta.Entiteti;
using SlojPoslovneLogike.Servisi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
using System.Windows.Controls;
using SlojUpravljanjaSBazomPodataka.repozitoriji;

namespace PrezentacijskiSlojWPF.prozori
{
    /// <summary>
    /// Interaction logic for FormaZaRegistraciju.xaml
    /// </summary>
    public partial class FormaZaRegistraciju : System.Windows.Controls.UserControl
    {
        private bool skeniranjeLica = false;
        System.Windows.Controls.ContentControl contentControl;
        MainWindow _main;
        public FormaZaRegistraciju(System.Windows.Controls.ContentControl sadrzaj, MainWindow main)
        {
            InitializeComponent();
            contentControl = sadrzaj;
            _main = main;
        }

        private void btnRegistriraj_Click(object sender, RoutedEventArgs e)
        {
            int tip = rbtnRoditelj.IsChecked == true ? 1 : 0;
            byte[][] lice = new byte[5][];
            if (skeniranjeLica && Directory.Exists(Directory.GetCurrentDirectory() + @"\slike"))
            {
                lice = ucitajLice();
            }

            if (validanUnos())
            {
                try
                {
                    var lozinkaServis = new LozinkaServis();
                    string lozinkaProblemi = lozinkaServis.ProblemiSaLozinkom(txtLozinka.Password, txtKorIme.Text);
                    if (!string.IsNullOrEmpty(lozinkaProblemi)){
                        MessageBox.Show(lozinkaProblemi);
                    }
                    else
                    {
                        if ((DateTime)dpDatumRodenja.SelectedDate < DateTime.Now)
                        {
                            registrirajKorinika(tip, lice);
                        }
                        else
                        {
                            MessageBox.Show("Datum rođenja nije validan!");
                        }
                    }
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Lozinka nesmije biti razmak!");
                }
                catch (FormatException)
                {
                    MessageBox.Show("Loša E-mail adresa!");
                }
            }
            else
            {
                MessageBox.Show("Za registraciju popuni sve!");
            }
        }

        private void registrirajKorinika(int tip, byte[][] lice)
        {
            MailAddress email = new MailAddress(txtMail.Text);
            try
            {
                var lozinkaServis = new LozinkaServis();
                string lozinka = lozinkaServis.HashirajLozinku(txtLozinka.Password, txtKorIme.Text);
                var korisnik = new Korisnik
                {
                    Ime = txtIme.Text,
                    Prezime = txtPrezime.Text,
                    Datum_rodenja = (DateTime)dpDatumRodenja.SelectedDate,
                    Korisnicko_ime = txtKorIme.Text,
                    Email = txtMail.Text,
                    Lozinka = lozinka,
                    ID_tip_korisnika = tip,
                    Lice = lice[0],
                    Lice2 = lice[1],
                    Lice3 = lice[2],
                    Lice4 = lice[3],
                    Lice5 = lice[4],
                };
                var repo = new KorisnikRepozitorij();
                var servis = new KorisnikServis(repo);
                bool uspjeh = servis.RegistrirajKorisnika(korisnik);

                if (!uspjeh)
                {
                    MessageBox.Show("To korisnčko ime već postoji!");
                }
                else
                {
                    MessageBox.Show("Korisnik je uspješno registriran!");
                    contentControl.Content = new FormaZaPrijavu(contentControl, _main);

                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Lozinka nesmije biti razmak!");
            }
        }

        private bool validanUnos()
        {
            return txtIme.Text != "" && txtPrezime.Text != "" && dpDatumRodenja.SelectedDate != null &&
                txtKorIme.Text != "" && txtMail.Text != "" && txtLozinka.Password != "" &&
                (rbtnDijete.IsChecked == true || rbtnRoditelj.IsChecked == true);
        }

        private byte[][] ucitajLice()
        {
            byte[][] lice = new byte[5][];
            int broj = 0;
            string putanja = Directory.GetCurrentDirectory() + @"\slike";
            string[] slike = Directory.GetFiles(putanja, "*.jpg", SearchOption.AllDirectories);
            foreach (string slikaTekst in slike)
            {
                System.Drawing.Image slika = System.Drawing.Image.FromFile(slikaTekst);
                ImageConverter imageConverter = new ImageConverter();
                lice[broj] = (byte[])imageConverter.ConvertTo(slika, typeof(byte[]));
                broj++;
            }
            return lice;
        }

        private void btnLice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var capture = new Capture())
                {
                    var frame = capture.QueryFrame();
                    if (frame != null)
                    {
                        var repo = new KorisnikRepozitorij();
                        var servis = new KorisnikServis(repo);
                        servis.SkeniranjeLica();
                        skeniranjeLica = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska!");
            }
        }
    }
}
