using SlojEntiteta.Entiteti;
using SlojUpravljanjaSBazomPodataka.Sucelja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojUpravljanjaSBazomPodataka.repozitoriji
{
    public class KategorijaRepozitorij : Repozitori<Kategorija>, IKategorijaRepozitorij
    {
        public KategorijaRepozitorij() : base(new ChoriumModel())
        {
        }

        public override int Azuriraj(Kategorija entitet)
        {
            throw new NotImplementedException();
        }
    }
}
