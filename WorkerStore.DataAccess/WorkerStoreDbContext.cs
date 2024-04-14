using Microsoft.EntityFrameworkCore;
using WorkerStore.DataAccess.Entites;

namespace WorkerStore.DataAccess
{
    public class WorkerStoreDbContext : DbContext
    {
        public WorkerStoreDbContext(DbContextOptions<WorkerStoreDbContext> options)
            : base(options) 
        {
   
        }

        public DbSet<WorkerEntity> Workers { get; set; }
    }
}
