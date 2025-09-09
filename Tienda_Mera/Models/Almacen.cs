namespace Tienda_Mera.Models
{
    public class Almacen
    {
        public int IdAlmacen { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaEntrada { get; set; }

        // Relaciones
        public Producto Producto { get; set; }
    }
}
