using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebAplikacija.DBModels;
using TestWebAplikacija.Models;

namespace TestWebAplikacija.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TestViewModel testViewModel)
        {
            using (var context = new TestContext())
            {
                Test test = new Test()
                {
                    Naziv = testViewModel.Naziv,
                    DatumKreiranja = testViewModel.DatumKreiranja,
                    Opis = testViewModel.Opis,
                    ProcenatBodovaZaPolaganje = testViewModel.ProcenatBodovaZaPolaganje
                };

                context.Tests.Add(test);
                context.SaveChanges();

                return RedirectToAction("Index", "Pocetna");
            }
        }

        public ActionResult Edit(string id)
        {
            using (var context = new TestContext())
            {
                var testId = Convert.ToInt32(id);
                var test = context.Tests.Find(testId);
                var testViewModel = new TestViewModel()
                {
                    Opis = test.Opis,
                    Naziv = test.Naziv,
                    DatumKreiranja = test.DatumKreiranja,
                    ProcenatBodovaZaPolaganje = test.ProcenatBodovaZaPolaganje,
                    TestId = test.TestId
                };
                return View(testViewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(TestViewModel testViewModel)
        {
            using (var context = new TestContext())
            {
                var test = context.Tests.Find(testViewModel.TestId);
                test.Naziv = testViewModel.Naziv;
                test.Opis = testViewModel.Opis;
                test.ProcenatBodovaZaPolaganje = testViewModel.ProcenatBodovaZaPolaganje;
                test.DatumKreiranja = testViewModel.DatumKreiranja;

                context.SaveChanges();
                return RedirectToAction("Index", "Pocetna");
            }
        }

        public ActionResult Detail(string id)
        {
            using (var context = new TestContext())
            {
                var testId = Convert.ToInt32(id);
                var test = context.Tests.Find(testId);

                var detaljiTestViewModel = new TestDetaljiViewModel()
                {
                    Naziv = test.Naziv,
                    Opis = test.Opis,
                    DatumKreiranja = test.DatumKreiranja,
                    ProcenatBodovaZaPolaganje = test.ProcenatBodovaZaPolaganje,
                    TestId = test.TestId,
                    Pitanjas = test.Pitanjes
                    .Select(p => new PitanjaViewModel()
                    {
                        BrojBodova = p.BrojBodova,
                        PitanjeId = p.PitanjeId,
                        Tekst = p.Tekst,
                        TestId = p.TestId,
                        Odgovors = p.Odgovors
                        .Select(o => new OdgovorViewModel()
                        {
                            Tekst = o.Tekst,
                            Tacan = o.Tacan,
                            PitanjeId = o.PitanjeId,
                            OdgovorId = o.OdgovorId
                        }).ToList()
                    }).ToList()
                };



                return View(detaljiTestViewModel);
            }
        }

        public ActionResult Polozi(string id)
        {
            using(var context = new TestContext())
            {

                var testId = Convert.ToInt32(id);

                var test = context.Tests.Find(testId).Pitanjes
                    .Select(t => new UradiTestViewModel()
                    {
                        Tekst = t.Tekst,
                        PitanjeId = t.PitanjeId,
                        PrviOdgovor = t.Odgovors.ElementAt(0).Tekst,
                        DrugiOdgovor = t.Odgovors.ElementAt(1).Tekst,
                        TreciOdgovor = t.Odgovors.ElementAt(2).Tekst,
                        CetvrtiOdgovor = t.Odgovors.ElementAt(3).Tekst,
                        PrviId = t.Odgovors.ElementAt(0).OdgovorId,
                        DrugiId = t.Odgovors.ElementAt(1).OdgovorId,
                        TreciId = t.Odgovors.ElementAt(2).OdgovorId,
                        CetvrtiId = t.Odgovors.ElementAt(3).OdgovorId,
                        BrojBodova = t.BrojBodova,
                        TestId = t.TestId
                    }).ToList();

                return View(test);
            }
        }

        [HttpPost]
        public ActionResult Polozi(List<UradiTestViewModel> Model)
        {
            using (var context = new TestContext())
            {

                var korisnik = context.Korisniks.ToList().FirstOrDefault(k => k.KorisnickoIme == User.Identity.Name);

                KorisnikTest korisnikTest = new KorisnikTest()
                {
                    Datum = DateTime.Now,
                    KorisnikId = korisnik.KorisnikId,
                    TestId = Model.ElementAt(0).TestId,
                    BrojBodova = (short)Model.Where(p => context.Odgovors.Find(p.TacanOdgovor).Tacan != null).Sum(p => p.BrojBodova)
                };
                context.KorisnikTests.Add(korisnikTest);
                context.SaveChanges();
                return RedirectToAction("Index", "Pocetna");
            }
        }

    }
}