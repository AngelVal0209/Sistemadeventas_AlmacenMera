namespace Tienda_Mera.Models
{
    public class PagoFiado
    {
        public int IdPagoFiado { get; set; }
        public int IdFiado { get; set; }
        public decimal MontoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; }

        // Relaciones
        public Fiado Fiado { get; set; }
    }
}
