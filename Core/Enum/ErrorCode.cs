namespace Core.Enum;
public enum ErrorCode
{
    NoError = 0,
    InstructorNotAssignedToCourse = 1,
    InvalidQuestionIds = 2,
    DatabaseSaveError = 3,
    MissingRequiredFields = 4,
    CourseNotFound = 5,
    QuestionChoicesNotMatching = 6,
    StudentsNotEnrolledInCourse = 7,
    InsufficientQuestion = 8,
    UnbalancedDifficulty = 9,

}
