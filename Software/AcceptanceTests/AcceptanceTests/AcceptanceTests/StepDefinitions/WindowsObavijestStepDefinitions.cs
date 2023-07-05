using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class WindowsObavijestStepDefinitions
    {
        [Given(@"ja sam prijavljen")]
        public void GivenJaSamPrijavljen()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            var btnPrijava = driver.FindElementByAccessibilityId("btnPrijava");
            btnPrijava.Click();
            var TxtKorime = driver.FindElementByAccessibilityId("TxtKorime");
            var TxtLozinka = driver.FindElementByAccessibilityId("TxtLozinka");
            TxtKorime.SendKeys("stan");
            TxtLozinka.SendKeys("stan");
            var BtnPrijava = driver.FindElementByAccessibilityId("BtnPrijava");
            BtnPrijava.Click();
        }

        [When(@"ja postavim vrijeme za 1 minutu")]
        public void WhenJaPostavimVrijemeZaMinutu()
        {
            DateTime sad = DateTime.Now;
            int min = sad.Minute + 1;
            int sat = sad.Hour;
            var driver = GuiDriver.GetDriver();
            var cmbSati = driver.FindElementByAccessibilityId("cmbSati");
            var cmbMinute = driver.FindElementByAccessibilityId("cmbMinute");
            cmbSati.Click();
            cmbSati.SendKeys(sat.ToString());
            cmbSati.SendKeys(Keys.Enter);
            cmbMinute.Click();
            cmbMinute.SendKeys(min.ToString());
            cmbMinute.SendKeys(Keys.Enter);
        }

        [When(@"ja odaberem posao")]
        public void WhenJaOdaberemPosao()
        {
            var driver = GuiDriver.GetDriver();
            var posao = driver.FindElementByName("Posao");
            posao.Click();
        }

        [When(@"ja pritisnem Postavi Obavijest")]
        public void WhenJaPritisnemPostaviObavijest()
        {
            var driver = GuiDriver.GetDriver();
            var btnObavijest = driver.FindElementByAccessibilityId("btnObavijest");
            btnObavijest.Click();
        }

        [Then(@"ja vidim poruku da je obavijest postavljena")]
        public void ThenJaVidimPorukuDaJeObavijestPostavljena()
        {
            var driver = GuiDriver.GetDriver();
            var tekst = driver.FindElementByName("Obavijest je postavljena!");
            var poruka = tekst.Text;
            var pravaPoruka = "Obavijest je postavljena!";
            var btnOk = driver.FindElementByName("OK");
            btnOk.Click();
            Assert.AreEqual(poruka, pravaPoruka);
        }

        [Then(@"ja vidim poruku da se obavijest ne može postaviti u to vrijeme")]
        public void ThenJaVidimPorukuDaSeObavijestNeMozePostavitiUToVrijeme()
        {
            var driver = GuiDriver.GetDriver();
            var tekst = driver.FindElementByName("Obavijest se ne može postaviti u to vrijeme!");
            var poruka = tekst.Text;
            var pravaPoruka = "Obavijest se ne može postaviti u to vrijeme!";
            var btnOk = driver.FindElementByName("OK");
            btnOk.Click();
            Assert.AreEqual(poruka, pravaPoruka);
        }

        [Then(@"ja vidim poruku da trebam odabrati vrijeme i posao")]
        public void ThenJaVidimPorukuDaTrebamOdabratiVrijemeIPosao()
        {
            var driver = GuiDriver.GetDriver();
            var tekst = driver.FindElementByName("Odaberi posao i vrijeme!");
            var poruka = tekst.Text;
            var pravaPoruka = "Odaberi posao i vrijeme!";
            var btnOk = driver.FindElementByName("OK");
            btnOk.Click();
            Assert.AreEqual(poruka, pravaPoruka);
        }
    }
}
