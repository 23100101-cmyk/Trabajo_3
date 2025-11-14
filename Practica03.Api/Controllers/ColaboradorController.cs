using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practica03.Core.DTOs;
using Practica03.Core.Services;
using Proyecto3.CORE.Data;

namespace Practica03.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradorController : ControllerBase
    {
        private readonly ColaboradorService _service;

        public ColaboradorController(ColaboradorService service)
        {
            _service = service;
        }

        // POST: api/Colaborador
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ColaboradorDTO dto)
        {
            if (dto == null) return BadRequest("Payload inválido");

            var (success, error, entity) = await _service.HandleCreateAsync(dto);
            if (!success)
            {
                return BadRequest(new { Message = error });
            }

            // Devolver Created con la ubicación del nuevo recurso
            return CreatedAtAction(nameof(GetById), new { id = entity.ColaboradorId }, new
            {
                entity.ColaboradorId,
                entity.NombreCompleto,
                entity.CuentaProyecto,
                entity.RolActual
            });
        }

        // GET: api/Colaborador/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();

            var dto = new ColaboradorDTO
            {
                ColaboradorId = entity.ColaboradorId,
                NombreCompleto = entity.NombreCompleto,
                CuentaProyecto = entity.CuentaProyecto,
                RolActual = entity.RolActual,
                Skills = (await _service.GetByIdAsync(id)) is var e && e != null ?
                            (await Task.FromResult(e.HabilidadesColaborador?.Select(h => new Core.DTOs.SkillNivelDTO { SkillId = h.SkillId, NivelDominio = h.NivelDominio }).ToList()) ?? new System.Collections.Generic.List<Core.DTOs.SkillNivelDTO>())
                          : new System.Collections.Generic.List<Core.DTOs.SkillNivelDTO>()
            };

            return Ok(dto);
        }

        // PUT: api/Colaborador/{id}
        // Actualiza datos del colaborador y ejecuta la lógica de actualización periódica de skills
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ColaboradorDTO dto)
        {
            if (dto == null) return BadRequest("Payload inválido");

            // Asegurar que el id es consistente
            dto.ColaboradorId = id;

            // Ejecutar la actualización periódica de skills (se espera que el servicio maneje validación)
            var (success, error) = await _service.UpdateSkillsAsync(id, dto.Skills);
            if (!success) return BadRequest(new { Message = error });

            // Actualizar datos básicos del colaborador
            var entity = new Colaboradores
            {
                ColaboradorId = id,
                NombreCompleto = dto.NombreCompleto,
                CuentaProyecto = dto.CuentaProyecto,
                RolActual = dto.RolActual
            };

            var updated = await _service.UpdateAsync(entity);
            if (!updated) return NotFound(new { Message = "No se encontró o no se pudo actualizar el colaborador." });

            return NoContent();
        }

        // DELETE: api/Colaborador/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
