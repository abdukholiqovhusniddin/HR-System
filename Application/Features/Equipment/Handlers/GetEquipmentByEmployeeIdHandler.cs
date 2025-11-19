using Application.Commons;
using Application.DTOs.Equipments.Responses;
using Application.Exceptions;
using Application.Features.Equipment.Queries;
using Domain.Interfaces;
using Mapster;
using MediatR;

namespace Application.Features.Equipment.Handlers;
public class GetEquipmentByEmployeeIdHandler(IEquipmentRepository equipmentRepository)
    : IRequestHandler<GetEquipmentByEmployeeIdQuery, ApiResponse<List<EquipmentDtoResponse>>>
{
    private readonly IEquipmentRepository _equipmentRepository = equipmentRepository;

    public async Task<ApiResponse<List<EquipmentDtoResponse>>> Handle(GetEquipmentByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        Guid employeeId = request.EmployeeId;
        if (employeeId == Guid.Empty)
            throw new ApiException("EmployeeId is required");

        var equipments = await _equipmentRepository.GetEquipmentByEmployeeId(employeeId, cancellationToken)
            ?? throw new ApiException("No equipment found for the given employee ID.");

        var response = equipments.Adapt<List<EquipmentDtoResponse>>();

        return new ApiResponse<List<EquipmentDtoResponse>>(response);
    }
}