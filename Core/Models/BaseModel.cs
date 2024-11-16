namespace Core.Models;
public class BaseModel
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; }
    public string CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
}
