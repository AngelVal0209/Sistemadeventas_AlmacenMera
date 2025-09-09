namespace Tienda_Mera.Models
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

        // Relaciones
        public ICollection<Producto> Productos { get; set; }
    }
}
