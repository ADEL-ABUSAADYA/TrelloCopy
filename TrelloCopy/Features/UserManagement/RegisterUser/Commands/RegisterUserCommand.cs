using MailKit.Net.Smtp;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.userManagement.RegisterUser.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Features.userManagement.RegisterUser.Commands;

public record RegisterUserCommand(string email, string password, string name, string phoneNo, string country) : IRequest<RequestResult<bool>>;

public class RegisterUserCommandHandler : BaseRequestHandler<RegisterUserCommand, RequestResult<bool>>
{
    public RegisterUserCommandHandler(BaseRequestHandlerParameters parameters) : base(parameters)
    {
    }

    public async override Task<RequestResult<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var reponse = await _mediator.Send(new IsUserExistQuery(request.email));
        if (reponse.isSuccess)
            return RequestResult<bool>.Failure(ErrorCode.UserAlreadyExist);
        
        PasswordHasher<string> passwordHasher = null;
        var password = passwordHasher.HashPassword(null, request.password);
        var user = new User
        {
            Email = request.email,
            Password = password,
            Name = request.name,
            PhoneNo = request.phoneNo,
            Country = request.country,
            IsActive = true,
            ConfirmationToken = Guid.NewGuid().ToString()
        };
        var userID = await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
        
        if (userID < 0)
        return RequestResult<bool>.Failure(ErrorCode.UnKnownError);
        
        var confirmationLink = $"https://yourdomain.com/confirm?email={user.Email}&token={user.ConfirmationToken}";
        
        var emailSent = await SendConfirmationEmail(user.Email, user.Name, confirmationLink);
        if (!emailSent.isSuccess)
            return RequestResult<bool>.Failure(ErrorCode.EmailNotSent);

        return RequestResult<bool>.Success(true);
    }
    
    private async Task<RequestResult<bool>> SendConfirmationEmail(string email, string name, string confirmationLink)
    {
        
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Test", "emile.murphy59@ethereal.email"));
        message.To.Add(new MailboxAddress($"{name}", email));
        message.Subject = "UpSkilling Final Project";
        message.Body = new TextPart("html") { Text = $"hello {name}! \n {confirmationLink}" };

        message.Body = new TextPart("plain")
        {
            Text = $"Please confirm your registration by clicking the following link: {confirmationLink}"
        };

        try
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.ethereal.email", 587, false);
                await client.AuthenticateAsync("emile.murphy59@ethereal.email", "J4kFQAfdux87RAghJb");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            return RequestResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return RequestResult<bool>.Failure(ErrorCode.UnKnownError, ex.Message);
        }
        
    }
}