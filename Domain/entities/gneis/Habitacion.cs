using System;
using System.Collections.Generic;

namespace Domain.Entities.Gneis;

/// <summary>
/// Guarda las habitaciones del sistema
/// </summary>
public partial class Habitacion
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Descripcion { get; set; }

    public long? IdTipo { get; set; }

    public double? Precio { get; set; }

    public bool? Disponibilidad { get; set; }

    public long? Usos { get; set; }

    public virtual TipoHabitacion? IdTipoNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
