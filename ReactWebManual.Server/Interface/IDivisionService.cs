using DataModels.Entites;
using DataModels.Models;

namespace ReactWebManual.Server.Interface;

public interface IDivisionService
{
    public Task<List<DivisionEntity>> GetAll();
    public Task<DivisionEntity?> GetDivision(int id);
    Task<DivisionTreeNode> GetTree();
    public Task<(bool IsSuccess, string? Error)> Remove(int id);
    public Task<(bool IsSuccess, List<string> ErrorsList)> Add(DivisionDTO divisionRequest);
    public Task<(bool IsSuccess, List<string> ErrorsList)> Update(DivisionDTO divisionRequest);
}