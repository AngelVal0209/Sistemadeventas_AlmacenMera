namespace Tienda_Mera.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdCategoria { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }

        // Relaciones
        public Proveedor Proveedor { get; set; }
        public Categoria Categoria { get; set; }
        public ICollection<Almacen> Almacenes { get; set; }
        public ICollection<DetalleVenta> DetallesVenta { get; set; }
        public ICollection<HistorialPrecio> HistorialPrecios { get; set; }
        public ICollection<HistorialEntradaSalida> HistorialEntradasSalidas { get; set; }
    }
}
