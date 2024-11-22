using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfig;
internal class ExamQuestionConfig : IEntityTypeConfiguration<ExamQuestion>
{
    public void Configure(EntityTypeBuilder<ExamQuestion> builder)
    {
        builder.HasOne(eq => eq.Exam)
                  .WithMany()
                  .HasForeignKey(eq => eq.ExamId)
                  .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(eq => eq.Course)
               .WithMany()
               .HasForeignKey(eq => eq.CourseId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
