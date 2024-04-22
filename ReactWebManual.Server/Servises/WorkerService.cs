using DataModels.Entites;
using DataModels.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using ReactWebManual.Server.Interface;
using WorkerStore.DataAccess;

namespace ReactWebManual.Server.Servises;

public class WorkerService : IWorkerService
{
    private readonly WorkerStoreDbContext _db;

	public WorkerService(WorkerStoreDbContext db)
	{
        _db = db;
	}

	public List<WorkerEntity> GetAll()
		=> _db.Workers.ToList();

	public (bool, string) Remove(int id)
	{
        var worker = _db.Workers.FirstOrDefault(d => d.Id == id);
        if (worker is null)
            return (false, "ID не найден");

        _db.Workers.Remove(worker);
        _db.SaveChanges();
        return (true, null);
    }

    public (bool, List<string>) Add(WorkerDTO workerRequest)
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

        _db.Workers.Add(result);
        _db.SaveChanges();

        return (true, errors);
    }

    public (bool, List<string>) Update(WorkerDTO workerRequest)
    {

        var errors = Validate(workerRequest);

        if (errors.Count > 0)
            return (false, errors);

        var worker = _db.Workers.FirstOrDefault(x => x.Id == workerRequest.ID);
        if (worker is null)
        {
            errors.Add("не пришла модель из БД по ID");
            return (false, errors);
        }

        worker.FirstName = workerRequest.FirstName;
        worker.LastName = workerRequest.LastName;
        worker.MiddleName = workerRequest.MiddleName;
        worker.Sex = workerRequest.Sex;
        worker.DateBithday = (DateTime)workerRequest.DateBithday;
        worker.Post = workerRequest.Post;
        worker.DriversLicense = workerRequest.DriversLicense;

        _db.Workers.Update(worker);
        _db.SaveChanges();

        return (true, errors);
    }

    private List<string> Validate(WorkerDTO entity)
    {
        var errors = new List<string>();
        var trueAge = 5840;

        if (string.IsNullOrWhiteSpace(entity.FirstName))
            errors.Add("Имя пустое");

        if (string.IsNullOrWhiteSpace(entity.LastName))
            errors.Add("Фамилия пустая");

        if (entity.DateBithday == default)
            errors.Add("Дефолтная дата");

        var age = DateTime.Now.Subtract(entity.DateBithday);
        if (age.Days < trueAge)
            errors.Add("Нет 16 лет");

        return errors;
    }
}