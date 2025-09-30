using Application.Commons;
using Application.Exceptions;
using Application.Features.Contracts.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Contracts.Handlers;
public class UpdateContractHandler(IContractsRepository contractsRepository)
    : IRequestHandler<UpdateContractCommon, ApiResponse<Contract>>
{
    private readonly IContractsRepository _contractsRepository = contractsRepository;

    public async Task<ApiResponse<Contract>> Handle(UpdateContractCommon request, CancellationToken cancellationToken)
    {
        var updateContract = await _contractsRepository.ExistAsync(request.UpdateContractDtoRequest)
            ?? throw new NotFoundException("Contract or Employee not found");

        await _contractsRepository.UpdateAsync(updateContract);

        return new ApiResponse<Contract>(updateContract);
    }
}
