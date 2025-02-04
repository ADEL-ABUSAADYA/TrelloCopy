using MailKit.Net.Smtp;
using MailKit.Security;
using MediatR;
using Microsoft.Extensions.Options;
using MimeKit;
using TrelloCopy.Common.BaseHandlers;
using TrelloCopy.Common.Views;
using TrelloCopy.Models;

namespace TrelloCopy.Features.Common.Command
{
    public record SendEamilQuary(string email, string name, string confirmationLink) : IRequest<RequestResult<bool>>;

    public class SendEmailQuaryHandler : BaseRequestHandler<SendEamilQuary, RequestResult<bool> , User>
    {

        readonly MailSettings mailSettings;
        public SendEmailQuaryHandler(BaseWithoutRepositoryRequestHandlerParameter<User> parameters, IOptions<MailSettings> _mailSettings) : base(parameters)
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
