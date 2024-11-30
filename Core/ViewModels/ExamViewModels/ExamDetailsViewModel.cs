namespace Core.ViewModels.ExamViewModels;

using Core.Enum;
using Core.Models;
using Core.ViewModels.ChoiceViewModels;
using Core.ViewModels.QuestionViewModels;
public class ExamDetailsViewModel
{
    public string Title { get; set; }
    public int? DurationInMinutes { get; set; }
    public DateTime? StartDateTime { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }
    public int MaxScore { get; set; }

    public ExamType ExamType { get; set; }
    public int NumberOfQuestions { get; set; }
    public bool IsManualAssignment { get; set; }
    public int CourseId { get; set; }
    public List<QuestionDetailsViewModel> QuestionViewModels { get; set; }

}
public static class ExamExtensions
{
    public static IEnumerable<ExamDetailsViewModel> ToDetailsViewModel(this IQueryable<Exam> exams)
    {

        return exams.Select(exam => new ExamDetailsViewModel
        {
            IsActive = exam.IsActive,
            Description = exam.Description,
            CourseId = exam.CourseId,
            ExamType = exam.ExamType,
            NumberOfQuestions = exam.NumberOfQuestions,
            MaxScore = exam.MaxScore,
            StartDateTime = exam.StartDateTime,
            Title = exam.Title,
            DurationInMinutes = exam.DurationInMinutes,
            QuestionViewModels = exam.ExamQuestions.Select(eq => new QuestionDetailsViewModel
            {
                Id = eq.Question.Id,
                Marks = eq.Marks,
                QuestionOrder = eq.QuestionOrder,
                ChoicesViewModel = eq.Question.Choices.Select(choice => new ChoiceDetailsViewModel
                {
                    Id = choice.Id,
                    Text = choice.Text
                }).ToList()
            }).ToList()
        });


    }
}
