using Azure.Core;
using Core.Models;
using Infrastructure.GenericRepository;

namespace Infrastructure.UnitOfWork;
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Request> Requests { get; }
    IGenericRepository<Instructor> Instructors { get; }
    IGenericRepository<Student> Students { get; }
   

    Task<int> Complete();

}

