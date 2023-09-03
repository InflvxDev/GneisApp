namespace Web.Dtos
{
    public class ReservaDTO
    {
        public long Id { get; set; }

        public string? FechaReserva { get; set; }

        public string? Cedula { get; set; }

        public string? NombreCliente { get; set; }

        public long? IdHabitacion { get; set; }

        public string? DescripcionHabitacion { get; set; }

        public string? CostoDia { get; set; }

        public long? Acompañantes { get; set; }

        public string? FechaEntrada { get; set; }

        public string? FechaSalida { get; set; }

        public long? DiaHospedaje { get; set; }

        public string? Total { get; set; }
    }
}
