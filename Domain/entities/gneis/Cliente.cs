using System;
using System.Collections.Generic;

namespace Domain.Entities.Gneis;

/// <summary>
/// Guarda los clientes en el sistema
/// </summary>
public partial class Cliente
{
    public string Cedula { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? Nombre { get; set; }

    public long? Edad { get; set; }

    public string? Sexo { get; set; }

    public long? Telefono { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
