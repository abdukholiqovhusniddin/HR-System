using Application.Commons;
using Application.DTOs.Employees.Responses;
using Application.Exceptions;
using Application.Features.Employees.Queries;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Employees.Handlers;

public class GetEmployeeDirectoryHandler(IEmployeesRepository directoryRepository) 
    : IRequestHandler<GetEmployeeDirectory, ApiResponse<List<ResponseDirectoryDto>>>
{
    private readonly IEmployeesRepository _directoryRepository = directoryRepository;

    public async Task<ApiResponse<List<ResponseDirectoryDto>>> Handle(GetEmployeeDirectory request, CancellationToken cancellationToken)
    {
        var directoryDto = await _directoryRepository.GetAllDirectory();

        return directoryDto is null
            ? throw new NotFoundException("Directory not found")
            : new ApiResponse<List<ResponseDirectoryDto>>
        {
            Data = [.. directoryDto.Select(a => new ResponseDirectoryDto(a.FullName!,
                a.Position, a.Department))]
        };
    }
}
