using DataModels.Entites;
using DataModels.Models;

namespace ReactWebManual.Server.Interface;

public interface IWorkerService
{
    public List<WorkerEntity> GetAll();
    public (bool IsSuccess, string Error) Remove(int id);
    public (bool IsSuccess, List<string> ErrorsList) Add(WorkerDTO workerRequest);
    public (bool IsSuccess, List<string> ErrorsList) Update(WorkerDTO workerRequest);
}
