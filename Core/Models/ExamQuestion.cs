namespace Core.Models;
public class ExamQuestion :BaseModel
{
    public int ExamId { get; set; }       
    public Exam Exam { get; set; }         

    public int QuestionId { get; set; }     
    public Question Question { get; set; }   

    public int QuestionOrder { get; set; }  
    public int Marks { get; set; }   
}
