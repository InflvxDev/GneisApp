using System;
using System.Collections.Generic;

namespace Domain.Entities.Gneis;

/// <summary>
/// Guarda los usuarios del sistema
/// </summary>
public partial class Usuario
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public long? IdRol { get; set; }

    public string? Clave { get; set; }

    public bool? EsActivo { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
