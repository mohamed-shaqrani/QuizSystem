using Core.ViewModels;
using Core.ViewModels.CourseViewModel;

namespace Infrastructure.Services.CourseService;
public interface ICourseServicee<Entity> where Entity : class
{
    Task<ResponseViewModel<int>> EnrollStudentInCourse(int courseId, int studentId, string userName);
    Task<ResponseViewModel<int>> CreateNewCourse(CreateCourseViewModel model);

    Task<ResponseViewModel<int>> UpdateCourse(UpdateCourseViewModel model);
    Task<ResponseViewModel<int>> DeleteCourse(int courseId);


}
