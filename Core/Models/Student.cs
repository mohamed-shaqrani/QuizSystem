namespace Core.Models;
public class Student :BaseModel
{
    public string FirstName {  get; set; }
    public string LastName { get; set; }

    public string Address { get; set; }
    public string Mobile { get; set; }
    public DateTime DateOfBirth { get; set; }
}
