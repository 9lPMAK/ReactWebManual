using DataModels.Entites;
using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using ReactWebManual.Server.Interface;
using WorkerStore.DataAccess;

namespace ReactWebManual.Server.Servises;

public class DivisionService(WorkerStoreDbContext db) : IDivisionService
{
    private const int RootDivisionId = 0;

    public Task<List<DivisionEntity>> GetAll() => db.Divisions.ToListAsync();

    public async Task<DivisionEntity?> GetDivision(int id) => await db.Divisions.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<(bool, string?)> Remove(int id)
    {
        if (id == RootDivisionId)
            return (false, "Невозможно удалить корневое подразделение");

        var division = await db.Divisions.FirstOrDefaultAsync(d => d.Id == id);
        if (division is null)
            return (false, "ID не найден");

        // пример без использования рекурсии
        var divisionChilds = await db.Divisions.Where(d => d.ParentID == id).ToListAsync();

        for (int i = 0; i < divisionChilds.Count; i++)
        {
            var divisionChild = divisionChilds[i];
            divisionChilds.AddRange(await db.Divisions.Where(d => d.ParentID == divisionChild.Id).ToListAsync());
            db.Divisions.Remove(divisionChild);
        }

        db.Divisions.Remove(division);
        await db.SaveChangesAsync();
        return (true, null);
    }


    public async Task<(bool, List<string>)> Add(DivisionDTO divisionRequest)
    {
        var errors = await Validate(divisionRequest);

        if (errors.Count > 0)
            return (false, errors);

        var result = new DivisionEntity()
        {
            ParentID = divisionRequest.ParentID,
            Name = divisionRequest.Name,
            Description = divisionRequest.Description,
        };

        db.Divisions.Add(result);
        await db.SaveChangesAsync();

        return (true, errors);
    }

    public async Task<(bool, List<string>)> Update(DivisionDTO divisionRequest)
    {
        var errors = await Validate(divisionRequest);

        if (errors.Count > 0)
            return (false, errors);

        var division = await db.Divisions.FirstOrDefaultAsync(x => x.Id == divisionRequest.ID);
        if (division is null)
        {
            errors.Add("не пришла модель из БД по ID");
            return (false, errors);
        }

        division.Name = divisionRequest.Name;
        division.ParentID = divisionRequest.ParentID;
        division.Description = divisionRequest.Description;

        db.Divisions.Update(division);
        await db.SaveChangesAsync();

        return (true, errors);
    }
    /// <summary>
    /// Валидация подразделения
    /// </summary>
    /// <param name="entity">модель подразделения</param>
    /// <returns></returns>
    private async Task<List<string>> Validate(DivisionDTO entity)
    {
        var errors = new List<string>();

        var isValidName = await db.Divisions.FirstOrDefaultAsync(x => x.Name == entity.Name && x.Id != entity.ID);
        if (isValidName is not null)
            errors.Add("Наименование не уникально");

        var isValidParentID = await db.Divisions.FirstOrDefaultAsync(x => x.Id == entity.ParentID);
        if (isValidParentID is null)
            errors.Add("Id совпадает с ParentID");

        return errors;
    }

    public async Task<DivisionTreeNode> GetTree()
    {
        var rootNode = await GetRootNode();
        var childs = await db.Divisions
            .Where(x => x.ParentID == Convert.ToInt32(rootNode.Id) && x.Id != RootDivisionId)
            .ToListAsync();

        // пример с использованием рекурсии
        await FillTreeNodeRecursion(childs, rootNode);
        return rootNode;
    }

    private async Task<DivisionTreeNode> GetRootNode()
    {
        var division = await db.Divisions.FirstAsync(x => x.Id == RootDivisionId);
        return new DivisionTreeNode(division.Id,
            division.ParentID,
            division.Name,
            []);
    }
    /// <summary>
    /// Возвращает рекурсию узлов заполнения дерева подразделений
    /// </summary>
    /// <param name="divisions">Модель подразделения</param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public async Task FillTreeNodeRecursion(List<DivisionEntity> divisions, DivisionTreeNode parent)
    {
        foreach (var division in divisions)
        {
            var node = new DivisionTreeNode(division.Id, division.ParentID, division.Name, []);
            parent.Children.Add(node);

            var childs = await db.Divisions.Where(x => x.ParentID == division.Id).ToListAsync();
            await FillTreeNodeRecursion(childs, node);
        }
    }
}