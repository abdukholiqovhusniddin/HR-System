using Application.Commons;
using MediatR;

namespace Application.Features.Contracts.Queries;
public record GetContractQuery(Guid EmployeeId) : IRequest<ApiResponse<List<Contract>>>; // dto response
