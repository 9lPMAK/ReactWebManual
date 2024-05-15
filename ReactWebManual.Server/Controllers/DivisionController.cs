using DataModels.Entites;
using DataModels.Models;
using Microsoft.AspNetCore.Mvc;
using ReactWebManual.Server.Interface;

namespace ReactWebManual.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DivisionController : ControllerBase
{
    private readonly IDivisionService _divisionService;
    public DivisionController(IDivisionService divisionservice)
    {
        _divisionService = divisionservice;
    }

    [HttpGet]
    public async Task<ActionResult<DivisionTreeNode>> GetTree()
        => Ok(await _divisionService.GetTree());

    [HttpGet("{id}")]
    public async Task<ActionResult<DivisionEntity>> GetDivision(int id)
        => Ok(await _divisionService.GetDivision(id));

    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(int id)
    {
        var resultDelete = await _divisionService.Remove(id);
        if (!resultDelete.IsSuccess)
            return BadRequest(resultDelete);

        return Ok(resultDelete);
    }

    [HttpPost]
    public async Task<ActionResult<DivisionEntity>> Add(DivisionDTO divisionRequest)
    {
        if (divisionRequest is null)
            return BadRequest();

        var resultPost = await _divisionService.Add(divisionRequest);
        if (!resultPost.IsSuccess)
            return BadRequest(resultPost);

        return Ok(resultPost);
    }

    [HttpPut]
    public async Task<ActionResult<DivisionEntity>> Update(DivisionDTO divisionRequest)
    {
        if (divisionRequest is null)
            return BadRequest();

        var resultPut = await _divisionService.Update(divisionRequest);
        if (!resultPut.IsSuccess)
            return BadRequest(resultPut);

        return Ok(resultPut);
    }
}
