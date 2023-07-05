using FakeItEasy;
using SlojEntiteta.Entiteti;
using SlojPoslovneLogike;
using SlojPoslovneLogike.Servisi;
using SlojUpravljanjaSBazomPodataka;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using SlojUpravljanjaSBazomPodataka.Sucelja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace IntegracijskoTestiranje
{
    public class KucanskiPosao_Test
    {
        private void dodajPosaoUBazu()
        {
            using (var context = new ChoriumModel())
            {
                var korisnik = context.Korisnik.SingleOrDefault(k => k.ID == 43);
                var statusNeDovrsen = context.Status.SingleOrDefault(s => s.ID == 0);
                var statusDovrsen = context.Status.SingleOrDefault(s => s.ID == 1);
                var kategorija = context.Kategorija.SingleOrDefault(c => c.ID == 1);
                var korisnici = new List<Korisnik> { korisnik };
                Kucanski_posao posao1 = new Kucanski_posao
                {
                    Naziv = "posaoTest1",
                    Datum_pocetka = DateTime.Now,
                    Datum_kraja = DateTime.Now,
                    Status = statusNeDovrsen,
                    Korisnik = korisnik,
                    Kategorija = kategorija,
                    Korisnik1 = korisnici
                };
                Kucanski_posao posao2 = new Kucanski_posao
                {
                    Naziv = "posaoTest2",
                    Datum_pocetka = DateTime.Now,
                    Datum_kraja = DateTime.Now,
                    Status = statusDovrsen,
                    Korisnik = korisnik,
                    Kategorija = kategorija,
                    Korisnik1 = korisnici
                };

                context.Kucanski_posao.Add(posao1);
                context.Kucanski_posao.Add(posao2);

                context.SaveChanges();
            }
        }
        private void izbrisiPosaoIzBaze()
        {
            using (var context = new ChoriumModel())
            {
                var posao1 = context.Kucanski_posao.Include("Korisnik1").FirstOrDefault(p => p.Naziv == "posaoTest1");
                var posao2 = context.Kucanski_posao.Include("Korisnik1").FirstOrDefault(p => p.Naziv == "posaoTest2");

                if (posao1 != null)
                {
                    posao1.Korisnik1.Clear();
                    context.Kucanski_posao.Remove(posao1);
                }

                if (posao2 != null)
                {
                    posao2.Korisnik1.Clear();
                    context.Kucanski_posao.Remove(posao2);
                }

                context.SaveChanges();
            }
        }

        [Fact]
        public void PostaviSlanjeMaila_GivenItIs8InTheMorning_SendMail()
        {
            dodajPosaoUBazu();

            var posaoRepo = new KucanskiPosaoRepozitorij();
            var korRepo = new KorisnikRepozitorij();
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo, korRepo);
            var datumUjutro = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);

            try
            {
                servis.PostaviSlanjeMaila(datumUjutro);

                izbrisiPosaoIzBaze();
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Assert.True(false, $"Iznimka: {ex.Message}");
            }
        }

        [Fact]
        public void PostaviSlanjeMaila_GivenItIs21InTheEvening_SendMail()
        {
            dodajPosaoUBazu();

            var posaoRepo = new KucanskiPosaoRepozitorij();
            var korRepo = new KorisnikRepozitorij();
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo, korRepo);
            var datumNavecer = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 0, 0);

            try
            {
                servis.PostaviSlanjeMaila(datumNavecer);
                Thread.Sleep(1000);
                izbrisiPosaoIzBaze();

            }
            catch (Exception ex)
            {
                Assert.True(false, $"Iznimka: {ex.Message}");
            }
        }

        [Fact]
        public void GenerirajGraf_GivenUsersHaveJobs_ReturnValidListOfUsersSortedAscending()
        {
            DateTime datum = new DateTime(2023, 4, 20);
            List<KorisnikPoslovi> ocekivaniRezultat = new List<KorisnikPoslovi>
            {
                new KorisnikPoslovi("kvardijan", 7),
                new KorisnikPoslovi("stan", 3)
            };
            var posaoRepo = new KucanskiPosaoRepozitorij();
            var korRepo = new KorisnikRepozitorij();
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo, korRepo);

            var rezultat = servis.GenerirajGraf(datum);

            Assert.Equal(ocekivaniRezultat[0].KorisnickoIme, rezultat[0].KorisnickoIme);
            Assert.Equal(ocekivaniRezultat[0].BrojPoslova, rezultat[0].BrojPoslova);
            Assert.Equal(ocekivaniRezultat[1].KorisnickoIme, rezultat[1].KorisnickoIme);
            Assert.Equal(ocekivaniRezultat[1].BrojPoslova, rezultat[1].BrojPoslova);
        }

        [Fact]
        public void GenerirajPopisPoslova_GivenUsersHaveJobs_ReturnValidListOfKorisnikPosloviTablica()
        {
            DateTime datum = new DateTime(2023, 4, 20);
            var posaoRepo = new KucanskiPosaoRepozitorij();
            var korRepo = new KorisnikRepozitorij();
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo, korRepo);

            var rezultat = servis.GenerirajPopisPoslova(datum);

            Assert.Equal("Posao1", rezultat[0].Naziv);
            Assert.Equal(12, rezultat.Count());
        }

        [Fact]
        public void PostaviObavijest_GivenTimeIsNow_SendNotification()
        {
            var kucanskiPosao = new Kucanski_posao
            {
                Naziv = "posaoTest",
                Datum_pocetka = new DateTime(2023, 5, 30),
                Korisnik = new Korisnik { Korisnicko_ime = "korisnik2" },
                Kategorija = new Kategorija { Naziv = "kategorija2" }
            };
            var posaoRepo = new KucanskiPosaoRepozitorij();
            KucanskiPosaoServis servis = new KucanskiPosaoServis(posaoRepo);

            servis.PostaviObavijest(DateTime.Now, kucanskiPosao);

        }

        [Fact]
        public void DodajKucanskiPosao_GivenValidKucanskiPosao_ReturnsPosao()
        {
            var posaoRepo = new KucanskiPosaoRepozitorij();
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);

            dodajPosaoUBazu();
            var posao = kucanskiPosaoServis.PrikaziPoslove().FirstOrDefault(p => p.Naziv == "posaoTest1");
            izbrisiPosaoIzBaze();

            Assert.NotNull(posao);
        }

        [Fact]
        public void RijesiPosao_GivenValidKucanskiPosao_ReturnsTrue()
        {
            var posaoRepo = new KucanskiPosaoRepozitorij();
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);
            dodajPosaoUBazu();
            Kucanski_posao posao;
            using (var context = new ChoriumModel())
            {
                posao = context.Kucanski_posao.FirstOrDefault(p => p.Naziv == "posaoTest1");
            }

            bool provjera = kucanskiPosaoServis.RijesiPosao(posao);
            izbrisiPosaoIzBaze();

            Assert.True(provjera);
        }

        [Fact]
        public void StaviPosaoNaCekanje_GivenValidKucanskiPosao_ReturnsTrue()
        {
            var posaoRepo = new KucanskiPosaoRepozitorij();
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);
            dodajPosaoUBazu();
            Kucanski_posao posao;
            using (var context = new ChoriumModel())
            {
                posao = context.Kucanski_posao.FirstOrDefault(p => p.Naziv == "posaoTest1");
            }

            bool provjera = kucanskiPosaoServis.StaviPosaoNaCekanje(posao);
            izbrisiPosaoIzBaze();

            Assert.True(provjera);
        }

        [Fact]
        public void AzurirajPosao_GivenValidKucanskiPosao_ReturnsTrue()
        {
            var posaoRepo = new KucanskiPosaoRepozitorij();
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);
            Kucanski_posao posao;
            dodajPosaoUBazu();

            using (var context = new ChoriumModel())
            {
                posao = context.Kucanski_posao.FirstOrDefault(p => p.Naziv == "posaoTest1");

                var datumKraja = DateTime.Now.AddDays(200);
                posao.Datum_kraja = datumKraja;

                bool provjera = kucanskiPosaoServis.AzurirajPosao(posao);
                posao = context.Kucanski_posao.FirstOrDefault(p => p.Naziv == "posaoTest1");
                Assert.Equal(datumKraja, posao.Datum_kraja);
            }

            izbrisiPosaoIzBaze();
        }
        [Fact]
        public void PrikaziPoslove_GivenNoArguments_ReturnsListOfJobs()
        {
            var posaoRepo = new KucanskiPosaoRepozitorij();
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);
            var brojPoslova = kucanskiPosaoServis.PrikaziPoslove().Count();
            dodajPosaoUBazu();
            var brojPoslovaPoslije = kucanskiPosaoServis.PrikaziPoslove().Count();

            Assert.True(brojPoslova+2 == brojPoslovaPoslije);
            izbrisiPosaoIzBaze();
        }

        public void PrikaziPoslove_GivenUser_ReturnsListOfJobsForUser()
        {

            using (var context = new ChoriumModel())
            {
                var korisnik = context.Korisnik.SingleOrDefault(k => k.ID == 1029);
                var posaoRepo = new KucanskiPosaoRepozitorij();
                KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);
                var brojPoslova = kucanskiPosaoServis.PrikaziPoslove(korisnik).Count();
                Assert.Equal(5,brojPoslova);
            }
            
        }

        public void PrikaziPoslove_GivenUserCategoryAndState_ReturnsListOfJobs()
        {

            using (var context = new ChoriumModel())
            {
                var korisnik = context.Korisnik.SingleOrDefault(k => k.ID == 26);
                var status = context.Status.SingleOrDefault(s => s.ID == 0);
                var kategorija = context.Kategorija.SingleOrDefault(k => k.ID == 2);
                var posaoRepo = new KucanskiPosaoRepozitorij();
                KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);
                var brojPoslova = kucanskiPosaoServis.PrikaziPoslove(korisnik,status,kategorija).Count();
                Assert.Equal(6, brojPoslova);
            }

        }

    }
}
