using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestWebAplikacija.Models
{
    public class NalogViewModel
    {
        [Required()]
        [MaxLength(20)]
        [MinLength(5)]
        public string KorisnickoIme { get; set; }
        [Required()]
        [MaxLength(20)]
        [MinLength(4)]
        public string Lozinka { get; set; }
    }
}