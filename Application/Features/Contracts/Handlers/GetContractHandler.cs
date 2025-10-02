using Application.Commons;
using Application.Exceptions;
using Application.Features.Contracts.Queries;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Contracts.Handlers;
public class GetContractHandler(IContractsRepository contractsRepository): IRequestHandler<GetContractQuery,ApiResponse<List<Contract>>>
{
    private readonly IContractsRepository _contractsRepository = contractsRepository;
    public async Task<ApiResponse<List<Contract>>> Handle(GetContractQuery request, CancellationToken cancellationToken)
    {
        if (request.EmployeeId == Guid.Empty)
            throw new ApiException("Id is empty");

        var contract = await _contractsRepository.GetByEmployeeId(request.EmployeeId);

        return contract is null || contract.Count == 0
            ? throw new NotFoundException("Contract not found")
            : new ApiResponse<List<Contract>>(contract);
    }
}
