using ExecutionPca.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExecutionPca.Api.Repositories
{
    public interface IDepartamentoRepository
    {
        Task<IEnumerable<Departamento>> GetAllAsync();
        Task<Departamento?> GetByIdAsync(int empcod, int depcod);
        Task AddAsync(Departamento departamento);
        Task UpdateAsync(Departamento departamento);
        Task DeleteAsync(int empcod, int depcod);
    }
}
