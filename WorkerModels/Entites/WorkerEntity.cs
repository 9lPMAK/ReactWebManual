using System.ComponentModel.DataAnnotations;

namespace DataModels.Entites;

public class WorkerEntity
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateBithday { get; set; }
    public string Sex { get; set; }
    public string Post { get; set; }
    public bool DriversLicense { get; set; } 
}