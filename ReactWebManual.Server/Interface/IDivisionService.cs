using DataModels.Entites;
using DataModels.Models;

namespace ReactWebManual.Server.Interface;

public interface IDivisionService
{
    /// <summary>
    /// Возвращает список всех подразделений
    /// </summary>
    public Task<List<DivisionEntity>> GetAll();

    /// <summary>
    /// Возвращает подразделение по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор подразделения</param>
    public Task<DivisionEntity?> GetDivision(int id);

    /// <summary>
    /// Возвращает дерево всех подразделений
    /// </summary>
    /// <returns></returns>
    Task<DivisionTreeNode> GetTree();

    /// <summary>
    /// Удаляет подразделение и его детей
    /// </summary>
    /// <param name="id">Идентификатор подразделения</param>
    /// <returns></returns>
    public Task<(bool IsSuccess, string? Error)> Remove(int id);

    /// <summary>
    /// Добавляет новое подразделение
    /// </summary>
    /// <param name="divisionRequest">Транспортировочная модель подразделения</param>
    /// <returns></returns>
    public Task<(bool IsSuccess, List<string> ErrorsList)> Add(DivisionDTO divisionRequest);

    /// <summary>
    /// Редактирует существующее подразделение
    /// </summary>
    /// <param name="divisionRequest">Транспортировочная модель подразделения</param>
    /// <returns></returns>
    public Task<(bool IsSuccess, List<string> ErrorsList)> Update(DivisionDTO divisionRequest);
}