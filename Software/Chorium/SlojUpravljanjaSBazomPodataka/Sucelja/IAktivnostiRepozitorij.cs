using SlojEntiteta.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojUpravljanjaSBazomPodataka.Sucelja
{
    public interface IAktivnostiRepozitorij : IRepozitori<Aktivnost>
    {
        IQueryable<Aktivnost> DajAktivnostiZaDan(DateTime dan);

        int DodajKorisnikaUAktivnost(Aktivnost aktivnost, Korisnik korisnik);

        int DodajAktivnost(Aktivnost aktivnost, Korisnik korisnik);
    }
}
