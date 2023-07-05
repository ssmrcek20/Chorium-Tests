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
    public class AktivnostServis
    {
        private IAktivnostiRepozitorij _repo;

        public AktivnostServis(IAktivnostiRepozitorij repo)
        {
            _repo = repo;
        }
        public List<Aktivnost> DajAktivnostiZaDan(DateTime dan)
        {
            List<Aktivnost> aktivnosti = _repo.DajAktivnostiZaDan(dan).ToList();
            return aktivnosti;
        }

        public bool DodajKorisnikaUAktivnost(Aktivnost aktivnost, Korisnik korisnik)
        {
            int redovi = _repo.DodajKorisnikaUAktivnost(aktivnost, korisnik);
            bool uspjeh = redovi > 0;

            return uspjeh;
        }

        public bool DodajAktivnost(Aktivnost aktivnost, Korisnik korisnik)
        {
            int redovi = _repo.DodajAktivnost(aktivnost, korisnik);
            bool uspjeh = redovi > 0;

            return uspjeh;
        }
    }
}
