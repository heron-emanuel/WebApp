using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Modelo.Tabelas;
using Modelo.Cadastros;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Persistencia.Contexts
{
    public class EFContext : DbContext
    {
        public EFContext() : base("WebAppDB")
        {
            Database.SetInitializer<EFContext>(
            new DropCreateDatabaseIfModelChanges<EFContext>());
        }

        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}