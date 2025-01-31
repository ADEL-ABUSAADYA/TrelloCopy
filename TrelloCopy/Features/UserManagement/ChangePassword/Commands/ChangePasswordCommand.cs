using MediatR;
using Microsoft.AspNetCore.Identity;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.ChangePassword.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.UserManagement.ChangePassword.Commands;
public record ChangePasswordCommand(string CurrentPassword,string newPassword) : IRequest<RequestResult<bool>>;
public class ChangePasswordCommandHandler : BaseRequestHandler<ChangePasswordCommand, RequestResult<bool>, User>
{
    public ChangePasswordCommandHandler(BaseRequestHandlerParameters<User> parameters) : base(parameters)
    {
    }

    public async override Task<RequestResult<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var CurrentDatabasePassword = await _mediator.Send(new GetPasswordByIDQuery());

        if (!CurrentDatabasePassword.isSuccess)
            return RequestResult<bool>.Failure(ErrorCode.UserNotFound);

        PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
        var newPassword = passwordHasher.HashPassword(null, request.newPassword);
        

        var isOldPasswordCorrect = CheckPassword(request.CurrentPassword, CurrentDatabasePassword.data);
        
        if (!isOldPasswordCorrect)
            return RequestResult<bool>.Failure(ErrorCode.InvalidInput);

        
        var user = new User()
        {
            ID = _userInfo.ID,
            Password = request.newPassword,
        };
           
        await  _repository.SaveIncludeAsync(user, nameof(User.Password));
           
        await _repository.SaveChangesAsync(); 

        return RequestResult<bool>.Success(true);

    }

    private bool CheckPassword(string requestCurrentPassword, string databasePassword)
    {
        var passwordHasher = new PasswordHasher<string>();
        return passwordHasher.VerifyHashedPassword(null, databasePassword, requestCurrentPassword) != PasswordVerificationResult.Failed;
    }

}
