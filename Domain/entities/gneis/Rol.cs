using System;
using System.Collections.Generic;

namespace Domain.Entities.Gneis;

/// <summary>
/// Guarda los roles del sistema
/// </summary>
public partial class Rol
{
    public long Id { get; set; }

    public string? Nombre { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<MenuRol> MenuRols { get; set; } = new List<MenuRol>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
