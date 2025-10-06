using Application.Commons;
using Application.DTOs.Contract.Responses;
using MediatR;

namespace Application.Features.Contracts.Queries;
public record GetContractQuery(Guid EmployeeId) : IRequest<ApiResponse<List<ContractDtoResponse>>>; 