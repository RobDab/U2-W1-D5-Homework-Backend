using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U2_W1_D5_Homework_Backend.Models;

namespace U2_W1_D5_Homework_Backend.Controllers
{
    public class HomeController : Controller
    {
        // Pagina home con lista trasgressori (Index), pagina aggiungi trasgressore (Create), pagina dettagli trasgressore, pagina aggiungi verbale, pagina modifica trasgressore
        public ActionResult Index()
        {
            return View(Trasgressore.GetTrasgressori());
        }

        public ActionResult Create()
        {          
            return View();
        }

        [HttpPost]
        public ActionResult Create(Trasgressore trasg)
        {
            Trasgressore.CreateTrasgressore(trasg);
            return RedirectToAction("Index");
        }

        public ActionResult PagModificaTrasg(int id)
        {
            return View(Trasgressore.GetTrasgressore(id));
        }

        [HttpPost]
        public ActionResult PagModificaTrasg(Trasgressore trasg, int id)
        {
            Trasgressore.EditTrasgressore(trasg, id);
            return RedirectToAction("Index");
        }

        public ActionResult PagCancellaTrasg(int id)
        {
            Trasgressore.DeleteTrasgressore(id);
            return View("Index", Trasgressore.GetTrasgressori());
        }

        public ActionResult PagDettagliTrasg(int id)
        {
            return View(Trasgressore.GetTrasgressore(id));
        }

        public ActionResult PagAggiungiVerbale(int id)
        {
            TempData["IdTrasgressore"] = id;
            ViewBag.ListaDropDownViolazioni = Violazione.GetListaDropdownViolazione();
            return View();
        }

        [HttpPost]
        public ActionResult PagAggiungiVerbale(Verbale verb) 
        {
            int id = Convert.ToInt32(TempData["IdTrasgressore"]);
            Verbale.CreateVerbale(verb, id);
            return RedirectToAction("Index");
        }

        public ActionResult PartialViewPagListaVerbaliTrasg(int id)
        {
            return PartialView("_PartialViewPagListaVerbaliTrasg", Verbale.GetVerbaliPerTrasg(id));
        }

        public ActionResult PagModificaVerb(int id)
        {
            //int IDViol = Convert.ToInt32(IDViolazione);
            ViewBag.DropdownViolazione = Violazione.GetListaDropdownViolazione();
            return View(Verbale.GetVerbale(id));
        }
        [HttpPost]
        public ActionResult PagModificaVerb(Verbale verb, int id)
        {
            Verbale.EditVerbale(verb, id);
            return RedirectToAction("Index");
        }

        // Pagina lista violazioni, pagina aggiungi violazioni
        public ActionResult Violazioni()
        {
            return View(Violazione.GetViolazioni());
        }

        public ActionResult CreateActionViolazione()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateActionViolazione(Violazione viol)
        {
            Violazione.CreateViolazione(viol);
            return RedirectToAction("Violazioni");
        }

        public ActionResult PagModificaViolazione(int id)
        {
            return View(Violazione.GetViolazione(id));
        }
        [HttpPost]
        public ActionResult PagModificaViolazione(Violazione viol, int id)
        {
            Violazione.EditViolazione(viol, id);
            return RedirectToAction("Violazioni");
        }

        public ActionResult PagCancellaViolazione(int id)
        {
            Violazione.DeleteViolazione(id);
            return RedirectToAction("Violazioni", Violazione.GetViolazioni());
        }

        //Pagina dedicata ai verbali con numero totale verbali, lista per numero verbali, lista per punti decurtati, lista filtrata per punti decurtati, lista filtrata per importi
        public ActionResult PagRegistroVerbali()
        {
            ViewBag.NumeroVerbali = Verbale.GetNumeroVerbali();
            return View();
        }

        public ActionResult PartialViewClassificaVerbali()
        {
            return PartialView("_PartialViewClassificaVerbali", Verbale.GetVerbaliChart());
        }

        public ActionResult PartialViewClassificaDecurtamenti()
        {
            return PartialView("_PartialViewClassificaDecurtamenti", Verbale.GetDecurtamentiChart());
        }

        public ActionResult PartialViewClassificaDecurtamentiMaggioriDi()
        {
            return PartialView("_PartialViewClassificaDecurtamentiMaggioriDi", Verbale.GetDecurtamentiMaggioriDi(10));
        }

        [HttpPost]
        public ActionResult PartialViewClassificaDecurtamentiMaggioriDi(string punti)
        {
            return PartialView("_PartialViewClassificaDecurtamentiMaggioriDi", Verbale.GetDecurtamentiMaggioriDi(Convert.ToInt32(punti)));
        }

        public ActionResult PartialViewClassificaImportiMaggioriDi()
        {
            return PartialView("_PartialViewClassificaImportiMaggioriDi", Verbale.GetImportiMaggioriDi(400));
        }

        [HttpPost]
        public ActionResult PartialViewClassificaImportiMaggioriDi(string importo)
        {
            return PartialView("_PartialViewClassificaImportiMaggioriDi", Verbale.GetImportiMaggioriDi(Convert.ToInt32(importo)));
        }
    }
}