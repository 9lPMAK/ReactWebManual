using DataModels.Entites;
using DataModels.Enums;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<WorkerEntity>()
            .Property(x => x.Gender)
            .HasConversion(
                v => (int)v,
                v => (Gender)v);

        base.OnModelCreating(modelBuilder);
    }
}
