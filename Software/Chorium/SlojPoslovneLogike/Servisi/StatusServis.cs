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
    public class StatusServis
    {
        private IStatusRepozitorij _repo;
        public StatusServis(IStatusRepozitorij repo)
        {
            _repo = repo;
        }
        public List<Status> DohvatiStatuse()
        {
                return _repo.DajSve().ToList();         
        }
    }
}
