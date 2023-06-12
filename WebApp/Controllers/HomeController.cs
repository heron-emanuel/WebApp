using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Context;
using WebApp.Models;
using System.Data.Entity;

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

        public ActionResult Categoria(int id)
        {
            var categorias = context.Categorias.Include("Produtos.Categoria");
            var fabricantes = context.Fabricantes.Include("Produtos.Fabricante");
            var home = new Home() { Fabricantes = fabricantes, Categorias = categorias };
            ViewData["item"] = categorias.Where(c => c.CategoriaId == id).First().Produtos;
            ViewData["categoria"] = true;
            return View("Index", home);
        }

        public ActionResult Fabricante(int id)
        {
            var categorias = context.Categorias.Include("Produtos.Categoria");
            var fabricantes = context.Fabricantes.Include("Produtos.Fabricante");
            var home = new Home() { Fabricantes = fabricantes, Categorias = categorias };
            ViewData["item"] = fabricantes.Where(f => f.FabricanteId == id).First().Produtos;
            ViewData["categoria"] = false;
            return View("Index", home);
        }
    }
}