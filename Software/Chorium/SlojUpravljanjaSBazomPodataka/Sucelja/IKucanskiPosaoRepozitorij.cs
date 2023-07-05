using SlojEntiteta.Entiteti;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojUpravljanjaSBazomPodataka.Sucelja
{
    public interface IKucanskiPosaoRepozitorij : IRepozitori<Kucanski_posao>
    {
        int Azuriraj(Kucanski_posao entitet);
        int Dodaj(Kucanski_posao kucanski_Posao);
        IQueryable<Kucanski_posao> DohvatiPosloveUjutro(DateTime datum, Korisnik korisnik);
        IQueryable<Kucanski_posao> DohvatiPosloveNavecer(DateTime datum, Korisnik korisnik);
        IQueryable<Kucanski_posao> DohvatiObavljenePosloveKorisnika(DateTime datum, Korisnik korisnik);
        IQueryable<Kucanski_posao> DohvatiPosloveKorisnika(Korisnik korisnik);
        int Rijesi(Kucanski_posao kucanski_Posao);
        int StaviNaCekanje(Kucanski_posao kucanski_Posao);
    }
}
