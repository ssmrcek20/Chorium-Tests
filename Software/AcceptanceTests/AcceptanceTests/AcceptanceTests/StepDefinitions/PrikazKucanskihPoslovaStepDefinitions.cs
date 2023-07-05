using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class PrikazKucanskihPoslovaStepDefinitions
    {
        [Given(@"nalazim se na prikazu poslova")]
        public void GivenNalazimSeNaPrikazuPoslova()
        {
            var driver = GuiDriver.GetDriver();
            var gumbPoslovi = driver.FindElementByAccessibilityId("btnKucanskiPoslovi");
            gumbPoslovi.Click();
        }

        [Then(@"prikaz poslova za sve clanove kucanstva je prikazan")]
        public void ThenPrikazPoslovaZaSveClanoveKucanstvaJePrikazan()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var posao = driver.FindElementByName("Posao1");
                var korisnik = driver.FindElementByName("fsostaric20");
                Assert.IsTrue(korisnik != null && posao!=null);
            }
            catch
            {
                Assert.IsTrue(false);
            }           
        }

        [Given(@"Korisnik je prijavljen")]
        public void GivenKorisnikJePrijavljen()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var gumbPrijava = driver.FindElementByAccessibilityId("btnPrijava");
                gumbPrijava.Click();
            }
            catch
            {

            }
            
            
            var korime = driver.FindElementByAccessibilityId("TxtKorime");
            var lozinka = driver.FindElementByAccessibilityId("TxtLozinka");
            korime.SendKeys("stan");
            lozinka.SendKeys("stan");
            var prijava = driver.FindElementByAccessibilityId("BtnPrijava");
            prijava.Click();

        }

        [Then(@"osobni prikaz poslova je prikazan")]
        public void ThenOsobniPrikazPoslovaJePrikazan()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                var posao = driver.FindElementByName("Posao");
                Assert.IsTrue(posao != null);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}
