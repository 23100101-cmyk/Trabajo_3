using System;
using System.Collections.Generic;

namespace Proyecto3.CORE.Data;

public partial class HabilidadesColaborador
{
    public int HabilidadColaboradorId { get; set; }

    public int ColaboradorId { get; set; }

    public int SkillId { get; set; }

    public string NivelDominio { get; set; } = null!;

    public DateOnly? FechaUltimaEvaluacion { get; set; }

    public virtual Colaboradores Colaborador { get; set; } = null!;

    public virtual Skills Skill { get; set; } = null!;
}
