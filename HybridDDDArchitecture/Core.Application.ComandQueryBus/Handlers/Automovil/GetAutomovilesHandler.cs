using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.ComandQueryBus.DTOs.Automovil;
using Core.Application.ComandQueryBus.DTOs.Shared;
using Core.Application.ComandQueryBus.Repositories;

namespace Core.Application.ComandQueryBus.Handlers.Automovil
{
    public class GetAutomovilesHandler
    {
        private readonly IAutomovilRepository _repo;
        public GetAutomovilesHandler(IAutomovilRepository repo) => _repo = repo;

        public async Task<PagedResult<AutomovilDto>> HandleAsync(ListAutomovilQuery query, CancellationToken ct)
        {
            var (items, total) = await _repo.GetPagedAsync(query.Search, query.Page, query.PageSize, ct);

            var dtos = items.Select(a => new AutomovilDto
            {
                Id = a.Id,
                Marca = a.Marca,
                Modelo = a.Modelo,
                Color = a.Color,
                Fabricacion = a.Fabricacion,
                NumeroMotor = a.NumeroMotor,
                NumeroChasis = a.NumeroChasis
            }).ToList();

            return new PagedResult<AutomovilDto>
            {
                Page = query.Page,
                PageSize = query.PageSize,
                TotalItems = total,
                Items = dtos
            };
        }
    }
}
