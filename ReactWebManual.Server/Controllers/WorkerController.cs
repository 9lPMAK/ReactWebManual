using DataModels.Entites;
using DataModels.Models;
using Microsoft.AspNetCore.Mvc;
using ReactWebManual.Server.Interface;

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
    /// <summary>
    /// Возвращает всех работников
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<WorkerEntity>>> GetAll()
        => Ok(await _workerService.GetAll());

    /// <summary>
    /// Возвращает работников по подразделению
    /// </summary>
    /// <param name="divisionId">Идентификатор подразделения</param>
    /// <returns></returns>
    [HttpGet("divisionId/{divisionId}")]

    public async Task<ActionResult<List<WorkerEntity>>> GetWorkers(int divisionId)
        => Ok(await _workerService.GetWorkers(divisionId));

    /// <summary>
    /// Возвращает работника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор работника</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<List<WorkerEntity>>> GetWorker(int id)
        => Ok(await _workerService.GetWorker(id));

    /// <summary>
    /// Удаляет работника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор работника</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(int id)
    {
        var resultDelete = await _workerService.Remove(id);
        if (!resultDelete.IsSuccess)
            return BadRequest(resultDelete);

        return Ok(resultDelete);
    }

    /// <summary>
    /// Добавляет нового работника 
    /// </summary>
    /// <param name="workerRequest">Модель работника</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<WorkerEntity>> Add(WorkerDTO workerRequest)
    {
        if (workerRequest is null)
            return BadRequest();

        var resultPost = await _workerService.Add(workerRequest);
        if (!resultPost.IsSuccess)
            return BadRequest(resultPost);

        return Ok(resultPost);
    }

    /// <summary>
    /// Редактирует существующего работника
    /// </summary>
    /// <param name="workerRequest">Модель работника</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult<DivisionEntity>> Update(WorkerDTO workerRequest)
    {
        if (workerRequest is null)
            return BadRequest();

        var resultPut = await _workerService.Update(workerRequest);
        if (!resultPut.IsSuccess)
            return BadRequest(resultPut);

        return Ok(resultPut);
    }
}
