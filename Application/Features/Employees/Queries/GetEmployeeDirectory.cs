using Application.Commons;
using Application.DTOs.Employees.Responses;
using MediatR;

namespace Application.Features.Employees.Queries;
public record GetEmployeeDirectory() : IRequest<ApiResponse<List<ResponseDirectoryDto>>>;