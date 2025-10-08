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
    public async Task<ApiResponse<Unit>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {

        var updateDto = request.UpdateEmployee;

        if (updateDto.ManagerId.ToString() == "3fa85f64-5717-4562-b3fc-2c963f66afa6")
            updateDto.ManagerId = null;

        var employee = await _employeesRepository.GetById(updateDto.Id)
            ?? throw new NotFoundException("Employee not found");

        if (updateDto.Photo is not null)
        {
            employee = updateDto.Adapt(employee);

            var photoPath = await fileService.SaveAsync(updateDto.Photo, "Employees");

            if (employee.PhotoUrl is not null)
            {
                await fileService.RemoveAsync(employee.PhotoUrl);

                var getPhoto = await _employeesRepository.GetImgByEmployeeId(updateDto.Id);

                getPhoto.Name = photoPath.Name;
                getPhoto.Url = photoPath.Url;
                getPhoto.Size = photoPath.Size;
                getPhoto.Extension = photoPath.Extension;

            }
            else
            {
                var getPhoto = new EmployeeFile
                {
                    EmployeeId = updateDto.Id,
                    Name = photoPath.Name,
                    Url = photoPath.Url,
                    Size = photoPath.Size,
                    Extension = photoPath.Extension
                };
            }
            employee.PhotoUrl = photoPath.Url;

        }
        else
        {
            _ = updateDto.Adapt(employee);
        }
        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        return new ApiResponse<Unit>();
    }
}