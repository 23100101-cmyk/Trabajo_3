using System.Collections.Generic;

namespace Practica03.Core.DTOs
{
    // DTO para operaciones de entrada/salida sobre Colaboradores
    public class ColaboradorDTO
    {
        public int? ColaboradorId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string? CuentaProyecto { get; set; }
        public string? RolActual { get; set; }

        // Lista de skills con nivel para las actualizaciones periódicas
        public List<SkillNivelDTO> Skills { get; set; } = new();
    }

    public class SkillNivelDTO
    {
        public int SkillId { get; set; }
        public string NivelDominio { get; set; } = string.Empty;
    }
}
