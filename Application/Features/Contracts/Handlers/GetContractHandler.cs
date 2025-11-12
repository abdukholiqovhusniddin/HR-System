using Application.Commons;
using Application.DTOs.Contract.Responses;
using Application.Exceptions;
using Application.Features.Contracts.Queries;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Contracts.Handlers;
public class GetContractHandler(IContractsRepository contractsRepository) : IRequestHandler<GetContractQuery, ApiResponse<List<ContractDtoResponse>>>
{
    private readonly IContractsRepository _contractsRepository = contractsRepository;
    public async Task<ApiResponse<List<ContractDtoResponse>>> Handle(GetContractQuery request, CancellationToken cancellationToken)
    {
        if (request.EmployeeId == Guid.Empty)
            throw new ApiException("EmployeeId cannot be empty.");

        var contracts = await _contractsRepository.GetAllByEmployeeIdAsync(request.EmployeeId, cancellationToken);

        if (contracts is null || contracts.Count == 0)
            throw new NotFoundException("No active contracts found for this employee.");

        var contractDtos = contracts.Adapt<List<ContractDtoResponse>>();

        return new ApiResponse<List<ContractDtoResponse>>(contractDtos);
    }
}