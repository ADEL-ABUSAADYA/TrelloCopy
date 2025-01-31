using MediatR;
using TrelloCopy.Common.Views;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using Microsoft.EntityFrameworkCore;
using TrelloCopy.Common.CommonQuary;
using TrelloCopy.Models;


namespace TrelloCopy.Features.UserManagement.SendFrogetPasswordResetEmail.Queries
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
                        u.Name,
                        u.IsEmailConfirmed,
                        u.ConfirmationToken
                        )).FirstOrDefaultAsync();

            if (resetInfo is null)
                return RequestResult<FrogetPasswordInfoDTO>.Failure(ErrorCode.UserNotFound, "this user not found");

            if (!resetInfo.IsEmailConfirmed || string.IsNullOrEmpty(resetInfo.EmailConfirmationToken))
            {
                return RequestResult<FrogetPasswordInfoDTO>.Failure(ErrorCode.AccountNotVerified, "Verify your email address");
            }
            return RequestResult<FrogetPasswordInfoDTO>.Success(resetInfo);
            
        }
    }


}
