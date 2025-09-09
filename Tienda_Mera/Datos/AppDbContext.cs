using Microsoft.EntityFrameworkCore;
using Tienda_Mera.Models;

namespace Tienda_Mera.Datos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets (cada tabla de tu BD)
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<HistorialPrecio> HistorialPrecios { get; set; }
        public DbSet<Fiado> Fiados { get; set; }
        public DbSet<PagoFiado> PagosFiados { get; set; }
        public DbSet<HistorialEntradaSalida> HistorialEntradasSalidas { get; set; }
        public DbSet<HistorialVentaUsuario> HistorialVentasUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // =========================
            // Tabla Roles
            // =========================
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("roles");

                entity.HasKey(r => r.IdRol);
                entity.Property(r => r.IdRol).HasColumnName("id_rol");

                entity.Property(r => r.NombreRol)
                      .HasColumnName("nombre_rol")
                      .HasMaxLength(50)
                      .IsRequired();
                entity.HasData(
            new Rol { IdRol = 1, NombreRol = "administrador" },
            new Rol { IdRol = 2, NombreRol = "empleado" }
        );
            });

            // =========================
            // Tabla Usuarios
            // =========================
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasKey(u => u.IdUsuario);
                entity.Property(u => u.IdUsuario).HasColumnName("id_usuario");

                entity.Property(u => u.Nombre)
                      .HasColumnName("nombre")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(u => u.Email)
                      .HasColumnName("email")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.Contraseña)
                      .HasColumnName("contraseña")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(u => u.IdRol).HasColumnName("id_rol");

                entity.Property(u => u.FechaCreacion)
                      .HasColumnName("fecha_creacion")
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(u => u.Estado)
                      .HasColumnName("estado")
                      .HasMaxLength(10)
                      .HasDefaultValue("activo");

                // Relaciones
                entity.HasOne(u => u.Rol)
                      .WithMany(r => r.Usuarios)
                      .HasForeignKey(u => u.IdRol);

                entity.HasData(
            new Usuario
            {
                IdUsuario = 1,
                Nombre = "Administrador",
                Email = "admin@correo.com",
                Contraseña = "admin123",
                IdRol = 1,
                Estado = "activo",
                FechaCreacion = new DateTime(2025, 01, 01),
            },
            new Usuario
            {
                IdUsuario = 2,
                Nombre = "Empleado",
                Email = "empleado@correo.com",
                Contraseña = "empleado123",
                IdRol = 2,
                Estado = "activo",
                FechaCreacion = new DateTime(2025, 01, 01),
            }
        );
            });

            // =========================
            // Tabla Proveedores
            // =========================
            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.ToTable("proveedores");

                entity.HasKey(p => p.IdProveedor);
                entity.Property(p => p.IdProveedor).HasColumnName("id_proveedor");

                entity.Property(p => p.NombreProveedor)
                      .HasColumnName("nombre_proveedor")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(p => p.Telefono).HasColumnName("telefono").HasMaxLength(20);
                entity.Property(p => p.Email).HasColumnName("email").HasMaxLength(100);
                entity.Property(p => p.Direccion).HasColumnName("direccion");
            });

            // =========================
            // Tabla Categorías
            // =========================
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categorias");

                entity.HasKey(c => c.IdCategoria);
                entity.Property(c => c.IdCategoria).HasColumnName("id_categoria");

                entity.Property(c => c.NombreCategoria)
                      .HasColumnName("nombre_categoria")
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // =========================
            // Tabla Productos
            // =========================
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos");

                entity.HasKey(p => p.IdProducto);
                entity.Property(p => p.IdProducto).HasColumnName("id_producto");

                entity.Property(p => p.NombreProducto)
                      .HasColumnName("nombre_producto")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(p => p.Descripcion).HasColumnName("descripcion");
                entity.Property(p => p.Precio).HasColumnName("precio").HasColumnType("decimal(10,2)");
                entity.Property(p => p.Stock).HasColumnName("stock");

                entity.Property(p => p.IdProveedor).HasColumnName("id_proveedor");
                entity.Property(p => p.IdCategoria).HasColumnName("id_categoria");

                entity.Property(p => p.FechaCreacion)
                      .HasColumnName("fecha_creacion")
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(p => p.FechaVencimiento).HasColumnName("fecha_vencimiento");

                entity.HasOne(p => p.Proveedor)
                      .WithMany(pr => pr.Productos)
                      .HasForeignKey(p => p.IdProveedor);

                entity.HasOne(p => p.Categoria)
                      .WithMany(c => c.Productos)
                      .HasForeignKey(p => p.IdCategoria);
            });

            // =========================
            // Tabla Almacén
            // =========================
            modelBuilder.Entity<Almacen>(entity =>
            {
                entity.ToTable("almacen");

                entity.HasKey(a => a.IdAlmacen);
                entity.Property(a => a.IdAlmacen).HasColumnName("id_almacen");

                entity.Property(a => a.IdProducto).HasColumnName("id_producto");
                entity.Property(a => a.Cantidad).HasColumnName("cantidad");

                entity.Property(a => a.FechaEntrada)
                      .HasColumnName("fecha_entrada")
                      .HasDefaultValueSql("GETDATE()");

                entity.HasOne(a => a.Producto)
                      .WithMany(p => p.Almacenes)
                      .HasForeignKey(a => a.IdProducto);
            });

            // =========================
            // Tabla Ventas
            // =========================
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("ventas");

                entity.HasKey(v => v.IdVenta);
                entity.Property(v => v.IdVenta).HasColumnName("id_venta");

                entity.Property(v => v.IdUsuario).HasColumnName("id_usuario");
                entity.Property(v => v.FechaVenta).HasColumnName("fecha_venta").HasDefaultValueSql("GETDATE()");
                entity.Property(v => v.Total).HasColumnName("total").HasColumnType("decimal(10,2)");
                entity.Property(v => v.Estado).HasColumnName("estado").HasMaxLength(10).HasDefaultValue("pendiente");
                entity.Property(v => v.TipoVenta).HasColumnName("tipo_venta").HasMaxLength(20).HasDefaultValue("contado");

                entity.HasOne(v => v.Usuario)
                      .WithMany(u => u.Ventas)
                      .HasForeignKey(v => v.IdUsuario);
            });

            // =========================
            // Tabla Detalle Ventas
            // =========================
            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.ToTable("detalle_ventas");

                entity.HasKey(d => d.IdDetalle);
                entity.Property(d => d.IdDetalle).HasColumnName("id_detalle");

                entity.Property(d => d.IdVenta).HasColumnName("id_venta");
                entity.Property(d => d.IdProducto).HasColumnName("id_producto");
                entity.Property(d => d.Cantidad).HasColumnName("cantidad");
                entity.Property(d => d.PrecioUnitario).HasColumnName("precio_unitario").HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.Venta)
                      .WithMany(v => v.DetallesVenta)
                      .HasForeignKey(d => d.IdVenta);

                entity.HasOne(d => d.Producto)
                      .WithMany(p => p.DetallesVenta)
                      .HasForeignKey(d => d.IdProducto);
            });

            // =========================
            // Tabla Historial Precios
            // =========================
            modelBuilder.Entity<HistorialPrecio>(entity =>
            {
                entity.ToTable("historial_precios");

                entity.HasKey(h => h.IdPrecio);
                entity.Property(h => h.IdPrecio).HasColumnName("id_precio");

                entity.Property(h => h.IdProducto).HasColumnName("id_producto");
                entity.Property(h => h.Precio).HasColumnName("precio").HasColumnType("decimal(10,2)");
                entity.Property(h => h.FechaCambio).HasColumnName("fecha_cambio").HasDefaultValueSql("GETDATE()");

                entity.HasOne(h => h.Producto)
                      .WithMany(p => p.HistorialPrecios)
                      .HasForeignKey(h => h.IdProducto);
            });

            // =========================
            // Tabla Fiados
            // =========================
            modelBuilder.Entity<Fiado>(entity =>
            {
                entity.ToTable("fiados");

                entity.HasKey(f => f.IdFiado);
                entity.Property(f => f.IdFiado).HasColumnName("id_fiado");

                entity.Property(f => f.IdVenta).HasColumnName("id_venta");
                entity.Property(f => f.MontoTotal).HasColumnName("monto_total").HasColumnType("decimal(10,2)");
                entity.Property(f => f.MontoPagado).HasColumnName("monto_pagado").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
                entity.Property(f => f.SaldoPendiente).HasColumnName("saldo_pendiente").HasColumnType("decimal(10,2)");
                entity.Property(f => f.FechaInicio).HasColumnName("fecha_inicio").HasDefaultValueSql("GETDATE()");
                entity.Property(f => f.FechaVencimiento).HasColumnName("fecha_vencimiento");
                entity.Property(f => f.Estado).HasColumnName("estado").HasMaxLength(10).HasDefaultValue("activo");

                entity.HasOne(f => f.Venta)
                      .WithMany(v => v.Fiados)
                      .HasForeignKey(f => f.IdVenta);
            });

            // =========================
            // Tabla Pagos Fiados
            // =========================
            modelBuilder.Entity<PagoFiado>(entity =>
            {
                entity.ToTable("pagos_fiados");

                entity.HasKey(p => p.IdPagoFiado);
                entity.Property(p => p.IdPagoFiado).HasColumnName("id_pago_fiado");

                entity.Property(p => p.IdFiado).HasColumnName("id_fiado");
                entity.Property(p => p.MontoPago).HasColumnName("monto_pago").HasColumnType("decimal(10,2)");
                entity.Property(p => p.FechaPago).HasColumnName("fecha_pago").HasDefaultValueSql("GETDATE()");
                entity.Property(p => p.MetodoPago).HasColumnName("metodo_pago").HasMaxLength(50);

                entity.HasOne(p => p.Fiado)
                      .WithMany(f => f.PagosFiados)
                      .HasForeignKey(p => p.IdFiado);
            });

            // =========================
            // Tabla Historial Entradas y Salidas
            // =========================
            modelBuilder.Entity<HistorialEntradaSalida>(entity =>
            {
                entity.ToTable("historial_entradas_salidas");

                entity.HasKey(h => h.IdHistorial);
                entity.Property(h => h.IdHistorial).HasColumnName("id_historial");

                entity.Property(h => h.IdProducto).HasColumnName("id_producto");
                entity.Property(h => h.Cantidad).HasColumnName("cantidad");
                entity.Property(h => h.TipoMovimiento).HasColumnName("tipo_movimiento").HasMaxLength(10);
                entity.Property(h => h.FechaMovimiento).HasColumnName("fecha_movimiento").HasDefaultValueSql("GETDATE()");
                entity.Property(h => h.Observaciones).HasColumnName("observaciones");

                entity.HasOne(h => h.Producto)
                      .WithMany(p => p.HistorialEntradasSalidas)
                      .HasForeignKey(h => h.IdProducto);
            });

            // =========================
            // Tabla Historial Ventas Usuario
            // =========================
            modelBuilder.Entity<HistorialVentaUsuario>(entity =>
            {
                entity.ToTable("historial_ventas_usuario");

                entity.HasKey(h => h.IdHistorialVenta);
                entity.Property(h => h.IdHistorialVenta).HasColumnName("id_historial_venta");

                entity.Property(h => h.IdVenta).HasColumnName("id_venta");
                entity.Property(h => h.NombreUsuario).HasColumnName("nombre_usuario").HasMaxLength(100).IsRequired();
                entity.Property(h => h.FechaVenta).HasColumnName("fecha_venta").HasDefaultValueSql("GETDATE()");
                entity.Property(h => h.TotalVenta).HasColumnName("total_venta").HasColumnType("decimal(10,2)");

                entity.HasOne(h => h.Venta)
                      .WithMany(v => v.HistorialVentasUsuario)
                      .HasForeignKey(h => h.IdVenta);
            });
        }
    }
}
