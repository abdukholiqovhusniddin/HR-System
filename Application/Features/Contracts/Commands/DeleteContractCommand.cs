using Application.Commons;
using MediatR;

namespace Application.Features.Contracts.Commands;
public record DeleteContractCommand(Guid ContractId) : IRequest<ApiResponse<Unit>>;
