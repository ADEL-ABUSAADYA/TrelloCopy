namespace TrelloCopy.Features.UserManagement.GetAllUsers;

public record UserResponseViewModel
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string PhoneNo { get; set; }
    public bool IsActive { get; set; }
}
    