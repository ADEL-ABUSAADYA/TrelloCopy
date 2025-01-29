

namespace TrelloCopy.Common.Data.Enums
{
    public enum Feature
    {
        // user 0 - 100
        RegisterUser = 0,
        GetUserByID = 1,
        GetUserByEmail = 2,
        GetUserByName = 3,
        UpdateUserByID = 4,
        UpdateUserByEmail = 5,
        UpdateUserByName = 6,
        DeleteUserByID = 7,
        DeleteUserByEmail = 8,
        DeleteUserByName = 9,
        ActivateUser2FA = 10,
        
        // group 100 - 200
        GetAllGroups = 101,
        GetGroupByID = 102,
        GetGroupByName = 103,
        AddGroup = 104,
        UpdateGroup = 105,
        DeleteGroup = 106,
        
        // quiz 200 - 300
        
        // student 300 - 400
        TakeQuiz = 301,
        
        // instructor 400 -500
        CreateQuiz = 401,
        UpdateQuiz = 402,
        DeleteQuiz = 403,
        AssignQuizToStudent = 404,
        AddQuestion = 405,
        UpdateQuestion = 406,
        DeleteQuestion = 407,
        AddQuestionToQuiz = 408,
        
        // question 500 - 600
        GetAllQuestions = 501,
        
        
        
        
        
        
        
    }
}
