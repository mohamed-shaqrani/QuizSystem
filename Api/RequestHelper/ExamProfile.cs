using AutoMapper;
using Core.Models;
using Core.ViewModels.ChoiceViewModels;
using Core.ViewModels.ExamViewModels;
using Core.ViewModels.QuestionViewModels;

namespace Api.RequestHelper;

public class ExamProfile : Profile
{
    public ExamProfile()
    {
        CreateMap<ExamDetailsViewModel, Exam>().ReverseMap()
             .ForMember(des => des.QuestionViewModels, opt => opt.MapFrom(s => s.ExamQuestions.Select(x => x.Question)));


        CreateMap<QuestionDetailsViewModel, Question>().ReverseMap();

        CreateMap<ChoiceDetailsViewModel, Choice>().ReverseMap();
        CreateMap<QuestionDetailsViewModel, Question>().ReverseMap()
           .ForMember(des => des.ChoicesViewModel,
                      opt => opt.MapFrom(s => s.Choices));

        // Map Choice to ChoiceDetailsViewModel
        CreateMap<ChoiceDetailsViewModel, Choice>().ReverseMap();
    }
}
