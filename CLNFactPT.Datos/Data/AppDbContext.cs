using CLNFactPT.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace CLNFactPT.Datos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<TasaCambio> TasaCambios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<TasaCambio>()
           .Property(f => f.TasaDeCambio)
           .HasColumnType("decimal(18, 2)");

            builder.Entity<Producto>()
           .Property(f => f.PrecioDolar)
           .HasColumnType("decimal(18, 2)");

            builder.Entity<Factura>()
            .Property(f => f.Subtotal)
            .HasColumnType("decimal(18, 2)");

            builder.Entity<Factura>()
           .Property(f => f.IVA)
           .HasColumnType("decimal(18, 2)");

            builder.Entity<Factura>()
           .Property(f => f.Total)
           .HasColumnType("decimal(18, 2)");

            builder.Entity<FacturaDetalle>()
           .Property(f => f.PrecioUnitario)
           .HasColumnType("decimal(18, 2)");
        }
    }
}
