using System.ComponentModel.DataAnnotations;

namespace DataModels.Entites;

/// <summary>
/// Модель подразделения
/// </summary>
public class DivisionEntity
{
    /// <summary>
    /// Идентификатор подразделения
    /// </summary>
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// Идентификатор родительского подразделения
    /// </summary>
    public int ParentID { get; set; }
    /// <summary>
    /// Название подразделения
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Дата создания подразделения
    /// </summary>
    public DateTime CreateDate { get; set; } = DateTime.Now;
    /// <summary>
    /// Описание подразделения 
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
}
