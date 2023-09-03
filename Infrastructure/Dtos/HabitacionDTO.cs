namespace Infrastructure.Dtos
{
    public class HabitacionDTO
    {
        public long Id { get; set; }

        public string? Descripcion { get; set; }

        public long? IdTipo { get; set; }

        public string? DescripcionTipo { get; set; }

        public string? Precio { get; set; }

        public bool? Disponibilidad { get; set; }

        public long? Usos { get; set; }
    }
}
