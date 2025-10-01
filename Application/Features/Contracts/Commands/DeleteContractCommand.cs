using Application.Commons;
using Application.DTOs.Contract.Responses;
using MediatR;

namespace Application.Features.Contracts.Commands;
public record DeleteContractCommand(Guid ContractId) : IRequest<ApiResponse<Contract>>;
