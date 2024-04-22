using DataModels.Entites;
using DataModels.Models;
using Microsoft.AspNetCore.Mvc;
using ReactWebManual.Server.Interface;
using ReactWebManual.Server.Servises;

namespace ReactWebManual.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkerController : ControllerBase
{
    private readonly IWorkerService _workerService;

    public WorkerController(IWorkerService workerservice)
    {
        _workerService = workerservice;
    }

    [HttpGet]

    public async Task<ActionResult<List<WorkerEntity>>> GetAll()
        => Ok(_workerService.GetAll());

    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(int id)
    {
        var resultDelete = _workerService.Remove(id);
        if (!resultDelete.IsSuccess)
            return BadRequest(resultDelete);

        return Ok(resultDelete);
    }

    [HttpPost]
    public async Task<ActionResult<WorkerEntity>> Add(WorkerDTO workerRequest)
    {
        if (workerRequest is null)
            return BadRequest();

        var resultPost = _workerService.Add(workerRequest);
        if (!resultPost.IsSuccess)
            return BadRequest(resultPost);

        return Ok(resultPost);
    }

    [HttpPut]
    public async Task<ActionResult<DivisionEntity>> Update(WorkerDTO workerRequest)
    {
        if (workerRequest is null)
            return BadRequest();

        var resultPut = _workerService.Update(workerRequest);
        if (!resultPut.IsSuccess)
            return BadRequest(resultPut);

        return Ok(resultPut);
    }
}
