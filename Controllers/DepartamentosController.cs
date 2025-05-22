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
    public class DepartamentosController : ControllerBase
    {
        private readonly DepartamentoService _service;

        public DepartamentosController(DepartamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.ObtenerTodosAsync();
            return Ok(result);
        }

        [HttpGet("{empcod:int}/{depcod:int}")]
        public async Task<IActionResult> GetById(int empcod, int depcod)
        {
            var item = await _service.ObtenerPorIdAsync(empcod, depcod);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] DepartamentoDto dto)
        {
            await _service.CrearAsync(dto);
            return Ok(new { mensaje = "Departamento creado correctamente." });
        }

        [HttpPut("{empcod:int}/{depcod:int}")]
        public async Task<IActionResult> Actualizar(int empcod, int depcod, [FromBody] DepartamentoDto dto)
        {
            if (dto.Empcod != empcod || dto.Depcod != depcod)
                return BadRequest("Los códigos no coinciden.");

            await _service.ActualizarAsync(dto);
            return Ok(new { mensaje = "Departamento actualizado correctamente." });
        }

        [HttpDelete("{empcod:int}/{depcod:int}")]
        public async Task<IActionResult> Eliminar(int empcod, int depcod)
        {
            await _service.EliminarAsync(empcod, depcod);
            return Ok(new { mensaje = "Departamento eliminado correctamente." });
        }
    }
}
