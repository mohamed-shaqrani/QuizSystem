namespace Core.Models;
public class BaseModel
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; }
    public int CreatedBy { get; set; } = 1;
    public int? UpdatedBy { get; set; }
}
