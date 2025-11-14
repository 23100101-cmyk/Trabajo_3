using System;
using System.Collections.Generic;

namespace Proyecto3.CORE.Data;

public partial class RequisitosVacante
{
    public int RequisitoId { get; set; }

    public int VacanteId { get; set; }

    public int SkillId { get; set; }

    public string NivelMinimoRequerido { get; set; } = null!;

    public virtual Skills Skill { get; set; } = null!;

    public virtual PerfilesVacante Vacante { get; set; } = null!;
}
