using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parcial1DPWA.Models;

namespace Parcial1DPWA.Data
{
    public class AppDbContext : IdentityDbContext<UsuariosApp>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
             : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Destinatario> Destinatarios { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<Paquete> Paquetes { get; set; }
        public DbSet<EstadosEnvio> EstadosEnvios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Paquete)
                .WithMany()
                .HasForeignKey(e => e.PaqueteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}