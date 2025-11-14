using Application.DTOs.CommonsDto;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Users.Requests;
public class UserRegisterRequestDto : CreateAndUpdateEmployeeDto
{
    public required IFormFile Photo { get; set; }
}