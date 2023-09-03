using System;
using System.Collections.Generic;

namespace Domain.Entities.Gneis;

/// <summary>
/// Define que usuario tiene permisos a los menus
/// </summary>
public partial class MenuRol
{
    public long Id { get; set; }

    public long? IdMenu { get; set; }

    public long? IdRol { get; set; }

    public virtual Menu? IdMenuNavigation { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
