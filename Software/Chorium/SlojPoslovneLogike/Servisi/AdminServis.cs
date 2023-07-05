using SlojEntiteta.Entiteti;
using SlojUpravljanjaSBazomPodataka.repozitoriji;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPoslovneLogike.Servisi
{
    public class AdminServis
    {
        public bool UpdateUsername(Korisnik korisnik)
        {
            if (korisnik == null) return false;

            KorisnikRepozitorij korisnikRepozitorij = new KorisnikRepozitorij();
            KorisnikServis korisnikServis = new KorisnikServis(korisnikRepozitorij);
            
            return korisnikServis.AzurirajKorisnika(korisnik) > 0;
        }

        public bool CheckEnteredUsername(string username)
        {
            return !string.IsNullOrEmpty(username.Trim());
        }
    }
}
