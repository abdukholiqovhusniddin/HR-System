using Application.DTOs.Responses;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Interfaces;
using Mapster;
using Application.Features.Employees.Models;
using Application.Commons;
using MediatR;

namespace Application.Service;

public class EmployeesService(IEmployeesRepository directory) : 
{
    private readonly IEmployeesRepository _directoryRepository = directory;

    public async Task<GetEmployeeDto> GetById(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new ApiException("Id is empty");

        var employee = await _directoryRepository.GetById(id);

        return employee is null ? throw new NotFoundException("Employee not found")
            : employee.Adapt<GetEmployeeDto>();
    }

    public async Task<List<GetDirectoryDto>> GetDirectory(CancellationToken cancellationToken)
    {
        var directoryDto = await _directoryRepository.GetAllDirectory();

        return [.. directoryDto.Select(a => new GetDirectoryDto(a.FullName!,
            a.Position, a.Department))];
    }
}

//public sealed record GetEmployees : IRequest<ApiResponse<List<GetDirectoryDto>>>;

//public sealed class GetEmployeesHandler(IEmployeesRepository employeesRepository) : IRequestHandler<GetEmployees, ApiResponse<List<GetDirectoryDto>>>
//{
//    private readonly IEmployeesRepository _employeesRepository = employeesRepository;

//    public async Task<ApiResponse<List<GetDirectoryDto>>> GetDirectory(GetEmployees request, CancellationToken cancellationToken)
//    {
//        var directory = await _employeesRepository.GetAllDirectory();
//    }
//}
