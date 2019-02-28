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
        [AllowAnonymous]
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
                }).OrderByDescending(t => t.DatumKreiranja).ToList();

                var najpopularnijitestovi = context.Tests
                        .Select(t => new NajpopularnijiTestoviViewModel()
                        {
                            TestId = t.TestId,
                            Naziv = t.Naziv,
                            Opis = t.Opis,
                            BrojPolaganja = t.KorisnikTests.Count
                        }).ToList().OrderByDescending(t => t.BrojPolaganja).Take(5);

                ViewBag.NajpopularnijiTestovi = najpopularnijitestovi;

                var triNajboljaKandidata = context.Korisniks.Where(k => k.Uloga.Naziv == "Korisnik" && k.KorisnikTests.Count > 0)
                    .Select(k => new TriNajboljaKandidataViewModel() {
                        Ime = k.Ime,
                        Prezime = k.Prezime,
                        KorisnickoIme = k.KorisnickoIme,
                        Prosjek = k.KorisnikTests.Average(t => ((((double)t.BrojBodova / (double)(t.Test.Pitanjes.Sum(p => p.BrojBodova))) * 100.00)))
                    }).OrderByDescending(k => k.Prosjek).Take(3).ToList();

                ViewBag.triNajboljaKandidata = triNajboljaKandidata;

                return View(testovi);
            }
        }
    }
}