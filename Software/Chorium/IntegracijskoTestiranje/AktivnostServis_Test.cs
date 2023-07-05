using SlojEntiteta.Entiteti;
using SlojPoslovneLogike.Servisi;
using SlojUpravljanjaSBazomPodataka;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegracijskoTestiranje
{
    public class AktivnostServis_Test
    {
        private void DodajAktivnostUBazu()
        {
            using (var context = new ChoriumModel())
            {
                var korisnik = context.Korisnik.SingleOrDefault(k => k.ID == 26);
                Aktivnost aktivnost = new Aktivnost
                {
                    Naziv = "IntTest - Testna Aktivnost",
                    Datum_pocetka = new DateTime(2023, 2, 2),
                    Datum_kraja = DateTime.Now,
                    Korisnik = new List<Korisnik> { korisnik}
                };
                context.Aktivnost.Add(aktivnost);
                context.SaveChanges();
            }
        }

        private void IzbrisiAktivnostIzBaze()
        {
            using (var context = new ChoriumModel())
            {
                var aktivnost = context.Aktivnost.Include("Korisnik").FirstOrDefault(a => a.Naziv == "IntTest - Testna Aktivnost");
                if (aktivnost != null)
                {
                    aktivnost.Korisnik.Clear();
                    context.Aktivnost.Remove(aktivnost);
                }
                context.SaveChanges();
            }
        }

        [Fact]
        public void DajAktivnostiZaDan_GivenValidDate_ReturnsActivityList()
        {
            var repo = new AktivnostiRepozitorij();
            AktivnostServis servis = new AktivnostServis(repo);
            DodajAktivnostUBazu();

            var aktivnosti = servis.DajAktivnostiZaDan(new DateTime(2023, 2, 2));
            IzbrisiAktivnostIzBaze();

            Action[] actions =
             {
                () => Assert.NotEmpty(aktivnosti),
                () => Assert.NotNull(aktivnosti.FirstOrDefault(a => a.Naziv == "IntTest - Testna Aktivnost"))
             };
            Assert.Multiple(actions);
        }

        [Fact]
        public void DodajKorisnikaUAktivnost_GivenValidActivityAndUser_UserIsAdded()
        {
            var repo = new AktivnostiRepozitorij();
            AktivnostServis servis = new AktivnostServis(repo);
            DodajAktivnostUBazu();
            var aktivnost = servis.DajAktivnostiZaDan(new DateTime(2023, 2, 2)).FirstOrDefault(a => a.Naziv == "IntTest - Testna Aktivnost");
            Korisnik korisnik;
            using (var context = new ChoriumModel())
            {
                korisnik = context.Korisnik.FirstOrDefault(k => k.ID == 43);
            }

            servis.DodajKorisnikaUAktivnost(aktivnost, korisnik);
            aktivnost = servis.DajAktivnostiZaDan(new DateTime(2023, 2, 2)).FirstOrDefault(a => a.Naziv == "IntTest - Testna Aktivnost");
            IzbrisiAktivnostIzBaze();

            Assert.NotNull(aktivnost.Korisnik.FirstOrDefault(k => k.ID == 43));
        }

        [Fact]
        public void DodajAktivnost_GivenValidActivityAndUser_ActivityIsAdded()
        {
            var aktivnostRepo = new AktivnostiRepozitorij();
            var aktivnostServis = new AktivnostServis(aktivnostRepo);

            DodajAktivnostUBazu();
            var aktivnost = aktivnostServis.DajAktivnostiZaDan(new DateTime(2023, 2, 2)).FirstOrDefault(a => a.Naziv == "IntTest - Testna Aktivnost");
            IzbrisiAktivnostIzBaze();

            Assert.NotNull(aktivnost.Korisnik.FirstOrDefault(k => k.ID == 26));
        }
    }
}
