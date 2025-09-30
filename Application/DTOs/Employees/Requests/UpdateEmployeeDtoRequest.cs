using System.ComponentModel.DataAnnotations;
using Application.DTOs.CommonsDto;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Employees.Requests;
public class UpdateEmployeeDtoRequest: CreateAndUpdateEmployeeDto
{
    public required Guid Id { get; set; }
}