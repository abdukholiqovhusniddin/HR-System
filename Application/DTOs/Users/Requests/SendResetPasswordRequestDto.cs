namespace Application.DTOs.Users.Requests;
public class SendResetPasswordRequestDto
{
    public required Guid UserId { get; set; }
}
