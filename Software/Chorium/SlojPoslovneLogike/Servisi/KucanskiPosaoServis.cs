using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.VisualBasic.FileIO;
using OpenTK.Graphics.OpenGL;
using SlojEntiteta.Entiteti;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using SlojUpravljanjaSBazomPodataka.Sucelja;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlojPoslovneLogike.Servisi {
    public class KucanskiPosaoServis {
        private IKucanskiPosaoRepozitorij _repozitorij;
        private IKorisnikRepozitorij _korRepozitorij;
        public KucanskiPosaoServis(IKucanskiPosaoRepozitorij repozitorij, IKorisnikRepozitorij korRepozitorij) {
            _repozitorij = repozitorij;
            _korRepozitorij = korRepozitorij;
        }

        public KucanskiPosaoServis(IKucanskiPosaoRepozitorij repozitorij) {
            _repozitorij = repozitorij;
        }

        private readonly SmtpClient smtpClient = new SmtpClient("smtp.gmail.com") {
            Port = 587,
            Credentials = new NetworkCredential("stanko.smrcek2329@gmail.com", "sjpgyjxvtdbzfbjy"),
            EnableSsl = true,
        };
        public void PostaviSlanjeMaila(DateTime datumSada) {
            var datumUjutro = new DateTime(datumSada.Year, datumSada.Month, datumSada.Day, 8, 0, 0);
            var datumNavecer = new DateTime(datumSada.Year, datumSada.Month, datumSada.Day, 21, 0, 0);
            PostaviTimer(datumSada, datumUjutro, true);
            PostaviTimer(datumSada, datumNavecer, false);
        }

        private void PostaviTimer(DateTime datumSada, DateTime datum, bool jutro) {
            TimeSpan ts;
            if (datum >= datumSada)
                ts = datum - datumSada;
            else {
                datum = datum.AddDays(1);
                ts = datum - datumSada;
            }

            Task.Delay(ts).ContinueWith((x) => {

                if (jutro) {
                    var servis = new KorisnikServis(_korRepozitorij);
                    List<Korisnik> korisnici = servis.DohvatiKorisnike();

                    foreach (Korisnik korisnik in korisnici) {
                        MailMessage mail = PostaviUjutro(korisnik, DateTime.Now);
                        if (mail != null) smtpClient.Send(mail);
                    }
                } else {
                    var servis = new KorisnikServis(_korRepozitorij);
                    List<Korisnik> korisnici = servis.DohvatiKorisnike();

                    foreach (Korisnik korisnik in korisnici) {
                        MailMessage mail = PostaviNavecer(korisnik, DateTime.Now);
                        if (mail != null) smtpClient.Send(mail);
                    }
                }

                PostaviTimer(DateTime.Now, datum.AddDays(1), jutro);
            });

        }

        public MailMessage PostaviNavecer(Korisnik korisnik, DateTime datum) {
            List<Kucanski_posao> kucanskiPoslovi = _repozitorij.DohvatiPosloveNavecer(datum, korisnik).ToList();

            if (kucanskiPoslovi.Count > 0) {
                StringBuilder porukaBuilder = new StringBuilder();
                StringBuilder porukaObavljeniBuilder = new StringBuilder("<html><body><h3>Obavljeni poslovi:</h3><ul>");
                StringBuilder porukaNeobavljeniBuilder = new StringBuilder("<h3>Neobavljeni poslovi:</h3><ul>");
                foreach (Kucanski_posao kucanskiPosao in kucanskiPoslovi) {
                    if (kucanskiPosao.ID_status == 1) {
                        porukaObavljeniBuilder.Append("<li><b>").Append(kucanskiPosao.Naziv).Append("</b></li>");
                    } else {
                        porukaNeobavljeniBuilder.Append("<li><b>").Append(kucanskiPosao.Naziv).Append("</b> napravi do ").Append(kucanskiPosao.Datum_kraja.ToString("HH:mm")).Append("</li>");
                    }
                }
                porukaObavljeniBuilder.Append("</ul>");
                porukaNeobavljeniBuilder.Append("</ul></body></html>");
                porukaBuilder.Append(porukaObavljeniBuilder).Append(porukaNeobavljeniBuilder);
                var mail = new MailMessage {
                    From = new MailAddress("stanko.smrcek2329@gmail.com"),
                    Subject = "Popis poslova za " + DateTime.Now.ToString("d.M.yyyy") + " - Chorium",
                    Body = porukaBuilder.ToString(),
                    IsBodyHtml = true,
                };
                mail.To.Add(korisnik.Email);

                return mail;
            }
            return null;
        }

        public MailMessage PostaviUjutro(Korisnik korisnik, DateTime datum) {
            List<Kucanski_posao> kucanskiPoslovi = _repozitorij.DohvatiPosloveUjutro(datum, korisnik).ToList();

            if (kucanskiPoslovi.Count > 0) {
                StringBuilder porukaBuilder = new StringBuilder("<html><body><h3>Poslovi koje trebaš obaviti:</h3><ul>");
                foreach (Kucanski_posao kucanskiPosao in kucanskiPoslovi) {
                    porukaBuilder.Append("<li><b>").Append(kucanskiPosao.Naziv).Append("</b> napravi do ").Append(kucanskiPosao.Datum_kraja.ToString("HH:mm")).Append("</li>");
                }
                porukaBuilder.Append("</ul></body></html>");
                var mail = new MailMessage {
                    From = new MailAddress("stanko.smrcek2329@gmail.com"),
                    Subject = "Popis poslova za " + DateTime.Now.ToString("d.M.yyyy") + " - Chorium",
                    Body = porukaBuilder.ToString(),
                    IsBodyHtml = true,
                };
                mail.To.Add(korisnik.Email);

                return mail;
            }
            return null;

        }

        public List<Kucanski_posao> PrikaziPoslove() {
            List<Kucanski_posao> poslovi = new List<Kucanski_posao>();
            poslovi = _repozitorij.DajSve().ToList();
            return poslovi;

        }

        public List<Kucanski_posao> PrikaziPoslove(Korisnik korisnik, Status status, Kategorija kategorija) {
            List<Kucanski_posao> poslovi = new List<Kucanski_posao>();
            poslovi = _repozitorij.DohvatiPosloveKorisnika(korisnik).ToList();
            poslovi = poslovi.FindAll(e => e.Status.ID == status.ID);
            poslovi = poslovi.FindAll(e => e.Kategorija.ID == kategorija.ID);
            return poslovi;
        }

        public List<Kucanski_posao> PrikaziPoslove(Korisnik korisnik) {
            List<Kucanski_posao> poslovi = new List<Kucanski_posao>();
            poslovi = _repozitorij.DohvatiPosloveKorisnika(korisnik).ToList();
            return poslovi;
        }

        public List<KorisnikPosloviTablica> GenerirajPopisPoslova(DateTime datum) {
            List<KorisnikPosloviTablica> popisPoslova = new List<KorisnikPosloviTablica>();
            var servis = new KorisnikServis(_korRepozitorij);
            List<Korisnik> korisnici = servis.DohvatiKorisnike();
            foreach (Korisnik korisnik in korisnici) {
                var poslovi = _repozitorij.DohvatiObavljenePosloveKorisnika(datum, korisnik);
                foreach (var posao in poslovi) {
                    KorisnikPosloviTablica korisnikPosloviTablica = new KorisnikPosloviTablica(korisnik.Korisnicko_ime, posao.Naziv, posao.Datum_pocetka, posao.Korisnik.Korisnicko_ime, posao.Kategorija.Naziv);
                    popisPoslova.Add(korisnikPosloviTablica);
                }
            }
            return popisPoslova;
        }

        public List<KorisnikPoslovi> GenerirajGraf(DateTime datum) {
            List<KorisnikPoslovi> listaKorisnika = new List<KorisnikPoslovi>();
            var servis = new KorisnikServis(_korRepozitorij);
            List<Korisnik> korisnici = servis.DohvatiKorisnike();
            foreach (Korisnik korisnik in korisnici) {
                var poslovi = _repozitorij.DohvatiObavljenePosloveKorisnika(datum, korisnik).Count();
                KorisnikPoslovi korisnikPoslovi = new KorisnikPoslovi(korisnik.Korisnicko_ime, poslovi);
                listaKorisnika.Add(korisnikPoslovi);
            }
            return listaKorisnika.OrderByDescending(k => k.BrojPoslova).ToList();
        }
        public void PostaviObavijest(DateTime vrijeme, Kucanski_posao posao) {
            TimeSpan ts = vrijeme - DateTime.Now;
            Task.Delay(ts).ContinueWith((x) => {

                new ToastContentBuilder()
                    .AddText("Vrijeme je da napraviš " + posao.Naziv + "!")
                    .Show();
            });
        }

        public bool DodajKucanskiPosao(Kucanski_posao posao) {
            int redovi = _repozitorij.Dodaj(posao);
            bool uspjeh = redovi > 0;
            return uspjeh;
        }

        public bool RijesiPosao(Kucanski_posao posao) {
            int redovi = _repozitorij.Rijesi(posao);
            bool uspjeh = redovi > 0;
            return uspjeh;
        }

        public string ProvjeriStatusPosla(Kucanski_posao posao) {
            return posao.Status.Naziv;
        }

        public bool StaviPosaoNaCekanje(Kucanski_posao posao) {
            int redovi = _repozitorij.StaviNaCekanje(posao);
            bool uspjeh = redovi > 0;
            return uspjeh;
        }

        public bool ObrisiPosao(Kucanski_posao posao) {
            int redovi = _repozitorij.Izbrisi(posao);
            bool uspjeh = redovi > 0;
            return uspjeh;
        }

        public bool AzurirajPosao(Kucanski_posao posao) {
            int redovi = _repozitorij.Azuriraj(posao);
            bool uspjeh = redovi > 0;
            return uspjeh;
        }
        public bool UpisiPodatke(string putDoDatoteke) {
            if (File.Exists(putDoDatoteke)){
                using (TextFieldParser parser = new TextFieldParser(putDoDatoteke)) {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    string[] headers = parser.ReadFields();
                    if (ProvjeriSadrzajHeadera(headers)) {
                        while (!parser.EndOfData) {
                            var posao = StvoriPosao(parser.ReadFields());
                            if (!DodajKucanskiPosao(posao)) {
                                return false;
                            }
                        } 
                    } else return false;


                }
            } else {
                return false;
            }
            return true;
        }

        public bool ProvjeriSadrzajHeadera(string[] header) {
            if (header != null) {
                if (header.Length == 7 && header[0] == "Naziv" && header[1] == "Datum_pocetka" && header[2] == "Datum_kraja" && header[3] == "ID_status" && header[4] == "ID_korisnik_dodao" && header[5] == "ID_kategorija" && header[6] == "ZaduzeniKorisnik") {
                    return true;
                }
            }
            
            MessageBox.Show("Nevažeći CSV format");
            return false;
        }

        public Kucanski_posao StvoriPosao(string[] posaoFields) {

            Status status = dohvatiObjektStatusa(Convert.ToInt32(posaoFields[3]));
            Korisnik korisnik = dohvatiObjektKorisnika(Convert.ToInt32(posaoFields[4]));
            Kategorija kategorija = dohvatiObjektKategorije(Convert.ToInt32(posaoFields[5]));
            List<Korisnik> korisnici = new List<Korisnik> {
                dohvatiObjektKorisnika(Convert.ToInt32(posaoFields[6]))
            };

            Kucanski_posao posao = new Kucanski_posao {
                Naziv = posaoFields[0],
                Datum_pocetka = Convert.ToDateTime(posaoFields[1]),
                Datum_kraja = Convert.ToDateTime(posaoFields[2]),
                Status = status,
                Korisnik = korisnik,
                Kategorija = kategorija,
                Korisnik1 = korisnici
            };
            return posao;    
        }

        private Korisnik dohvatiObjektKorisnika(int id) {
            KorisnikRepozitorij korisnikRepozitorij = new KorisnikRepozitorij();
            KorisnikServis korisnikServis = new KorisnikServis(korisnikRepozitorij);
            return korisnikServis.DohvatiKorisnike().Find(k => k.ID == id);
        }

        private Status dohvatiObjektStatusa(int id) {
            StatusRepozitorij statusRepozitorij = new StatusRepozitorij();
            StatusServis statusServis = new StatusServis(statusRepozitorij);
            return statusServis.DohvatiStatuse().Find(s => s.ID == id);
        }

        private Kategorija dohvatiObjektKategorije(int id) {
            KategorijaRepozitorij kategorijaRepozitorij = new KategorijaRepozitorij();
            KategorijaServis kategorijaServis = new KategorijaServis(kategorijaRepozitorij);
            return kategorijaServis.DohvatiKategorije().Find(k => k.ID == id);
        }
    }
}
