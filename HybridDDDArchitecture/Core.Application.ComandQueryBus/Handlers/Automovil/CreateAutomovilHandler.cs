using Core.Application.ComandQueryBus.DTOs.Automovil;
using Core.Application.ComandQueryBus.Repositories;
using Core.Domain.Entities;

namespace Core.Application.ComandQueryBus.Handlers.Automovil;
    public class CreateAutomovilHandler
    {
        private readonly IAutomovilRepository _repo;

        public CreateAutomovilHandler(IAutomovilRepository repo)
        {
            _repo = repo;
        }

        public async Task<AutomovilDto> HandleAsync(CreateAutomovilDto dto, CancellationToken ct = default)
        {
            // Validaciones mínimas (obligatorios)
            if (string.IsNullOrWhiteSpace(dto.Marca)) throw new ArgumentNullException(nameof(dto.Marca));
            if (string.IsNullOrWhiteSpace(dto.Modelo)) throw new ArgumentNullException(nameof(dto.Modelo));
            if (string.IsNullOrWhiteSpace(dto.Color)) throw new ArgumentNullException(nameof(dto.Color));
            if (string.IsNullOrWhiteSpace(dto.NumeroMotor)) throw new ArgumentNullException(nameof(dto.NumeroMotor));
            if (string.IsNullOrWhiteSpace(dto.NumeroChasis)) throw new ArgumentNullException(nameof(dto.NumeroChasis));

            // Reglas de unicidad (previas a BD)
            if (await _repo.ExistsByNumeroMotorAsync(dto.NumeroMotor, ct))
                throw new InvalidOperationException("Ya existe un automóvil con ese número de motor.");
            if (await _repo.ExistsByNumeroChasisAsync(dto.NumeroChasis, ct))
                throw new InvalidOperationException("Ya existe un automóvil con ese número de chasis.");

            var entity = new Core.Domain.Entities.Automovil(
                dto.Marca, dto.Modelo, dto.Color, dto.Fabricacion, dto.NumeroMotor, dto.NumeroChasis);

            await _repo.AddAsync(entity, ct);
            await _repo.SaveChangesAsync(ct);

            return new AutomovilDto
            {
                Id = entity.Id,
                Marca = entity.Marca,
                Modelo = entity.Modelo,
                Color = entity.Color,
                Fabricacion = entity.Fabricacion,
                NumeroMotor = entity.NumeroMotor,
                NumeroChasis = entity.NumeroChasis
            };
        }
    }

