namespace DataModels.Models;

public record WorkerDTO(int? ID, string FirstName, string LastName, string MiddleName, DateTime DateBithday, string Sex, string Post, bool DriversLicense);
