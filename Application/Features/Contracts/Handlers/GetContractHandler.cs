using Application.Commons;
using Application.Features.Contracts.Queries;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Contracts.Handlers;
public class GetContractHandler(IContractsRepository contractsRepository): IRequestHandler<GetContractQuery,ApiResponse<Contract>>
{
    private readonly IContractsRepository _contractsRepository = contractsRepository;
    public async Task<ApiResponse<Contract>> Handle(GetContractQuery request, CancellationToken cancellationToken)
    {
        var contract = await _contractsRepository.GetByEmployeeId(request.EmployeeId);
        if (contract is null)
            return new ApiResponse<Contract>
            {
                StatusCode = 404,
                Error = "Contract not found"
            };

        return new ApiResponse<Contract>
        {
            Data = contract,
            StatusCode = 200
        };
    }
}
