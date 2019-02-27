using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWebAplikacija.Models
{
    public class TestViewModel
    {
        public int TestId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public short ProcenatBodovaZaPolaganje { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatumKreiranja { get; set; }
    }
}