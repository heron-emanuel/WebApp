using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Context;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class FabricantesController : Controller
    {
        private readonly EFContext context = new EFContext();
        // GET: Fabricantes
        public ActionResult Index()
        {
            return View(context.Fabricantes.OrderBy(f => f.Nome));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricante fabricante)
        {
            if (ModelState.IsValid)
            {
                context.Fabricantes.Add(fabricante);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}