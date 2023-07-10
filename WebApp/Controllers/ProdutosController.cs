using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelo.Cadastros;
using System.Data.Entity;
using System.Net;
using System.IO;
using Servico.Cadastros;
using Servico.Tabelas;

namespace WebApp.Controllers
{
    public class ProdutosController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        // GET: Produtos
        public ActionResult Index()
        {
            return View(produtoServico.ObterProdutosClassificadosPorNome());
        }

        private ActionResult ObterVisaoProdutoPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = produtoServico.ObterProdutoPorId((long)id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        private void PopularViewBag(Produto produto = null)
        {
            if (produto == null)
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(),
                "CategoriaId", "Nome");
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(),
                "FabricanteId", "Nome");
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(),
                "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(),
                "FabricanteId", "Nome", produto.FabricanteId);
            }
        }

        private ActionResult GravarProduto(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produtoServico.GravarProduto(produto);
                    return RedirectToAction("Index");
                }
                return View(produto);
            }
            catch
            {
                return View(produto);
            }
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            return GravarProduto(produto);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(long? id)
        {
            PopularViewBag(produtoServico.ObterProdutoPorId((long)id));
            return ObterVisaoProdutoPorId(id);
        }

        [HttpPost]
        public ActionResult Edit(Produto produto, HttpPostedFileBase logotipo = null, string chkRemoverImagem = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (chkRemoverImagem != null)
                    {
                        produto = null;
                    }

                    if (logotipo != null)
                    {
                        var buffer = new byte[logotipo.ContentLength];
                        logotipo.InputStream.Read(buffer, 0, logotipo.ContentLength);
                        produto.Logotipo = buffer;
                        produto.LogotipoMimeType = logotipo.ContentType;
                        produto.NomeArquivo = logotipo.FileName;
                        produto.TamanhoArquivo = logotipo.ContentLength;
                    }

                    return GravarProduto(produto);
                }
                return View(produto);
            }
            catch
            {
                return View(produto);
            }
        }

        public ActionResult  DownloadArquivo(long id)
        {
            Produto produto = produtoServico.ObterProdutoPorId(id);

            //FileStream fileStream = new FileStream(Server.MapPath(
            //"~/App_Data/" + produto.NomeArquivo), FileMode.Create,
            //FileAccess.Write);
            //fileStream.Write(produto.Logotipo, 0,
            //Convert.ToInt32(produto.TamanhoArquivo));
            //fileStream.Close();
            return File(produto.Logotipo, produto.LogotipoMimeType, produto.NomeArquivo);
        }
            
        public FileContentResult GetLogotipo(long id)
        {
            Produto produto = produtoServico.ObterProdutoPorId(id);
            if (produto != null)
            {
                return File(produto.Logotipo, produto.LogotipoMimeType);
            }
            return null;
        }

        // GET: Produtos/Details/5
        public ActionResult Details(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        public ActionResult Delete(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Produto produto = produtoServico.EliminarProdutoPorId(id);
                TempData["Message"] = "Produto " + produto.Nome.ToUpper() + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
