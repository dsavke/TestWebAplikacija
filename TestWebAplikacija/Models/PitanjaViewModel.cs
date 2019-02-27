using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebAplikacija.Models
{
    public class PitanjaViewModel
    {
        public int PitanjeId { get; set; }
        public int TestId { get; set; }
        public string Tekst { get; set; }
        public short BrojBodova { get; set; }

        public List<OdgovorViewModel> Odgovors { get; set; }
    }
}