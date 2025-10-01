using Application.Commons;
using Application.DTOs.Contract.Responses;
using Application.Exceptions;
using Application.Features.Contracts.Commands;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Contracts.Handlers;
public class DeleteContractHandler(IContractsRepository contractsRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteContractCommand, ApiResponse<Contract>>
{
    private readonly IContractsRepository _contractsRepository = contractsRepository;
    public async Task<ApiResponse<Contract>> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
    {
        Guid contractId = request.ContractId;

        var contract = await _contractsRepository.GetContractById(contractId)
            ?? throw new NotFoundException("Contract not found");

        contract.IsDeleted = true;
        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        return new ApiResponse<Contract>(contract);
    }
}
