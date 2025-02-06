using FluentValidation;
using TrelloCopy.Data;

namespace TrelloCopy.Features.TaskManagement.Tasks.UpdateTaskStatus
{
    public class RequsetUpdateTaskModel
    { 

         public TaskStatus Status { get; set; } 

         public int id { get; set; }

    }


    public class RequsetUpdateTaskModelValidator :AbstractValidator<RequsetUpdateTaskModel>
    {
        

         
    }
}