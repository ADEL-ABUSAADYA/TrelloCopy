using MediatR;
using Microsoft.AspNetCore.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
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
        
        var userID = await _repository.AddAsync(new User
        { 
          Email=request.email,
          Password = password,
          Name = request.name,
          PhoneNo= request.phoneNo,
          Country= request.country,
          IsActive = true,
          ConfirmationToken = Guid.NewGuid().ToString()
        });
        await _repository.SaveChangesAsync();
        
        if (userID < 0)
        return RequestResult<bool>.Failure(ErrorCode.UnKnownError);
        
        var emailSent = await SendConfirmationEmail(request.email, request.name);
        if (!emailSent.isSuccess)
            return RequestResult<bool>.Failure(ErrorCode.EmailNotSent);

        return RequestResult<bool>.Success(true);
    }
    
    private async Task<RequestResult<bool>> SendConfirmationEmail(string email, string name)
    {
        try
        {
            var client = new SendGridClient("_sendGridApiKey");
            var from = new EmailAddress("adelabusaadya@gmail.com", "ProjectManagementSystem");
            var subject = "Confirm Your Registration";
            var to = new EmailAddress(email, name);
            var plainTextContent = $"Hello {name},\n\nPlease confirm your registration by clicking the link below.";
            var htmlContent = $"<p>Hello {name},</p><p>Please confirm your registration by clicking the link below:</p><p><a href='https://UpSkillingProjectManagementSystem.com/confirm?email={email}'>Confirm Email</a></p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            
            var response = await client.SendEmailAsync(msg);
            var isSent = response.StatusCode == System.Net.HttpStatusCode.Accepted;
            if (isSent)
            return RequestResult<bool>.Success(isSent);
            
            return RequestResult<bool>.Failure(ErrorCode.UnKnownError);
        }
        catch (Exception ex)
        {
            return RequestResult<bool>.Failure(ErrorCode.UnKnownError, ex.Message);
        }
    }
}