using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class Mjesecni_IzvjestajStepDefinitions
    {
        [When(@"ja kliknem na gumb Mjesecni izvjestaj")]
        public void WhenJaKliknemNaGumbMjesecniIzvjestaj()
        {
            var driver = GuiDriver.GetDriver();
            var btnIzvjestaj = driver.FindElementByAccessibilityId("btnIzvjestaj");
            btnIzvjestaj.Click();
        }

        [Then(@"ja vidim prozor za Mjesecni izvjestaj")]
        public void ThenJaVidimProzorZaMjesecniIzvjestaj()
        {
            var driver = GuiDriver.GetDriver();
            bool postojiGumbIzvjestaj = driver.FindElementByAccessibilityId("btnGenerirajIzvjestaj") != null;
            Assert.IsTrue(postojiGumbIzvjestaj);
        }

        [Given(@"ja sam na prozoru za Mjesecne izvjestaje")]
        public void GivenJaSamNaProzoruZaMjesecneIzvjestaje()
        {
            var driver = GuiDriver.GetOrCreateDriver();
            var btnIzvjestaj = driver.FindElementByAccessibilityId("btnIzvjestaj");
            btnIzvjestaj.Click();
            bool postojiGumbIzvjestaj = driver.FindElementByAccessibilityId("btnGenerirajIzvjestaj") != null;
            Assert.IsTrue(postojiGumbIzvjestaj);
        }

        [When(@"ja unesem trenutni mjesec")]
        public void WhenJaUnesemTrenutniMjesec()
        {
            var driver = GuiDriver.GetDriver();
            var cmbMjesec = driver.FindElementByAccessibilityId("cmbMjesec");
            cmbMjesec.Click();
            cmbMjesec.SendKeys("4");
            cmbMjesec.SendKeys(Keys.Enter);
        }

        [When(@"ja pritisnem gumb Generiraj izvjestaj")]
        public void WhenJaPritisnemGumbGenerirajIzvjestaj()
        {
            var driver = GuiDriver.GetDriver();
            var btnGenerirajIzvjestaj = driver.FindElementByAccessibilityId("btnGenerirajIzvjestaj");
            btnGenerirajIzvjestaj.Click();
        }

        [Then(@"Izvjestaj se generira")]
        public void ThenIzvjestajSeGenerira()
        {
            var driver = GuiDriver.GetDriver();
            bool postojiGumbIPrint = driver.FindElementByName("Position") != null;
            Assert.IsTrue(postojiGumbIPrint);
        }

        [When(@"ja unesem prosli mjesec")]
        public void WhenJaUnesemProsliMjesec()
        {
            var driver = GuiDriver.GetDriver();
            var cmbMjesec = driver.FindElementByAccessibilityId("cmbMjesec");
            cmbMjesec.Click();
            cmbMjesec.SendKeys("3");
            cmbMjesec.SendKeys(Keys.Enter);
        }

        [When(@"ja unesem naknadni mjesec")]
        public void WhenJaUnesemNaknadniMjesec()
        {
            var driver = GuiDriver.GetDriver();
            var cmbMjesec = driver.FindElementByAccessibilityId("cmbMjesec");
            cmbMjesec.Click();
            cmbMjesec.SendKeys("5");
            cmbMjesec.SendKeys(Keys.Enter);
        }

        [Then(@"ja vidim poruku da se izvještaj ne može generirati za taj mjesec")]
        public void ThenJaVidimPorukuDaSeIzvjestajNeMozeGeneriratiZaTajMjesec()
        {
            var driver = GuiDriver.GetDriver();
            bool postojiGumbIPrint = driver.FindElementByName("Position") == null;
            Assert.IsTrue(postojiGumbIPrint);
        }

    }
}
