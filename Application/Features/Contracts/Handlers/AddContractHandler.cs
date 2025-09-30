using Application.Commons;
using Application.Exceptions;
using Application.Features.Contracts.Commands;
using Application.Interfaces;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Contracts.Handlers;

public class AddContractHandler(IContractsRepository contractsRepository,
    IFileService fileService) : IRequestHandler<AddContractCommand, ApiResponse<Contract>>
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
            var documentPath = await fileService.SaveAsync(contract.DocumentPdf);

            newContract.DocumentPdf = new ContractFile()
            {
                ContractId = contract.EmployeeId,
                Name = documentPath.Name,
                Url = documentPath.Url,
                Size = documentPath.Size,
                Extension = documentPath.Extension,

            };

            newContract.DocumentUrl = documentPath.Url;
        }


        return new ApiResponse<Contract>
        {
            Data = newContract,
            StatusCode = 201
        };
    }
}