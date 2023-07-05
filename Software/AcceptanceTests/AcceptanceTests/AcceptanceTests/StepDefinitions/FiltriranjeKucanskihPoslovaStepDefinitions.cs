using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class FiltriranjeKucanskihPoslovaStepDefinitions
    {
        [When(@"pritisnem gumb resetiraj")]
        public void WhenPritisnemGumbResetiraj()
        {
            var driver = GuiDriver.GetDriver();
            var gumbResetiraj = driver.FindElementByAccessibilityId("BtnResetiraj");
            gumbResetiraj.Click();
        }

        [When(@"odaberem opciju stanko")]
        public void WhenOdaberemOpcijuStanko()
        {
            var driver = GuiDriver.GetDriver();
            var ComboboxKorisnik = driver.FindElementByAccessibilityId("CmbKorisnik");
            ComboboxKorisnik.Click();
            var korisnik = driver.FindElementByName("stanko");
            korisnik.Click();
        }

        [When(@"pritisnem gumb filtriraj")]
        public void WhenPritisnemGumbFiltriraj()
        {
            var driver = GuiDriver.GetDriver();
            var gumbfiltriraj = driver.FindElementByAccessibilityId("BtnFiltriraj");
            gumbfiltriraj.Click();
        }

        [Then(@"prikaz poslova za korisnika stanko je prikazan")]
        public void ThenPrikazPoslovaZaKorisnikaStankoJePrikazan()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var posao = driver.FindElementByName("sdfghjk");
                Assert.IsTrue(posao != null);
            }
            catch
            {
                Assert.Fail();
            }
            
            
        }

        [When(@"odaberem opciju kvardijan")]
        public void WhenOdaberemOpcijuKvardijan()
        {
            var driver = GuiDriver.GetDriver();
            var ComboboxKorisnik = driver.FindElementByAccessibilityId("CmbKorisnik");
            ComboboxKorisnik.Click();
            var korisnik = driver.FindElementByName("kvardijan");
            korisnik.Click();
        }

        [When(@"odaberem opciju dovrsen")]
        public void WhenOdaberemOpcijuDovrsen()
        {
            var driver = GuiDriver.GetDriver();
            var Comboboxstanje = driver.FindElementByAccessibilityId("CmbStanje");
            Comboboxstanje.Click();
            var stanje = driver.FindElementByName("dovrsen");
            stanje.Click();
        }

        [Then(@"prikaz dovrsenih poslova za korisnika kvardijan je prikazan")]
        public void ThenPrikazDovrsenihPoslovaZaKorisnikaKvardijanJePrikazan()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var posao = driver.FindElementByName("Posao1");
                Assert.IsTrue(posao != null);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [When(@"odaberem opciju Pospremanje")]
        public void WhenOdaberemOpcijuPospremanje()
        {
            var driver = GuiDriver.GetDriver();
            var Comboboxkategorija = driver.FindElementByAccessibilityId("CmbKategorije");
            Comboboxkategorija.Click();
            var kategorija = driver.FindElementByName("Pospremanje");
            kategorija.Click();
        }

        [Then(@"prikaz poslova kategorije Pospremanje za korisnika kvardijan je prikazan")]
        public void ThenPrikazPoslovaKategorijePospremanjeZaKorisnikaKvardijanJePrikazan()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var posao = driver.FindElementByName("Testni posao");
                Assert.IsTrue(posao != null);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [When(@"odaberem opciju na_cekanju")]
        public void WhenOdaberemOpcijuNa_Cekanju()
        {
            var driver = GuiDriver.GetDriver();
            var Comboboxstanje = driver.FindElementByAccessibilityId("CmbStanje");
            Comboboxstanje.Click();
            var stanje = driver.FindElementByName("na_cekanju");
            stanje.Click();
        }

        [Then(@"prikaz poslova na cekanju je prikazan")]
        public void ThenPrikazPoslovaNaCekanjuJePrikazan()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var posao = driver.FindElementByName("Test");
                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [When(@"odaberem opciju Rad u vrtu")]
        public void WhenOdaberemOpcijuRadUVrtu()
        {
            var driver = GuiDriver.GetDriver();
            var Comboboxkategorija = driver.FindElementByAccessibilityId("CmbKategorije");
            Comboboxkategorija.Click();
            var kategorija = driver.FindElementByName("Rad u vrtu");
            kategorija.Click();
        }

        [Then(@"prikaz poslova kategorije Rad u vrtu na cekanju za korisnika kvardijan je prikazan")]
        public void ThenPrikazPoslovaKategorijeRadUVrtuNaCekanjuZaKorisnikaKvardijanJePrikazan()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var posao = driver.FindElementByName("Test");
                Assert.Fail();
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [Then(@"prikaz poslova kategorije Rad u vrtu je prikazan")]
        public void ThenPrikazPoslovaKategorijeRadUVrtuJePrikazan()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var posao = driver.FindElementByName("Posao1");
                Assert.IsTrue(posao != null);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Then(@"prikaz dovrsenih poslova kategorije Rad u vrtu za korisnika kvardijan je prikazan")]
        public void ThenPrikazDovrsenihPoslovaKategorijeRadUVrtuZaKorisnikaKvardijanJePrikazan()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var posao = driver.FindElementByName("Posao2");
                Assert.IsTrue(posao != null); ;
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Given(@"filtriranje je resetirano")]
        public void GivenFiltriranjeJeResetirano()
        {
            var driver = GuiDriver.GetDriver();
            var gumbResetiraj = driver.FindElementByAccessibilityId("BtnResetiraj");
            gumbResetiraj.Click();
        }

    }
}
