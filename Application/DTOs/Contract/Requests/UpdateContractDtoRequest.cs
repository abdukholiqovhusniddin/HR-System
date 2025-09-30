using Application.DTOs.CommonsDto;

namespace Application.DTOs.Contract.Requests;
public class UpdateContractDtoRequest: AddAndUpdateContractDto
{
    public Guid ContractId { get; set; }
}
