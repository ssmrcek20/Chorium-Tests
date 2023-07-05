using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class RegistracijaStepDefinitions
    {
        [When(@"ja pokrenem program")]
        public void WhenJaPokrenemProgram()
        {
            var driver = GuiDriver.GetOrCreateDriver();
        }

        [When(@"ja kliknem na gumb Registracija")]
        public void WhenJaKliknemNaGumbRegistracija()
        {
            var driver = GuiDriver.GetDriver();
            var btnRegistracija = driver.FindElementByAccessibilityId("btnRegistracija");
            btnRegistracija.Click();
        }

        [Then(@"ja vidim prozor za Registraciju")]
        public void ThenJaVidimProzorZaRegistraciju()
        {
            var driver = GuiDriver.GetDriver();
            bool postojiGumbRegistriraj = driver.FindElementByAccessibilityId("btnRegistriraj") != null;
            Assert.IsTrue(postojiGumbRegistriraj);

        }

        [Given(@"ja sam na prozoru za Registraciju")]
        public void GivenJaSamNaProzoruZaRegistraciju()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            var btnRegistracija = driver.FindElementByAccessibilityId("btnRegistracija");
            btnRegistracija.Click();
            btnRegistracija.Click();
            bool postojiGumbRegistriraj = driver.FindElementByAccessibilityId("btnRegistriraj") != null;
            Assert.IsTrue(postojiGumbRegistriraj);
        }

        [When(@"ja unesem validne podatke")]
        public void WhenJaUnesemValidnePodatke()
        {
            var driver = GuiDriver.GetDriver();
            var txtIme = driver.FindElementByAccessibilityId("txtIme");
            var txtPrezime = driver.FindElementByAccessibilityId("txtPrezime");
            var dpDatumRodenja = driver.FindElementByAccessibilityId("dpDatumRodenja");
            var txtKorIme = driver.FindElementByAccessibilityId("txtKorIme");
            var txtMail = driver.FindElementByAccessibilityId("txtMail");
            var txtLozinka = driver.FindElementByAccessibilityId("txtLozinka");
            Random random = new Random();
            char slovo = (char)random.Next('A', 'Z' + 1);

            txtIme.SendKeys("test1");
            txtPrezime.SendKeys("test1");
            dpDatumRodenja.SendKeys("1.1.2001.");
            txtKorIme.SendKeys("test1" + slovo);
            txtMail.SendKeys("test1@gmail.com");
            txtLozinka.SendKeys("test1");
        }

        [When(@"ja odaberem Status Dijete")]
        public void WhenJaOdaberemStatusDijete()
        {
            var driver = GuiDriver.GetDriver();
            var rbtnDijete = driver.FindElementByAccessibilityId("rbtnDijete");
            rbtnDijete.Click();
        }

        [When(@"ja odaberem Status Roditelj")]
        public void WhenJaOdaberemStatusRoditelj()
        {
            var driver = GuiDriver.GetDriver();
            var rbtnRoditelj = driver.FindElementByAccessibilityId("rbtnRoditelj");
            rbtnRoditelj.Click();
        }

        [When(@"ja pritisnem gumb Registriraj")]
        public void WhenJaPritisnemGumbRegistriraj()
        {
            var driver = GuiDriver.GetDriver();
            var btnRegistriraj = driver.FindElementByAccessibilityId("btnRegistriraj");
            btnRegistriraj.Click();
        }

        [Then(@"ja sam prebacen na prozor Prijava")]
        public void ThenJaSamPrebacenNaProzorPrijava()
        {
            var driver = GuiDriver.GetDriver();
            bool postojiGumbPrijava = driver.FindElementByAccessibilityId("BtnPrijava") != null;
            Assert.IsTrue(postojiGumbPrijava);
        }

        [Then(@"ja vidim poruku da sam se uspjesno registrirao")]
        public void ThenJaVidimPorukuDaSamSeUspjesnoRegistrirao()
        {
            var driver = GuiDriver.GetDriver();
            var tekst = driver.FindElementByName("Korisnik je uspješno registriran!");
            var poruka = tekst.Text;
            var pravaPoruka = "Korisnik je uspješno registriran!";
            var btnOk = driver.FindElementByName("OK");
            btnOk.Click();
            Assert.AreEqual(poruka, pravaPoruka);
        }

        [Then(@"ja vidim poruku da trebam unjeti sve podatke")]
        public void ThenJaVidimPorukuDaTrebamUnjetiSvePodatke()
        {
            var driver = GuiDriver.GetDriver();
            var tekst = driver.FindElementByName("Za registraciju popuni sve!");
            var poruka = tekst.Text;
            var pravaPoruka = "Za registraciju popuni sve!";
            var btnOk = driver.FindElementByName("OK");
            btnOk.Click();
            Assert.AreEqual(poruka, pravaPoruka);
        }

        [When(@"ja unesem validne podatke osim korisničkog imena")]
        public void WhenJaUnesemValidnePodatkeOsimKorisnickogImena()
        {
            var driver = GuiDriver.GetDriver();
            var txtIme = driver.FindElementByAccessibilityId("txtIme");
            var txtPrezime = driver.FindElementByAccessibilityId("txtPrezime");
            var dpDatumRodenja = driver.FindElementByAccessibilityId("dpDatumRodenja");
            var txtKorIme = driver.FindElementByAccessibilityId("txtKorIme");
            var txtMail = driver.FindElementByAccessibilityId("txtMail");
            var txtLozinka = driver.FindElementByAccessibilityId("txtLozinka");

            txtIme.SendKeys("test1");
            txtPrezime.SendKeys("test1");
            dpDatumRodenja.SendKeys("1.1.2001.");
            txtKorIme.SendKeys("stan");
            txtMail.SendKeys("test1@gmail.com");
            txtLozinka.SendKeys("test1");
        }

        [Then(@"ja vidim poruku da se to korisnicko ime vec koristi")]
        public void ThenJaVidimPorukuDaSeToKorisnickoImeVecKoristi()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var tekst = driver.FindElementByName("Korisničko ime već postoji!");
                var poruka = tekst.Text;
                var pravaPoruka = "Korisničko ime već postoji!";
                var btnOk = driver.FindElementByName("OK");
                btnOk.Click();
                Assert.AreEqual(poruka, pravaPoruka);
            }
            catch (Exception ex)
            {
                var btnOk = driver.FindElementByName("OK");
                btnOk.Click();
                Assert.IsTrue(false);
            }
            
        }

        [When(@"ja unesem validne podatke osim E-maila")]
        public void WhenJaUnesemValidnePodatkeOsimE_Maila()
        {
            var driver = GuiDriver.GetDriver();
            var txtIme = driver.FindElementByAccessibilityId("txtIme");
            var txtPrezime = driver.FindElementByAccessibilityId("txtPrezime");
            var dpDatumRodenja = driver.FindElementByAccessibilityId("dpDatumRodenja");
            var txtKorIme = driver.FindElementByAccessibilityId("txtKorIme");
            var txtMail = driver.FindElementByAccessibilityId("txtMail");
            var txtLozinka = driver.FindElementByAccessibilityId("txtLozinka");

            txtIme.SendKeys("test2");
            txtPrezime.SendKeys("test2");
            dpDatumRodenja.SendKeys("1.1.2001.");
            txtKorIme.SendKeys("tes2");
            txtMail.SendKeys("test2");
            txtLozinka.SendKeys("test2");
        }

        [Then(@"ja vidim poruku da E-mail adresa nije ispravna")]
        public void ThenJaVidimPorukuDaE_MailAdresaNijeIspravna()
        {
            var driver = GuiDriver.GetDriver();
            var tekst = driver.FindElementByName("Loša E-mail adresa!");
            var poruka = tekst.Text;
            var pravaPoruka = "Loša E-mail adresa!";
            var btnOk = driver.FindElementByName("OK");
            btnOk.Click();
            Assert.AreEqual(poruka, pravaPoruka);
        }

        [When(@"ja unesem validne podatke osim datuma rodenja")]
        public void WhenJaUnesemValidnePodatkeOsimDatumaRodenja()
        {
            var driver = GuiDriver.GetDriver();
            var txtIme = driver.FindElementByAccessibilityId("txtIme");
            var txtPrezime = driver.FindElementByAccessibilityId("txtPrezime");
            var dpDatumRodenja = driver.FindElementByAccessibilityId("dpDatumRodenja");
            var txtKorIme = driver.FindElementByAccessibilityId("txtKorIme");
            var txtMail = driver.FindElementByAccessibilityId("txtMail");
            var txtLozinka = driver.FindElementByAccessibilityId("txtLozinka");

            txtIme.SendKeys("test4");
            txtPrezime.SendKeys("test4");
            dpDatumRodenja.SendKeys("1.1.2050.");
            txtKorIme.SendKeys("test4");
            txtMail.SendKeys("test4@gmail.com");
            txtLozinka.SendKeys("test4");
        }

        [Then(@"ja vidim poruku da datum rodenja nije ispravan")]
        public void ThenJaVidimPorukuDaDatumRodenjaNijeIspravan()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var tekst = driver.FindElementByName("Datum rođenja nije ispravan!");
                var poruka = tekst.Text;
                var pravaPoruka = "Datum rođenja nije ispravan!";
                var btnOk = driver.FindElementByName("OK");
                btnOk.Click();
                Assert.AreEqual(poruka, pravaPoruka);
            }
            catch (Exception ex)
            {
                var btnOk = driver.FindElementByName("OK");
                btnOk.Click();
                Assert.IsTrue(false);
            }
            
        }
    }
}
