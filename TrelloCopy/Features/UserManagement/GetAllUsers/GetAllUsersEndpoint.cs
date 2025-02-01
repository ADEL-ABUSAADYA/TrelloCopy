using Microsoft.AspNetCore.Mvc;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.AuthManagement.LogInUser;
using TrelloCopy.Features.AuthManagement.LogInUser.Commands;
using TrelloCopy.Features.UserManagement.GetAllUsers.Queries;

namespace TrelloCopy.Features.UserManagement.GetAllUsers;

public class GetAllUsersEndpoint : BaseWithoutTRequestEndpoint<UserResponseViewModel>
{
   public GetAllUsersEndpoint(BaseWithoutTRequestEndpointParameters parameters) : base(parameters)
   {
   }

   [HttpGet]
   public async Task<EndpointResponse<UserResponseViewModel>> GetAllUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
   {
      var allUsers = await _mediator.Send(new GetAllUsersQuery(pageNumber, pageSize));

      if (!allUsers.isSuccess)
         return EndpointResponse<UserResponseViewModel>.Failure(allUsers.errorCode, allUsers.message);


      var paginatedResult = allUsers.data;
      var response = new UserResponseViewModel
      {
         Users = paginatedResult.Items.Select(u => new UserDTO
         {
            Email = u.Email,
            Name = u.Name,
            PhoneNo = u.PhoneNo,
            IsActive = u.IsActive
         }).ToList(),
         TotalCount = paginatedResult.TotalCount,
         PageNumber = paginatedResult.PageNumber,
         PageSize = paginatedResult.PageSize
      };

      return EndpointResponse<UserResponseViewModel>.Success(response);
   }
}
