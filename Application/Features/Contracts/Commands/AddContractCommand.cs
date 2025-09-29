using Application.Commons;
using Application.DTOs.Contract.Requests;
using MediatR;

namespace Application.Features.Contracts.Commands;
public record AddContractCommand(AddContractDtoRequest AddContractDtoRequest) : IRequest<ApiResponse<Contract>>;
