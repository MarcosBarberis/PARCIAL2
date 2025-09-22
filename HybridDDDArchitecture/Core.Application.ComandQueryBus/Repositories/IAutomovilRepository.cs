using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Entities;


namespace Core.Application.ComandQueryBus.Repositories
{
    public interface IAutomovilRepository
    {
        Task AddAsync(Automovil entity, CancellationToken ct = default);
        Task<bool> ExistsByNumeroMotorAsync(string numeroMotor, CancellationToken ct = default);
        Task<bool> ExistsByNumeroChasisAsync(string numeroChasis, CancellationToken ct = default);
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task<Automovil?> FindByIdAsync(int id);
        void Remove(Automovil entity);
    }
}
