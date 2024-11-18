using AutoMapper;
using Core.Models;
using Core.ViewModels.CourseViewModel;

namespace Api.RequestHelper;

public class CourseProfile :Profile
{
    public CourseProfile()
    {
            CreateMap<Course,CreateCourseViewModel>().ReverseMap();

    }
}
