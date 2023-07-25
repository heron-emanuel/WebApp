using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using System.Data.Entity;
using Servico.Cadastros;
using Servico.Tabelas;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        // GET: Home
        public ActionResult Index()
        {
            var categorias = categoriaServico.ObterCategoriasClassificadasPorNome();
            var fabricantes = fabricanteServico.ObterFabricantesClassificadosPorNome();
            var home = new Home() { Fabricantes = fabricantes, Categorias = categorias};
            return View(home);
        }

        public ActionResult Categoria(int id)
        {
            var categorias = categoriaServico.ObterCategoriasComProdutos();
            var fabricantes = fabricanteServico.ObterFabricantesComProdutos();
            var home = new Home() { Fabricantes = fabricantes, Categorias = categorias };
            ViewData["item"] = categorias.Where(c => c.CategoriaId == id).First().Produtos;
            ViewData["categoria"] = true;
            return View("Index", home);
        }

        public ActionResult Fabricante(int id)
        {
            var categorias = categoriaServico.ObterCategoriasComProdutos();
            var fabricantes = fabricanteServico.ObterFabricantesComProdutos();
            var home = new Home() { Fabricantes = fabricantes, Categorias = categorias };
            ViewData["item"] = fabricantes.Where(f => f.FabricanteId == id).First().Produtos;
            ViewData["categoria"] = false;
            return View("Index", home);
        }
    }
}