using SlojEntiteta.Entiteti;
using SlojUpravljanjaSBazomPodataka.Sucelja;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojUpravljanjaSBazomPodataka.repozitoriji
{
    public class AktivnostiRepozitorij : Repozitori<Aktivnost>, IAktivnostiRepozitorij
    {
        public AktivnostiRepozitorij() : base(new ChoriumModel())
        {
        }

        public IQueryable<Aktivnost> DajAktivnostiZaDan(DateTime dan)
        {
            var query = from a in Entiteti.Include("Korisnik")
                        where DbFunctions.TruncateTime(a.Datum_pocetka) == DbFunctions.TruncateTime(dan)
                        select a;
            return query;
        }

        public int DodajKorisnikaUAktivnost(Aktivnost aktivnost, Korisnik korisnik)
        {
            Entiteti.Attach(aktivnost);
            var noviKorisnik = Context.Korisnik.SingleOrDefault(k => k.ID == korisnik.ID);
            aktivnost.Korisnik.Add(noviKorisnik);
            return SpremiPromjene();
        }

        public int DodajAktivnost(Aktivnost aktivnost, Korisnik korisnik)
        {
            var noviKorisnik = Context.Korisnik.SingleOrDefault(k => k.ID == korisnik.ID);
            aktivnost.Korisnik.Add(noviKorisnik);
            Entiteti.Add(aktivnost);
            return SpremiPromjene();
        }

        public override int Azuriraj(Aktivnost entitet)
        {
            throw new NotImplementedException();
        }
    }
}
