using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        [Given(@"aplikacija je upaljena")]
        public void GivenAplikacijaJeUpaljena()
        {
            var driver = GuiDriver.GetOrCreateDriver();
        }

        [Given(@"nalazim se na prijavi")]
        public void GivenNalazimSeNaPrijavi()
        {
            var driver = GuiDriver.GetDriver();
            var gumbPrijava = driver.FindElementByAccessibilityId("btnPrijava");
            gumbPrijava.Click();
        }


        [When(@"unesem tocno korisnicko ime stan i lozinku stan")]
        public void WhenUnesemTocnoKorisnickoImeStanILozinkuStan()
        {
            var driver = GuiDriver.GetDriver();
            var korime = driver.FindElementByAccessibilityId("TxtKorime");
            var lozinka = driver.FindElementByAccessibilityId("TxtLozinka");

            korime.SendKeys("stan");
            lozinka.SendKeys("stan");
        }

        [When(@"pritisnem Login gumb")]
        public void WhenPritisnemLoginGumb()
        {
            var driver = GuiDriver.GetDriver();
            var prijava = driver.FindElementByAccessibilityId("BtnPrijava");
            prijava.Click();
        }

        [Then(@"trebao bi vidjeti popis kucanskih poslova")]
        public void ThenTrebaoBiVidjetiPopisKucanskihPoslova()
        {
            var driver = GuiDriver.GetDriver();
            bool stackpanel = driver.FindElementByName("Filtriraj") != null;
            Assert.IsTrue(stackpanel); 

        }

        [When(@"unesem korisnicko ime (.*) i lozinku (.*)")]
        public void WhenUnesemKorisnickoImeStanILozinkuKorisnik(string p, string p2)
        {
            var driver = GuiDriver.GetDriver();
            var korime = driver.FindElementByAccessibilityId("TxtKorime");
            var lozinka = driver.FindElementByAccessibilityId("TxtLozinka");

            korime.SendKeys(p);
            lozinka.SendKeys(p2);
        }

        [When(@"pritisnem FaceLogin gumb")]
        public void WhenPritisnemFaceLoginGumb()
        {
            var driver = GuiDriver.GetDriver();
            var Facelogin = driver.FindElementByAccessibilityId("BtnFaceLogin");
            Facelogin.Click();
        }

        [Then(@"trebao bi dobiti poruku (.*)")]
        public void ThenTrebaoBiDobitiPorukuGreske(string greska)
        {
            string porukagreske = "";
            var driver = GuiDriver.GetDriver();
            try
            {
                if (driver.FindElementByName("Neispravno korisničko ime ili lozinka") != null)
                {
                    var dobivenaGreska = driver.FindElementByName("Neispravno korisničko ime ili lozinka");
                    porukagreske = dobivenaGreska.Text;
                    var uredu = driver.FindElementByName("OK");
                    uredu.Click();

                }
            }
            catch
            {

            }
            try
            {

                if (driver.FindElementByName("Unesite korisničko ime i lozinku") != null)
                {
                    var dobivenaGreska = driver.FindElementByName("Unesite korisničko ime i lozinku");
                    porukagreske = dobivenaGreska.Text;
                    var uredu = driver.FindElementByName("OK");
                    uredu.Click();

                }


            }
            catch
            {

            }
            try
            {
                if (driver.FindElementByName("Nije pronađeno lice") != null)
                {
                    var dobivenaGreska = driver.FindElementByName("Nije pronađeno lice");
                    porukagreske = dobivenaGreska.Text;
                    var uredu = driver.FindElementByName("OK");
                    uredu.Click();
                }
            }
            catch
            {
                try
                {
                    var uredu = driver.FindElementByName("OK");
                    uredu.Click();
                    Assert.IsTrue(false);
                }
                catch
                {

                }
            }
                Assert.IsTrue(porukagreske == greska);
        }
    }
}
