using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestWebAplikacija.DBModels;
using TestWebAplikacija.Models;

namespace TestWebAplikacija.Controllers
{
    public class PitanjaController : Controller
    {
        
        public ActionResult Create(string id)
        {
            return View(new PitanjaCreateViewModel() { TestId = Convert.ToInt32(id)});
        }

        [HttpPost]
        public ActionResult Create(PitanjaCreateViewModel pitanjaCreateViewModel)
        {
            using (var context = new TestContext())
            {
                Pitanje pitanje = new Pitanje()
                {
                    BrojBodova = pitanjaCreateViewModel.BrojBodova,
                    Tekst = pitanjaCreateViewModel.Tekst,
                    TestId = pitanjaCreateViewModel.TestId
                };

                pitanje.Odgovors.Add(new Odgovor() { Tekst = pitanjaCreateViewModel.PrviOdgovor });
                pitanje.Odgovors.Add(new Odgovor() { Tekst = pitanjaCreateViewModel.DrugiOdgovor });
                pitanje.Odgovors.Add(new Odgovor() { Tekst = pitanjaCreateViewModel.TreciOdgovor });
                pitanje.Odgovors.Add(new Odgovor() { Tekst = pitanjaCreateViewModel.CetvrtiOdgovor });

                for(int i = 0; i < pitanje.Odgovors.Count; i++)
                {
                    if ((i + 1) == pitanjaCreateViewModel.TacanOdgovor)
                        pitanje.Odgovors.ElementAt(i).Tacan = true;
                }

                context.Pitanjes.Add(pitanje);
                context.SaveChanges();

                return RedirectToAction("Detail", "Test", new { id = pitanjaCreateViewModel.TestId });
            }
        }
    }
}