using Application.Commons;
using Application.DTOs.Contract.Responses;
using Application.Exceptions;
using Application.Features.Contracts.Commands;
using Application.Interfaces;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Contracts.Handlers;

public class AddContractHandler(IContractsRepository contractsRepository,
    IFileService fileService, IUnitOfWork unitOfWork) : IRequestHandler<AddContractCommand, ApiResponse<ContractDtoResponse>>
{
    private readonly IContractsRepository _contractsRepository = contractsRepository;
    public async Task<ApiResponse<ContractDtoResponse>> Handle(AddContractCommand request, CancellationToken cancellationToken)
    {
        var contract = request.AddContractDtoRequest;

        bool isAktive = await _contractsRepository.IsEmployeeAktive(contract.EmployeeId);
        if (!isAktive)
            throw new ApiException("Employee is not aktive or not exist");

        var newContract = new Contract
        {
            Id = Guid.NewGuid(),
            EmployeeId = contract.EmployeeId,
            ContractType = contract.ContractType,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            Terms = contract.Terms,
            DocumentPdf = null!,
            DocumentUrl = null!
        };

        if (contract.DocumentPdf is not null)
        {
            var documentPath = await fileService.SaveAsync(contract.DocumentPdf, "Contracts");

            newContract.DocumentPdf = new List<ContractFile>
            {
                new ContractFile
                {
                    ContractId = newContract.Id,
                    Name = documentPath.Name,
                    Url = documentPath.Url,
                    Size = documentPath.Size,
                    Extension = documentPath.Extension,
                }
            };

            newContract.DocumentUrl = documentPath.Url;
        }


        await _contractsRepository.CreateAsync(newContract);

        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        var response = newContract.Adapt<ContractDtoResponse>();

        return new ApiResponse<ContractDtoResponse>
        {
            Data = response,
            StatusCode = 201
        };
    }
}