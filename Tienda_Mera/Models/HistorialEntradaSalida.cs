namespace Tienda_Mera.Models
{
    public class HistorialEntradaSalida
    {
        public int IdHistorial { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string TipoMovimiento { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string Observaciones { get; set; }

        // Relaciones
        public Producto Producto { get; set; }
    }
}
