using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebAplikacija.DBModels;
using TestWebAplikacija.Models;

namespace TestWebAplikacija.Controllers
{
    public class PocetnaController : Controller
    {
     
        public ActionResult Index()
        {
            using (var context = new TestContext())
            {
                var testovi = context.Tests.Select(t =>
                new PocetnaAdministratorViewModel()
                {
                    Naziv = t.Naziv,
                    DatumKreiranja = t.DatumKreiranja,
                    Opis = t.Opis,
                    ProcenatBodovaZaPolaganje = t.ProcenatBodovaZaPolaganje,
                    TestId = t.TestId
                }).ToList();
                return View(testovi);
            }
        }
    }
}