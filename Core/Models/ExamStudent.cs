using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models;
public class ExamStudent :BaseModel
{
    public int ExamId { get; set; } 
    public Exam Exam { get; set; }

    public int StudentId { get; set; }  
    public Student Student { get; set; }

    public DateTime DateTaken { get; set; }  
    public int? Score { get; set; } 
    public bool IsCompleted { get; set; }  
}
