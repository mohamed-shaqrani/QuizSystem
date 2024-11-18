using AutoMapper;
using Core.Models;
using Core.ViewModels.CourseViewModel;

namespace Api.RequestHelper;

public class CourseStudentProfile : Profile
{
    public CourseStudentProfile()
    {
        CreateMap<Course, CreateCourseViewModel>().ReverseMap();

    }
}
