//using Core.Models;
//using Infrastructure.AuthService;
//using Infrastructure.ViewModel;
//using Microsoft.AspNetCore.Identity;

//namespace Infrastructure.Data
//{
//    public static class SeedData
//    {
//        public static async Task SeedInstructorsAndCourseInstructors(AppDbContext context, IAuthService authService, UserManager<AppUser> userManager)
//        {
//            context.Database.EnsureCreated();

//            if (context.Instructors.Any()) return;

//            var courseIds = Enumerable.Range(1, 10).ToList();

//            var instructors = new List<RegisterModel>();

//            for (int i = 1; i <= 20; i++)
//            {

//                var instructor = new RegisterModel
//                {
//                    FirstName = "Instructor" + i,
//                    LastName = "Last" + i,
//                    UserName = "instructor" + i + "@school.com",
//                    Email = "instructor" + i + "@school.com",
//                    Password = "P@ssw0rd" + i,
//                    Mobile = "1234567890",
//                    Address = "Some Address " + i,
//                    DateOfBirth = DateTime.Now.AddYears(-30)
//                };
//                instructors.Add(instructor);
//            }

//            foreach (var instructorModel in instructors)
//            {
//                var appUser = new AppUser
//                {
//                    UserName = instructorModel.UserName,
//                    Email = instructorModel.Email,


//                };
//                try
//                {
//                    var result = await userManager.CreateAsync(appUser, instructorModel.Password);
//                    if (result.Succeeded)
//                    {
//                        var instructor = new Instructor
//                        {
//                            IdentityId = appUser.Id,
//                            FirstName = instructorModel.FirstName,
//                            LastName = instructorModel.LastName,
//                            Address = instructorModel.Address,
//                            Mobile = instructorModel.Mobile,
//                            CreatedBy = appUser.UserName,

//                            CourseInstructors = new List<CourseInstructor>
//                            {
//                                new CourseInstructor
//                                {
//                                    CourseId =new Random().Next(1, 10),
//                                    CreatedBy = appUser.Id,
//                                    CreatedDate = DateTime.UtcNow,
//                                }
//                            },

//                        };
//                        context.Instructors.Add(instructor);

//                        await context.SaveChangesAsync();

//                    }


//                }
//                catch (Exception ex)
//                {

//                    throw;
//                }


//            }

//        }
//    }
//}

