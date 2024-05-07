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

    public async Task<(bool, string?)> Remove(int id)
    {
        var division = await db.Divisions.FirstOrDefaultAsync(d => d.Id == id);
        if (division is null)
            return (false, "ID не найден");

        var divisionChildCollec = await db.Divisions.Where(d => d.ParentID == id).ToListAsync();
        if (divisionChildCollec.Count == 0)
            return (false, "Дочерние элементы не найдены");

        for (int i = 0; i < divisionChildCollec.Count; i++)
        {
            var divisionChild = divisionChildCollec[i];
            divisionChildCollec.AddRange(await db.Divisions.Where(d => d.ParentID == divisionChild.Id).ToListAsync());
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

        await Recursion(childs, rootNode);
        return rootNode;
    }

    private async Task<DivisionTreeNode> GetRootNode()
    {
        var division = await db.Divisions.FirstAsync(x => x.ParentID == RootDivisionId);
        return new DivisionTreeNode(division.Id.ToString(),
            division.ParentID.ToString(),
            division.Name,
            []);
    }

    public async Task Recursion(List<DivisionEntity> divisions, DivisionTreeNode parent)
    {
        foreach (var division in divisions)
        {
            var node = new DivisionTreeNode(division.Id.ToString(), division.ParentID.ToString(), division.Name, []);
            parent.Children.Add(node);

            var childs = await db.Divisions.Where(x => x.ParentID == division.Id).ToListAsync();
            await Recursion(childs, node);
        }
    }
}