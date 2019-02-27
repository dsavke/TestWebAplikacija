using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebAplikacija.Models
{
    public class OdgovorViewModel
    {
        public int OdgovorId { get; set; }
        public int PitanjeId { get; set; }
        public string Tekst { get; set; }
        public Nullable<bool> Tacan { get; set; }
    }
}