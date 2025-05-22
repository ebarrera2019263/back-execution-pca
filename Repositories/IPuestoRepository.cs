using ExecutionPca.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExecutionPca.Api.Repositories
{
    public interface IPuestoRepository
    {
        Task<IEnumerable<Puesto>> GetAllAsync();
        Task<Puesto?> GetByCodigoAsync(int codigo);
        Task AddAsync(Puesto puesto);
        Task UpdateAsync(Puesto puesto);
        Task DeleteAsync(int codigo);
    }
}
