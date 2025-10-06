using Application.Commons;
using Application.DTOs.Contract.Requests;
using MediatR;

namespace Application.Features.Contracts.Commands;
public record UpdateContractCommon(UpdateContractDtoRequest UpdateContractDtoRequest): 
    IRequest<ApiResponse<Guid>>; /// IDContract
