using Core.Constants;
using Core.Enum;
using Core.Models;
using Core.ViewModels;
using Core.ViewModels.CourseViewModel;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Services.CourseService;
public class CourseService<Entity> : ICourseServicee<Entity> where Entity : class
{
    private readonly IUnitOfWork _unitOfWork;

    public CourseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseViewModel<int>> EnrollStudentInCourse(int courseId, int studentId, string userName)
    {
        var response = new ResponseViewModel<int>();
        if (!await DoesCourseExist(courseId))
        {
            response.IsSuccess = false;
            response.ErrorCode = ErrorCode.CourseNotFound;
            response.Message = "course was not found";
            return response;
        }
        if (await IsEnrolledInCourse(courseId, studentId))
        {
            response.IsSuccess = false;
            response.ErrorCode = ErrorCode.CourseNotFound;
            response.Message = "Student Enrolled in this course before";
            return response;
        }
        var studentCoures = new CourseStudent
        {
            CourseId = courseId,
            StudentId = studentId,
            CreatedBy = userName,
        };

        await _unitOfWork.CourseStudents.AddAsync(studentCoures);
        await _unitOfWork.Complete();
        return response;
    }
    private async Task<bool> DoesCourseExist(int courseId)
    {
        return await _unitOfWork.Courses.AnyAsync(x => x.Id == courseId);
    }
    private async Task<bool> IsEnrolledInCourse(int courseId, int studentId)
    {
        return await _unitOfWork.CourseStudents.AnyAsync(x => x.CourseId == courseId
                                                            && x.StudentId == studentId);


    }
    public async Task<ResponseViewModel<int>> CreateNewCourse(CreateCourseViewModel model)
    {
        var response = new ResponseViewModel<int>();
        if (await DoesCourseNameExist(model.Name))
        {
            response.ErrorCode = ErrorCode.CourseAlreadyExist;
            response.Message = "Course Already Exist";
            response.IsSuccess = false;
            return response;
        }
        return await CourseCreation(model, response);
    }

    private async Task<bool> DoesCourseNameExist(string courseName)
    {
        return await _unitOfWork.Courses.AnyAsync(course => course.Name == courseName);

    }
    private async Task<ResponseViewModel<int>> CourseCreation(CreateCourseViewModel model, ResponseViewModel<int> response)
    {
        var course = new Course
        {
            Name = model.Name,
            Description = model.Description,
            CreatedBy = model.UserName,
            CreatedDate = DateTime.UtcNow,

        };
        await _unitOfWork.Courses.AddAsync(course);
        var result = await _unitOfWork.Complete() > 0;
        if (result)
        {
            response.ErrorCode = 0;
            response.IsSuccess = true;
            return response;
        }
        else
        {
            response.ErrorCode = ErrorCode.DatabaseSaveError;
            response.IsSuccess &= false;
            response.Message = "Something went wrong";
            return response;
        }
    }
    public async Task<ResponseViewModel<int>> UpdateCourse(UpdateCourseViewModel model)
    {
        var response = new ResponseViewModel<int>();
        var course = new Course
        {
            UpdatedBy = model.userName,
            Description = model.Description,
            Name = model.Name,
            Id = model.Id
        };
        _unitOfWork.Courses.SaveInclude(course, nameof(Course.UpdatedBy), nameof(Course.Name), nameof(Course.Description));
        var result = await _unitOfWork.Complete() > 0;
        if (result)
        {
            response.IsSuccess = true;
            return response;
        }
        else
        {
            return DataBaseError.DataBaseErrorResponse(response);
        }
    }



    public async Task<ResponseViewModel<int>> DeleteCourse(int courseId)
    {
        var response = new ResponseViewModel<int>();
        if (await DoesCourseHasEnrolledStudents(courseId))
        {
            response.ErrorCode = ErrorCode.StudentsAlreadyEnrolledInCourse;
            response.IsSuccess = false;
            response.Message = "Students are already enrolled in this course, so it cannot be deleted.";
            return response;
        }
        var course = await _unitOfWork.Courses.GetById(courseId);
        _unitOfWork.Courses.SoftDelete(course);
        var result = await _unitOfWork.Complete() > 0;

        if (result)
        {
            response.IsSuccess = true;
            return response;
        }
        else
        {
            return DataBaseError.DataBaseErrorResponse(response);
        }

    }
    private async Task<bool> DoesCourseHasEnrolledStudents(int courseId)
    {
        return await _unitOfWork.CourseStudents.AnyAsync(x => x.CourseId == courseId && !x.isDeleted);

    }
}
