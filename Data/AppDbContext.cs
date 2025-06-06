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

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<LocalMonitorado> LocaisMonitorados { get; set; }
        public DbSet<HotspotEvento> HotspotsEventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HotspotEvento>().ToTable("HOTSPOTS_EVENTOS");
            modelBuilder.Entity<Usuario>().ToTable("USUARIOS");

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.LocaisMonitorados)
                .WithOne(l => l.Usuario!)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<LocalMonitorado>()
                .HasIndex(l => l.UserId)
                .HasDatabaseName("IDX_LOCAIS_MONITORADOS_USER_ID");
        }
    }
}
