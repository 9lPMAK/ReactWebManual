using DataModels.Entites;
using DataModels.Models;
using ReactWebManual.Server.Servises;

namespace ReactWebManual.Server.Interface;

public interface IDivisionService
{
    public List<DivisionEntity> GetAll();
    public (bool IsSuccess, string Error) Remove(int id);
    public (bool IsSuccess, List<string> ErrorsList) Add(DivisionDTO divisionRequest);
    public (bool IsSuccess, List<string> ErrorsList) Update(DivisionDTO divisionRequest);
}