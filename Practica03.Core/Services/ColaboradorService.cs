using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practica03.Core.DTOs;
using Practica03.Core.Interfaces;
using Practica03.Core.Validators;
using Proyecto3.CORE.Data;

namespace Practica03.Core.Services
{
    // Servicio que orquesta validación y operaciones de repositorio
    public class ColaboradorService
    {
        private readonly IColaboradorRepository _repository;
        private readonly ColaboradorValidator _validator;

        public ColaboradorService(IColaboradorRepository repository)
        {
            _repository = repository;
            _validator = new ColaboradorValidator();
        }

        // Maneja la creación de un colaborador
        public async Task<(bool Success, string? Error, Colaboradores? Entity)> HandleCreateAsync(ColaboradorDTO dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
            {
                return (false, string.Join(';', validation.Errors.Select(e => e.ErrorMessage)), null);
            }

            // Mapear DTO a entidad (simulado)
            var entity = new Colaboradores
            {
                NombreCompleto = dto.NombreCompleto,
                CuentaProyecto = dto.CuentaProyecto,
                RolActual = dto.RolActual,
            };

            var created = await _repository.CreateAsync(entity);

            // Si hay skills, agregarlas
            if (dto.Skills != null && dto.Skills.Any())
            {
                var habilidades = dto.Skills.Select(s => new HabilidadesColaborador
                {
                    ColaboradorId = created.ColaboradorId,
                    SkillId = s.SkillId,
                    NivelDominio = s.NivelDominio
                }).ToList();

                await _repository.AddSkillsAsync(habilidades);
            }

            return (true, null, created);
        }

        // Actualización periódica de skills: elimina actuales y agrega nuevos
        public async Task<(bool Success, string? Error)> UpdateSkillsAsync(int colaboradorId, List<SkillNivelDTO> nuevasSkills)
        {
            // Validar entrada mínima
            if (nuevasSkills == null) return (false, "Lista de skills vacía");

            // Verificar colaborador existe
            var colaborador = await _repository.GetByIdAsync(colaboradorId);
            if (colaborador == null) return (false, "Colaborador no encontrado");

            // Eliminar skills antiguos
            var deleted = await _repository.DeleteSkillsByColaboradorAsync(colaboradorId);
            if (!deleted) return (false, "No se pudieron eliminar las skills antiguas");

            // Preparar nuevas habilidades
            var nuevas = nuevasSkills.Select(s => new HabilidadesColaborador
            {
                ColaboradorId = colaboradorId,
                SkillId = s.SkillId,
                NivelDominio = s.NivelDominio
            }).ToList();

            var added = await _repository.AddSkillsAsync(nuevas);
            if (!added) return (false, "No se pudieron agregar las nuevas skills");

            return (true, null);
        }

        // Otros métodos CRUD simples que delegan al repositorio
        public Task<List<Colaboradores>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Colaboradores?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<bool> UpdateAsync(Colaboradores entity) => _repository.UpdateAsync(entity);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
