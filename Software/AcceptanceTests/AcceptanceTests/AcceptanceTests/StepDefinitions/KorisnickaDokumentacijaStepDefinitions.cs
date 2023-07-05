using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class KorisnickaDokumentacijaStepDefinitions
    {
        [When(@"ja kliknem F1")]
        public void WhenJaKliknemF()
        {
            var driver = GuiDriver.GetDriver();
            var chorium = driver.FindElementByName("Chorium");
            chorium.SendKeys(Keys.F1);
        }

        [Then(@"ja vidim korisnicku dokumentaciju")]
        public void ThenJaVidimKorisnickuDokumentaciju()
        {
            var driver = GuiDriver.GetDriver();
            bool postojiGumbPrint = driver.FindElementByName("Print") != null;
            Assert.IsTrue(postojiGumbPrint);
        }
    }
}
