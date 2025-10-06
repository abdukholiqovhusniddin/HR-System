using Application.Commons;
using Application.Exceptions;
using Application.Features.Employees.Commands;
using Application.Interfaces;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Employees.Handlers;
public class UpdateEmployeeHandler(IEmployeesRepository employeesRepository, IUnitOfWork unitOfWork, IFileService fileService)
    : IRequestHandler<UpdateEmployeeCommand, ApiResponse<Unit>>
{
    private readonly IEmployeesRepository _employeesRepository = employeesRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ApiResponse<Unit>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {

        var updateDto = request.UpdateEmployee;
        var employee = await _employeesRepository.GetById(updateDto.Id)
            ?? throw new NotFoundException("Employee not found");
        if (employee.PhotoUrl is not null && updateDto.Photo is not null)
        {
            employee = updateDto.Adapt(employee);

            await fileService.RemoveAsync(employee.PhotoUrl);

            var photoPath = await fileService.SaveAsync(updateDto.Photo, "Employees");
            employee.Image = new EmployeeFile()
            {
                EmployeeId = updateDto.Id,
                Name = photoPath.Name,
                Url = photoPath.Url,
                Size = photoPath.Size,
                Extension = photoPath.Extension,
            };

            employee.PhotoUrl = photoPath.Url;

        }
        else
        {
            _ = updateDto.Adapt(employee);
        }
        await _unitOfWork.SaveChangesAsync(CancellationToken.None);

        return new ApiResponse<Unit>();
    }
}
