using Core.Application.ComandQueryBus.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Repositories.Sql
{
    public class AutomovilRepository : IAutomovilRepository
    {
        private readonly AppDbContext _ctx;
        public AutomovilRepository(AppDbContext ctx) => _ctx = ctx;

        public Task<bool> ExistsByNumeroMotorAsync(string numeroMotor, CancellationToken ct = default) =>
            _ctx.Automoviles.AnyAsync(a => a.NumeroMotor == numeroMotor, ct);

        public Task<bool> ExistsByNumeroChasisAsync(string numeroChasis, CancellationToken ct = default) =>
            _ctx.Automoviles.AnyAsync(a => a.NumeroChasis == numeroChasis, ct);

        public Task AddAsync(Automovil entity, CancellationToken ct = default) =>
            _ctx.Automoviles.AddAsync(entity, ct).AsTask();

        public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
            _ctx.SaveChangesAsync(ct);

        public Task<Automovil?> FindByIdAsync(int id) =>
            _ctx.Automoviles.FirstOrDefaultAsync(a => a.Id == id);

        public void Remove(Automovil entity) =>
            _ctx.Automoviles.Remove(entity);
    }
}
