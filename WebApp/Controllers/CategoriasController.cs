using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using Modelo.Tabelas;
using System.Data.Entity;
using Servico.Tabelas;

namespace WebApp.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CategoriaServico categoriaServico = new CategoriaServico();

        // GET: Categorias
        public ActionResult Index()
        {
            return View(categoriaServico.ObterCategoriasClassificadasPorNome());
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            categoriaServico.GravarCategoria(categoria);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria c = categoriaServico.ObterCategoriaPorId(id.Value);
            if (c == null)
            {
                return HttpNotFound();
            }

            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }

            categoriaServico.GravarCategoria(categoria);
            return RedirectToAction("Index");
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria c = context.Categorias.Where(ca => ca.CategoriaId == id)
                            .Include("Produtos.Categoria").First();
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria c = context.Categorias.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Categoria c= context.Categorias.Find(id);
            context.Categorias.Remove(c);
            context.SaveChanges();
            TempData["message"] = $"Categoria {c.Nome} foi removida com sucesso";
            return RedirectToAction("Index");
        }
    }
}