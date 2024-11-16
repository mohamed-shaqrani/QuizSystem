using Azure.Core;
using Core.Models;
using Infrastructure.Data;
using Infrastructure.GenericRepository;

namespace Infrastructure.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IGenericRepository<Request> Requests { get; private set; }
    public IGenericRepository<Instructor> Instructors { get; private set; }
    public IGenericRepository<Student> Students { get; private set; }

    public IGenericRepository<Course> Courses { get; private set; }

    public IGenericRepository<Exam> Exams { get; private set; }

    public IGenericRepository<Question> Questions { get; private set; }

    public IGenericRepository<Answer> Answers { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Requests = new GenericRepository<Request>(_context);
        Instructors = new GenericRepository<Instructor>(_context);
        Students = new GenericRepository<Student>(_context);
        Exams = new GenericRepository<Exam>(_context);
        Questions = new GenericRepository<Question>(_context);  
        Answers = new GenericRepository<Answer>(_context);
        Courses = new GenericRepository<Course>(_context);

    }
    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
