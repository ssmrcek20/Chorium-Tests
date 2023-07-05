using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class DodavanjeUObiteljskiKalendarStepDefinitions
    {
        public string nasumicniBroj;

        [Given(@"kliknem na Obiteljski kalendar gumb")]
        public void GivenKliknemNaObiteljskiKalendarGumb()
        {
            var driver = GuiDriver.GetDriver();
            var btnKalendar = driver.FindElementByAccessibilityId("btnKalendar");
            btnKalendar.Click();
        }

        [When(@"kliknem na gumb Dodaj novu aktivnost")]
        public void WhenKliknemNaGumbDodajNovuAktivnost()
        {
            var driver = GuiDriver.GetDriver();
            var btnDodaj = driver.FindElementByAccessibilityId("btnNovaAktivnost");
            btnDodaj.Click();
        }

        [When(@"upisem naziv (.*)")]
        public void WhenUpisemNazivAktivnosti(string naziv)
        {
            var driver = GuiDriver.GetDriver();
            var txtNaziv = driver.FindElementByAccessibilityId("txtNaziv");
            txtNaziv.SendKeys(naziv);
        }

        [When(@"upisem datum pocetka (.*) s vremenom (.*)")]
        public void WhenUpisemDatumPocetka(string datum, string vrijeme)
        {
            var driver = GuiDriver.GetDriver();
            var dtpDatumPocetka = driver.FindElementByAccessibilityId("dtpDatumPocetka");
            var txtPocetakH = driver.FindElementByAccessibilityId("txtPocetakH");
            var txtPocetakM = driver.FindElementByAccessibilityId("txtPocetakM");
            var txtPocetakS = driver.FindElementByAccessibilityId("txtPocetakS");

            dtpDatumPocetka.SendKeys(datum);
            txtPocetakH.SendKeys(vrijeme.Split(':')[0]);
            txtPocetakM.SendKeys(vrijeme.Split(':')[1]);
            txtPocetakS.SendKeys(vrijeme.Split(':')[2]);
        }

        [When(@"upisem datum kraja (.*) s vremenom (.*)")]
        public void WhenUpisemDatumKraja(string datum, string vrijeme)
        {
            var driver = GuiDriver.GetDriver();
            var dtpDatumZavrsetka = driver.FindElementByAccessibilityId("dtpDatumZavrsetka");
            var txtKrajH = driver.FindElementByAccessibilityId("txtKrajH");
            var txtKrajM = driver.FindElementByAccessibilityId("txtKrajM");
            var txtKrajS = driver.FindElementByAccessibilityId("txtKrajS");

            dtpDatumZavrsetka.SendKeys(datum);
            txtKrajH.SendKeys(vrijeme.Split(':')[0]);
            txtKrajM.SendKeys(vrijeme.Split(':')[1]);
            txtKrajS.SendKeys(vrijeme.Split(':')[2]);
        }

        [When(@"odaberem clana (.*)")]
        public void WhenOdaberemClana(string clan)
        {
            var driver = GuiDriver.GetDriver();
            var cmbClan = driver.FindElementByAccessibilityId("cmbClanKucanstva");
            cmbClan.Click();
            cmbClan.SendKeys(clan);
            cmbClan.SendKeys(Keys.Enter);
        }


        [When(@"kliknem na gumb Dodaj aktivnost")]
        public void WhenKliknemNaGumbDodajAktivnost()
        {
            var driver = GuiDriver.GetDriver();
            var btnDodajAktivnost = driver.FindElementByAccessibilityId("btnDodajAktivnost");
            btnDodajAktivnost.Click();
        }

        [Then(@"Dobivamo poruku uspjeh")]
        public void ThenDobivamoPorukuUspjeh()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var provjeraPoruke = driver.FindElementByName("Aktivnost dodana.");
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
                Assert.IsTrue(true);
            }
            catch
            {
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
                Assert.IsTrue(false);
            }
        }

        [Then(@"Dobivamo poruku greska")]
        public void ThenDobivamoPorukuGreska()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var provjeraPoruke = driver.FindElementByName("Provjerite podatke!");
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
                Assert.IsTrue(true);
            }
            catch
            {
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
                Assert.IsTrue(false);
            }
        }
        
        [Given(@"postoji aktivnost u kojoj nisam")]
        public void GivenPostojiAktivnostUKojojNisam()
        {
            var driver = GuiDriver.GetDriver();
            var odabraniDatum = driver.FindElementByName("11. travnja 2023.");
            odabraniDatum.Click();

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var nasumicni = rand.Next(1, 1000).ToString();

            var btnNovaAktivnost = driver.FindElementByAccessibilityId("btnNovaAktivnost");
            btnNovaAktivnost.Click();
            btnNovaAktivnost.Click();

            var txtNaziv = driver.FindElementByAccessibilityId("txtNaziv");
            txtNaziv.SendKeys("AktivnostNisam"+nasumicni);
            nasumicniBroj = nasumicni;

            var dtpDatumPocetka = driver.FindElementByAccessibilityId("dtpDatumPocetka");
            var txtPocetakH = driver.FindElementByAccessibilityId("txtPocetakH");
            var txtPocetakM = driver.FindElementByAccessibilityId("txtPocetakM");
            var txtPocetakS = driver.FindElementByAccessibilityId("txtPocetakS");

            dtpDatumPocetka.SendKeys("11.4.2023.");
            txtPocetakH.SendKeys("1");
            txtPocetakM.SendKeys("1");
            txtPocetakS.SendKeys("1");

            var dtpDatumZavrsetka = driver.FindElementByAccessibilityId("dtpDatumZavrsetka");
            var txtKrajH = driver.FindElementByAccessibilityId("txtKrajH");
            var txtKrajM = driver.FindElementByAccessibilityId("txtKrajM");
            var txtKrajS = driver.FindElementByAccessibilityId("txtKrajS");

            dtpDatumZavrsetka.SendKeys("11.4.2023.");
            txtKrajH.SendKeys("1");
            txtKrajM.SendKeys("1");
            txtKrajS.SendKeys("1");

            var cmbClan = driver.FindElementByAccessibilityId("cmbClanKucanstva");
            cmbClan.Click();
            cmbClan.SendKeys("stan");
            cmbClan.SendKeys(Keys.Enter);

            var btnDodajNovuAktivnost = driver.FindElementByAccessibilityId("btnDodajAktivnost");
            btnDodajNovuAktivnost.Click();

            try
            {
                var provjeraPoruke = driver.FindElementByName("Aktivnost dodana.");
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
            }
            catch
            {
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
            }
        }

        [When(@"kliknem na AktivnostNisam")]
        public void WhenKliknemNaAktivnostNisam()
        {
            var driver = GuiDriver.GetDriver();
            var odabranaAktivnost = driver.FindElementByName("AktivnostNisam"+nasumicniBroj);
            odabranaAktivnost.Click();
            odabranaAktivnost.Click();
        }

        [When(@"kliknem na gumb Pridruži se aktivnosti")]
        public void WhenKliknemNaGumbPridruziSeAktivnosti()
        {
            var driver = GuiDriver.GetDriver();
            var pridruziSeGumb = driver.FindElementByName("Pridruži se aktivnosti");
            pridruziSeGumb.Click();
        }

        [Then(@"pridružen sam aktivnosti")]
        public void ThenPridruzenSamAktivnosti()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var provjeraPoruke = driver.FindElementByName("Korisnik dodan u aktivnost.");
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
                Assert.IsTrue(true);
            }
            catch
            {
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
                Assert.IsTrue(false);
            }
        }

        [Then(@"dobivam poruku kako nisam pridruzen aktivnosti")]
        public void ThenDobivamPorukuKakoNisamPridruzenAktivnosti()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var provjeraPoruke = driver.FindElementByName("Korisnik je već uključen u aktivnost.");
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
                Assert.IsTrue(true);
            }
            catch
            {
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
                Assert.IsTrue(false);
            }
        }
    }
}
