using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WorkerStore.DataAccess.Extantions;

public static class MigrationExtantions
{
    public static void AplayMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<WorkerStoreDbContext>();
        context.Database.Migrate();
    }
}
