using Core.Enum;
using Core.Models;
using Infrastructure.Data;

namespace Core.Seeding
{

    public class SeedQuestions
    {

        public static List<InstViewModel> GetInstructorsIds(AppDbContext context)
        {
            var ins = context.Instructors.Select(x => new InstViewModel
            {
                Id = x.Id,
                UserName = x.AppUser.UserName,
            }).OrderBy(x => x.Id).ToList();
            return ins;
        }

        public static void SeedQuestionsForCourses(AppDbContext context)
        {
            if (context.Questions.Any()) return;
            var listOfInstructors = GetInstructorsIds(context);

            var courses = context.Courses.ToList();

            if (!courses.Any()) return;

            foreach (var course in courses)
            {
                var questions = GenerateQuestions(course.Id, listOfInstructors, course.Name);

                foreach (var question in questions)
                {
                    context.Questions.Add(question);
                }
            }

            context.SaveChanges();
        }

        private static List<Question> GenerateQuestions(int courseId, List<InstViewModel> ints, string courseName)
        {
            var questions = new List<Question>();
            var random = new Random();

            for (int i = 1; i <= 20; i++)
            {

                var randomInstId = random.Next(ints.Select(x => x.Id).FirstOrDefault(), ints.Select(x => x.Id).LastOrDefault());
                (string questionText, List<string> options, string correctAnswer) = courseName switch
                {
                    "Vue Js" => (
                        "What is the purpose of Vue.js directives in component templates?",
                        new List<string> { "To apply conditional rendering", "To manage state directly", "To enable server-side rendering", "To connect Vue.js to a database" },
                        "To apply conditional rendering"
                    ),
                    "Advanced SQL" => (
                        "Which SQL clause is used to filter records in a query?",
                        new List<string> { "WHERE", "GROUP BY", "HAVING", "ORDER BY" },
                        "WHERE"
                    ),
                    "C# Fundamentals" => (
                        "What is the key difference between a struct and a class in C#?",
                        new List<string> { "Structs are value types, classes are reference types", "Structs support inheritance, classes do not", "Classes cannot contain methods, structs can", "Structs are immutable, classes are not" },
                        "Structs are value types, classes are reference types"
                    ),
                    "ASP.NET Core" => (
                        "How does ASP.NET Core handle dependency injection?",
                        new List<string> { "Using IServiceCollection", "By creating singletons manually", "Through global static classes", "By using an event-driven model" },
                        "Using IServiceCollection"
                    ),
                    "Entity Framework Core" => (
                        "What is the purpose of migrations in Entity Framework Core?",
                        new List<string> { "To track database changes", "To manage user roles", "To configure entity relationships", "To handle data caching" },
                        "To track database changes"
                    ),
                    "JavaScript Essentials" => (
                        "What is a closure in JavaScript?",
                        new List<string> { "A function with access to its lexical scope", "A method to iterate over arrays", "A design pattern for asynchronous code", "A variable declared with `let`" },
                        "A function with access to its lexical scope"
                    ),
                    "React.js Basics" => (
                        "What is the main purpose of React's Virtual DOM?",
                        new List<string> { "To improve performance", "To store application data", "To enforce type safety", "To manage CSS styles" },
                        "To improve performance"
                    ),
                    "Python for Data Science" => (
                        "Which Python library is commonly used for data manipulation?",
                        new List<string> { "Pandas", "NumPy", "Matplotlib", "Scikit-learn" },
                        "Pandas"
                    ),
                    "Machine Learning with Python" => (
                        "Which algorithm is commonly used for classification tasks?",
                        new List<string> { "Linear Regression", "Decision Tree", "K-Means Clustering", "Principal Component Analysis" },
                        "Decision Tree"
                    ),
                    "Docker and Kubernetes" => (
                        "What is the primary role of Kubernetes in containerized applications?",
                        new List<string> { "Container orchestration", "Container creation", "Application monitoring", "Code compilation" },
                        "Container orchestration"
                    ),
                    _ => (
                        "What is the purpose of this course?",
                        new List<string> { "Option A", "Option B", "Option C", "Option D" },
                        "Option A"
                    )
                };
                var createdBy = ints.Where(x => x.Id == randomInstId).Select(x => x.UserName).First();
                questions.Add(new Question
                {
                    Text = questionText,
                    Level = (DifficultyLevel)(i % 3),
                    CourseId = courseId,
                    InstructorId = randomInstId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = ints.Where(x => x.Id == randomInstId).Select(x => x.UserName).First(),
                    Choices = options.Select(o => new Choice
                    {
                        Text = o,
                        IsCorrect = o == correctAnswer,
                        CreatedBy = createdBy
                    }).ToList()
                });


            }
            return questions;
        }

    }
    public class InstViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }

}