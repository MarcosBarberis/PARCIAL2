using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.ComandQueryBus.DTOs.Automovil;
using Core.Application.ComandQueryBus.Repositories;

namespace Core.Application.ComandQueryBus.Handlers.Automovil
{
    public class GetAutomovilByIdHandler
    {
        private readonly IAutomovilRepository _repo;
        public GetAutomovilByIdHandler(IAutomovilRepository repo) => _repo = repo;

        public async Task<AutomovilDto> HandleAsync(int id, CancellationToken ct)
        {
            var a = await _repo.FindByIdAsync(id);
            if (a == null) throw new KeyNotFoundException("No existe un automóvil con ese Id.");

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
