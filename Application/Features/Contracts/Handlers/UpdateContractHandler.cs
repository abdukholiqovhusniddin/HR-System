using Application.Commons;
using Application.Exceptions;
using Application.Features.Contracts.Commands;
using Application.Interfaces;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Contracts.Handlers;
public class UpdateContractHandler(IContractsRepository contractsRepository, IFileService fileService, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateContractCommon, ApiResponse<Guid>>
{
    private readonly IContractsRepository _contractsRepository = contractsRepository;

    public async Task<ApiResponse<Guid>> Handle(UpdateContractCommon request, CancellationToken cancellationToken)
    {
        var contract = request.UpdateContractDtoRequest;

        var updateContract = await _contractsRepository.GetContractById(contract.ContractId)
            ?? throw new NotFoundException("Contract not found");

        if (contract.DocumentPdf is not null && updateContract.DocumentUrl is not null)
        {
            await fileService.RemoveAsync(updateContract.DocumentUrl);

            updateContract = contract.Adapt(updateContract);


            var documentPath = await fileService.SaveAsync(contract.DocumentPdf, "Contracts");

            if (updateContract.DocumentPdf is null)
            {
                updateContract.DocumentPdf = new List<ContractFile>
                {
                    new ContractFile
                    {
                        ContractId = updateContract.Id,
                        Name = documentPath.Name,
                        Url = documentPath.Url,
                        Size = documentPath.Size,
                        Extension = documentPath.Extension,
                    }
                };

            }
            else
            {
                updateContract.DocumentPdf.Clear();

                updateContract.DocumentPdf = new List<ContractFile>
                {
                    new ContractFile
                    {
                        ContractId = updateContract.Id,
                        Name = documentPath.Name,
                        Url = documentPath.Url,
                        Size = documentPath.Size,
                        Extension = documentPath.Extension,
                    }
                };
            }

            updateContract.DocumentUrl = documentPath.Url;
        }
        else
        {
            updateContract = contract.Adapt(updateContract);
        }

        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        return new ApiResponse<Guid>(updateContract.EmployeeId);
    }
}