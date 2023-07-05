using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class PrikazObiteljskogKalendaraStepDefinitions
    {
        [Given(@"otvorim aplikaciju")]
        public void GivenOtvorimAplikaciju()
        {
            var driver = GuiDriver.GetOrCreateDriver();
        }

        [Given(@"prijavljen sam u sustav")]
        public void GivenPrijavljenSamUSustav()
        {
            var driver = GuiDriver.GetDriver();
            var btnFormaPrijave = driver.FindElementByAccessibilityId("btnPrijava");
            btnFormaPrijave.Click();

            var txtUsername = driver.FindElementByAccessibilityId("TxtKorime");
            var txtPassword = driver.FindElementByAccessibilityId("TxtLozinka");
            txtUsername.SendKeys("kvardijan");
            txtPassword.SendKeys("qwert");

            var btnPrijava = driver.FindElementByAccessibilityId("BtnPrijava");
            btnPrijava.Click();
        }

        [When(@"kliknem na Obiteljski kalendar gumb")]
        public void WhenKliknemNaObiteljskiKalendarGumb()
        {
            var driver = GuiDriver.GetDriver();
            var btnKalendar = driver.FindElementByAccessibilityId("btnKalendar");
            btnKalendar.Click();
        }

        [Then(@"vidim prikaz obiteljskog kalendara")]
        public void ThenVidimPrikazObiteljskogKalendara()
        {
            var driver = GuiDriver.GetDriver();
            bool provjeraKalendara = driver.FindElementByAccessibilityId("dgPopisAktivnosti") != null;
            Assert.IsTrue(provjeraKalendara);
        }

        [When(@"kliknem na datum (.*)")]
        public void WhenKliknemNaDatum(string datum)
        {
            var driver = GuiDriver.GetDriver();
            var odabraniDatum = driver.FindElementByName(datum);
            odabraniDatum.Click();
        }

        [Then(@"vidim aktivnost (.*)")]
        public void ThenVidimAktivnost(string nazivAktivnosti)
        {
            var driver = GuiDriver.GetDriver();
            bool odabranaAktivnostProvjera = driver.FindElementByName(nazivAktivnosti) != null;

            Assert.IsTrue(odabranaAktivnostProvjera);
        }

        [When(@"kliknem na aktivnost (.*)")]
        public void WhenKliknemNaAktivnost(string nazivAktivnosti)
        {
            var driver = GuiDriver.GetDriver();
            var odabranaAktivnost = driver.FindElementByName(nazivAktivnosti);
            odabranaAktivnost.Click();
            odabranaAktivnost.Click();
        }

        [Then(@"vidim sudionika (.*)")]
        public void ThenVidimSudionika(string nazivSudionika)
        {
            var driver = GuiDriver.GetDriver();
            bool odabranSudionikProvjera = driver.FindElementByName(nazivSudionika) != null;

            Assert.IsTrue(odabranSudionikProvjera);
        }

    }
}
