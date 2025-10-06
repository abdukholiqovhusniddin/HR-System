using Application.Commons;
using Application.DTOs.Contract.Requests;
using Application.DTOs.Contract.Responses;
using MediatR;

namespace Application.Features.Contracts.Commands;
public record AddContractCommand(AddContractDtoRequest AddContractDtoRequest) : IRequest<ApiResponse<ContractDtoResponse>>; 
