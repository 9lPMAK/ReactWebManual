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

    /// <summary>
    /// Возвращает дерево всех подразделений
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<DivisionTreeNode>> GetTree()
        => Ok(await _divisionService.GetTree());


    /// <summary>
    /// Возвращает подразделение по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор подразделения</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<DivisionEntity>> GetDivision(int id)
        => Ok(await _divisionService.GetDivision(id));

    /// <summary>
    /// Удаляет подразделение и его детей
    /// </summary>
    /// <param name="id">Идентификатор подразделения</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Remove(int id)
    {
        var resultDelete = await _divisionService.Remove(id);
        if (!resultDelete.IsSuccess)
            return BadRequest(resultDelete);

        return Ok(resultDelete);
    }
    /// <summary>
    /// Добавляет новое подразделение
    /// </summary>
    /// <param name="divisionRequest">Транспортировочная модель подразделения</param>
    /// <returns></returns>
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

    /// <summary>
    /// Редактирует существующее подразделение
    /// </summary>
    /// <param name="divisionRequest">Транспортировочная модель подразделения</param>
    /// <returns></returns>
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
