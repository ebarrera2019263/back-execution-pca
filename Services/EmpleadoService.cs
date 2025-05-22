using ExecutionPca.Api.Dtos;
using ExecutionPca.Api.Models;
using ExecutionPca.Api.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExecutionPca.Api.Services
{
    public class EmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmpleadoService(IEmpleadoRepository empleadoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _empleadoRepository = empleadoRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Empleado>> ObtenerTodosAsync()
        {
            return await _empleadoRepository.GetAllAsync();
        }

        public async Task<Empleado?> ObtenerPorCodigoAsync(int codigo)
        {
            return await _empleadoRepository.GetByCodigoAsync(codigo);
        }

        public async Task CrearAsync(EmpleadoDto dto)
        {
            var usuario = ObtenerUsuarioDelToken();
            var nuevoEmpleado = new Empleado
            {
                Emplcod = dto.Emplcod ?? await GenerarNuevoCodigoAsync(),
                PrimerNombre = dto.PrimerNombre,
                SegundoNombre = dto.SegundoNombre,
                PrimerApellido = dto.PrimerApellido,
                SegundoApellido = dto.SegundoApellido,
                FechaNacimiento = dto.FechaNacimiento,
                Email = dto.Email,
                Sexo = dto.Sexo,
                CodigoReal = dto.CodigoReal,
                UsuarioId = decimal.Parse(usuario),
                SiempreN = "N",
                Bitacora = $"Created by {usuario} on {DateTime.UtcNow}"
            };

            await _empleadoRepository.AddAsync(nuevoEmpleado);
        }

        public async Task ActualizarAsync(EmpleadoDto dto)
        {
            if (dto.Emplcod == null)
                throw new ArgumentException("Código de empleado requerido para actualizar.");

            var empleadoExistente = await _empleadoRepository.GetByCodigoAsync((int)dto.Emplcod.Value);
            if (empleadoExistente == null)
                throw new InvalidOperationException("Empleado no encontrado.");

            var usuario = ObtenerUsuarioDelToken();

            empleadoExistente.PrimerNombre = dto.PrimerNombre;
            empleadoExistente.SegundoNombre = dto.SegundoNombre;
            empleadoExistente.PrimerApellido = dto.PrimerApellido;
            empleadoExistente.SegundoApellido = dto.SegundoApellido;
            empleadoExistente.FechaNacimiento = dto.FechaNacimiento;
            empleadoExistente.Email = dto.Email;
            empleadoExistente.Sexo = dto.Sexo;
            empleadoExistente.CodigoReal = dto.CodigoReal;
            empleadoExistente.Bitacora = $"Updated by {usuario} on {DateTime.UtcNow}";

            await _empleadoRepository.UpdateAsync(empleadoExistente);
        }

        public async Task EliminarAsync(int codigo)
        {
            await _empleadoRepository.DeleteAsync(codigo);
        }

        private string ObtenerUsuarioDelToken()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException("Usuario no autenticado.");
        }

        private async Task<decimal> GenerarNuevoCodigoAsync()
        {
            var empleados = await _empleadoRepository.GetAllAsync();
            return empleados.Any() ? empleados.Max(e => e.Emplcod) + 1 : 1;
        }
    }
}
