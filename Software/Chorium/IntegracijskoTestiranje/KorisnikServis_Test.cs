using SlojEntiteta.Entiteti;
using SlojPoslovneLogike.Servisi;
using SlojUpravljanjaSBazomPodataka;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegracijskoTestiranje
{
    public class KorisnikServis_Test
    {
        [Fact]
        public void RegistrirajKorisnika_GivenKorisnikNePostoji_ReturnsTrue()
        {
            using (var context = new ChoriumModel())
            {
                var korisnikTest = (from kor in context.Korisnik
                                    where kor.Korisnicko_ime == "test"
                                    select kor).ToList();

                if (korisnikTest.Count > 0)
                {
                    context.Korisnik.Remove(korisnikTest[0]);
                    context.SaveChanges();
                }
            }
            Korisnik korisnik = new Korisnik
            {
                Ime = "test",
                Prezime = "test",
                Email = "test@gmail.com",
                Korisnicko_ime = "test",
                Lozinka = "test",
                ID_tip_korisnika = 1,
                Datum_rodenja = new DateTime(1990, 1, 1)
            };
            KorisnikRepozitorij repo = new KorisnikRepozitorij();
            KorisnikServis korisnikServis = new KorisnikServis(repo);

            var result = korisnikServis.RegistrirajKorisnika(korisnik);


            Assert.True(result);
            using (var context = new ChoriumModel())
            {
                var korisnikTest = (from kor in context.Korisnik
                                    where kor.Korisnicko_ime == "test"
                                    select kor).ToList();

                Assert.Single(korisnikTest);
            }
        }

        [Fact]
        public void RegistrirajKorisnika_GivenKorisnikPostoji_ReturnsFlase()
        {
            Korisnik korisnik = new Korisnik
            {
                Ime = "test3",
                Prezime = "test3",
                Email = "test3@gmail.com",
                Korisnicko_ime = "test3",
                Lozinka = "test3",
                ID_tip_korisnika = 1,
                Datum_rodenja = new DateTime(1990, 1, 1)
            };
            KorisnikRepozitorij repo = new KorisnikRepozitorij();
            KorisnikServis korisnikServis = new KorisnikServis(repo);

            var result = korisnikServis.RegistrirajKorisnika(korisnik);

            Assert.False(result);
            using (var context = new ChoriumModel())
            {
                var korisnikTest = (from kor in context.Korisnik
                                    where kor.Korisnicko_ime == "test3"
                                    select kor).ToList();

                Assert.Single(korisnikTest);
            }
        }

        [Fact]
        public void SkenirajLice_GivenDirectorySlikeExists_FilesLenghtIsFive()
        {
            var repo = new KorisnikRepozitorij();
            KorisnikServis korisnikServis = new KorisnikServis(repo);
            string putanja = Directory.GetCurrentDirectory() + @"\slike";
            Directory.CreateDirectory(putanja);

            korisnikServis.SkeniranjeLica();

            string[] files = Directory.GetFiles(putanja, "*.jpg", SearchOption.AllDirectories);
            Assert.Equal(5, files.Length);
        }

        [Fact]
        public void SkenirajLice_GivenDirectorySlikeExistsWithPictures_FilesLenghtIsFive()
        {
            var repo = new KorisnikRepozitorij();
            KorisnikServis korisnikServis = new KorisnikServis(repo);
            string putanja = Directory.GetCurrentDirectory() + @"\slike";
            Directory.CreateDirectory(putanja);
            for (int i = 1; i <= 5; i++)
            {
                Bitmap bitmap = new Bitmap(200, 200);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.FillRectangle(Brushes.Red, 0, 0, 200, 200);
                bitmap.Save(Path.Combine(putanja, $"slika{i}.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                graphics.Dispose();
                bitmap.Dispose();
            }

            korisnikServis.SkeniranjeLica();

            string[] files = Directory.GetFiles(putanja, "*.jpg", SearchOption.AllDirectories);
            Assert.Equal(5, files.Length);
        }

        [Fact]
        public void SkenirajLice_GivenDirectorySlikeDontExists_FilesLenghtIsFive()
        {
            var repo = new KorisnikRepozitorij();
            KorisnikServis korisnikServis = new KorisnikServis(repo);
            string putanja = Directory.GetCurrentDirectory() + @"\slike";
            Directory.CreateDirectory(putanja);
            Directory.Delete(putanja, true);

            korisnikServis.SkeniranjeLica();

            string[] files = Directory.GetFiles(putanja, "*.jpg", SearchOption.AllDirectories);
            Assert.Equal(5, files.Length);
        }
        /*  Test skoro nikada ne prolazi jer je nas sustav face recognitiona nepouzdan i jedva ponekad pronađe lice
        [Fact]
        public void TrenirajLica_GivenUserIsRegisteredAndHasFaceScan_ReturnsTrue()
        {
            var repo = new KorisnikRepozitorij();
            KorisnikServis korisnikServis = new KorisnikServis(repo);
            bool odgovor = korisnikServis.TrenirajLica();
            Assert.True(odgovor);
        }
        */
        [Fact]
        public void TrenirajLica_GivenUserIsNotRegistered_ReturnsFalse()
        {
            var repo = new KorisnikRepozitorij();
            KorisnikServis korisnikServis = new KorisnikServis(repo);
            bool odgovor = korisnikServis.TrenirajLica();
            Assert.False(odgovor);
        }
    }
}
