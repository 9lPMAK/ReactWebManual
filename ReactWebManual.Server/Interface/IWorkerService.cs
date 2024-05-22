using DataModels.Entites;
using DataModels.Models;

namespace ReactWebManual.Server.Interface;

public interface IWorkerService
{
    /// <summary>
    /// Возвращает всех работников
    /// </summary>
    /// <returns></returns>
    Task<List<WorkerEntity>> GetAll();

    /// <summary>
    /// Возвращает работников по подразделению
    /// </summary>
    /// <param name="divisionId">Идентификатор подразделения</param>
    /// <returns></returns>
    Task<List<WorkerEntity>> GetWorkers(int divisionId);

    /// <summary>
    /// Возвращает работника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор работника</param>
    /// <returns></returns>
    Task<WorkerEntity?> GetWorker(int id);

    /// <summary>
    /// Удаляет работника по идентификатору 
    /// </summary>
    /// <param name="id">Идентификатор работника</param>
    /// <returns></returns>
    Task<(bool IsSuccess, string? Error)> Remove(int id);

    /// <summary>
    /// Добавляет нового работника
    /// </summary>
    /// <param name="workerRequest">Транспортировочная модель работника</param>
    /// <returns></returns>
    Task<(bool IsSuccess, List<string> ErrorsList)> Add(WorkerDTO workerRequest);

    /// <summary>
    /// Редактирует существующего работника
    /// </summary>
    /// <param name="workerRequest">Транспортировочная модель работника</param>
    /// <returns></returns>
    Task<(bool IsSuccess, List<string> ErrorsList)> Update(WorkerDTO workerRequest);
}
