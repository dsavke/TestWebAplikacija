using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebAplikacija.Models
{
    public class SvaPolaganjaViewModel
    {
        public string KorisnickoIme { get; set; }
        public DateTime DatumPolaganja { get; set; }
        public short ProcenatOsvojenihBodova { get; set; }
        public short BrojBodova { get; set; }
    }
}