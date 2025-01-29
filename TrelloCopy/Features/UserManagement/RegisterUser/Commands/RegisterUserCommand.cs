using FoodApp.Api.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.CommonQuary;
using TrelloCopy.Features.UserManagement.RegisterUser.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.userManagement.RegisterUser.Commands;

public record RegisterUserCommand(string email, string password, string name, string phoneNo, string country) : IRequest<RequestResult<bool>>;

public class RegisterUserCommandHandler : UserBaseRequestHandler<RegisterUserCommand, RequestResult<bool>>
{
    readonly MailSettings mailSettings;
    public RegisterUserCommandHandler(UserBaseRequestHandlerParameters parameters , IOptions<MailSettings> options) : base(parameters)
    {
         
        mailSettings = options.Value;
    }

    public async override Task<RequestResult<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var reponse = await _mediator.Send(new IsUserExistQuery(request.email));
        if (reponse.isSuccess)
            return RequestResult<bool>.Failure(ErrorCode.UserAlreadyExist);
        
        PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
        var password = passwordHasher.HashPassword(null, request.password);
        
        var user = new User
        {
            Email = request.email,
            Password = password,
            RoleID = 2,
            Name = request.name,
            PhoneNo = request.phoneNo,
            Country = request.country,
            IsActive = true,
            ConfirmationToken = Guid.NewGuid().ToString()
        };
        
       
        var userID = await _userRepository.AddAsync(user);
       
        if (userID < 0)
        return RequestResult<bool>.Failure(ErrorCode.UnKnownError);

        await _userRepository.SaveChangesAsync();
        
        
        
        var confirmationLink = $"https://localhost:7015/confirm?email={user.Email}&token={user.ConfirmationToken}";
        
        var emailSent = await _mediator.Send(new SendEamilQuary(user.Email, user.Name, confirmationLink));
        if (!emailSent.isSuccess)
            return RequestResult<bool>.Failure(ErrorCode.EmailNotSent);

        return RequestResult<bool>.Success(true);
    }
    
 }
