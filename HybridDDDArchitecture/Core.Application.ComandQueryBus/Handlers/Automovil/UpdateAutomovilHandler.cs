using Core.Application.ComandQueryBus.DTOs.Automovil;
using Core.Application.ComandQueryBus.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

// Alias para evitar colisión con el namespace "Handlers.Automovil"
using AutomovilEntity = Core.Domain.Entities.Automovil;

namespace Core.Application.ComandQueryBus.Handlers.Automovil
{
    public class UpdateAutomovilHandler
    {
        private readonly IAutomovilRepository _repo;

        public UpdateAutomovilHandler(IAutomovilRepository repo)
        {
            _repo = repo;
        }

        public async Task<AutomovilEntity> HandleAsync(int id, UpdateAutomovilDto dto, CancellationToken ct)
        {
            // Tu interfaz tiene FindByIdAsync SIN CancellationToken; lo usamos así.
            var entity = await _repo.FindByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("No existe un automóvil con ese Id.");

            // ---- Validaciones de unicidad para UPDATE ----
            // Motor: si cambia y el nuevo motor ya está en uso por otro, conflicto.
            if (!string.IsNullOrWhiteSpace(dto.NumeroMotor) &&
                !dto.NumeroMotor.Equals(entity.NumeroMotor, StringComparison.OrdinalIgnoreCase))
            {
                var motorEnUso = await _repo.ExistsByNumeroMotorAsync(dto.NumeroMotor, ct);
                if (motorEnUso)
                    throw new InvalidOperationException("Ya existe otro automóvil con el mismo número de motor.");
            }

            // Chasis: si cambia y el nuevo chasis ya está en uso por otro, conflicto.
            if (!string.IsNullOrWhiteSpace(dto.NumeroChasis) &&
                !dto.NumeroChasis.Equals(entity.NumeroChasis, StringComparison.OrdinalIgnoreCase))
            {
                var chasisEnUso = await _repo.ExistsByNumeroChasisAsync(dto.NumeroChasis, ct);
                if (chasisEnUso)
                    throw new InvalidOperationException("Ya existe otro automóvil con el mismo número de chasis.");
            }

            // ---- Asignaciones parciales (solo lo que venga en el body) ----
            entity.ApplyUpdate(
           dto.Marca,
           dto.Modelo,
           dto.Color,
           dto.Fabricacion,
           dto.NumeroMotor,
           dto.NumeroChasis
       );


            await _repo.SaveChangesAsync(ct);
            return entity; // si preferís devolver DTO, avisá y te lo dejo mapeado
        }
    }
}
