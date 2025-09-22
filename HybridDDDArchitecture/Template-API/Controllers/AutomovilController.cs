using Core.Application.ComandQueryBus.DTOs.Automovil;
using Core.Application.ComandQueryBus.Handlers.Automovil;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/automovil")]
    public class AutomovilController : ControllerBase
    {
        private readonly CreateAutomovilHandler _create;
        private readonly DeleteAutomovilHandler _delete;

        public AutomovilController(
            CreateAutomovilHandler create,
            DeleteAutomovilHandler delete)
        {
            _create = create;
            _delete = delete;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAutomovilDto dto, CancellationToken ct)
        {
            try
            {
                var created = await _create.HandleAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
            catch (ArgumentNullException ex) { return BadRequest(new { message = $"Campo requerido: {ex.ParamName}" }); }
        }

        // Stub mínimo para que CreatedAtAction tenga destino
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) => NoContent();

        // ✅ DELETE /api/v1/automovil/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var ok = await _delete.HandleAsync(id, ct);
            if (!ok)
                return NotFound(new { message = $"No existe un automóvil con id {id}." });

            return Ok(new { message = $"Automóvil {id} eliminado correctamente." });
        }
    }
}
