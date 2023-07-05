using SlojEntiteta.Entiteti;
using SlojUpravljanjaSBazomPodataka.Sucelja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojUpravljanjaSBazomPodataka.repozitoriji
{
    public class StatusRepozitorij : Repozitori<Status>, IStatusRepozitorij
    {
        public StatusRepozitorij() : base(new ChoriumModel())
        {

        }

        public override int Azuriraj(Status entitet)
        {
            throw new NotImplementedException();
        }
    }
}
