namespace DataModels.Models;

/// <summary>
/// Транспортировочная модель работника
/// </summary>
/// <param name="ID">Идентификатор работника</param>
/// <param name="FirstName">Имя работника</param>
/// <param name="LastName">Фамилия работника</param>
/// <param name="MiddleName">Отчество работника</param>
/// <param name="DateBithday">Дата рождения работника</param>
/// <param name="Gender">Пол работника</param>
/// <param name="Post">Должность работника</param>
/// <param name="IsDriversLicense">Наличие водительского удостоверения работника</param>
/// <param name="DivisionId">Идетификатор подразделения к которому относится работник</param>
public record WorkerDTO(int? ID, string FirstName, string LastName, string MiddleName, DateTime DateBithday, int Gender, string Post, bool IsDriversLicense, int DivisionId);
