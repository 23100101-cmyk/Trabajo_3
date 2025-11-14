using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto3.CORE.Data;

namespace Practica03.Core.Interfaces
{
    // Define las operaciones CRUD mínimas para Colaboradores
    public interface IColaboradorRepository
    {
        Task<Colaboradores> CreateAsync(Colaboradores entity);
        Task<bool> UpdateAsync(Colaboradores entity);
        Task<bool> DeleteAsync(int id);
        Task<Colaboradores?> GetByIdAsync(int id);
        Task<List<Colaboradores>> GetAllAsync();

        // Métodos para manejar habilidades relacionadas
        Task<List<HabilidadesColaborador>> GetSkillsByColaboradorAsync(int colaboradorId);
        Task<bool> DeleteSkillsByColaboradorAsync(int colaboradorId);
        Task<bool> AddSkillsAsync(List<HabilidadesColaborador> habilidades);
    }
}
