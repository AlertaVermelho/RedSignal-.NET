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

            // Relacionamento 1:N entre Usuario e LocaisMonitorados
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.LocaisMonitorados)
                .WithOne(l => l.Usuario!)
                .HasForeignKey(l => l.id_usuario_registrou);

            // Adicionando índice para a chave estrangeira em LocaisMonitorados
            modelBuilder.Entity<LocalMonitorado>()
                .HasIndex(l => l.id_usuario_registrou)
                .HasDatabaseName("IDX_LOCAIS_MONITORADOS_USER_ID");
        }
    }
}
