namespace DataModels.Models;

/// <summary>
/// Транспортировочная модель подразделения
/// </summary>
/// <param name="ID">Идентификатор подразделения</param>
/// <param name="ParentID">Идентификатор родительского подразделения</param>
/// <param name="CreateDate">Дата создания подразделения</param>
/// <param name="Name">Название подразделения</param>
/// <param name="Description">Описание подразделения</param>
public record DivisionDTO(int? ID, int ParentID, DateTime? CreateDate, string Name, string Description);


