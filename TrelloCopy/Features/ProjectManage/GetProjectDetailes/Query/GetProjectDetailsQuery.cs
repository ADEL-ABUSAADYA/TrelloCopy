using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Helper;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Pagination;
using TrelloCopy.Features.UserManagement.GetAllUsers;
using TrelloCopy.Models;

namespace TrelloCopy.Features.ProjectManage.GetProjectDetailes.Query
{
    public record GetProjectDetailsQuery(int id) : IRequest<RequestResult<GetProjectRequestDTO>>;

    public class GetProjectDetailsQueryHandler : BaseRequestHandler<GetProjectDetailsQuery, RequestResult<GetProjectRequestDTO>, Project>
    {
        public GetProjectDetailsQueryHandler(BaseWithoutRepositoryRequestHandlerParameter<Project> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<GetProjectRequestDTO>> Handle(GetProjectDetailsQuery request, CancellationToken cancellationToken)
        {
            var query = await _repository.Get(c => c.ID == request.id && !c.Deleted).Select(c => new
            {
                c.Tasks,
                c.CreatedDate,
                c.Description,
                c.Title
            }).FirstOrDefaultAsync();

            if (query == null) return RequestResult<GetProjectRequestDTO>.Failure(ErrorCode.ProjectNotFound, "there is no project with this id ");

            var project = new GetProjectRequestDTO
            {
                title = query.Title,
                description = query.Description,
                NumTask = query.Tasks.Select(c => c.ID).Count(),
                NumUSers = query.Tasks.Select(c => c.UserId).Distinct().Count(),
                CreatedDate = query.CreatedDate,
            };

            return RequestResult<GetProjectRequestDTO>.Success(project);
        }
    }
}
