﻿using Core.Models;
using Infrastructure.GenericRepository;

namespace Infrastructure.UnitOfWork;
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Instructor> Instructors { get; }
    IGenericRepository<Student> Students { get; }

    IGenericRepository<CourseStudent> CourseStudents { get; }
    IGenericRepository<CourseInstructor> CourseInstructors { get; }

    IGenericRepository<Course> Courses { get; }
    IGenericRepository<Exam> Exams { get; }
    IGenericRepository<Question> Questions { get; }
    IGenericRepository<Answer> Answers { get; }

    Task<int> Complete();

}

