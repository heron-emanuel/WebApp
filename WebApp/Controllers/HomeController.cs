using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Context;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly EFContext context = new EFContext();

        // GET: Home
        public ActionResult Index()
        {
            var categorias = context.Categorias;
            var fabricantes = context.Fabricantes;
            var home = new Home() { Fabricantes = fabricantes, Categorias = categorias};
            return View(home);
        }
    }
}