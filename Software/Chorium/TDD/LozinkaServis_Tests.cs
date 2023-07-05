using SlojPoslovneLogike.Servisi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TDD
{
    public class LozinkaServis_Tests
    {
        [Fact]
        public void HashirajLozinku_ProsljedenaLozinkaJeNull_BaciGresku()
        {
            var servis = new LozinkaServis();
            string lozinka = null;
            string korIme = null;

            Action act = () => servis.HashirajLozinku(lozinka,korIme);

            Assert.Throws<ArgumentNullException>(act);
        }
        [Fact]
        public void HashirajLozinku_ProsljedenJePrazanTekst_BaciGresku()
        {
            var servis = new LozinkaServis();
            string lozinka = "";
            string korIme = "";

            Action act = () => servis.HashirajLozinku(lozinka,korIme);

            Assert.Throws<ArgumentNullException>(act);
        }
        [Fact]
        public void HashirajLozinku_ProsljedenaJeLozinka_VratiHashiranuLozinku()
        {
            var servis = new LozinkaServis();
            string lozinka = "lozinka";
            string korIme = "korIme";
            string ocekivanaLozinka = "9espfD9NyWNl66d6S0JTrIXBcO4lXNthBcAoHFljD7w=";

            string hashiranaLozinka = servis.HashirajLozinku(lozinka,korIme);

            Assert.Equal(ocekivanaLozinka, hashiranaLozinka);
        }
        [Fact]
        public void HashirajLozinku_ProsljedenaJeIstaLozinkaSDrukcijimKorisnickimImenom_VraceneLozinkeNisuIste()
        {
            var servis = new LozinkaServis();
            string lozinka = "lozinka";
            string korIme1 = "korIme1";
            string korIme2 = "korIme2";

            string hashiranaLozinka1 = servis.HashirajLozinku(lozinka, korIme1);
            string hashiranaLozinka2 = servis.HashirajLozinku(lozinka, korIme2);

            Assert.NotEqual(hashiranaLozinka1 , hashiranaLozinka2);
        }
        [Fact]
        public void HashirajLozinku_ProsljedenaJeLozinkaSaSpecijalnimZnakovima_VratiHashiranuLozinku()
        {
            var servis = new LozinkaServis();
            string lozinka = "123Lozi nka&/%(Čž";
            string korIme = "korIme";
            string ocekivanaLozinka = "XdnugtCVLHEQrHucUkN9ErTvmZ4PUpdonEoLmmQFusI=";

            string hashiranaLozinka = servis.HashirajLozinku(lozinka, korIme);

            Assert.Equal(ocekivanaLozinka, hashiranaLozinka);
        }
        [Fact]
        public void HashirajLozinku_ProsljedenJeKorImeSaSpecijalnimZnakovima_VratiHashiranuLozinku()
        {
            var servis = new LozinkaServis();
            string lozinka = "lozinka";
            string korIme = "123Kor Ime&/%(Čž";
            string ocekivanaLozinka = "8p62MNa9LQmD/YP24i7z9LeN0GuQvNrSR71w2G88/dI=";

            string hashiranaLozinka = servis.HashirajLozinku(lozinka, korIme);

            Assert.Equal(ocekivanaLozinka, hashiranaLozinka);
        }


        [Fact]
        public void ProblemiSaLozinkom_ProsljedenaLozinkaJeNull_BaciGresku()
        {
            var servis = new LozinkaServis();
            string lozinka = null;
            string korIme = null;

            Action act = () => servis.ProblemiSaLozinkom(lozinka, korIme);

            Assert.Throws<ArgumentNullException>(act);
        }
        [Fact]
        public void ProblemiSaLozinkom_ProsljedenJePrazanTekst_BaciGresku()
        {
            var servis = new LozinkaServis();
            string lozinka = "";
            string korIme = "";

            Action act = () => servis.ProblemiSaLozinkom(lozinka, korIme);

            Assert.Throws<ArgumentNullException>(act);
        }
        [Fact]
        public void ProblemiSaLozinkom_ProsljedenJeIspravnaLozinka_VratiPrazanString()
        {
            var servis = new LozinkaServis();
            string lozinka = "trEj334!sD";
            string korIme = "test";

            string problemi = servis.ProblemiSaLozinkom(lozinka, korIme);

            Assert.Empty(problemi);
        }
        [Fact]
        public void ProblemiSaLozinkom_ProsljedenJeKratkaLozinka_VratiGresku()
        {
            var servis = new LozinkaServis();
            string lozinka = "trEj334";
            string korIme = "test";
            string ocekivaniProblem = "- Lozinka mora imati barem 8 znakova.\n";

            string problemi = servis.ProblemiSaLozinkom(lozinka, korIme);

            Assert.Equal(ocekivaniProblem, problemi);
        }
        [Fact]
        public void ProblemiSaLozinkom_ProsljedenJeLozinkaBezBroja_VratiGresku()
        {
            var servis = new LozinkaServis();
            string lozinka = "trEjadfaFAA";
            string korIme = "test";
            string ocekivaniProblem = "- Lozinka mora sadržavati barem jedno malo slovo, jedno veliko slovo i jedan broj.\n";

            string problemi = servis.ProblemiSaLozinkom(lozinka, korIme);

            Assert.Equal(ocekivaniProblem, problemi);
        }
        [Fact]
        public void ProblemiSaLozinkom_ProsljedenJeLozinkaBezMalogSlova_VratiGresku()
        {
            var servis = new LozinkaServis();
            string lozinka = "ASKJDKD2176";
            string korIme = "test";
            string ocekivaniProblem = "- Lozinka mora sadržavati barem jedno malo slovo, jedno veliko slovo i jedan broj.\n";

            string problemi = servis.ProblemiSaLozinkom(lozinka, korIme);

            Assert.Equal(ocekivaniProblem, problemi);
        }
        [Fact]
        public void ProblemiSaLozinkom_ProsljedenJeLozinkaBezVelikogSlova_VratiGresku()
        {
            var servis = new LozinkaServis();
            string lozinka = "jshdasfd34324";
            string korIme = "test";
            string ocekivaniProblem = "- Lozinka mora sadržavati barem jedno malo slovo, jedno veliko slovo i jedan broj.\n";

            string problemi = servis.ProblemiSaLozinkom(lozinka, korIme);

            Assert.Equal(ocekivaniProblem, problemi);
        }
        [Fact]
        public void ProblemiSaLozinkom_ProsljedenJeLozinkaSaKorImenomUSebi_VratiGresku()
        {
            var servis = new LozinkaServis();
            string lozinka = "SStest12345";
            string korIme = "test";
            string ocekivaniProblem = "- Lozinka ne smije imati korisničko ime u sebi.\n";

            string problemi = servis.ProblemiSaLozinkom(lozinka, korIme);

            Assert.Equal(ocekivaniProblem, problemi);
        }
        [Fact]
        public void ProblemiSaLozinkom_ProsljedenJeLozinkaSaKorImenomUSebiAliVelikimSlovima_VratiGresku()
        {
            var servis = new LozinkaServis();
            string lozinka = "ssTESTs12345";
            string korIme = "test";
            string ocekivaniProblem = "- Lozinka ne smije imati korisničko ime u sebi.\n";

            string problemi = servis.ProblemiSaLozinkom(lozinka, korIme);

            Assert.Equal(ocekivaniProblem, problemi);
        }
        [Fact]
        public void ProblemiSaLozinkom_ProsljedenJeLozinkaSaSvimGreskama_VratiGresku()
        {
            var servis = new LozinkaServis();
            string lozinka = "test";
            string korIme = "test";
            string ocekivaniProblem = "- Lozinka mora imati barem 8 znakova.\n" +
                "- Lozinka mora sadržavati barem jedno malo slovo, jedno veliko slovo i jedan broj.\n" +
                "- Lozinka ne smije imati korisničko ime u sebi.\n";

            string problemi = servis.ProblemiSaLozinkom(lozinka, korIme);

            Assert.Equal(ocekivaniProblem, problemi);
        }
    }
}
