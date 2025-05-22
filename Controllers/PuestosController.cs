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
	public class PuestosController : ControllerBase
	{
		private readonly PuestoService _service;

		public PuestosController(PuestoService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll() =>
			Ok(await _service.GetAllAsync());

		[HttpGet("{codigo}")]
		public async Task<IActionResult> GetByCodigo(int codigo)
		{
			var puesto = await _service.GetByCodigoAsync(codigo);
			if (puesto == null) return NotFound();
			return Ok(puesto);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] PuestoDto dto)
		{
			await _service.AddAsync(dto);
			return CreatedAtAction(nameof(GetByCodigo), new { codigo = dto.Codigo }, dto);
		}

		[HttpPut("{codigo}")]
		public async Task<IActionResult> Update(int codigo, [FromBody] PuestoDto dto)
		{
			if (codigo != dto.Codigo)
				return BadRequest("Código no coincide.");

			await _service.UpdateAsync(dto);
			return NoContent();
		}

		[HttpDelete("{codigo}")]
		public async Task<IActionResult> Delete(int codigo)
		{
			await _service.DeleteAsync(codigo);
			return NoContent();
		}
	}
}
