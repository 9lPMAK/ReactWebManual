using DataModels.Entites;
using DataModels.Models;

namespace ReactWebManual.Server.Interface;

public interface IWorkerService
{
    public Task<List<WorkerEntity>> GetAll();
    public Task<(bool IsSuccess, string? Error)> Remove(int id);
    public Task<(bool IsSuccess, List<string> ErrorsList)> Add(WorkerDTO workerRequest);
    public Task<(bool IsSuccess, List<string> ErrorsList)> Update(WorkerDTO workerRequest);
}
