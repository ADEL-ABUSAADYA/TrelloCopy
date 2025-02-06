using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Helper;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Pagination;
using TrelloCopy.Features.UserManagement.GetAllUsers;
using TrelloCopy.Models;

namespace TrelloCopy.Features.ProjectManage.GetAllProject.Query
{


    public record GetAllProjectsQuery(int PageNumber, int PageSize, string? title) : IRequest<RequestResult<Pagination<GetProjectDTo>>>;

    public class GetAllProjectsQueryHandler : BaseRequestHandler<GetAllProjectsQuery, RequestResult<Pagination<GetProjectDTo>>, Project>
    {
        public GetAllProjectsQueryHandler(BaseWithoutRepositoryRequestHandlerParameter<Project> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<Pagination<GetProjectDTo>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll();

            if (!query.Any()) return RequestResult<Pagination<GetProjectDTo>>.Failure(ErrorCode.NoProjectAdd, "there is no project add");

            if (!string.IsNullOrEmpty(request.title)) query = query.Where(c => c.Title.Contains(request.title));

            //mapping 

            var projects = query.Select(c => new GetProjectDTo
            {
                title = c.Title,
                description = c.Description,
                NumTask = c.Tasks.Select(c => c.ID).Count(),
                NumUSers = c.Tasks.Select(c => c.UserId).Distinct().Count(),
                CreatedDate = c.CreatedDate,
            });

            var paginatedResult = await Pagination<GetProjectDTo>.ToPagedList(projects, request.PageNumber, request.PageSize);

            return RequestResult<Pagination<GetProjectDTo>>.Success(paginatedResult);
        }
    }
}
