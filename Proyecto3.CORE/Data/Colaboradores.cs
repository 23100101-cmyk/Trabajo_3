using System;
using System.Collections.Generic;

namespace Proyecto3.CORE.Data;

public partial class Colaboradores
{
    public int ColaboradorId { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string RolActual { get; set; } = null!;

    public string? CuentaProyecto { get; set; }

    public bool DisponibilidadMovilidad { get; set; }

    public virtual ICollection<Certificaciones> Certificaciones { get; set; } = new List<Certificaciones>();

    public virtual ICollection<HabilidadesColaborador> HabilidadesColaborador { get; set; } = new List<HabilidadesColaborador>();
}
