using FakeItEasy;
using SlojEntiteta.Entiteti;
using SlojPoslovneLogike.Servisi;
using SlojUpravljanjaSBazomPodataka.Sucelja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JedinicnoTestiranje
{
    public class KategorijaServis_Tests
    {
        [Fact]
        public void DohvatiKategorije_ReturnsListOfCategories()
        {
            var kategorijaRepo = A.Fake<IKategorijaRepozitorij>();
            KategorijaServis katServis = new KategorijaServis(kategorijaRepo);
            A.CallTo(() => kategorijaRepo.DajSve()).Returns(new List<Kategorija> {
                new Kategorija {
                    ID = 0,
                    Naziv = "rad u vrtu"
                    },
                new Kategorija {
                    ID = 1,
                    Naziv = "pospremanje",
                    },
                new Kategorija {
                    ID = 2,
                    Naziv = "kuhanje",
                    }
            }.AsQueryable());

            var listaKategorija = katServis.DohvatiKategorije();

            Assert.Equal(3, listaKategorija.Count);
        }
    }
}
