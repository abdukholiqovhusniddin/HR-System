using Application.Commons;
using Application.DTOs.Contract.Responses;
using Application.Exceptions;
using Application.Features.Contracts.Queries;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Contracts.Handlers;
public class GetContractHandler(IContractsRepository contractsRepository): IRequestHandler<GetContractQuery,ApiResponse<List<ContractDtoResponse>>>
{
    private readonly IContractsRepository _contractsRepository = contractsRepository;
    public async Task<ApiResponse<List<ContractDtoResponse>>> Handle(GetContractQuery request, CancellationToken cancellationToken)
    {
        if (request.EmployeeId == Guid.Empty)
            throw new ApiException("Id is empty");

        var contract = await _contractsRepository.GetByEmployeeId(request.EmployeeId)
            ?? throw new NotFoundException("Contract not found");

        if(contract.Count == 0)
            throw new NotFoundException("Contract not found");

        var response = contract.Adapt<List<ContractDtoResponse>>();

        return new ApiResponse<List<ContractDtoResponse>>(response);
    }
}
