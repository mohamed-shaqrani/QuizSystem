﻿using System.ComponentModel.DataAnnotations;

namespace Core.Models;
public class Student : BaseModel
{
    [Required, StringLength(250)]

    public string FirstName { get; set; }
    [Required, StringLength(250)]

    public string LastName { get; set; }
    public string IdentityId { get; set; }

    [Required, StringLength(500)]

    public string Address { get; set; }
    [Required, StringLength(30)]

    public string Mobile { get; set; }
    [Required]

    public DateTime DateOfBirth { get; set; }
    public AppUser AppUser { get; set; }
    public ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();


}
