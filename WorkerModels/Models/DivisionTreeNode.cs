namespace DataModels.Models;

public record DivisionTreeNode(int Id, int ParentId, string Name, List<DivisionTreeNode> Children);


