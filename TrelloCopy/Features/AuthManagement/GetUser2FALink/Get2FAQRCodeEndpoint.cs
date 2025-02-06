using Microsoft.AspNetCore.Mvc;
using OtpNet;
using TrelloCopy.Common.BaseEndpoints;
using TrelloCopy.Common.Data.Enums;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.Common.Users.Queries;

namespace TrelloCopy.Features.AuthManagement.GetUser2FALink;

public class Get2FAQRCodeEndpoint : BaseEndpoint<int, string>
{
    public Get2FAQRCodeEndpoint(BaseEndpointParameters<int> parameters) : base(parameters)
    {
    }
    
    //[HttpGet]
    //public  async Task<EndpointResponse<string>> GetLink()
    //{
    //    var user2FADataCommand =await _mediator.Send(new GetUser2FAInfoQuery());
    //    if (!user2FADataCommand.isSuccess)
    //        return EndpointResponse<string>.Failure(user2FADataCommand.errorCode, user2FADataCommand.message);

    //    if (!user2FADataCommand.data.Is2FAEnabled || user2FADataCommand.data.TwoFactorAuthSecretKey is null)
    //        return EndpointResponse<string>.Failure(ErrorCode.Uasr2FAIsNotEnabled, "please activate 2FA first");
        
    //    var appName = "UpSkilling-FoodApp-JSB2";
    //    string otpUrl = $"otpauth://totp/{appName}:{_userInfo.ID}?secret={user2FADataCommand.data.TwoFactorAuthSecretKey}&issuer={appName}";

    //    return EndpointResponse<string>.Success(otpUrl);
    //}
}