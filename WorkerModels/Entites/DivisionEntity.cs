using System.ComponentModel.DataAnnotations;

namespace DataModels.Entites;

public class DivisionEntity
{
    [Key]
    public int Id { get; set; }

    public int ParentID { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime CreateDate { get; set; } = DateTime.Now;

    public string Description { get; set; } = string.Empty;
    
}
