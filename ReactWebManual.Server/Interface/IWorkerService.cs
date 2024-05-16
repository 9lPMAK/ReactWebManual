using DataModels.Entites;
using DataModels.Models;

namespace ReactWebManual.Server.Interface;

public interface IWorkerService
{
    Task<List<WorkerEntity>> GetAll();
    Task<List<WorkerEntity>> GetWorkers(int divisionId);
    Task<WorkerEntity?> GetWorker(int id);
    Task<(bool IsSuccess, string? Error)> Remove(int id);
    Task<(bool IsSuccess, List<string> ErrorsList)> Add(WorkerDTO workerRequest);
    Task<(bool IsSuccess, List<string> ErrorsList)> Update(WorkerDTO workerRequest);
}
