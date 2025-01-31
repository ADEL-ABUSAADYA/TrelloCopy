namespace TrelloCopy.Features.UserManagement.SendFrogetPasswordResetEmail;

public record FrogetPasswordInfoDTO(string Name, bool IsEmailConfirmed, string EmailConfirmationToken);