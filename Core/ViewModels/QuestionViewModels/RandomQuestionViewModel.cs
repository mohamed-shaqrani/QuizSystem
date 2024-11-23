using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.QuestionViewModels;
public class RandomQuestionViewModel
{
    [Required]
    public int QuestionId { get; set; }
    [Required]
    public int QuestionOrder { get; set; }
    public DifficultyLevel?  DifficultyLevel { get; set; }
    [Required]

    public int Marks { get; set; }
    [Required]
    public List<ChoicesViewModel> ChoicesViewModel { get; set; } = new List<ChoicesViewModel>();
}
