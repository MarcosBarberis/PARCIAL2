using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.ComandQueryBus.Repositories;

namespace Core.Application.ComandQueryBus.Handlers.Automovil
{
    public class DeleteAutomovilHandler
    {
        private readonly IAutomovilRepository _repo;

        public DeleteAutomovilHandler(IAutomovilRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> HandleAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.FindByIdAsync(id);
            if (entity is null) return false;

            _repo.Remove(entity);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}