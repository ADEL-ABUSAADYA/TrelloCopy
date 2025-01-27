using MediatR;
using MailKit.Net.Smtp;
using MimeKit;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.ReSendRegistrationEmail.Queries;


namespace TrelloCopy.Features.UserManagement.ReSendRegistrationEmail.Commands;

public record ReSendRegistrationEmailCommand(string email) : IRequest<RequestResult<bool>>;

public class ReSendRegistrationEmailCommandHandler : UserBaseRequestHandler<ReSendRegistrationEmailCommand, RequestResult<bool>>
{
    public ReSendRegistrationEmailCommandHandler(UserBaseRequestHandlerParameters parameters) : base(parameters)
    {
    }

    public override async Task<RequestResult<bool>> Handle(ReSendRegistrationEmailCommand request,
        CancellationToken cancellationToken)
    {
        var RegisterationData = await _mediator.Send(new GetUserRegistrationInfoQuery(request.email));

        if (!RegisterationData.isSuccess)
            return RequestResult<bool>.Failure(RegisterationData.errorCode, RegisterationData.message);
        
        if (RegisterationData.data.IsRegistered)
            return RequestResult<bool>.Failure(ErrorCode.UserAlreadyRegistered, "user already registered please login");
  
        var confirmationLink = $"https://yourdomain.com/confirm?email={RegisterationData.data.Email}&token={RegisterationData.data.ConfirmationToken }";
        
        var emailSent = await SendConfirmationEmail(RegisterationData.data.Email, RegisterationData.data.Name, confirmationLink);
        if (!emailSent.isSuccess)
            return RequestResult<bool>.Failure(ErrorCode.EmailNotSent);

        return RequestResult<bool>.Success(true);
    }
    
    private async Task<RequestResult<bool>> SendConfirmationEmail(string email, string name, string confirmationLink)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Test", "emile.murphy59@ethereal.email"));
        message.To.Add(new MailboxAddress(name, email));
        message.Subject = "UpSkilling Final Project";

        // Create multipart content for both plain text and HTML
        var bodyBuilder = new BodyBuilder
        {
            TextBody = $"Please confirm your registration by clicking the following link: {confirmationLink}",
            HtmlBody = $"<p>Hello {name}!</p><p>Please confirm your registration by clicking <a href='{confirmationLink}'>this link</a>.</p>"
        };

        message.Body = bodyBuilder.ToMessageBody();

        try
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("emile.murphy59@ethereal.email", "J4kFQAfdux87RAghJb");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

            return RequestResult<bool>.Success(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            return RequestResult<bool>.Failure(ErrorCode.UnKnownError, ex.Message);
        }
    }
}

