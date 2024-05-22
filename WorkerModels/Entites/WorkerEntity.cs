using System.ComponentModel.DataAnnotations;
using DataModels.Enums;

namespace DataModels.Entites;
/// <summary>
/// Модель работника
/// </summary>
public class WorkerEntity
{
    /// <summary>
    /// Идентификатор работника
    /// </summary>
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// Имя работника
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия работника
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Отчество работника
    /// </summary>
    public string MiddleName { get; set; }

    /// <summary>
    /// День рождения работника
    /// </summary>
    public DateTime DateBithday { get; set; }

    /// <summary>
    /// Пол работника
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Должность работника
    /// </summary>
    public string Post { get; set; }

    /// <summary>
    /// Наличие водительского удостоверения работника
    /// </summary>
    public bool IsDriversLicense { get; set; } 

    /// <summary>
    /// Идентификатор подразделения к которому работник относится 
    /// </summary>
    public int DivisionId { get; set; }
}
