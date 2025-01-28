using Microsoft.AspNetCore.Mvc;
using OtpNet;
using TrelloCopy.Common;
using TrelloCopy.Common.Views;
using TrelloCopy.Features.UserManagement.LogInUser;

namespace TrelloCopy.Features.UserManagement.TwoFactorAuthLink;

public class Generate2FAQRCodeEndpoint : BaseEndpoint<LogInInfoDTO, string>
{
    public Generate2FAQRCodeEndpoint(BaseEndpointParameters<LogInInfoDTO> parameters) : base(parameters)
    {
    }
    
    [HttpGet]
    public async Task<EndpointResponse<string>> GetLink(LogInInfoDTO viewmodel)
    {
        var secretKey = KeyGeneration.GenerateRandomKey(20);
        var base32Key = Base32Encoding.ToString(secretKey);

        var appName = "UpSkilling-FoodApp-JSB2";

        string otpUrl = $"otpauth://totp/{appName}:{_userInfo.Name}?secret={base32Key}&issuer={appName}";

        return EndpointResponse<string>.Success(otpUrl);
    }
}