using MediatR;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Projects.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.ProjectManage.AddProject.Command
{
    public record AddProjectCommand(string title, string describtion) : IRequest<RequestResult<bool>>;


    public class AddProjectCommandHandler : BaseRequestHandler<AddProjectCommand, RequestResult<bool>, Project>
    {
        public AddProjectCommandHandler(BaseWithoutRepositoryRequestHandlerParameter<Project> parameters) : base(parameters)
        {


        }

        public override async Task<RequestResult<bool>> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            var userId = _userInfo.ID;

            // check this project is already exist 
            var project = await _mediator.Send(new IsProjectExistQuery(request.title, userId));

            if (!project.isSuccess) return RequestResult<bool>.Failure(project.errorCode, project.message);


            var NewProject = new Project
            {
                CreatedBy = userId,
                Description = request.describtion,
                Title = request.title,
                CreatedDate = DateTime.Now,
                CreatorID = userId,
            };

            _repository.Add(NewProject);
            await _repository.SaveChangesAsync();

            return RequestResult<bool>.Success(true, "project created sucssfuly");
        }
    }



}
