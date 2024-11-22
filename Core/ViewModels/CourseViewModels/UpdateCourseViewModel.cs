using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.CourseViewModel;
public class UpdateCourseViewModel
{
    [Required]
    public string Name { get; set; }
    [Required]

    public string Description { get; set; }
    [Required]

    public int Id { get; set; }
}
