using FakeItEasy;
using SlojEntiteta.Entiteti;
using SlojPoslovneLogike;
using SlojPoslovneLogike.Servisi;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using SlojUpravljanjaSBazomPodataka.Sucelja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JedinicnoTestiranje
{
    public class KucanskiPosaoServis_Tests
    {
        [Fact]
        public void PostaviSlanjeMaila_GivenItIs8InTheMorning_RunPostaviUjutro()
        {
            List<Korisnik> korisnik = new List<Korisnik>
            {
                new Korisnik{Ime = "test",Prezime = "test",Email = "test.test@gmail.com",Korisnicko_ime = "test",Lozinka = "test",ID_tip_korisnika = 1,Datum_rodenja = new DateTime(1990, 1, 1)},
                new Korisnik{Ime = "test",Prezime = "test",Email = "test.test@gmail.com",Korisnicko_ime = "test2",Lozinka = "test",ID_tip_korisnika = 1,Datum_rodenja = new DateTime(1990, 1, 1)}
            };
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            A.CallTo(() => posaoRepo.DohvatiPosloveUjutro(DateTime.Now, korisnik[0])).Returns(null);
            var korRepo = A.Fake<IKorisnikRepozitorij>();
            A.CallTo(() => korRepo.DajSve()).Returns(korisnik.AsQueryable());
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo, korRepo);
            var datumUjutro = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);


            servis.PostaviSlanjeMaila(datumUjutro);

        }

        [Fact]
        public void PostaviSlanjeMaila_GivenItIs21InTheEvening_RunPostaviNavecer()
        {
            List<Korisnik> korisnik = new List<Korisnik>
            {
                new Korisnik{Ime = "test",Prezime = "test",Email = "test.test@gmail.com",Korisnicko_ime = "test",Lozinka = "test",ID_tip_korisnika = 1,Datum_rodenja = new DateTime(1990, 1, 1)},
                new Korisnik{Ime = "test",Prezime = "test",Email = "test.test@gmail.com",Korisnicko_ime = "test2",Lozinka = "test",ID_tip_korisnika = 1,Datum_rodenja = new DateTime(1990, 1, 1)}
            };
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            A.CallTo(() => posaoRepo.DohvatiPosloveNavecer(DateTime.Now, korisnik[0])).Returns(null);
            var korRepo = A.Fake<IKorisnikRepozitorij>();
            A.CallTo(() => korRepo.DajSve()).Returns(korisnik.AsQueryable());
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo, korRepo);
            var datumNavecer = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 0, 0);


            servis.PostaviSlanjeMaila(datumNavecer);

        }

        [Fact]
        public void PostaviNavecer_GivenKorisniDontHaveJobs_ReturnNull()
        {
            Korisnik korisnik = new Korisnik
            {
                Ime = "test",
                Prezime = "test",
                Email = "test.test@gmail.com",
                Korisnicko_ime = "test",
                Lozinka = "test",
                ID_tip_korisnika = 1,
                Datum_rodenja = new DateTime(1990, 1, 1)
            };
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            A.CallTo(() => posaoRepo.DohvatiPosloveNavecer(DateTime.Now, korisnik)).Returns(new List<Kucanski_posao>().AsQueryable());
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo);

            var mail = servis.PostaviNavecer(korisnik, DateTime.Now);

            Assert.Null(mail);
        }

        [Fact]
        public void PostaviNavecer_GivenKorisniHaveJobs_ReturnMail()
        {
            Korisnik korisnik = new Korisnik
            {
                Ime = "test",
                Prezime = "test",
                Email = "test.test@gmail.com",
                Korisnicko_ime = "test",
                Lozinka = "test",
                ID_tip_korisnika = 1,
                Datum_rodenja = new DateTime(1990, 1, 1)
            };
            List<Kucanski_posao> kucanksiPosao = new List<Kucanski_posao>
            {
                new Kucanski_posao{ID = 1,
                Naziv = "Obavljen posao",
                Datum_pocetka = new DateTime(2023, 5, 29),
                Datum_kraja = new DateTime(2023, 6, 1),
                ID_status = 1,
                ID_korisnik_dodao = 2,
                ID_kategorija = 3,},
                new Kucanski_posao{ID = 1,
                Naziv = "Ne obavljen posao",
                Datum_pocetka = new DateTime(2023, 5, 29),
                Datum_kraja = new DateTime(2023, 6, 1),
                ID_status = 0,
                ID_korisnik_dodao = 2,
                ID_kategorija = 3,}
            };
            MailMessage ocekivaniMail = new MailMessage();
            ocekivaniMail.Body = "<html><body><h3>Obavljeni poslovi:</h3><ul><li><b>Obavljen posao</b></li></ul><h3>Neobavljeni poslovi:</h3><ul><li><b>Ne obavljen posao</b> napravi do 00:00</li></ul></body></html>";
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            A.CallTo(() => posaoRepo.DohvatiPosloveNavecer(A<DateTime>.Ignored, korisnik)).Returns(kucanksiPosao.AsQueryable());
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo);

            var mail = servis.PostaviNavecer(korisnik, DateTime.Now);

            Assert.Equal(ocekivaniMail.Body, mail.Body);
        }

        [Fact]
        public void PostaviUjutro_GivenKorisniDontHaveJobs_ReturnNull()
        {
            Korisnik korisnik = new Korisnik
            {
                Ime = "test",
                Prezime = "test",
                Email = "test.test@gmail.com",
                Korisnicko_ime = "test",
                Lozinka = "test",
                ID_tip_korisnika = 1,
                Datum_rodenja = new DateTime(1990, 1, 1)
            };
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            A.CallTo(() => posaoRepo.DohvatiPosloveUjutro(DateTime.Now, korisnik)).Returns(new List<Kucanski_posao>().AsQueryable());
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo);

            var mail = servis.PostaviUjutro(korisnik, DateTime.Now);

            Assert.Null(mail);
        }

        [Fact]
        public void PostaviUjutro_GivenKorisniHaveJobs_ReturnMail()
        {
            Korisnik korisnik = new Korisnik
            {
                Ime = "test",
                Prezime = "test",
                Email = "test.test@gmail.com",
                Korisnicko_ime = "test",
                Lozinka = "test",
                ID_tip_korisnika = 1,
                Datum_rodenja = new DateTime(1990, 1, 1)
            };
            List<Kucanski_posao> kucanksiPosao = new List<Kucanski_posao>
            {
                new Kucanski_posao{ID = 1,
                Naziv = "Ne obavljen posao",
                Datum_pocetka = new DateTime(2023, 5, 29),
                Datum_kraja = new DateTime(2023, 6, 1),
                ID_status = 0,
                ID_korisnik_dodao = 2,
                ID_kategorija = 3,}
            };
            MailMessage ocekivaniMail = new MailMessage();
            ocekivaniMail.Body = "<html><body><h3>Poslovi koje trebaš obaviti:</h3><ul><li><b>Ne obavljen posao</b> napravi do 00:00</li></ul></body></html>";
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            A.CallTo(() => posaoRepo.DohvatiPosloveUjutro(A<DateTime>.Ignored, korisnik)).Returns(kucanksiPosao.AsQueryable());
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo);

            var mail = servis.PostaviUjutro(korisnik, DateTime.Now);

            Assert.Equal(ocekivaniMail.Body, mail.Body);
        }

        [Fact]
        public void GenerirajGraf_GivenUsersHaveJobs_ReturnValidListOfUsersSortedAscending()
        {
            DateTime datum = new DateTime(2023, 5, 30);
            List<Korisnik> korisnici = new List<Korisnik>
            {
                new Korisnik { Korisnicko_ime = "korisnik1" },
                new Korisnik { Korisnicko_ime = "korisnik2" }
            };
            List<Kucanski_posao> kucanksiPosao1 = new List<Kucanski_posao>
            {
                new Kucanski_posao{}
            };
            List<Kucanski_posao> kucanksiPosao2 = new List<Kucanski_posao>
            {
                new Kucanski_posao{},
                new Kucanski_posao{}
            };
            List<KorisnikPoslovi> ocekivaniRezultat = new List<KorisnikPoslovi>
            {
                new KorisnikPoslovi("korisnik2", 2),
                new KorisnikPoslovi("korisnik1", 1)                
            };
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            A.CallTo(() => posaoRepo.DohvatiObavljenePosloveKorisnika(A<DateTime>.Ignored, korisnici[0])).Returns(kucanksiPosao1.AsQueryable());
            A.CallTo(() => posaoRepo.DohvatiObavljenePosloveKorisnika(A<DateTime>.Ignored, korisnici[1])).Returns(kucanksiPosao2.AsQueryable());
            var korRepo = A.Fake<IKorisnikRepozitorij>();
            A.CallTo(() => korRepo.DajSve()).Returns(korisnici.AsQueryable());
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo, korRepo);

            var rezultat = servis.GenerirajGraf(datum);

            Assert.Equal(ocekivaniRezultat[0].KorisnickoIme, rezultat[0].KorisnickoIme);
            Assert.Equal(ocekivaniRezultat[1].KorisnickoIme, rezultat[1].KorisnickoIme);
        }

        [Fact]
        public void GenerirajPopisPoslova_GivenUsersHaveJobs_ReturnValidListOfKorisnikPosloviTablica()
        {
            DateTime datum = new DateTime(2023, 5, 30);
            List<Korisnik> korisnici = new List<Korisnik>
            {
                new Korisnik { Korisnicko_ime = "korisnik1" },
                new Korisnik { Korisnicko_ime = "korisnik2" }
            };
            List<Kucanski_posao> posloviKorisnika1 = new List<Kucanski_posao>
            {
                new Kucanski_posao
                {
                    Naziv = "posao1",
                    Datum_pocetka = new DateTime(2023, 5, 30),
                    Korisnik = new Korisnik { Korisnicko_ime = "korisnik1" },
                    Kategorija = new Kategorija { Naziv = "kategorija1" }
                }
            };
            List<Kucanski_posao> posloviKorisnika2 = new List<Kucanski_posao>
            {
                new Kucanski_posao
                {
                    Naziv = "posao2",
                    Datum_pocetka = new DateTime(2023, 5, 30),
                    Korisnik = new Korisnik { Korisnicko_ime = "korisnik2" },
                    Kategorija = new Kategorija { Naziv = "kategorija2" }
                },
                new Kucanski_posao
                {
                    Naziv = "posao3",
                    Datum_pocetka = new DateTime(2023, 5, 30),
                    Korisnik = new Korisnik { Korisnicko_ime = "korisnik2" },
                    Kategorija = new Kategorija { Naziv = "kategorija3" }
                }
            };
            List<KorisnikPosloviTablica> ocekivaniRezultat = new List<KorisnikPosloviTablica>
            {
                new KorisnikPosloviTablica("korisnik1", "posao1", new DateTime(2023, 5, 30), "korisnik1", "kategorija1"),
                new KorisnikPosloviTablica("korisnik2", "posao2", new DateTime(2023, 5, 30), "korisnik2", "kategorija2"),
                new KorisnikPosloviTablica("korisnik2", "posao3", new DateTime(2023, 5, 30), "korisnik2", "kategorija3")
            };
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            A.CallTo(() => posaoRepo.DohvatiObavljenePosloveKorisnika(A<DateTime>.Ignored, korisnici[0])).Returns(posloviKorisnika1.AsQueryable());
            A.CallTo(() => posaoRepo.DohvatiObavljenePosloveKorisnika(A<DateTime>.Ignored, korisnici[1])).Returns(posloviKorisnika2.AsQueryable());
            var korRepo = A.Fake<IKorisnikRepozitorij>();
            A.CallTo(() => korRepo.DajSve()).Returns(korisnici.AsQueryable());
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo, korRepo);

            var rezultat = servis.GenerirajPopisPoslova(datum);

            Assert.Equal(ocekivaniRezultat[0].Naziv, rezultat[0].Naziv);
            Assert.Equal(ocekivaniRezultat[1].Naziv, rezultat[1].Naziv);
            Assert.Equal(ocekivaniRezultat[2].Naziv, rezultat[2].Naziv);
        }

        [Fact]
        public void PostaviObavijest_GivenTimeIsNow_SendNotification()
        {
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo);

            servis.PostaviObavijest(DateTime.Now, new Kucanski_posao());

        }

        [Fact]
        public void DodajKucanskiPosao_GivenValidKucanskiPosao_ReturnsTrue()
        {
            Kucanski_posao kucanski_Posao = new Kucanski_posao();
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            A.CallTo(() => posaoRepo.Dodaj(kucanski_Posao)).Returns(1);
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);

            bool provjera = kucanskiPosaoServis.DodajKucanskiPosao(kucanski_Posao);

            Assert.True(provjera);
        }

        [Fact]
        public void RijesiPosao_GivenValidKucanskiPosao_ReturnsTrue()
        {
            Kucanski_posao kucanski_Posao = new Kucanski_posao();
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            A.CallTo(() => posaoRepo.Rijesi(kucanski_Posao)).Returns(1);
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);

            bool provjera = kucanskiPosaoServis.RijesiPosao(kucanski_Posao);

            Assert.True(provjera);
        }

        [Fact]
        public void ProvjeriStatusPosla_GivenValidKucanskiPosaoNedovsen_ReturnsNedovrsen()
        {
            Status status = new Status
            {
                Naziv = "nedovrsen"
            };
            Kucanski_posao kucanski_Posao = new Kucanski_posao
            {
                Status = status
            };
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);

            string nazivStatusa = kucanskiPosaoServis.ProvjeriStatusPosla(kucanski_Posao);

            Assert.Equal(status.Naziv, nazivStatusa);
        }

        [Fact]
        public void ProvjeriStatusPosla_GivenValidKucanskiPosaoNaCekanju_ReturnsNaCekanju()
        {
            Status status = new Status
            {
                Naziv = "na_cekanju"
            };
            Kucanski_posao kucanski_Posao = new Kucanski_posao
            {
                Status = status
            };
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);

            string nazivStatusa = kucanskiPosaoServis.ProvjeriStatusPosla(kucanski_Posao);

            Assert.Equal(status.Naziv, nazivStatusa);
        }
        [Fact]
        public void StaviPosaoNaCekanje_GivenValidKucanskiPosao_ReturnsTrue()
        {
            Kucanski_posao kucanski_Posao = new Kucanski_posao();
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            A.CallTo(() => posaoRepo.StaviNaCekanje(kucanski_Posao)).Returns(1);
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);

            bool provjera = kucanskiPosaoServis.StaviPosaoNaCekanje(kucanski_Posao);

            Assert.True(provjera);
        }
        [Fact]
        public void ObrisiPosao_GivenValidKucanskiPosao_ReturnsTrue()
        {
            Kucanski_posao kucanski_Posao = new Kucanski_posao();
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            A.CallTo(() => posaoRepo.Izbrisi(kucanski_Posao)).Returns(1);
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);

            bool provjera = kucanskiPosaoServis.ObrisiPosao(kucanski_Posao);

            Assert.True(provjera);
        }

        [Fact]
        public void AzurirajPosao_GivenValidKucanskiPosao_ReturnsTrue()
        {
            Kucanski_posao kucanski_Posao = new Kucanski_posao();
            var posaoRepo = A.Fake<IKucanskiPosaoRepozitorij>();
            A.CallTo(() => posaoRepo.Azuriraj(kucanski_Posao)).Returns(1);
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);

            bool provjera = kucanskiPosaoServis.AzurirajPosao(kucanski_Posao);

            Assert.True(provjera);
        }
    }
}
