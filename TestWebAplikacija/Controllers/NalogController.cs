using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestWebAplikacija.DBModels;
using TestWebAplikacija.Models;

namespace TestWebAplikacija.Controllers
{
    public class NalogController : Controller
    {
        [AllowAnonymous]
        public ActionResult UlogujSe()
        {
            return View();
        }

        // POST: Account/Create
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UlogujSe(NalogViewModel viewModel, string returnUrl)
        {
            using (var context = new TestContext())
            {

                var korisnik = context.Korisniks.ToList().FirstOrDefault(k => k.KorisnickoIme == viewModel.KorisnickoIme && k.Lozinka == viewModel.Lozinka);

                if (korisnik != null)
                {
                    var authTicket = new FormsAuthenticationTicket(
                                                     1,
                                                     viewModel.KorisnickoIme,
                                                     DateTime.Now,
                                                     DateTime.Now.AddMinutes(30),
                                                     false,
                                                     korisnik.Uloga.Naziv
                                                     );

                    var encTicket = FormsAuthentication.Encrypt(authTicket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(cookie);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Pocetna");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Pogrešno korisničko ime ili lozinka");
                    return View();
                }

            }

        }

        [Authorize(Roles = "Administrator, Korisnik")]
        public ActionResult IzlogujSe()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("UlogujSe", "Nalog");
        }
    }
}