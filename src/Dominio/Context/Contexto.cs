using Dominio.Context.Mapping;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dominio.Context
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Colaborador> Colaborador { get; set; }
        public DbSet<Ferias> Ferias { get; set; }
        public DbSet<PeriodoDeFerias> PeriodosDeFerias { get; set; }
        public DbSet<HomologacaoDeFerias> HomologacoesDeFerias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.ApplyConfiguration(new ColaboradorMap());
            modelBuilder.ApplyConfiguration(new FeriasMap());
            modelBuilder.ApplyConfiguration(new PeriodoDeFeriasMap());
            modelBuilder.ApplyConfiguration(new HomologacaoDeFeriasMap());
        }
    }
}