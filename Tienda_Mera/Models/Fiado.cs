namespace Tienda_Mera.Models
{
    public class Fiado
    {
        public int IdFiado { get; set; }
        public int IdVenta { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Estado { get; set; }

        // Relaciones
        public Venta Venta { get; set; }
        public ICollection<PagoFiado> PagosFiados { get; set; }
    }
}
