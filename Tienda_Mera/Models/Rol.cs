namespace Tienda_Mera.Models
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; }

        // Relaciones
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
