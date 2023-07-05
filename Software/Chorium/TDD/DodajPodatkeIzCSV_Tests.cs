using SlojPoslovneLogike.Servisi;
using SlojUpravljanjaSBazomPodataka;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TDD {
    public class DodajPodatkeIzCSV_Tests {

        private void ObrisiNoveUnose() {
            using (var context = new ChoriumModel()) {
                var posao1 = context.Kucanski_posao.Include("Korisnik1").FirstOrDefault(p => p.Naziv == "posaoTest3");
                var posao2 = context.Kucanski_posao.Include("Korisnik1").FirstOrDefault(p => p.Naziv == "posaoTest4");
                var posao3 = context.Kucanski_posao.Include("Korisnik1").FirstOrDefault(p => p.Naziv == "posaoTest5");
                var posao4 = context.Kucanski_posao.Include("Korisnik1").FirstOrDefault(p => p.Naziv == "posaoTest6");

                if (posao1 != null) {
                    posao1.Korisnik1.Clear();
                    context.Kucanski_posao.Remove(posao1);
                }

                if (posao2 != null) {
                    posao2.Korisnik1.Clear();
                    context.Kucanski_posao.Remove(posao2);
                }

                if (posao3 != null) {
                    posao3.Korisnik1.Clear();
                    context.Kucanski_posao.Remove(posao3);
                }

                if (posao4 != null) {
                    posao4.Korisnik1.Clear();
                    context.Kucanski_posao.Remove(posao4);
                }

                context.SaveChanges();
            }
        }

        [Fact]
        public void UpisiPodatke_GivenValidCSV_ReturnsTrue() {
            ObrisiNoveUnose();
            var posaoRepo = new KucanskiPosaoRepozitorij();
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);
            string path = Directory.GetCurrentDirectory();
            bool rezultat = kucanskiPosaoServis.UpisiPodatke(path + "\\TestData.csv");
            Assert.True(rezultat);
            ObrisiNoveUnose();
        }
        [Fact]
        public void UpisiPodatke_GivenInvalidCSV_ReturnsFalse() {
            ObrisiNoveUnose();
            var posaoRepo = new KucanskiPosaoRepozitorij();
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);
            string path = Directory.GetCurrentDirectory();
            bool rezultat = kucanskiPosaoServis.UpisiPodatke(path + "\\TestData2.csv");
            Assert.False(rezultat);
            ObrisiNoveUnose();
        }
        [Fact]
        public void UpisiPodatke_GivenWrongPath_ReturnsFalse() {
            ObrisiNoveUnose();
            var posaoRepo = new KucanskiPosaoRepozitorij();
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);
            string path = Directory.GetCurrentDirectory();
            bool rezultat = kucanskiPosaoServis.UpisiPodatke(path + "\\TestDataaa.csv");
            Assert.False(rezultat);
        }
        [Fact]
        public void UpisiPodatke_GivenEmptyCSV_ReturnsFalse() {
            ObrisiNoveUnose();
            var posaoRepo = new KucanskiPosaoRepozitorij();
            KucanskiPosaoServis kucanskiPosaoServis = new KucanskiPosaoServis(posaoRepo);
            string path = Directory.GetCurrentDirectory();
            bool rezultat = kucanskiPosaoServis.UpisiPodatke(path + "\\TestData3.csv");
            Assert.False(rezultat);
        }
    }
}
