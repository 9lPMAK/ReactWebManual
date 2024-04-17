using DataModels.Entites;
using Microsoft.AspNetCore.Components.Web;
using WorkerStore.DataAccess;
using static ReactWebManual.Server.Controllers.DivisionController;

namespace ReactWebManual.Server.Servises;

public class DivisionService
{
    private readonly WorkerStoreDbContext _db;

    public DivisionService(WorkerStoreDbContext db)
    {
        _db = db;
    }

    public List<DivisionEntity> GetAll()
     => _db.Divisions.ToList();

    public bool Delete(int id)
    {
        var division = _db.Divisions.FirstOrDefault(d => d.Id == id);
        if (division is null)
            return false;

        _db.Divisions.Remove(division);
        _db.SaveChanges();
        return true;
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
        var isSucces = _db.SaveChanges();

      return (true, errors); 
    }

    public (bool, List<string>) Edit(DivisionDTO divisionRequest) 
    {
        List<string> errors = new();
        if (divisionRequest is null)
        {
            errors.Add("Модель не передана");
            return (false, errors);
        }

        errors.AddRange(Validate(divisionRequest));

        if (errors.Count > 0)
            return (false,errors);

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

        if (errors.Count > 0)
            return (false, errors);

        return (true, errors); ;
    }

    private List<string> Validate(DivisionDTO entity)
    {
        var errors = new List<string>();

        var isValidName = _db.Divisions.FirstOrDefault(x => x.Name == entity.Name && x.Id != entity.ID);
        if (isValidName is not null)
            errors.Add('Наименование не уникально');

        return errors;
    }

    public record DivisionDTO(int? ID, int ParentID, DateTime? CreateDate, string Name, string Description);
}
