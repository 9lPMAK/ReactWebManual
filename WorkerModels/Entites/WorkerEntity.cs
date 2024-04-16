namespace DataModels.Entites;

public class WorkerEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PatronymicName { get; set; }
    public string DR { get; set; }
    public string Sex { get; set; }
    public string Post { get; set; }
    public bool DriversLicense { get; set; }
}

