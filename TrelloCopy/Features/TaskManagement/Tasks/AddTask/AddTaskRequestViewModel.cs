using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using TrelloCopy.Data;

namespace TrelloCopy.Features.TaskManagement.Tasks.AddTask
{
    public class AddTaskRequestViewModel
    {
        public string Title { get;  set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
    public class AddTaskRequestViewModelValidator : AbstractValidator<AddTaskRequestViewModel>
    {
        private readonly Context context ;
       
        public AddTaskRequestViewModelValidator(Context context)
        {
            this.context = context;

            #region Title

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .Must(BeUniqueTitle);
               

            #endregion
            
            #region Description
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required");
                

            #endregion

            #region UserId
            RuleFor(x => x.UserId).NotEmpty()
                .WithMessage("UserId is required")
                .Must(UserExist);
            #endregion
            
            #region ProjectId 
            RuleFor(x => x.ProjectId).NotEmpty().WithMessage("ProjectId is required");
            #endregion

        }
        private bool BeUniqueTitle(string title )
        {
            return context.Tasks.All(x => x.Title != title);
        }
        private bool UserExist(int userId)
        {
            return context.Users.Any(x => x.ID == userId);
        }
        
    }
}
