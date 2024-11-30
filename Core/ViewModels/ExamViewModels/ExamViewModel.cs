namespace Core.ViewModels.ExamViewModels;
public class ExamViewModel
{
    public int ExamId { get; set; }
    public string Title { get; set; }
    public DateTime? StartDateTime { get; set; }
    public DateTime? EnrollmentEndDate { get; set; }

    public int? DurationInMinutes { get; set; }
    public string Description { get; set; }
    public int MaxScore { get; set; }
    public bool IsEnrolled { get; set; }
}
