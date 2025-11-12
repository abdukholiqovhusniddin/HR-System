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
        var contract = await _contractsRepository.GetContractById(request.ContractId, cancellationToken)
            ?? throw new NotFoundException("Contract not found");

        await fileService.RemoveAsync(contract.DocumentUrl);
        contract.IsAktive = false;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<Unit>
        {
            Data = Unit.Value,
            StatusCode = 204
        };
    }
}