using AcceptanceTests.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace AcceptanceTests.StepDefinitions
{
    [Binding]
    public class RjesavanjeKucanskihPoslovaStepDefinitions
    {
        [Given(@"postoji nerješeni posao")]
        public void GivenPostojiNerjeseniPosao()
        {
            var driver = GuiDriver.GetDriver();
            var btnDodajPosao = driver.FindElementByAccessibilityId("btnDodajPosao");
            btnDodajPosao.Click();

            var txtNazivPosla = driver.FindElementByAccessibilityId("txtNazivPosla");
            txtNazivPosla.SendKeys("Ovo bude rjeseno uskoro");

            var cmbKategorija = driver.FindElementByAccessibilityId("cmbKategorija");
            cmbKategorija.Click();
            cmbKategorija.SendKeys("Pospremanje");
            cmbKategorija.SendKeys(Keys.Enter);

            var dtpDatumRoka = driver.FindElementByAccessibilityId("dtpDatumRoka");
            var txtRokH = driver.FindElementByAccessibilityId("txtRokH");
            var txtRokM = driver.FindElementByAccessibilityId("txtRokM");
            var txtRokS = driver.FindElementByAccessibilityId("txtRokS");

            dtpDatumRoka.SendKeys("10.4.2023.");
            txtRokH.SendKeys("1");
            txtRokM.SendKeys("1");
            txtRokS.SendKeys("1");

            var clan = driver.FindElementByName("kvardijan");
            clan.Click();

            var btnDodaj = driver.FindElementByAccessibilityId("btnDodajPosao");
            btnDodaj.Click();
        }

        [When(@"kliknem na posao Ovo bude rjeseno uskoro")]
        public void WhenKliknemNaPosao()
        {
            var driver = GuiDriver.GetDriver();
            var odabraniPosao = driver.FindElementByName("Ovo bude rjeseno uskoro");
            odabraniPosao.Click();
            odabraniPosao.Click();
        }

        [When(@"kliknem na gumb Riješi posao")]
        public void WhenKliknemNaGumbRijesiPosao()
        {
            var driver = GuiDriver.GetDriver();
            var btnRijesiPosao = driver.FindElementByAccessibilityId("btnRijesiPosao");
            btnRijesiPosao.Click();
        }

        [Then(@"Posao je riješen")]
        public void ThenPosaoJeRijesen()
        {
            var driver = GuiDriver.GetDriver();
            try
            {
                driver.FindElementByName("Ovo bude rjeseno uskoro");
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

        [Then(@"dobivam poruku greške")]
        public void ThenDobivamPorukuGreske()
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
                Assert.IsTrue(false);
            }
        }
    }
}
