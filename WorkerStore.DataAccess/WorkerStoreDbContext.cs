using DataModels.Entites;
using Microsoft.EntityFrameworkCore;

namespace WorkerStore.DataAccess;
public class WorkerStoreDbContext : DbContext
{
    public DbSet<WorkerEntity> Workers { get; set; }
    public DbSet<DivisionEntity> Divisions { get; set; }

    public WorkerStoreDbContext(DbContextOptions options)
        : base(options)
    {
    }
}
