using System.ComponentModel.DataAnnotations;
using Application.DTOs.CommonsDto;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Contract.Requests;
public class AddContractDtoRequest: AddAndUpdateContractDto
{
    public required Guid EmployeeId { get; set; }
}
