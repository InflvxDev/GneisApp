namespace Infrastructure.Dtos
{
    public class DashboardDTO
    {
        public int TotalReservas { get; set; }

        public int TotalReservasUltimaSemana { get; set; }

        public string? TotalIngresos { get; set; }

        public List<ReservaSemanaDTO>? ReservasUltimaSemana { get; set; }
    }
}
