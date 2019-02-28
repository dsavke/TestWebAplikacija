using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWebAplikacija.Models
{
    public class RezultatTestaViewModel
    {
        [Display(Name = "Broj tacnih odgovora")]
        public int BrojTacnihBodova { get; set; }
        [Display(Name = "Broj pogresnih odgovora")]
        public int BrojNeTacnihBodova { get; set; }
        [Display(Name = "Ukupan broj bodova")]
        public int UkupanBrojBodova { get; set; }
        [Display(Name = "Procenat osvojenih bodova")]
        public int ProcenatOsvojenihBodova { get; set; }
    }
}