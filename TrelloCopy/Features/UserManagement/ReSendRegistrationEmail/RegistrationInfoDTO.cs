namespace TrelloCopy.Features.UserManagement.ResendRegistrationEmail;

public class RegistrationInfoDTO
{
    public string Email { get; set; }
    public string Token { get; set; }
    public bool IsRegistered { get; set; }
}