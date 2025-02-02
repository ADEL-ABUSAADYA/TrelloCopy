
using MediatR;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.GetUserDetalies.Queary
{
    public record GetUserDetailesQuery (int id) : IRequest<RequestResult<UserDetailsDTO>>;

    public class GetUserDetailesQueryHandler : BaseRequestHandler<GetUserDetailesQuery, RequestResult<UserDetailsDTO>, User>
    {
        public GetUserDetailesQueryHandler(BaseRequestHandlerParameters<User> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<UserDetailsDTO>> Handle(GetUserDetailesQuery request, CancellationToken cancellationToken)
        {
             var exsitUser= await _repository.Get(c=> c.ID == request.id).FirstOrDefaultAsync();

            if (exsitUser == null) return RequestResult<UserDetailsDTO>.Failure(ErrorCode.UserNotFound, "this user not found indatabasae");

    
            var user = new UserDetailsDTO
            {

                name = exsitUser.Name,
                IsActive = exsitUser.IsActive,
                PhoneNo = exsitUser.PhoneNo,
                email = exsitUser.Email,
                 CreatedTime = exsitUser.CreatedDate,
            };


            return RequestResult<UserDetailsDTO>.Success(user); 

        }
    }

}