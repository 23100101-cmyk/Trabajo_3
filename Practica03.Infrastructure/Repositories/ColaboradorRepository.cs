using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Practica03.Core.Interfaces;
using Proyecto3.CORE.Data;

namespace Practica03.Infrastructure.Repositories
{
    // Implementación simulada del repositorio usando DbTalentoInternoContext
    public class ColaboradorRepository : IColaboradorRepository
    {
        private readonly DbTalentoInternoContext _context;

        public ColaboradorRepository(DbTalentoInternoContext context)
        {
            _context = context;
        }

        public async Task<Colaboradores> CreateAsync(Colaboradores entity)
        {
            _context.Colaboradores.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(Colaboradores entity)
        {
            _context.Colaboradores.Update(entity);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Colaboradores.FindAsync(id);
            if (entity == null) return false;
            _context.Colaboradores.Remove(entity);
            var deleted = await _context.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Colaboradores?> GetByIdAsync(int id)
        {
            return await _context.Colaboradores
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ColaboradorId == id);
        }

        public async Task<List<Colaboradores>> GetAllAsync()
        {
            return await _context.Colaboradores.AsNoTracking().ToListAsync();
        }

        public async Task<List<HabilidadesColaborador>> GetSkillsByColaboradorAsync(int colaboradorId)
        {
            return await _context.HabilidadesColaborador
                .Where(h => h.ColaboradorId == colaboradorId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> DeleteSkillsByColaboradorAsync(int colaboradorId)
        {
            var list = _context.HabilidadesColaborador.Where(h => h.ColaboradorId == colaboradorId);
            _context.HabilidadesColaborador.RemoveRange(list);
            var deleted = await _context.SaveChangesAsync();
            return deleted >= 0;
        }

        public async Task<bool> AddSkillsAsync(List<HabilidadesColaborador> habilidades)
        {
            _context.HabilidadesColaborador.AddRange(habilidades);
            var added = await _context.SaveChangesAsync();
            return added > 0;
        }
    }
}
