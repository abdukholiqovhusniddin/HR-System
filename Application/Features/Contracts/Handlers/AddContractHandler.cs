using Application.Commons;
using Application.Features.Contracts.Commands;
using Application.Interfaces;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Contracts.Handlers;

public class AddContractHandler(IContractsRepository contractsRepository,
    IFileService fileService, IUnitOfWork unitOfWork) : IRequestHandler<AddContractCommand, ApiResponse<Contract>>
{
    private readonly IContractsRepository _contractsRepository = contractsRepository;
    public async Task<ApiResponse<Contract>> Handle(AddContractCommand request, CancellationToken cancellationToken)
    {
        var contract = request.AddContractDtoRequest;

        var newContract = new Contract
        {
            Id = Guid.NewGuid(),
            EmployeeId = contract.EmployeeId,
            ContractType = contract.ContractType,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            Terms = contract.Terms
        };

        if (contract.DocumentPdf is not null)
        {
            var documentPath = await fileService.SaveAsync(contract.DocumentPdf, "Contracts");

            newContract.DocumentPdf = new ContractFile()
            {
                ContractId = newContract.Id,
                Name = documentPath.Name,
                Url = documentPath.Url,
                Size = documentPath.Size,
                Extension = documentPath.Extension,
            };

            newContract.DocumentUrl = documentPath.Url;
        }


        await _contractsRepository.CreateAsync(newContract);

        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        return new ApiResponse<Contract>
        {
            Data = newContract,
            StatusCode = 201
        };
    }
}