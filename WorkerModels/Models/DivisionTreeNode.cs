namespace DataModels.Models;

/// <summary>
/// Узел дерева подразделений
/// </summary>
/// <param name="Id">Идентификатор подразделения</param>
/// <param name="ParentId">Идентификатор родительского подразделения</param>
/// <param name="Name">Название подразделения</param>
/// <param name="Children">Дочерние подразделения</param>
public record DivisionTreeNode(int Id, int ParentId, string Name, List<DivisionTreeNode> Children);


