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
    public class StatusServis_Tests
    {
        [Fact]
        public void DohvatiStatuse_ReturnsStatusList()
        {
            var statusRepo = A.Fake<IStatusRepozitorij>();
            StatusServis statusServis = new StatusServis(statusRepo);
            A.CallTo(() => statusRepo.DajSve()).Returns(new List<Status> {
                new Status {
                    ID = 0,
                    Naziv = "nedovrsen",
                    },
                new Status {
                    ID = 1,
                    Naziv = "dovrsen",
                    },
                new Status {
                    ID = 2,
                    Naziv = "na_cekanju",
                    }
            }.AsQueryable());

            var listaStatues = statusServis.DohvatiStatuse();

            Assert.Equal(3, listaStatues.Count);
        }
    }
}
