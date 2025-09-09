namespace Tienda_Mera.Models
{
    public class HistorialPrecio
    {
        public int IdPrecio { get; set; }
        public int IdProducto { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCambio { get; set; }

        // Relaciones
        public Producto Producto { get; set; }
    }
}
