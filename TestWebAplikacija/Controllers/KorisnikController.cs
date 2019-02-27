using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebAplikacija.DBModels;
using TestWebAplikacija.Models;
using System.Linq.Dynamic;

namespace TestWebAplikacija.Controllers
{
    public class KorisnikController : Controller
    {
        // GET: Korisnik
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult List(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                using (var context = new TestContext())
                {
                    var korisnici = context.Korisniks
                        .Select(k => new KorisnikViewModel()
                        {
                            UlogaId = k.UlogaId,
                            Email = k.Email,
                            Ime = k.Ime,
                            KorisnickoIme = k.KorisnickoIme,
                            KorisnikId = k.KorisnikId,
                            Lozinka = k.Lozinka,
                            Prezime = k.Prezime
                        }).ToList();

                    var count = korisnici.Count();
                    var records = korisnici.OrderBy(jtSorting).Skip(jtStartIndex).Take(jtPageSize).ToList();

                    //Return result to jTable
                    return Json(new { Result = "OK", Records = records, TotalRecordCount = count });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult Create(KorisnikViewModel korisnikViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }
                using (var context = new TestContext())
                {

                    Korisnik korisnik = new Korisnik()
                    {
                        Ime = korisnikViewModel.Ime,
                        Prezime = korisnikViewModel.Prezime,
                        Email = korisnikViewModel.Email,
                        KorisnickoIme = korisnikViewModel.KorisnickoIme,
                        Lozinka = korisnikViewModel.Lozinka,
                        UlogaId = korisnikViewModel.UlogaId
                    };

                    context.Korisniks.Add(korisnik);
                    context.SaveChanges();

                    return Json(new { Result = "OK", Record = korisnikViewModel });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult Update(KorisnikViewModel korisnikViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }
                using (var context = new TestContext())
                {
                    Korisnik korisnik = context.Korisniks.Find(korisnikViewModel.KorisnikId);

                    korisnik.Ime = korisnikViewModel.Ime;
                    korisnik.Prezime = korisnikViewModel.Prezime;
                    korisnik.Email = korisnikViewModel.Email;
                    korisnik.KorisnickoIme = korisnikViewModel.KorisnickoIme;
                    korisnik.UlogaId = korisnikViewModel.UlogaId;
                    context.SaveChanges();
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult Delete(int KorisnikId)
        {
            try
            {
                using (var context = new TestContext())
                {
                    context.Korisniks.Remove(context.Korisniks.Find(KorisnikId));
                    context.SaveChanges();
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public JsonResult ListaUloga()
        {
            try
            {
                using (var context = new TestContext())
                {
                    var uloge = context.Ulogas.Select(k =>
                    new
                    {
                        Value = k.UlogaId,
                        DisplayText = k.Naziv
                    }).ToList();

                    return Json(new { Result = "OK", Options = uloge });

                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


    }
}