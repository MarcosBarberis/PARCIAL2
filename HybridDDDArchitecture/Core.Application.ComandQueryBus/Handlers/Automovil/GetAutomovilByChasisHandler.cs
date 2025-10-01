using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.ComandQueryBus.DTOs.Automovil;
using Core.Application.ComandQueryBus.Repositories;

namespace Core.Application.ComandQueryBus.Handlers.Automovil
{
    public class GetAutomovilByChasisHandler
    {
        private readonly IAutomovilRepository _repo;
        public GetAutomovilByChasisHandler(IAutomovilRepository repo) => _repo = repo;

        public async Task<AutomovilDto> HandleAsync(string numeroChasis, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(numeroChasis))
                throw new ArgumentNullException(nameof(numeroChasis));

            var a = await _repo.FindByChasisAsync(numeroChasis.Trim(), ct);
            if (a == null)
                throw new KeyNotFoundException("No existe un automóvil con ese número de chasis.");

            return new AutomovilDto
            {
                Id = a.Id,
                Marca = a.Marca,
                Modelo = a.Modelo,
                Color = a.Color,
                Fabricacion = a.Fabricacion,
                NumeroMotor = a.NumeroMotor,
                NumeroChasis = a.NumeroChasis
            };
        }
    }
}
