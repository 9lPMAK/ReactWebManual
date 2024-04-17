using DataModels.Entites;
using DataModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using WorkerStore.DataAccess;

namespace ReactWebManual.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DivisionController : ControllerBase
{
    private readonly WorkerStoreDbContext _db;
    public DivisionController(WorkerStoreDbContext db)
    {
        _db = db;
    }
    [HttpGet]
    public async Task<ActionResult<List<DivisionEntity>>> Get()
    {
        return _db.Divisions.ToList();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        DivisionEntity division = _db.Divisions.FirstOrDefault(d => d.Id == id);
        if (division == null)
        {
            return NotFound();
        }
        _db.Divisions.Remove(division);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<DivisionEntity>> Post(DivisionDTO divisionRequest)
    {
        if (divisionRequest is null)
            return BadRequest();

        var errors = Validate(divisionRequest);

        if (errors.Count > 0)
            return BadRequest(errors);

        var result = new DivisionEntity()
        {
            Id = 0,
            ParentID = divisionRequest.ParentID,
            Name = divisionRequest.Name,
            Description = divisionRequest.Description,
        };

        _db.Divisions.Add(result);
        var newDivision = await _db.SaveChangesAsync();
        return Ok(newDivision);
    }

    [HttpPut]
    public async Task<ActionResult<DivisionEntity>> Put(DivisionDTO divisionRequest)
    {
        if (divisionRequest is null)
            return BadRequest();

        var errors = Validate(divisionRequest);

        if (errors.Count > 0)
            return BadRequest(errors);

        var division = _db.Divisions.FirstOrDefault(x => x.Id == divisionRequest.ID);
        if (division is null)
            return NotFound();

        division.Name = divisionRequest.Name;
        division.ParentID = divisionRequest.ParentID;
        division.Description = divisionRequest.Description;

        _db.Divisions.Update(division);

        var newDivision = await _db.SaveChangesAsync();
        return Ok(newDivision);
    }

    public List<string> Validate(DivisionDTO entity)
    {
        var errors = new List<string>();

        var isValidName = _db.Divisions.FirstOrDefault(x => x.Name == entity.Name && x.Id != entity.ID);
        if (isValidName is not null)
            errors.Add('Наименование не уникально');

        return errors;
    }

    public record DivisionDTO(int? ID, int ParentID, DateTime? CreateDate, string Name, string Description);
}
