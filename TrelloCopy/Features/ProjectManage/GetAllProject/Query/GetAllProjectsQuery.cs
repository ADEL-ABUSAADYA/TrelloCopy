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


    public record GetAllProjectsQuery(int PageNumber, int PageSize,string? title  ) : IRequest<RequestResult<Pagination<GetAllProjectDTo>>>;

    public class GetAllProjectsQueryHandler : BaseRequestHandler<GetAllProjectsQuery, RequestResult<Pagination<GetAllProjectDTo>>, Project>
    {
        public GetAllProjectsQueryHandler(BaseRequestHandlerParameters<Project> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<Pagination<GetAllProjectDTo>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll();

            if (!query.Any()) return RequestResult<Pagination<GetAllProjectDTo>>.Failure(ErrorCode.NoProjectAdd, "there is no project add");

            if (!string.IsNullOrEmpty(request.title)) query = query.Where(c => c.Title.Contains(request.title));

            //mapping 

           var projects = query.Select(c => new GetAllProjectDTo
            {
                title = c.Title,
                description = c.Description,
                NumTask = c.SprintItems.Select(c => c.ID).Count(), 
                 NumUSers= c.SprintItems.Select(c => c.UserID).Distinct().Count()
           });

            var paginatedResult = await Pagination<GetAllProjectDTo>.ToPagedList(projects, request.PageNumber , request.PageSize);

            return RequestResult<Pagination<GetAllProjectDTo>>.Success(paginatedResult);
        }
    }
}
