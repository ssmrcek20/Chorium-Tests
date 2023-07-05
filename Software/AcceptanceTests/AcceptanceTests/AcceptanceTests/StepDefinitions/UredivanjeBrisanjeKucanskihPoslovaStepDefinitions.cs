using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class UredivanjeBrisanjeKucanskihPoslovaStepDefinitions
    {
        [When(@"kliknem na gumb UrediBrisi")]
        public void WhenKliknemNaGumbUrediBrisi()
        {
            var driver = GuiDriver.GetDriver();
            var btnUrediBrisi = driver.FindElementByAccessibilityId("btnUrediBrisi");
            btnUrediBrisi.Click();
        }

        [When(@"promjenim informacije")]
        public void WhenPromjenimInformacije()
        {
            var driver = GuiDriver.GetDriver();
            var txtNazivPosla = driver.FindElementByAccessibilityId("txtNazivPosla");
            txtNazivPosla.SendKeys("Izmjenjen");
        }

        [When(@"kliknem na gumb Uredi posao")]
        public void WhenKliknemNaGumbUrediPosao()
        {
            var driver = GuiDriver.GetDriver();
            var btnUredi = driver.FindElementByAccessibilityId("btnUredi");
            btnUredi.Click();
        }

        [Then(@"posao je izmjenjen")]
        public void ThenPosaoJeIzmjenjen()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                driver.FindElementByName("IzmjenjenOvo bude rjeseno uskoro");
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Then(@"dobijem poruku greške")]
        public void ThenDobijemPorukuGreske()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                driver.FindElementByName("Prvo odaberite posao!");
                var btnOK = driver.FindElementByName("OK");
                btnOK.Click();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [When(@"kliknem na gumb Obriši posao")]
        public void WhenKliknemNaGumbObrisiPosao()
        {
            var driver = GuiDriver.GetDriver();
            var btnObrisi = driver.FindElementByAccessibilityId("btnObrisi");
            btnObrisi.Click();
        }

        [Then(@"posao je obrisan")]
        public void ThenPosaoJeObrisan()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                driver.FindElementByName("Ovo bude rjeseno uskoro-Izmjenjen");
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }
    }
}
