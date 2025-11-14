using System;
using System.Collections.Generic;

namespace Proyecto3.CORE.Data;

public partial class Certificaciones
{
    public int CertificacionId { get; set; }

    public int ColaboradorId { get; set; }

    public string NombreCertificacion { get; set; } = null!;

    public DateOnly? FechaObtencion { get; set; }

    public string? Institucion { get; set; }

    public virtual Colaboradores Colaborador { get; set; } = null!;
}
