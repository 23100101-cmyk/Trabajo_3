using System;
using System.Collections.Generic;

namespace Proyecto3.CORE.Data;

public partial class Skills
{
    public int SkillId { get; set; }

    public string NombreSkill { get; set; } = null!;

    public string TipoSkill { get; set; } = null!;

    public virtual ICollection<HabilidadesColaborador> HabilidadesColaborador { get; set; } = new List<HabilidadesColaborador>();

    public virtual ICollection<ReporteBrechas> ReporteBrechas { get; set; } = new List<ReporteBrechas>();

    public virtual ICollection<RequisitosVacante> RequisitosVacante { get; set; } = new List<RequisitosVacante>();
}
