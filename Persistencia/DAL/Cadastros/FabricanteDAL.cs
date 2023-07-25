using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.Contexts;
using Modelo.Cadastros;
using System.Data.Entity;

namespace Persistencia.DAL.Cadastros
{
    public class FabricanteDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Fabricante> ObterFabricantesClassificadosPorNome()
        {
            return context.Fabricantes.OrderBy(b => b.Nome);
        }

        public IQueryable<Fabricante> ObterFabricantesComProdutos()
        {
            return context.Fabricantes.Include("Produtos.Fabricante");
        }

        public Fabricante ObterFabricantePorId(long id)
        {
            return context.Fabricantes.Where(f => f.FabricanteId == id).First();
        }
        public void GravarFabricante(Fabricante fabricante)
        {
            if (fabricante.FabricanteId == 0)
            {
                context.Fabricantes.Add(fabricante);
            }
            else
            {
                context.Entry(fabricante).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public Fabricante EliminarFabricantePorId(long id)
        {
            Fabricante fabricante = ObterFabricantePorId(id);
            context.Fabricantes.Remove(fabricante);
            context.SaveChanges();
            return fabricante;
        }
    }
}
