using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.ExamViewModels;
public class CreateRandomExam
{
    [Required]
    public int CourseId { get; set; }
   
    public int? InstructorId { get; set; }
    public string? InstructorUserName { get; set; }
    public int? QustionCount { get; set; } = 9;

}
