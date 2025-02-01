using MediatR;
using TrelloCopy.Common.Views;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Data.Enums;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Models;


namespace TrelloCopy.Features.AuthManagement.SendFrogetPasswordResetEmail.Queries
{
    public record GetForgetPasswordInfoQuery(string email) : IRequest<RequestResult<FrogetPasswordInfoDTO>>;


    public class ResetPsswordQueryHandler : BaseRequestHandler<GetForgetPasswordInfoQuery, RequestResult<FrogetPasswordInfoDTO>, User>
    {
        public ResetPsswordQueryHandler(BaseRequestHandlerParameters<User> parameters) : base(parameters)
        {
        }

        public override async Task<RequestResult<FrogetPasswordInfoDTO>> Handle(GetForgetPasswordInfoQuery request, CancellationToken cancellationToken)
        {
            var resetInfo = await _repository.Get(U=> U.Email == request.email)
                .Select(u =>
                    new FrogetPasswordInfoDTO(
                        u.ID,
                        u.IsEmailConfirmed
                        )).FirstOrDefaultAsync();

            if (resetInfo is null )
                return RequestResult<FrogetPasswordInfoDTO>.Failure(ErrorCode.UserNotFound, "this user not found");

            if (!resetInfo.IsEmailConfirmed || resetInfo.UserID <= 0 )
            {
                return RequestResult<FrogetPasswordInfoDTO>.Failure(ErrorCode.AccountNotVerified, "Verify your email address");
            }
            return RequestResult<FrogetPasswordInfoDTO>.Success(resetInfo);
            
        }
    }


}
