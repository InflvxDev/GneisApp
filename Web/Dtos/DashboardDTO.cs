namespace Web.Dtos
{
    public class DashboardDTO
    {
        public int TotalReservas { get; set; }

        public string? TotalIngresos { get; set; }

        public List<ReservaSemanaDTO> ReservaUltimaSemana { get; set; }
    }
}
