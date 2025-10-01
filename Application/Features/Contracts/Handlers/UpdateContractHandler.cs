using Application.Commons;
using Application.Exceptions;
using Application.Features.Contracts.Commands;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Contracts.Handlers;
public class UpdateContractHandler(IContractsRepository contractsRepository)
    : IRequestHandler<UpdateContractCommon, ApiResponse<Contract>>
{
    private readonly IContractsRepository _contractsRepository = contractsRepository;

    public async Task<ApiResponse<Contract>> Handle(UpdateContractCommon request, CancellationToken cancellationToken)
    {
        var contract = request.UpdateContractDtoRequest;
        var updateContract = await _contractsRepository.GetContractById(contract.ContractId)
            ?? throw new NotFoundException("Contract not found");

        updateContract = contract.Adapt(updateContract);

        await _contractsRepository.UpdateAsync(updateContract);

        return new ApiResponse<Contract>(updateContract);
    }
}
