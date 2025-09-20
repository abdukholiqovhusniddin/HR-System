using Application.Commons;
using Application.Features.Employees.Models;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Employees.Queries;
public class GetEmployeeDirectory : IRequest<ApiResponse<List<GetDirectoryDto>>>
{
    public class GetEmployeeDirectoryHandler(IEmployeesRepository directoryRepository) : IRequestHandler<GetEmployeeDirectory, ApiResponse<List<GetDirectoryDto>>>
    {
        private readonly IEmployeesRepository _directoryRepository = directoryRepository;

        public async Task<ApiResponse<List<GetDirectoryDto>>> Handle(GetEmployeeDirectory request, CancellationToken cancellationToken)
        {
            var directoryDto = await _directoryRepository.GetAllDirectory();

            if (directoryDto is null || !directoryDto.Any())
            {
                return new ApiResponse<List<GetDirectoryDto>>
                {
                    StatusCode = 204,
                    Error = "Directory is empty"
                };
            }

            return new ApiResponse<List<GetDirectoryDto>>
            {
                Data = [.. directoryDto.Select(a => new GetDirectoryDto(a.FullName!,
                    a.Position, a.Department))],
                StatusCode = 200
            };
        }
    }

}
