using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReactWebManual.Server.Models;
using WorkerStore.DataAccess.Entites;

namespace WorkerStore.DataAccess.Configurations
{
    public class WorkerConfiguration : IEntityTypeConfiguration<WorkerEntity>
    {
        public void Configure(EntityTypeBuilder<WorkerEntity> builder) 
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.FIO)
                .HasMaxLength(Worker.MAX_FIO_LENGTH)
                .IsRequired();
        }
    }
}
