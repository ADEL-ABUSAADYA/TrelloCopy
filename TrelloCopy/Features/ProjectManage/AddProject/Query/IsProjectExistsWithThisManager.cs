using MediatR;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Models;

namespace TrelloCopy.Features.ProjectManage.AddProject.Query
{
    public record IsProjectExistQuery(string title, int createdBy ) : IRequest<RequestResult<bool>>;


    public class IsProjectExistQueryHandler : BaseRequestHandler<IsProjectExistQuery, RequestResult<bool>, Project>
    {
        public IsProjectExistQueryHandler(BaseRequestHandlerParameters<Project> parameters) : base(parameters)
        {


        }

        public override async Task<RequestResult<bool>> Handle(IsProjectExistQuery request, CancellationToken cancellationToken)
        {
            var projectExist = await _repository.AnyAsync(p=> p.CreatorID == request.createdBy && p.Title == request.title);

            if (projectExist) return RequestResult<bool>.Failure(ErrorCode.ProjectAlreadyExists, "this project is already created by this manager"); 

           
            return RequestResult<bool>.Success(projectExist);   

        }
    }
}
