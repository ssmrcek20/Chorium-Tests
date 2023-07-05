using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class DodavanjeKucanskogPoslaStepDefinitions
    {
        [Given(@"kliknem na Kućanski poslovi")]
        public void GivenKliknemNaKucanskiPoslovi()
        {
            var driver = GuiDriver.GetDriver();
            var btnKucanskiPoslovi = driver.FindElementByAccessibilityId("btnKucanskiPoslovi");
            btnKucanskiPoslovi.Click();
        }

        [When(@"kliknem na Dodaj posao")]
        public void WhenKliknemNaDodajPosao()
        {
            var driver = GuiDriver.GetDriver();
            var btnDodajPosao = driver.FindElementByAccessibilityId("btnDodajPosao");
            btnDodajPosao.Click();
        }

        [When(@"unesem naziv (.*)")]
        public void WhenUnesemNaziv(string naziv)
        {
            var driver = GuiDriver.GetDriver();
            var txtNazivPosla = driver.FindElementByAccessibilityId("txtNazivPosla");
            txtNazivPosla.SendKeys(naziv);
        }

        [When(@"odaberem kategoriju (.*)")]
        public void WhenOdaberemKategorijuKategorija(string kategorija)
        {
            var driver = GuiDriver.GetDriver();
            var cmbKategorija = driver.FindElementByAccessibilityId("cmbKategorija");
            cmbKategorija.Click();
            cmbKategorija.SendKeys(kategorija);
            cmbKategorija.SendKeys(Keys.Enter);
        }

        [When(@"upisem datum roka (.*) s vremenom (.*)")]
        public void WhenUpisemDatumRokaDatumRokaSVremenomVrijemeRoka(string datum, string vrijeme)
        {
            var driver = GuiDriver.GetDriver();
            var dtpDatumRoka = driver.FindElementByAccessibilityId("dtpDatumRoka");
            var txtRokH = driver.FindElementByAccessibilityId("txtRokH");
            var txtRokM = driver.FindElementByAccessibilityId("txtRokM");
            var txtRokS = driver.FindElementByAccessibilityId("txtRokS");

            dtpDatumRoka.SendKeys(datum);
            txtRokH.SendKeys(vrijeme.Split(':')[0]);
            txtRokM.SendKeys(vrijeme.Split(':')[1]);
            txtRokS.SendKeys(vrijeme.Split(':')[2]);
        }

        [When(@"odaberem zaduzenog (.*)")]
        public void WhenOdaberemZaduzenogZaduzeni(string zaduzeni)
        {
            var driver = GuiDriver.GetDriver();
            if (zaduzeni != "")
            {
                var listaClanova = driver.FindElementByAccessibilityId("lvZaduzeniClanovi");
                listaClanova.Click();
                listaClanova.Click();
                bool trazi = true;
                while (trazi)
                {
                    try
                    {
                        driver.FindElementByName(zaduzeni);
                        trazi = false;
                    }
                    catch
                    {
                    }
                    listaClanova.SendKeys(Keys.ArrowDown);
                }

                var zaduzeniClan = driver.FindElementByName(zaduzeni);

                zaduzeniClan.Click();
            }
        }

        [When(@"kliknem na gumb Dodaj")]
        public void WhenKliknemNaGumbDodaj()
        {
            var driver = GuiDriver.GetDriver();
            var btnDodaj = driver.FindElementByAccessibilityId("btnDodajPosao");
            btnDodaj.Click();
        }

        [Then(@"posao je zavrsen da")]
        public void ThenPosaoJeZavrsenDa()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var provjeraGumb = driver.FindElementByAccessibilityId("btnUrediBrisi");
                Assert.IsTrue(true);
            }
            catch
            {
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
                Assert.IsTrue(false);
            }
        }

        [Then(@"posao je zavrsen ne")]
        public void ThenPosaoJeZavrsenNe()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var provjeraPoruke = driver.FindElementByName("Provjerite podatke.");
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        [Then(@"posao je zavrsen dob")]
        public void ThenPosaoJeZavrsenDob()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var provjeraPoruke = driver.FindElementByName("Dobna granica kategorije je previsoka za neke od odabranih korisnika.");
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}
