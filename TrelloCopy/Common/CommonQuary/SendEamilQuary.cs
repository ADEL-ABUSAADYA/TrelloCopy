
using MailKit.Net.Smtp;
using MailKit.Security;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;
using TrelloCopy.Common;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.ForgetPassword.Queries;
using TrelloCopy.Models;

namespace TrelloCopy.Common.CommonQuary
{
    public record SendEamilQuary(string email, string name, string confirmationLink) : IRequest<RequestResult<bool>>;

    public class SendEmailQuaryHandler : BaseRequestHandler<SendEamilQuary, RequestResult<bool>, BaseModel>
    {

       readonly MailSettings mailSettings;
        public SendEmailQuaryHandler(BaseRequestHandlerParameters<BaseModel> parameters, IOptions<MailSettings> _mailSettings) : base(parameters)
        {
            mailSettings = _mailSettings.Value;  
        }

        public override async Task<RequestResult<bool>> Handle(SendEamilQuary request, CancellationToken cancellationToken)
        {
            
            
            var Email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(mailSettings.Email),
                Subject = request.name

            };
            Email.To.Add(MailboxAddress.Parse(request.email));


            var Builder = new BodyBuilder();

            Builder.HtmlBody = request.confirmationLink;
            Email.Body = Builder.ToMessageBody();
            Email.From.Add(new MailboxAddress(mailSettings.DisplayNAme, mailSettings.Email));


            using var Smtp = new SmtpClient();
            Smtp.Timeout = 10000;
            Smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
            Smtp.Authenticate(mailSettings.Email, mailSettings.Password);

            await Smtp.SendAsync(Email);

            Smtp.Disconnect(true);

            return RequestResult<bool>.Success(true);
        }
    }
}

