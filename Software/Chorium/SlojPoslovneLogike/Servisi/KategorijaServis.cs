using SlojEntiteta.Entiteti;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using SlojUpravljanjaSBazomPodataka.Sucelja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPoslovneLogike.Servisi
{
    public class KategorijaServis
    {
        private IKategorijaRepozitorij _repo;
        public KategorijaServis(IKategorijaRepozitorij repo)
        {
            _repo = repo;
        }
        public List<Kategorija> DohvatiKategorije()
        {
                return _repo.DajSve().ToList();
        }
    }
}
