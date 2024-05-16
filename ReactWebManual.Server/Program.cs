using Microsoft.EntityFrameworkCore;
using ReactWebManual.Server.Interface;
using ReactWebManual.Server.Servises;
using WorkerStore.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IDivisionService, DivisionService>();
builder.Services.AddTransient<IWorkerService, WorkerService>();


var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<WorkerStoreDbContext>();
    context.Database.Migrate();
}

app.Run();
