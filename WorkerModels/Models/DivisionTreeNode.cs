namespace DataModels.Models;

public record DivisionTreeNode(string? Id, string ParentId, string Name, List<DivisionTreeNode> Children);


