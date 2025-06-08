using Microsoft.EntityFrameworkCore;
using RedSignal.Models;

namespace RedSignal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tabelas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<LocalMonitorado> LocaisMonitorados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeia o nome da tabela USUARIOS (em maiúsculo, conforme Oracle)
            modelBuilder.Entity<Usuario>().ToTable("USUARIOS");

            // Mapeia o nome da tabela LOCAIS_MONITORADOS
            modelBuilder.Entity<LocalMonitorado>().ToTable("LOCAIS_MONITORADOS");

            // Relacionamento 1:N entre Usuario e LocaisMonitorados
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.LocaisMonitorados)
                .WithOne(l => l.Usuario!)
                .HasForeignKey(l => l.UserId);
        }
    }
}
