namespace Tienda_Mera.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int? IdUsuario { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public string TipoVenta { get; set; }

        // Relaciones
        public Usuario Usuario { get; set; }
        public ICollection<DetalleVenta> DetallesVenta { get; set; }
        public ICollection<Fiado> Fiados { get; set; }
        public ICollection<HistorialVentaUsuario> HistorialVentasUsuario { get; set; }
    }
}
