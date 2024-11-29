using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
    {
    }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<CourseInstructor> CourseInstructors { get; set; }
    public DbSet<CourseStudent> CourseStudents { get; set; }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<ExamQuestion> ExamQuestions { get; set; }

    public DbSet<Answer> Answers { get; set; }
    public DbSet<Choice> Choices { get; set; }
    public DbSet<ExamStudent> ExamStudents { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
            .HasOne(a => a.Instructor)
            .WithOne(x => x.AppUser)
            .HasForeignKey<Instructor>(a => a.IdentityId);

        builder.Entity<AppUser>()
            .HasOne(a => a.Student)
            .WithOne(x => x.AppUser)
            .HasForeignKey<Student>(a => a.IdentityId);
    }
}
