using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWebAplikacija.Models
{
    public class PocetnaAdministratorViewModel
    {
        public int TestId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        [Display(Name = "Procenat")]
        public short ProcenatBodovaZaPolaganje { get; set; }
        [Display(Name = "Datum")]
        public DateTime DatumKreiranja { get; set; }
    }
}