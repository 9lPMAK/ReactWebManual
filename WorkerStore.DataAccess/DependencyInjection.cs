using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace WorkerStore.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<WorkerStoreDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });


        return services;
    }
}