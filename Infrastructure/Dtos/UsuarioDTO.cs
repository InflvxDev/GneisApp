namespace Infrastructure.Dtos
{
    public class UsuarioDTO
    {
        public long Id { get; set; }

        public string? Nombre { get; set; }

        public string? Correo { get; set; }

        public long? IdRol { get; set; }

        public string? DescripcionRol { get; set; }

        public string? Clave { get; set; }

        public int? EsActivo { get; set; }

    }
}
