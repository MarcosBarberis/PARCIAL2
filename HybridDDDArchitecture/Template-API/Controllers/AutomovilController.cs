using Core.Application.ComandQueryBus.DTOs.Automovil;
using Core.Application.ComandQueryBus.DTOs.Shared;
using Core.Application.ComandQueryBus.Handlers.Automovil;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/automovil")] // => /api/v1/automovil
    public class AutomovilController : ControllerBase
    {
        private readonly CreateAutomovilHandler _create;
        private readonly UpdateAutomovilHandler _update;
        private readonly GetAutomovilByIdHandler _getById;
        private readonly GetAutomovilesHandler _getList;
        private readonly DeleteAutomovilHandler _delete;
        private readonly GetAutomovilByChasisHandler _getByChasis;

        public AutomovilController(
            CreateAutomovilHandler create,
            UpdateAutomovilHandler update,
            GetAutomovilByIdHandler getById,
            GetAutomovilesHandler getList,
            DeleteAutomovilHandler delete,
            GetAutomovilByChasisHandler getByChasis)
        {
            _create = create;
            _update = update;
            _getById = getById;
            _getList = getList;
            _delete = delete;
            _getByChasis = getByChasis;
        }

        // POST /api/v1/automovil
        [HttpPost]
        [ProducesResponseType(typeof(AutomovilDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromBody] CreateAutomovilDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var created = await _create.HandleAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
            catch (ArgumentNullException ex)     { return BadRequest(new { message = $"Campo requerido: {ex.ParamName}" }); }
            catch (DbUpdateException ex)         { return Conflict(new { message = "Conflicto al guardar en BD.", detail = ex.InnerException?.Message ?? ex.Message }); }
        }

        // GET /api/v1/automovil?search=&page=&pageSize=
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<AutomovilDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            var q = new ListAutomovilQuery
            {
                Search = search,
                Page = page < 1 ? 1 : page,
                PageSize = pageSize is < 1 or > 100 ? 10 : pageSize
            };
            var result = await _getList.HandleAsync(q, ct);
            return Ok(result);
        }

        // GET /api/v1/automovil/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AutomovilDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id, CancellationToken ct = default)
        {
            var dto = await _getById.HandleAsync(id, ct);
            return dto is null ? NotFound() : Ok(dto);
        }

        // PUT /api/v1/automovil/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAutomovilDto dto, CancellationToken ct)
        {
            if (id != dto.Id) return BadRequest("El id de la ruta no coincide con el del cuerpo.");

            // Tu handler devuelve la entidad actualizada (o null si no existe)
            var updated = await _update.HandleAsync(id, dto, ct);
            if (updated is null) return NotFound();

            return NoContent(); // (o Ok(updated) si querés devolverla)
        }

        // DELETE /api/v1/automovil/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var ok = await _delete.HandleAsync(id, ct);
            return ok
                ? Ok(new { message = $"Automóvil {id} eliminado correctamente." })
                : NotFound(new { message = $"No existe un automóvil con id {id}." });
        }
        /// <summary>Obtiene un automóvil por número de chasis.</summary>
        /// <remarks>Ruta: /api/v1/automovil/chasis/{numeroChasis}</remarks>
        [HttpGet("chasis/{numeroChasis}")]
        [ProducesResponseType(typeof(AutomovilDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByChasis([FromRoute] string numeroChasis, CancellationToken ct = default)
        {
            try
            {
                var dto = await _getByChasis.HandleAsync(numeroChasis, ct);
                return Ok(dto);
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (ArgumentNullException ex) { return BadRequest(new { message = $"Campo requerido: {ex.ParamName}" }); }
        }
    }
}
