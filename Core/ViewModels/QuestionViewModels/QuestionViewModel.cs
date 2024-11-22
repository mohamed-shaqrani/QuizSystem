using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.QuestionViewModels;
public class QuestionViewModel
{
    [Required]

    public int QuestionId { get; set; }
    [Required]
    public int QuestionOrder { get; set; }



}
