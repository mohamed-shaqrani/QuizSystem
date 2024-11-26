using Core.Models;
using Infrastructure.GenericRepository;
using Infrastructure.Services;
using Infrastructure.Services.CourseService;
using Infrastructure.Services.QuestionService;

namespace Infrastructure.UnitOfWork;
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Instructor> Instructors { get; }
    IGenericRepository<Student> Students { get; }

    IGenericRepository<CourseStudent> CourseStudents { get; }
    IGenericRepository<CourseInstructor> CourseInstructors { get; }
    IExamService<Exam> ExamService { get; }
    ICourseServicee<Course> CourseService { get; }
    IQuestionService<Question> QuestionService { get; }

    IGenericRepository<Course> Courses { get; }
    IGenericRepository<Exam> Exams { get; }
    IGenericRepository<Question> Questions { get; }
    IGenericRepository<Choice> Choices  { get; }

    IGenericRepository<Answer> Answers { get; }

    Task<int> Complete();

}

