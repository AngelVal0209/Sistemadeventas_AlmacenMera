using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sistemadeventas_AlmacenMera.Models;

namespace Sistemadeventas_AlmacenMera.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>(tb =>
            {
                tb.HasKey(col => col.IdProducto);

                tb.Property(col => col.IdProducto)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                tb.Property(col => col.NombreProducto)
                    .IsRequired()
                    .HasMaxLength(150);

                tb.Property(col => col.Descripcion)
                    .HasMaxLength(500);

                tb.Property(col => col.Precio)
                    .HasColumnType("decimal(18,2)");

                tb.Property(col => col.Stock)
                    .IsRequired();

                tb.Property(col => col.FechaCreacion)
                    .HasDefaultValueSql("GETDATE()");

                tb.Property(col => col.FechaVencimiento)
                    .IsRequired(false);

                // 🔗 Relaciones
                tb.HasOne(col => col.Proveedor)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(col => col.IdProveedor)
                    .OnDelete(DeleteBehavior.SetNull);

                tb.HasOne(col => col.Categoria)
                    .WithMany(c => c.Productos)
                    .HasForeignKey(col => col.IdCategoria)
                    .OnDelete(DeleteBehavior.SetNull);

                tb.HasMany(col => col.DetallesVentas)
                    .WithOne(dv => dv.Producto)
                    .HasForeignKey(dv => dv.IdProducto);

                tb.HasMany(col => col.HistorialPrecios)
                    .WithOne(hp => hp.Producto)
                    .HasForeignKey(hp => hp.IdProducto);

                tb.HasMany(col => col.HistorialEntradasSalidas)
                    .WithOne(hes => hes.Producto)
                    .HasForeignKey(hes => hes.IdProducto);
            });

            modelBuilder.Entity<Venta>(tb =>
            {
                tb.HasKey(v => v.IdVenta);

                tb.Property(v => v.IdVenta)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                tb.Property(v => v.FechaVenta)
                    .HasDefaultValueSql("GETDATE()");

                tb.Property(v => v.Total)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                tb.Property(v => v.Estado)
                    .HasMaxLength(50)
                    .HasDefaultValue("pendiente");

                tb.Property(v => v.TipoVenta)
                    .HasMaxLength(50)
                    .HasDefaultValue("contado");

                // 🔗 Relación con Usuario (1:N)
                tb.HasOne(v => v.Usuario)
                    .WithMany(u => u.Ventas)
                    .HasForeignKey(v => v.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);

                // 🔗 Relación con DetallesVentas (1:N)
                tb.HasMany(v => v.DetallesVentas)
                    .WithOne(dv => dv.Venta)
                    .HasForeignKey(dv => dv.IdVenta);

                // 🔗 Relación con Fiado (1:1 opcional)
                tb.HasOne(v => v.Fiado)
                    .WithOne(f => f.Venta)
                    .HasForeignKey<Fiado>(f => f.IdVenta);
            });

            modelBuilder.Entity<Usuario>(tb =>
            {
                tb.HasKey(u => u.IdUsuario);

                tb.Property(u => u.IdUsuario)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                tb.Property(u => u.Nombre)
                    .IsRequired()
                    .HasMaxLength(150);

                tb.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                tb.Property(u => u.Contraseña)
                    .IsRequired()
                    .HasMaxLength(150);

                tb.Property(u => u.Estado)
                    .HasMaxLength(50)
                    .HasDefaultValue("activo");

                tb.Property(u => u.FechaCreacion)
                    .HasDefaultValueSql("GETDATE()");

                // 🔗 Relación con Rol (1:N)
                tb.HasOne(u => u.Rol)
                    .WithMany(r => r.Usuarios)
                    .HasForeignKey(u => u.IdRol)
                    .OnDelete(DeleteBehavior.SetNull);

                // 🔗 Relación con Ventas (1:N)
                tb.HasMany(u => u.Ventas)
                    .WithOne(v => v.Usuario)
                    .HasForeignKey(v => v.IdUsuario);
            });

            modelBuilder.Entity<Rol>(tb =>
            {
                tb.HasKey(r => r.IdRol);

                tb.Property(r => r.IdRol)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                tb.Property(r => r.NombreRol)
                    .IsRequired()
                    .HasMaxLength(100);

                // 🔗 Relación con Usuario (1:N)
                tb.HasMany(r => r.Usuarios)
                    .WithOne(u => u.Rol)
                    .HasForeignKey(u => u.IdRol)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Proveedor>(tb =>
            {
                tb.HasKey(p => p.IdProveedor);

                tb.Property(p => p.IdProveedor)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                tb.Property(p => p.NombreProveedor)
                    .IsRequired()
                    .HasMaxLength(150);

                tb.Property(p => p.Telefono)
                    .HasMaxLength(20);

                tb.Property(p => p.Email)
                    .HasMaxLength(150);

                tb.Property(p => p.Direccion)
                    .HasMaxLength(250);

                // 🔗 Relación con Productos (1:N)
                tb.HasMany(p => p.Productos)
                    .WithOne(prod => prod.Proveedor)
                    .HasForeignKey(prod => prod.IdProveedor)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<PagoFiado>(tb =>
            {
                tb.HasKey(pf => pf.IdPagoFiado);

                tb.Property(pf => pf.IdPagoFiado)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                tb.Property(pf => pf.MontoPago)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                tb.Property(pf => pf.FechaPago)
                    .HasDefaultValueSql("GETDATE()");

                tb.Property(pf => pf.MetodoPago)
                    .IsRequired()
                    .HasMaxLength(50);

                // 🔗 Relación con Fiado (N:1)
                tb.HasOne(pf => pf.Fiado)
                    .WithMany(f => f.PagosFiados)
                    .HasForeignKey(pf => pf.IdFiado)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HistorialVentasUsuario>(tb =>
            {
                tb.HasKey(h => h.IdHistorialVenta);

                tb.Property(h => h.IdHistorialVenta)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                tb.Property(h => h.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(150);

                tb.Property(h => h.FechaVenta)
                    .HasDefaultValueSql("GETDATE()");

                tb.Property(h => h.TotalVenta)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                // 🔗 Relación con Venta (N:1)
                tb.HasOne(h => h.Venta)
                    .WithMany(v => v.HistorialVentasUsuarios)
                    .HasForeignKey(h => h.IdVenta)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HistorialPrecio>(tb =>
            {
                tb.HasKey(hp => hp.IdPrecio);

                tb.Property(hp => hp.IdPrecio)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                tb.Property(hp => hp.Precio)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                tb.Property(hp => hp.FechaCambio)
                    .HasDefaultValueSql("GETDATE()");

                // 🔗 Relación con Producto (N:1)
                tb.HasOne(hp => hp.Producto)
                    .WithMany(p => p.HistorialPrecios)
                    .HasForeignKey(hp => hp.IdProducto)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HistorialEntradaSalida>(tb =>
            {
                tb.HasKey(h => h.IdHistorial);

                tb.Property(h => h.IdHistorial)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                tb.Property(h => h.Cantidad)
                    .IsRequired();

                tb.Property(h => h.TipoMovimiento)
                    .IsRequired()
                    .HasMaxLength(50); // Ej: "Entrada", "Salida"

                tb.Property(h => h.FechaMovimiento)
                    .HasDefaultValueSql("GETDATE()");

                tb.Property(h => h.Observaciones)
                    .HasMaxLength(500);

                // 🔗 Relación con Producto (N:1)
                tb.HasOne(h => h.Producto)
                    .WithMany(p => p.HistorialEntradasSalidas)
                    .HasForeignKey(h => h.IdProducto)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Fiado>(tb =>
            {
                tb.HasKey(f => f.IdFiado);

                tb.Property(f => f.IdFiado)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                tb.Property(f => f.MontoTotal)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                tb.Property(f => f.MontoPagado)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0.00m);

                tb.Property(f => f.SaldoPendiente)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                tb.Property(f => f.FechaInicio)
                    .HasDefaultValueSql("GETDATE()");

                tb.Property(f => f.FechaVencimiento)
                    .IsRequired(false);

                tb.Property(f => f.Estado)
                    .HasMaxLength(50)
                    .HasDefaultValue("activo");

                // 🔗 Relación con Venta (1:1)
                tb.HasOne(f => f.Venta)
                    .WithOne(v => v.Fiado)
                    .HasForeignKey<Fiado>(f => f.IdVenta)
                    .OnDelete(DeleteBehavior.Cascade);

                // 🔗 Relación con PagosFiado (1:N)
                tb.HasMany(f => f.PagosFiado)
                    .WithOne(p => p.Fiado)
                    .HasForeignKey(p => p.IdFiado)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DetalleVenta>(tb =>
            {
                tb.HasKey(dv => dv.IdDetalle);

                tb.Property(dv => dv.IdDetalle)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                tb.Property(dv => dv.Cantidad)
                    .IsRequired();

                tb.Property(dv => dv.PrecioUnitario)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                // 🔗 Relación con Venta (N:1)
                tb.HasOne(dv => dv.Venta)
                    .WithMany(v => v.DetallesVentas)
                    .HasForeignKey(dv => dv.IdVenta)
                    .OnDelete(DeleteBehavior.Cascade);

                // 🔗 Relación con Producto (N:1)
                tb.HasOne(dv => dv.Producto)
                    .WithMany(p => p.DetallesVentas)
                    .HasForeignKey(dv => dv.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Categoria>(tb =>
            {
                tb.HasKey(c => c.IdCategoria);

                tb.Property(c => c.IdCategoria)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                tb.Property(c => c.NombreCategoria)
                    .IsRequired()
                    .HasMaxLength(100);

                // 🔗 Relación con Productos (1:N)
                tb.HasMany(c => c.Productos)
                    .WithOne(p => p.Categoria)
                    .HasForeignKey(p => p.IdCategoria)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Almacen>(tb =>
            {
                tb.HasKey(a => a.IdAlmacen);

                tb.Property(a => a.IdAlmacen)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd();

                tb.Property(a => a.Cantidad)
                    .IsRequired();

                tb.Property(a => a.FechaEntrada)
                    .HasDefaultValueSql("GETDATE()");

                // 🔗 Relación con Producto (N:1)
                tb.HasOne(a => a.Producto)
                    .WithMany()
                    .HasForeignKey(a => a.IdProducto)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        }

    }
}
