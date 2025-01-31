﻿using Core.Enum;
using Core.ViewModels.QuestionViewModels.ChoiceViewMode;

namespace Core.ViewModels.QuestionViewModels;
public class CreateQuestionViewModel
{
    public string Text { get; set; }
    public DifficultyLevel Level { get; set; }

    public int Grade { get; set; }
    public int InstructorId { get; set; }
    public int CourseId { get; set; }
    public string UserName { get; set; }
    public List<CreateChoicesViewModel> createChoicesViewModels { get; set; }

}
