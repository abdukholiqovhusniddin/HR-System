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
        var contractDto = request.UpdateContractDtoRequest;

        var updateContract = await _contractsRepository.GetContractById(contractDto.ContractId)
            ?? throw new NotFoundException("Contract not found");

        if (contractDto.DocumentPdf is not null)
        {
            await fileService.RemoveAsync(updateContract.DocumentUrl);

            updateContract = contractDto.Adapt(updateContract);


            var documentPath = await fileService.SaveAsync(contractDto.DocumentPdf, "Contracts");

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
            updateContract = contractDto.Adapt(updateContract);
        }

        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        return new ApiResponse<Guid>(updateContract.EmployeeId);
    }
}