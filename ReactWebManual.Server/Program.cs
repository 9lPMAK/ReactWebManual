using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReactWebManual.Server.Interface;
using ReactWebManual.Server.Servises;
using WorkerStore.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddTransient<ITransientService, TransientService>();
//builder.Services.AddScoped<IScopedService, ScopedService>();
//builder.Services.AddSingleton<ISingletonService, SingletonService>();
builder.Services.AddTransient<IDivisionService, DivisionService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();


// Configure the HTTP request pipeline.
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
    //context.Database.EnsureCreated();
}

app.Run();
