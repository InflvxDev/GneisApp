using System;
using System.Collections.Generic;

namespace Domain.Entities.Gneis;

/// <summary>
/// Guarda los Menus del sistema
/// </summary>
public partial class Menu
{
    public long Id { get; set; }

    public string? Nombre { get; set; }

    public string? Icono { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<MenuRol> MenuRols { get; set; } = new List<MenuRol>();
}
