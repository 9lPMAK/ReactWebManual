using DataModels.Entites;
using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using ReactWebManual.Server.Interface;
using WorkerStore.DataAccess;

namespace ReactWebManual.Server.Servises;

public class WorkerService(WorkerStoreDbContext db) : IWorkerService
{
    public Task<List<WorkerEntity>> GetAll() => db.Workers.ToListAsync();

	public async Task<(bool, string?)> Remove(int id)
	{
        var worker = await db.Workers.FirstOrDefaultAsync(d => d.Id == id);
        if (worker is null)
            return (false, "ID не найден");

        db.Workers.Remove(worker);
        await db.SaveChangesAsync();
        return (true, null);
    }

    public async Task<(bool, List<string>)> Add(WorkerDTO workerRequest)
    {
        var errors = Validate(workerRequest);

        if (errors.Count > 0)
            return (false, errors);

        var result = new WorkerEntity()
        {
            FirstName = workerRequest.FirstName,
            LastName = workerRequest.LastName,
            MiddleName = workerRequest.MiddleName,
            Sex = workerRequest.Sex,
            DateBithday = (DateTime)workerRequest.DateBithday,
            Post = workerRequest.Post,
            DriversLicense = workerRequest.DriversLicense,
        };

        db.Workers.Add(result);
        await db.SaveChangesAsync();

        return (true, errors);
    }

    public async Task<(bool, List<string>)> Update(WorkerDTO workerRequest)
    {
        var errors = Validate(workerRequest);

        if (errors.Count > 0)
            return (false, errors);

        var worker = await db.Workers.FirstOrDefaultAsync(x => x.Id == workerRequest.ID);
        if (worker is null)
        {
            errors.Add("не пришла модель из БД по ID");
            return (false, errors);
        }

        worker.FirstName = workerRequest.FirstName;
        worker.LastName = workerRequest.LastName;
        worker.MiddleName = workerRequest.MiddleName;
        worker.Sex = workerRequest.Sex;
        worker.DateBithday = workerRequest.DateBithday;
        worker.Post = workerRequest.Post;
        worker.DriversLicense = workerRequest.DriversLicense;

        db.Workers.Update(worker);
        await db.SaveChangesAsync();

        return (true, errors);
    }

    private List<string> Validate(WorkerDTO entity)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(entity.FirstName))
            errors.Add("Имя пустое");

        if (string.IsNullOrWhiteSpace(entity.LastName))
            errors.Add("Фамилия пустая");

        if (entity.DateBithday == default)
            errors.Add("Дефолтная дата");

        var trueAge = 5840;
        var age = DateTime.Now.Subtract(entity.DateBithday);
        if (age.Days < trueAge)
            errors.Add("Нет 16 лет");

        return errors;
    }
}