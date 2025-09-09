namespace Tienda_Mera.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }

        // Relaciones
        public ICollection<Producto> Productos { get; set; }
    }
}
