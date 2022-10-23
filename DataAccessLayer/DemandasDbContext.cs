using Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccessLayer
{

    public class DemandasDbContext : DbContext
    {
      

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Demanda> Demandas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DemandasDbContext(DbContextOptions<DemandasDbContext> ctx) : base(ctx)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}