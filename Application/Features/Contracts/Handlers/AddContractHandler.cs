using Application.Commons;
using Application.Exceptions;
using Application.Features.Contracts.Commands;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Contracts.Handlers;

public class AddContractHandler(IContractsRepository contractsRepository) : IRequestHandler<AddContractCommand, ApiResponse<Contract>>
{
    private readonly IContractsRepository _contractsRepository = contractsRepository;
    public async Task<ApiResponse<Contract>> Handle(AddContractCommand request, CancellationToken cancellationToken)
    {
        var contract = request.AddContractDtoRequest;
        if (await _contractsRepository.ExistsAsync(contract.EmployeeId))
            throw new ApiException("Contract already exists for this Employee");

        var newContract = new Contract
        {

        };
        //var newContract = new Contract
        //{
        //    EmployeeId = request.AddContractDtoRequest.EmployeeId,
        //    ContractType = request.AddContractDtoRequest.ContractType,
        //    StartDate = request.AddContractDtoRequest.StartDate,
        //    EndDate = request.AddContractDtoRequest.EndDate,
        //    Terms = request.AddContractDtoRequest.Terms,
        //    DocumentUrl = request.AddContractDtoRequest.DocumentUrl,
        //    DocumentName = request.AddContractDtoRequest.DocumentName,
        //    DocumentType = request.AddContractDtoRequest.DocumentType,
        //    CreatedAt = DateTime.UtcNow
        //};
        //var addedContract = await _contractsRepository.AddAsync(newContract);
        //return new ApiResponse<Contract>
        //{
        //    Data = addedContract,
        //    StatusCode = 201
        //};
    }
}