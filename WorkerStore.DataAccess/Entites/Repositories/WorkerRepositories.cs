

using Microsoft.EntityFrameworkCore;
using ReactWebManual.Server.Models;
using System.Diagnostics.Eventing.Reader;

namespace WorkerStore.DataAccess.Entites.Repositories
{
    public class WorkerRepositories : IWorkerRepositories
    {
        private readonly WorkerStoreDbContext _context;
        public WorkerRepositories(WorkerStoreDbContext context) 
        { 
            _context = context;
        }

        public async Task<List<Worker>> Get()
        {
            var workerEntities = await _context.Workers
                .AsNoTracking()
                .ToListAsync();

            var workers = workerEntities
                .Select(b => Worker.Create(b.Id, b.FIO, b.DR, b.Sex, b.Post, b.DriversLicense))
                .ToList();

            return workers;
        }

        public async Task<Guid> Create(Worker worker)
        {
            var workerEntity = new WorkerEntity
            {
                Id = worker.Id,
                FIO = worker.FIO,
                DR = worker.DR,
                Sex = worker.Sex,
                Post = worker.Post,
                DriversLicense = worker.DriversLicense,
            };

            await _context.Workers.AddAsync(workerEntity);
            await _context.SaveChangesAsync();

            return workerEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string fio, string dr, string sex, string post, Boolean driverslicense)
        {
            await _context.Workers
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.FIO, b => fio)
                    .SetProperty(b => b.DR, b => dr)
                    .SetProperty(b => b.Sex, b => sex)
                    .SetProperty(b => b.Post, b => post)
                    .SetProperty(b => b.DriversLicense, b => driverslicense)
                );

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Workers
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
