using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebAplikacija.Models
{
    public class KorisnikViewModel
    {
        public int KorisnikId { get; set; }
        public short UlogaId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
    }
}