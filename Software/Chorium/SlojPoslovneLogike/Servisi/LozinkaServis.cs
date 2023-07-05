using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;

namespace SlojPoslovneLogike.Servisi
{
    public class LozinkaServis
    {
        public string HashirajLozinku(string lozinka, string korIme)
        {
            if(string.IsNullOrWhiteSpace(lozinka) || string.IsNullOrWhiteSpace(korIme))
            {
                throw new ArgumentNullException();
            }
            byte[] salt = generirajSalt(korIme);
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(lozinka, salt, 100000);
            return Convert.ToBase64String(pbkdf2.GetBytes(32));
        }

        private byte[] generirajSalt(string korIme)
        {
            byte[] salt = Encoding.UTF8.GetBytes(korIme + "AGKHUCHELKJSLDIAJ");
            return  SHA256.Create().ComputeHash(salt);
        }

        public string ProblemiSaLozinkom(string lozinka, string korIme)
        {
            if (string.IsNullOrWhiteSpace(lozinka) || string.IsNullOrWhiteSpace(korIme))
            {
                throw new ArgumentNullException();
            }

            string poruka = string.Empty;
            if(lozinka.Length < 8)
            {
                poruka += "- Lozinka mora imati barem 8 znakova.\n";
            }
            if(!lozinka.Any(char.IsLower) || !lozinka.Any(char.IsUpper) || !lozinka.Any(char.IsNumber))
            {
                poruka += "- Lozinka mora sadržavati barem jedno malo slovo, jedno veliko slovo i jedan broj.\n";
            }
            if(lozinka.ToLower().Contains(korIme.ToLower()))
            {
                poruka += "- Lozinka ne smije imati korisničko ime u sebi.\n";
            }
            return poruka;
        }
    }
}
