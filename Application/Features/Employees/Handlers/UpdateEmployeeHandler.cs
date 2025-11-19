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
        if (updateDto.ManagerId == Guid.Empty)
            updateDto.ManagerId = null;

        var employee = await _employeesRepository.GetById(updateDto.Id, cancellationToken, false)
            ?? throw new NotFoundException("Employee not found");

        if (updateDto.Photo is not null)
        {
            if (!string.IsNullOrWhiteSpace(employee.PhotoUrl))
                await fileService.RemoveAsync(employee.PhotoUrl);

            var newPhoto = await fileService.SaveAsync(updateDto.Photo, "Employees");

            var fileEntity = await _employeesRepository.GetImgByEmployeeId(updateDto.Id);
            if(fileEntity is null)
            {
                fileEntity = new EmployeeFile
                {
                    EmployeeId = updateDto.Id,
                    Name = newPhoto.Name,
                    Url = newPhoto.Url,
                    Size = newPhoto.Size,
                    Extension = newPhoto.Extension,
                };
                await _employeesRepository.AddPhotoAsync(fileEntity);
            }
            else
            {
                fileEntity.Name = newPhoto.Name;
                fileEntity.Url = newPhoto.Url;
                fileEntity.Size = newPhoto.Size;
                fileEntity.Extension = newPhoto.Extension;
            }
            employee.PhotoUrl = newPhoto.Url;
        }
        updateDto.Adapt(employee);
        await unitOfWork.SaveChangesAsync(CancellationToken.None);

        return new ApiResponse<Unit>();
    }
}