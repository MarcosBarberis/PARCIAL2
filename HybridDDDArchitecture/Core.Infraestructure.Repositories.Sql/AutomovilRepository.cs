using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.ComandQueryBus.Repositories;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Repositories.Sql
{
    public class AutomovilRepository : IAutomovilRepository
    {
        private readonly AppDbContext _ctx;
        public AutomovilRepository(AppDbContext ctx) => _ctx = ctx;

        // ====== Lectura ======
        public Task<Automovil?> FindByIdAsync(int id, CancellationToken ct = default) =>
            _ctx.Automoviles.FirstOrDefaultAsync(a => a.Id == id, ct);

        public async Task<IEnumerable<Automovil>> GetAllAsync(CancellationToken ct = default) =>
            await _ctx.Automoviles.ToListAsync(ct);

        public async Task<(IReadOnlyList<Automovil> Items, int Total)> GetPagedAsync(
            string? search, int page, int pageSize, CancellationToken ct = default)
        {
            IQueryable<Automovil> q = _ctx.Automoviles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                q = q.Where(a =>
                    a.Marca.ToLower().Contains(s) ||
                    a.Modelo.ToLower().Contains(s) ||
                    a.Color.ToLower().Contains(s) ||
                    a.NumeroMotor.ToLower().Contains(s) ||
                    a.NumeroChasis.ToLower().Contains(s));
            }

            var total = await q.CountAsync(ct);
            var items = await q
                .OrderBy(a => a.Marca).ThenBy(a => a.Modelo)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, total);
        }

        public IQueryable<Automovil> Query() => _ctx.Automoviles.AsQueryable();

        // ====== Escritura ======
        public Task AddAsync(Automovil entity, CancellationToken ct = default) =>
            _ctx.Automoviles.AddAsync(entity, ct).AsTask();

        public async Task UpdateAsync(Automovil entity, CancellationToken ct = default)
        {
            _ctx.Automoviles.Update(entity);
            await _ctx.SaveChangesAsync(ct);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var ent = await _ctx.Automoviles.FindAsync(new object[] { id }, ct);
            if (ent is null) return false;
            _ctx.Automoviles.Remove(ent);
            await _ctx.SaveChangesAsync(ct);
            return true;
        }

        // ====== Utilidades ======
       public Task<bool> ExistsByNumeroMotorAsync(string numeroMotor, CancellationToken ct = default) =>
            _ctx.Automoviles.AnyAsync(a => a.NumeroMotor == numeroMotor, ct);

        public Task<bool> ExistsByNumeroChasisAsync(string numeroChasis, CancellationToken ct = default) =>
            _ctx.Automoviles.AnyAsync(a => a.NumeroChasis == numeroChasis, ct);

        public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
            _ctx.SaveChangesAsync(ct);

        public void Remove(Automovil entity) => _ctx.Automoviles.Remove(entity);
    }
}
