using FluentValidation;
using TrelloCopy.Data;
using TrelloCopy.Models.Enums;

namespace TrelloCopy.Features.TaskManagement.Tasks.UpdateTaskStatus
{
    public class RequsetUpdateTaskModel
    { 

         public TaskStatu Status { get; set; } 

         public int id { get; set; }

    }


    public class RequsetUpdateTaskModelValidator :AbstractValidator<RequsetUpdateTaskModel>
    {
        

         
    }
}