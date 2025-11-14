using System;
using System.Collections.Generic;

namespace Proyecto3.CORE.Data;

public partial class ReporteBrechas
{
    public int ReporteId { get; set; }

    public string? Area { get; set; }

    public int SkillId { get; set; }

    public decimal? BrechaPorcentaje { get; set; }

    public DateTime? FechaReporte { get; set; }

    public virtual Skills Skill { get; set; } = null!;
}
