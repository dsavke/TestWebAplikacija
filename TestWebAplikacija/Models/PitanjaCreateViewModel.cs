using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWebAplikacija.Models
{
    public class PitanjaCreateViewModel
    {
        public int PitanjeId { get; set; }
        public int TestId { get; set; }
        [AllowHtml]
        public string Tekst { get; set; }
        public short BrojBodova { get; set; }

        public string PrviOdgovor { get; set; }
        public string DrugiOdgovor { get; set; }
        public string TreciOdgovor { get; set; }
        public string CetvrtiOdgovor { get; set; }
        public short TacanOdgovor { get; set; }

    }
}