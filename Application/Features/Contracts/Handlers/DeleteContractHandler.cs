using Application.Commons;
using Application.Exceptions;
using Application.Features.Contracts.Commands;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Contracts.Handlers;
public class DeleteContractHandler(IContractsRepository contractsRepository,
    IUnitOfWork unitOfWork, IFileService fileService)
    : IRequestHandler<DeleteContractCommand, ApiResponse<Unit>>
{
    private readonly IContractsRepository _contractsRepository = contractsRepository;
    public async Task<ApiResponse<Unit>> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
    {
        Guid contractId = request.ContractId;

        var contract = await _contractsRepository.GetContractById(contractId)
            ?? throw new NotFoundException("Contract not found");

        await fileService.RemoveAsync(contract.DocumentUrl);

        contract.IsAktive = false;
        contract.DocumentUrl = null!;
        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        return new ApiResponse<Unit>();
    }
}