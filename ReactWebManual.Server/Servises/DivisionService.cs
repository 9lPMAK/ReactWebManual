using DataModels.Entites;
using DataModels.Models;
using ReactWebManual.Server.Interface;
using WorkerStore.DataAccess;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ReactWebManual.Server.Servises;

public class DivisionService : IDivisionService
{
    private readonly WorkerStoreDbContext _db;

    public DivisionService(WorkerStoreDbContext db)
    {
        _db = db;
    }

    public List<DivisionEntity> GetAll()
     => _db.Divisions.ToList();

    public (bool, string) Remove(int id)
    {
        var division = _db.Divisions.FirstOrDefault(d => d.Id == id);
        if (division is null)
            return (false, "ID не найден");

        List<DivisionEntity> divisionChildCollec = _db.Divisions.Where(d => d.ParentID == id).ToList();
        if (!divisionChildCollec.Any())
            return (false, "Дочерние элементы не найдены");


        for (int i = 0; i < divisionChildCollec.Count; i++)
        {
            DivisionEntity divisionChild = divisionChildCollec[i];
            divisionChildCollec.AddRange(_db.Divisions.Where(d => d.ParentID == divisionChild.Id).ToList());
            _db.Divisions.Remove(divisionChild);
        }

        _db.Divisions.Remove(division);
        _db.SaveChanges();
        return (true, null);
    }

    public (bool, List<string>) Add(DivisionDTO divisionRequest)
    {
        var errors = Validate(divisionRequest);

        if (errors.Count > 0)
            return (false, errors);

        var result = new DivisionEntity()
        {
            ParentID = divisionRequest.ParentID,
            Name = divisionRequest.Name,
            Description = divisionRequest.Description,
        };

        _db.Divisions.Add(result);
        _db.SaveChanges();

        return (true, errors);
    }

    public (bool, List<string>) Update(DivisionDTO divisionRequest)
    {
        var errors = Validate(divisionRequest);

        if (errors.Count > 0)
            return (false, errors);

        var division = _db.Divisions.FirstOrDefault(x => x.Id == divisionRequest.ID);
        if (division is null)
        {
            errors.Add("не пришла модель из БД по ID");
            return (false, errors);
        }

        division.Name = divisionRequest.Name;
        division.ParentID = divisionRequest.ParentID;
        division.Description = divisionRequest.Description;

        _db.Divisions.Update(division);
        _db.SaveChanges();

        return (true, errors);
    }

    private List<string> Validate(DivisionDTO entity)
    {
        var errors = new List<string>();

        var isValidName = _db.Divisions.FirstOrDefault(x => x.Name == entity.Name && x.Id != entity.ID);
        if (isValidName is not null)
            errors.Add("Наименование не уникально");

        var isValidParentID = _db.Divisions.FirstOrDefault(x => x.Id == entity.ParentID);
        if (isValidParentID is null)
            errors.Add("Id совпадает с ParentID");

        return errors;
    }
}