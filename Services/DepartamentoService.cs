using ExecutionPca.Api.Dtos;
using ExecutionPca.Api.Models;
using ExecutionPca.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExecutionPca.Api.Services
{
    public class DepartamentoService
    {
        private readonly IDepartamentoRepository _repository;

        public DepartamentoService(IDepartamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Departamento>> ObtenerTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Departamento?> ObtenerPorIdAsync(int empcod, int depcod)
        {
            return await _repository.GetByIdAsync(empcod, depcod);
        }

        public async Task CrearAsync(DepartamentoDto dto)
        {
            var nuevo = new Departamento
            {
                Empcod = dto.Empcod,
                Depcod = dto.Depcod,
                Depdes = dto.Depdes,
                Bitacora = dto.Bitacora,
                MisionParticipa = dto.MisionParticipa
            };

            await _repository.AddAsync(nuevo);
        }

        public async Task ActualizarAsync(DepartamentoDto dto)
        {
            var existente = await _repository.GetByIdAsync(dto.Empcod, dto.Depcod);
            if (existente == null) return;

            existente.Depdes = dto.Depdes;
            existente.Bitacora = dto.Bitacora;
            existente.MisionParticipa = dto.MisionParticipa;

            await _repository.UpdateAsync(existente);
        }

        public async Task EliminarAsync(int empcod, int depcod)
        {
            await _repository.DeleteAsync(empcod, depcod);
        }
    }
}
