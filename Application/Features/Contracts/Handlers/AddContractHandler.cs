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
        var dto = request.AddContractDtoRequest;

        if (!await _contractsRepository.IsEmployeeActive(dto.EmployeeId, cancellationToken))
            throw new ApiException("Employee is not active or does not exist.");

        var newContract = new Contract
        {
            Id = Guid.NewGuid(),
            EmployeeId = dto.EmployeeId,
            ContractType = dto.ContractType,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Terms = dto.Terms,
            DocumentPdf = new List<ContractFile>(),
            DocumentUrl = null!
        };

        if (dto.DocumentPdf is not null && dto.DocumentPdf.Length > 0)
        {
            var file = await fileService.SaveAsync(dto.DocumentPdf, "Contracts");

            newContract.DocumentPdf.Add(new ContractFile
            {
                ContractId = newContract.Id,
                Name = file.Name,
                Url = file.Url,
                Size = file.Size,
                Extension = file.Extension,
            });

            newContract.DocumentUrl = file.Url;
        }


        await _contractsRepository.CreateAsync(newContract);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = newContract.Adapt<ContractDtoResponse>();

        return new ApiResponse<ContractDtoResponse>
        {
            Data = response,
            StatusCode = 201
        };
    }
}