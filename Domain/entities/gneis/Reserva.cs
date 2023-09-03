using System;
using System.Collections.Generic;

namespace Domain.Entities.Gneis;

/// <summary>
/// Guarda las reservas del sistema
/// </summary>
public partial class Reserva
{
    public long Id { get; set; }

    public DateTime FechaReserva { get; set; }

    public string? Cedula { get; set; }

    public long? IdHabitacion { get; set; }

    public long? Acompañantes { get; set; }

    public DateTime? FechaEntrada { get; set; }

    public DateTime? FechaSalida { get; set; }

    public long? DiaHospedaje { get; set; }

    public double? Total { get; set; }

    public virtual Cliente? CedulaNavigation { get; set; }

    public virtual Habitacion? IdHabitacionNavigation { get; set; }
}
