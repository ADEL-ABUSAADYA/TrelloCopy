using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Pagination;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.GetAllUsers.Queries;

public record GetAllUsersQuery(int PageNumber , int PageSize) : IRequest<RequestResult<PaginatedResult<UserDTO>>>;

public class GetAllUsersQueryHandler : BaseRequestHandler<GetAllUsersQuery, RequestResult<PaginatedResult<UserDTO>>, User>
{
    public GetAllUsersQueryHandler(BaseWithoutRepositoryRequestHandlerParameter<User> parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<PaginatedResult<UserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _repository.GetAll();

        // Ensure PageNumber and PageSize are valid
        if (request.PageNumber < 1)
            return RequestResult<PaginatedResult<UserDTO>>.Failure(ErrorCode.InvalidInput, "PageNumber must be greater than 0");

        if (request.PageSize < 1 || request.PageSize > 100)
            return RequestResult<PaginatedResult<UserDTO>>.Failure(ErrorCode.InvalidInput, "PageSize must be between 1 and 100");

        // Get total count of users
        int totalUsers = await query.CountAsync(cancellationToken);

        // Apply pagination (Skip and Take)
        var users = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(u => new UserDTO
            {
                Name = u.Name,
                Email = u.Email,
                PhoneNo = u.PhoneNo,
                IsActive = u.IsActive
            })
            .ToListAsync(cancellationToken);

        if (!users.Any())
            return RequestResult<PaginatedResult<UserDTO>>.Failure(ErrorCode.NoUsersFound, "No users found");

        // Create the PaginatedResult
        var paginatedResult = new PaginatedResult<UserDTO>(users, totalUsers, request.PageNumber, request.PageSize);

        return RequestResult<PaginatedResult<UserDTO>>.Success(paginatedResult);
    }
}