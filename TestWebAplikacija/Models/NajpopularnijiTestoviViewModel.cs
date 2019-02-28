using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebAplikacija.Models
{
    public class NajpopularnijiTestoviViewModel
    {
        public int TestId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int BrojPolaganja { get; set; }
    }
}