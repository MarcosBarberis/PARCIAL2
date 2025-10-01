using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Entities;

namespace Core.Application.ComandQueryBus.Repositories // deja este namespace si así lo usa tu solución
{
    public interface IAutomovilRepository
    {
        // ===== Lectura =====
        Task<Automovil?> FindByIdAsync(int id, CancellationToken ct = default);

        /// <summary>
        /// Devuelve una página de Automóvil + total de registros.
        /// page: 1..N, pageSize > 0, search opcional.
        /// </summary>
        Task<(IReadOnlyList<Automovil> Items, int Total)> GetPagedAsync(
            string? search, int page, int pageSize, CancellationToken ct = default);

        IQueryable<Automovil> Query();
        Task<IEnumerable<Automovil>> GetAllAsync(CancellationToken ct = default);

        // ===== Escritura =====
        Task AddAsync(Automovil entity, CancellationToken ct = default);
        Task UpdateAsync(Automovil entity, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);

        // ===== Utilidades / Reglas de unicidad =====
        Task<bool> ExistsByNumeroMotorAsync(string numeroMotor, CancellationToken ct = default);
        Task<bool> ExistsByNumeroChasisAsync(string numeroChasis, CancellationToken ct = default);

        Task<int> SaveChangesAsync(CancellationToken ct = default);
     
        void Remove(Automovil entity);
    }
}
