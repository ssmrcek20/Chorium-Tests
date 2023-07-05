﻿using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Face;
using SlojEntiteta.Entiteti;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Diagnostics;
using SlojUpravljanjaSBazomPodataka.Sucelja;

namespace SlojPoslovneLogike.Servisi
{
    public class KorisnikServis
    {
        private IKorisnikRepozitorij _repotiorij;
        private Capture dohvatiVideo = null;
        private EigenFaceRecognizer recognizer;

        public static Korisnik PrijavljeniKorisnik = null;

        public KorisnikServis(IKorisnikRepozitorij repozitorij)
        {
            _repotiorij = repozitorij;
        }

        public Korisnik DohvatiTrenutnogKorisnika()
        {
            return PrijavljeniKorisnik;
        }

        public bool ProvjeriKorisnika()
        {
            bool tipRoditelj = false;
            if (PrijavljeniKorisnik.ID_tip_korisnika == 1)
            {
                tipRoditelj = true;
            }
            return tipRoditelj;
        }

        public bool RegistrirajKorisnika(Korisnik korisnik)
        {
            bool uspjeh = false;

            bool korisnikPostoji = _repotiorij.ProvjeriKorisnikoIme(korisnik.Korisnicko_ime);
            if (!korisnikPostoji)
            {
                int uredeniRedovi = _repotiorij.Dodaj(korisnik);
                uspjeh = uredeniRedovi > 0;
            }

            return uspjeh;
        }

        public EigenFaceRecognizer StvoriFaceRecognizer(List<Image<Gray, Byte>> slike)
        {
            if (slike.Count > 0)
            {
                var labele = new List<int>();
                for (int i = 1; i <= slike.Count; i++)
                {
                    labele.Add(i);
                }
                recognizer = new EigenFaceRecognizer(slike.Count, 2000);
                recognizer.Train(slike.ToArray(), labele.ToArray());
                return recognizer;
            } else return null;
        }

        public List<Image<Gray, Byte>> popuniPopisSlika(List<Korisnik> korisnici)
        {
            List<Image<Gray, Byte>> slike = new List<Image<Gray, byte>>();
            foreach (var korisnik in korisnici)
            {
                if (korisnik.Lice != null && korisnik.Lice2 != null && korisnik.Lice3 != null && korisnik.Lice4 != null && korisnik.Lice5 != null)
                {

                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(korisnik.Lice));
                    Bitmap bmp = (Bitmap)x;
                    Image<Gray, Byte> emguImage = new Image<Gray, Byte>(bmp);
                    slike.Add(emguImage);

                    Image x2 = (Bitmap)((new ImageConverter()).ConvertFrom(korisnik.Lice2));
                    Bitmap bmp2 = (Bitmap)x2;
                    Image<Gray, Byte> emguImage2 = new Image<Gray, Byte>(bmp2);
                    slike.Add(emguImage2);

                    Image x3 = (Bitmap)((new ImageConverter()).ConvertFrom(korisnik.Lice3));
                    Bitmap bmp3 = (Bitmap)x3;
                    Image<Gray, Byte> emguImage3 = new Image<Gray, Byte>(bmp3);
                    slike.Add(emguImage3);

                    Image x4 = (Bitmap)((new ImageConverter()).ConvertFrom(korisnik.Lice4));
                    Bitmap bmp4 = (Bitmap)x4;
                    Image<Gray, Byte> emguImage4 = new Image<Gray, Byte>(bmp4);
                    slike.Add(emguImage4);

                    Image x5 = (Bitmap)((new ImageConverter()).ConvertFrom(korisnik.Lice5));
                    Bitmap bmp5 = (Bitmap)x5;
                    Image<Gray, Byte> emguImage5 = new Image<Gray, Byte>(bmp5);
                    slike.Add(emguImage5);

                }
            }
            return slike;
        }

        public List<Korisnik> popuniPopisKorisnikaSaSlikama(List<Korisnik> korisnici)
        {
            List<Korisnik> popisKorisnika = new List<Korisnik>();
            foreach (var korisnik in korisnici)
            {
                if (korisnik.Lice != null && korisnik.Lice2 != null && korisnik.Lice3 != null && korisnik.Lice4 != null && korisnik.Lice5 != null)
                {
                    popisKorisnika.Add(korisnik);
                }
            }
            return popisKorisnika;
        }

        public bool procjeniLice(EigenFaceRecognizer recognizer, List<Korisnik> korisniciSaSlikama)
        {
            string path = Directory.GetCurrentDirectory() + @"\slike";
            string[] files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories);
            Image<Gray, byte> slikaZaUsporedbu = new Image<Gray, byte>(files[0]).Resize(200, 200, Inter.Cubic);
            var result = recognizer.Predict(slikaZaUsporedbu);
            if (result.Label != -1 && result.Distance < 2000)
            {
                PrijaviKorisnik(korisniciSaSlikama[(result.Label - 1) / 5]);
                Console.WriteLine("Korisnik prijavljen" + korisniciSaSlikama[(result.Label - 1) / 5].Korisnicko_ime);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TrenirajLica()
        {

            var korisnici = _repotiorij.DajSve().ToList();

            List<Image<Gray, Byte>> slike = popuniPopisSlika(korisnici);
            var korisniciSaSlikama = popuniPopisKorisnikaSaSlikama(korisnici);
            var recognizer = StvoriFaceRecognizer(slike);

            SkenirajLice();

            if (procjeniLice(recognizer,korisniciSaSlikama))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void SkeniranjeLica()
        {
            string putanja = Directory.GetCurrentDirectory() + @"\slike";
            if (Directory.Exists(putanja))
            {
                Directory.Delete(putanja, true);
            }
            string[] files;
            do
            {
                dohvatiVideo = new Capture();
                dohvatiVideo.ImageGrabbed += DohvatiOkvir();
                dohvatiVideo.Start();
                files = Directory.GetFiles(putanja, "*.jpg", SearchOption.AllDirectories);
            } while (files.Length<5);

        }

        public void SkenirajLice()
        {
            string putanja = Directory.GetCurrentDirectory() + @"\slike";
            if (Directory.Exists(putanja))
            {
                Directory.Delete(putanja, true);
            }
            string[] files;
            do
            {
                dohvatiVideo = new Capture();
                dohvatiVideo.ImageGrabbed += DohvatiOkvir();
                dohvatiVideo.Start();
                files = Directory.GetFiles(putanja, "*.jpg", SearchOption.AllDirectories);
            } while (files.Length < 1);

        }

        private EventHandler DohvatiOkvir()
        {
            CascadeClassifier cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_alt.xml");
            Mat okvir = new Mat();

            dohvatiVideo.Retrieve(okvir, 0);
            Image<Bgr, Byte> trenutniOkvir = okvir.ToImage<Bgr, Byte>();

            Mat sivaSlika = new Mat();
            CvInvoke.CvtColor(trenutniOkvir, sivaSlika, ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(sivaSlika, sivaSlika);

            Rectangle[] lica = cascadeClassifier.DetectMultiScale(sivaSlika, 1.1, 3, Size.Empty, Size.Empty);
            if (lica.Length > 0)
            {
                Image<Gray, Byte> rezultatSlika = trenutniOkvir.Convert<Gray, Byte>();
                rezultatSlika.ROI = lica[0];

                string putanja = Directory.GetCurrentDirectory() + @"\slike";
                if (!Directory.Exists(putanja))
                {
                    Directory.CreateDirectory(putanja);
                }
                rezultatSlika.Resize(200, 200, Inter.Cubic).Save(putanja + @"\korisnik-" + DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss") + ".jpg");
            }

            dohvatiVideo.Stop();
            dohvatiVideo.Dispose();
            return null;
        }

        public List<Korisnik> DohvatiKorisnike()
        {
            return _repotiorij.DajSve().ToList();
        }

        public bool ProvjeriDobneGranice(List<Korisnik> zaduzeniKorisnici, Kategorija kategorija)
        {
            bool ispravno = true;

            foreach (var kor in zaduzeniKorisnici)
            {
                var razlika = DateTime.Now - kor.Datum_rodenja;
                if (razlika.Days / 365 < kategorija.Dobna_granica) ispravno = false;
            }

            return ispravno;
        }

        public bool ProvjeriIspravnostPodataka(string korime, string lozinka)
        {
            Korisnik korisnik;
            bool autentificiran = false;

            korisnik = _repotiorij.DohvatiKorisnika(korime);

            if (korisnik != null && korisnik.Lozinka == lozinka)
            {
                PrijaviKorisnik(korisnik);
                autentificiran = true;
            }

            return autentificiran;


        }

        private void PrijaviKorisnik(Korisnik korisnik)
        {
            PrijavljeniKorisnik = korisnik;
        }

        public int AzurirajKorisnika(Korisnik korisnik)
        {
            return _repotiorij.Azuriraj(korisnik);
        }
    }
}
