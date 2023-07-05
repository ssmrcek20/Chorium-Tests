using SlojEntiteta.Entiteti;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojUpravljanjaSBazomPodataka.Sucelja
{
    public interface IKorisnikRepozitorij : IRepozitori<Korisnik>
    {
        bool ProvjeriKorisnikoIme(string korisnickoIme);
        Korisnik DohvatiKorisnika(string korime);

        int Azuriraj(Korisnik entitet);
    }
}
