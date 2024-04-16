using DataModels.Entites;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<IEnumerable<DivisionEntity>>> Get()
    {
        return _db.Divisions;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DivisionEntity>> Delete(int id)
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

    [HttpPut]
    public async Task<ActionResult<DivisionEntity>> Put(DivisionEntity division)
    {
        if (division == null)
            return BadRequest();

        //if (!_db.DivisionEntity.Any(x => x.Id == division.Id))
        //{
        //    return NotFound();
        //}
        var div = _db.Divisions.FirstOrDefault(d => d.Id == division.Id);

        if (div is null)
            return BadRequest();

        div.Description = division.Description;

        _db.Divisions.Update(div);
        await _db.SaveChangesAsync();
        return Ok(division);
    }

    [HttpPatch("{id}")]

    public async Task<ActionResult<DivisionEntity>> Patch()
    {
        return Ok();
    }
}
