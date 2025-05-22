using ExecutionPca.Api.Dtos;
using ExecutionPca.Api.Models;
using ExecutionPca.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExecutionPca.Api.Services
{
    public class PuestoService
    {
        private readonly IPuestoRepository _repository;

        public PuestoService(IPuestoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PuestoDto>> GetAllAsync()
        {
            var puestos = await _repository.GetAllAsync();
            return puestos.Select(p => new PuestoDto
            {
                Codigo = p.Puecod,
                Nombre = p.Puenom
            });
        }

        public async Task<PuestoDto?> GetByCodigoAsync(int codigo)
        {
            var puesto = await _repository.GetByCodigoAsync(codigo);
            if (puesto == null) return null;

            return new PuestoDto
            {
                Codigo = puesto.Puecod,
                Nombre = puesto.Puenom
            };
        }

        public async Task AddAsync(PuestoDto dto)
        {
            var puesto = new Puesto { Puecod = dto.Codigo, Puenom = dto.Nombre };
            await _repository.AddAsync(puesto);
        }

        public async Task UpdateAsync(PuestoDto dto)
        {
            var puesto = new Puesto { Puecod = dto.Codigo, Puenom = dto.Nombre };
            await _repository.UpdateAsync(puesto);
        }

        public async Task DeleteAsync(int codigo)
        {
            await _repository.DeleteAsync(codigo);
        }
    }
}
