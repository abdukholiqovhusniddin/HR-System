using Application.Commons;
using Application.Exceptions;
using Application.Features.Contracts.Queries;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Contracts.Handlers;
public class GetContractHandler(IContractsRepository contractsRepository): IRequestHandler<GetContractQuery,ApiResponse<Contract>>
{
    private readonly IContractsRepository _contractsRepository = contractsRepository;
    public async Task<ApiResponse<Contract>> Handle(GetContractQuery request, CancellationToken cancellationToken)
    {
        if (request.EmployeeId == Guid.Empty)
            throw new ApiException("Id is empty");

        var contract = await _contractsRepository.GetByEmployeeId(request.EmployeeId);

        return contract is null
            ? throw new NotFoundException("Contract not found")
            : new ApiResponse<Contract>(contract);
    }
}
