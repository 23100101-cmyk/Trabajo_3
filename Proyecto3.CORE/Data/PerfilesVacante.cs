using System;
using System.Collections.Generic;

namespace Proyecto3.CORE.Data;

public partial class PerfilesVacante
{
    public int VacanteId { get; set; }

    public string NombrePerfil { get; set; } = null!;

    public string? NivelDeseado { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaUrgencia { get; set; }

    public bool Activa { get; set; }

    public virtual ICollection<RequisitosVacante> RequisitosVacante { get; set; } = new List<RequisitosVacante>();
}
