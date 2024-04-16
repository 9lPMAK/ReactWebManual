using System.ComponentModel.DataAnnotations;

namespace DataModels.Entites;

public class DivisionEntity
{
    [Key]
    public int Id { get; set; }

    public int ParentID { get; set; }

    public string Name { get; set; }

    public DateTime CreateDate { get; set; }

    public string? Description { get; set; }
}
