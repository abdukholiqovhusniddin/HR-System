using Application.DTOs.CommonsDto;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.Contract.Requests;
public class UpdateContractDtoRequest : AddAndUpdateContractDto
{
    public Guid ContractId { get; set; }
    public IFormFile? DocumentPdf { get; set; }
}
