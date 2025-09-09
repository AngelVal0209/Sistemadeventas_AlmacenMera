namespace Tienda_Mera.Models
{
    public class HistorialVentaUsuario
    {
        public int IdHistorialVenta { get; set; }
        public int IdVenta { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal TotalVenta { get; set; }

        // Relaciones
        public Venta Venta { get; set; }
    }
}
