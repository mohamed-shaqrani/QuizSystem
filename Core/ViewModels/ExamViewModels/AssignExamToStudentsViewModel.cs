using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.ExamViewModels;
public class AssignExamToStudentsViewModel

{
    [Required]
    public List<int> StudentIds { get; set; }
    public int ExamId { get; set; }
    [Required]
    public DateTime DateTaken { get; set; }
    public int? Score { get; set; }
    public bool IsCompleted { get; set; }
    public string? InstructorUserName { get; set; }


}
