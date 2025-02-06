using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Helper;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Pagination;
using TrelloCopy.Features.ProjectManage.GetAllProject.Query;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.GetAllUsers.Queries;

public record GetAllUsersQuery(int PageNumber , int PageSize) : IRequest<RequestResult<Pagination<UserDTO>>>;

public class GetAllUsersQueryHandler : BaseRequestHandler<GetAllUsersQuery, RequestResult<Pagination<UserDTO>>, User>
{
    public GetAllUsersQueryHandler(BaseWithoutRepositoryRequestHandlerParameter<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<Pagination<UserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _repository.Get(c=>c.RoleID == 2);


        if (!query.Any()) return RequestResult<Pagination<UserDTO>>.Failure(ErrorCode.NoUsersFound, "there is no users added");



        var users = query.Select(u => new UserDTO
        {
            Name = u.Name,
            Email = u.Email,
            PhoneNo = u.PhoneNo,
            IsActive = u.IsActive
        }); 

  

        // Create the PaginatedResult
        var paginatedResult = await Pagination<UserDTO>.ToPagedList(users, request.PageNumber, request.PageSize);

        return RequestResult<Pagination<UserDTO>>.Success(paginatedResult);
    }
}