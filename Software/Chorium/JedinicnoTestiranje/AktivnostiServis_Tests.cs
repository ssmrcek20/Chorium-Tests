using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using SlojEntiteta.Entiteti;
using SlojPoslovneLogike.Servisi;
using SlojUpravljanjaSBazomPodataka.Sucelja;
using Xunit;

namespace JedinicnoTestiranje
{
    public class AktivnostiServis_Tests
    {

        [Fact]
        public void DajAktivnostiZaDan_GivenValidDate_ReturnsActivityList()
        {
            var fakeRepo = A.Fake<IAktivnostiRepozitorij>();
            AktivnostServis servis = new AktivnostServis(fakeRepo);
            A.CallTo(() => fakeRepo.DajAktivnostiZaDan(A<DateTime>.Ignored)).Returns(new List<Aktivnost> {
                new Aktivnost {
                    ID = 123,
                    Naziv = "testna aktivnost",
                    Datum_pocetka = DateTime.Now,
                    Datum_kraja = DateTime.Now
                    }
            }.AsQueryable());

            var aktivnosti = servis.DajAktivnostiZaDan(DateTime.Now);

            Action[] actions =
             {
                () => Assert.Single(aktivnosti),
                () => Assert.Equal(123, aktivnosti[0].ID)
             };
            Assert.Multiple(actions);
        }

        [Fact]
        public void DodajKorisnikaUAktivnost_GivenValidActivityAndUser_UserIsAdded()
        {
            var fakeRepo = A.Fake<IAktivnostiRepozitorij>();
            AktivnostServis servis = new AktivnostServis(fakeRepo);
            Korisnik korisnik = new Korisnik
            {
                ID = 321,
                Ime = "test",
                Prezime = "test",
                Email = "test.test@gmail.com",
                Korisnicko_ime = "test",
                Lozinka = "test",
                ID_tip_korisnika = 1,
                Datum_rodenja = new DateTime(1990, 1, 1)
            };
            Aktivnost aktivnost = new Aktivnost
            {
                ID = 123,
                Naziv = "testna aktivnost",
                Datum_pocetka = DateTime.Now,
                Datum_kraja = DateTime.Now.AddMilliseconds(10)
            };
            A.CallTo(() => fakeRepo.DodajKorisnikaUAktivnost(aktivnost, korisnik)).Returns(1);

            bool provjera = servis.DodajKorisnikaUAktivnost(aktivnost, korisnik);

            Assert.True(provjera);
        }

        [Fact]
        public void DodajAktivnost_GivenValidActivityAndUser_ActivityIsAdded()
        {
            var fakeRepo = A.Fake<IAktivnostiRepozitorij>();
            AktivnostServis servis = new AktivnostServis(fakeRepo);
            Korisnik korisnik = new Korisnik
            {
                ID = 321,
                Ime = "test",
                Prezime = "test",
                Email = "test.test@gmail.com",
                Korisnicko_ime = "test",
                Lozinka = "test",
                ID_tip_korisnika = 1,
                Datum_rodenja = new DateTime(1990, 1, 1)
            };
            Aktivnost aktivnost = new Aktivnost
            {
                ID = 123,
                Naziv = "testna aktivnost",
                Datum_pocetka = DateTime.Now,
                Datum_kraja = DateTime.Now.AddMilliseconds(10)
            };
            A.CallTo(() => fakeRepo.DodajAktivnost(aktivnost, korisnik)).Returns(1);

            bool provjera = servis.DodajAktivnost(aktivnost, korisnik);

            Assert.True(provjera);
        }
    }
}
