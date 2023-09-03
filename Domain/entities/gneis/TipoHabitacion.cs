using System;
using System.Collections.Generic;

namespace Domain.Entities.Gneis;

/// <summary>
/// Guarda los tipos de habitaciones del sistema
/// </summary>
public partial class TipoHabitacion
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Nombre { get; set; }

    public bool? EsActivo { get; set; }

    public virtual ICollection<Habitacion> Habitacions { get; set; } = new List<Habitacion>();
}
