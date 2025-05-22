using ExecutionPca.Api.Dtos;
using ExecutionPca.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExecutionPca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadoService _empleadoService;

        public EmpleadosController(EmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var empleados = await _empleadoService.ObtenerTodosAsync();
            return Ok(empleados);
        }

        [HttpGet("{codigo:int}")]
        public async Task<IActionResult> GetByCodigo(int codigo)
        {
            var empleado = await _empleadoService.ObtenerPorCodigoAsync(codigo);
            if (empleado == null) return NotFound();
            return Ok(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> CrearEmpleado([FromBody] EmpleadoDto dto)
        {
            await _empleadoService.CrearAsync(dto);
            return Ok(new { mensaje = "Empleado creado exitosamente" });
        }

        [HttpPut("{codigo:int}")]
        public async Task<IActionResult> ActualizarEmpleado(int codigo, [FromBody] EmpleadoDto dto)
        {
            if (dto.Emplcod != codigo)
                return BadRequest("El código en la URL no coincide con el del cuerpo.");

            await _empleadoService.ActualizarAsync(dto);
            return Ok(new { mensaje = "Empleado actualizado exitosamente" });
        }

        [HttpDelete("{codigo:int}")]
        public async Task<IActionResult> EliminarEmpleado(int codigo)
        {
            await _empleadoService.EliminarAsync(codigo);
            return Ok(new { mensaje = "Empleado eliminado exitosamente" });
        }
    }
}
