namespace Infrastructure.Dtos
{
    public class ReporteDTO
    {
        public long Id { get; set; }
        
        public string? Cedula { get; set; }

        public string? FechaRegistro { get; set;}

        public string? TotalReserva { get; set; }

        public string? Habitacion { get; set; }

        public string? DiasHospedaje { get; set; }

        public string? CostoDia { get; set; }
    }
}
