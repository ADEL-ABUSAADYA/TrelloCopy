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

public class RegisterUserCommandHandler : UserBaseRequestHandler<RegisterUserCommand, RequestResult<bool>>
{
    public RegisterUserCommandHandler(UserBaseRequestHandlerParameters parameters) : base(parameters)
    {
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
            RoleID = 3,
            Name = request.name,
            PhoneNo = request.phoneNo,
            Country = request.country,
            IsActive = true,
            ConfirmationToken = Guid.NewGuid().ToString()
        };
        
       
        var userID = await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        
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
        message.From.Add(new MailboxAddress("adel", "adel.fantasy15@gmail.com"));
        message.To.Add(new MailboxAddress(name, email));
        message.Subject = "UpSkilling Final Project";

        // Create multipart content for both plain text and HTML
        var bodyBuilder = new BodyBuilder
        {
            TextBody = $"Please confirm your registration by clicking the following link: {confirmationLink}",
            HtmlBody = $"<p>Hello {name}!</p><p>Please confirm your registration by clicking <a href='{confirmationLink}'>this link</a>.</p>"
        };

        message.Body = bodyBuilder.ToMessageBody();

        
        using (var client = new SmtpClient())
        {
            // Set the timeout for connection and authentication
            client.Timeout = 10000;  // Timeout after 10 seconds
        
            // Connect using StartTLS for security
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

            // Authenticate with the provided credentials
            await client.AuthenticateAsync("adel.fantasy15@gmail.com", "Dolla111");

            // Send the email
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        return RequestResult<bool>.Success(true);
        
    }
}