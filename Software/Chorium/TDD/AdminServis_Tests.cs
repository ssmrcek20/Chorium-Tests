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

namespace TDD
{
    public class AdminServis_Tests
    {
        private void DodajTestnogKorisnika()
        {
            using (var context = new ChoriumModel())
            {
                Korisnik korisnik = new Korisnik
                {
                    Ime = "testiranje",
                    Prezime = "testiranje",
                    Email = "testiranje@gmail.com",
                    Korisnicko_ime = "testiranje",
                    Lozinka = "testiranje",
                    ID_tip_korisnika = 1,
                    Datum_rodenja = new DateTime(1990, 1, 1)
                };
                context.Korisnik.Add(korisnik);
                context.SaveChanges();
            }
        }

        private void MakniTestnogKorisnika()
        {
            using (var context = new ChoriumModel())
            {
                var korisnik = context.Korisnik.FirstOrDefault(k => k.Email == "testiranje@gmail.com");
                if (korisnik != null)
                {
                    context.Korisnik.Remove(korisnik);
                }
                context.SaveChanges();
            }
        }

        [Fact]
        public void CheckEnteredUsername_GivenEmptyString_ReturnsFalse()
        {
            AdminServis adminServis = new AdminServis();
            string emptyString = "";

            bool check = adminServis.CheckEnteredUsername(emptyString);

            Assert.False(check);
        }

        [Fact]
        public void CheckEnteredUsername_GivenValidString_ReturnsTrue()
        {
            AdminServis adminServis = new AdminServis();
            string validString = "username123";

            bool check = adminServis.CheckEnteredUsername(validString);

            Assert.True(check);
        }

        [Fact]
        public void CheckEnteredUsername_GivenSpacebarOnlyString_ReturnsFalse()
        {
            AdminServis adminServis = new AdminServis();
            string spacebarString = "    ";

            bool check = adminServis.CheckEnteredUsername(spacebarString);

            Assert.False(check);
        }

        [Fact]
        public void UpdateUsername_GivenKorisniIsNull_ReturnsFalse() {
            AdminServis adminServis = new AdminServis();
            Korisnik korisnik = null;

            bool check = adminServis.UpdateUsername(korisnik);

            Assert.False(check);
        }

        [Fact]
        public void UpdateUsername_GivenKorisnikIsValidObject_UsernameIsUpdated()
        {
            AdminServis adminServis = new AdminServis();
            Korisnik korisnik;
            DodajTestnogKorisnika();

            using (var context = new ChoriumModel())
            {
                korisnik = context.Korisnik.FirstOrDefault(k => k.Korisnicko_ime == "testiranje");
                korisnik.Korisnicko_ime = "promjenjenoKorime";
                adminServis.UpdateUsername(korisnik);
                korisnik = context.Korisnik.FirstOrDefault(k => k.Korisnicko_ime == "promjenjenoKorime");
            }
            MakniTestnogKorisnika();

            Assert.NotNull(korisnik);
        }

        [Fact]
        public void AzurirajKorisnika_GivenKorisnikIsValidObject_KorisnikIsUpdated()
        {
            KorisnikRepozitorij korisnikRepozitorij = new KorisnikRepozitorij();
            KorisnikServis korisnikServis = new KorisnikServis(korisnikRepozitorij);
            Korisnik korisnik;
            DodajTestnogKorisnika();

            using (var context = new ChoriumModel())
            {
                korisnik = context.Korisnik.FirstOrDefault(k => k.Korisnicko_ime == "testiranje");
                korisnik.Korisnicko_ime = "promjenjenoKorime";
                korisnikServis.AzurirajKorisnika(korisnik);
                korisnik = context.Korisnik.FirstOrDefault(k => k.Korisnicko_ime == "promjenjenoKorime");
            }
            MakniTestnogKorisnika();

            Assert.NotNull(korisnik);
        }
    }
}
