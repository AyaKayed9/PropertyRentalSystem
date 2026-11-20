using PropertyRentalManagementSystem.Application.Features.Properties.DTOs;

namespace PropertyRentalManagementSystem.Application.Features.Properties.Interfaces
{
    public interface IPropertyService
    {
        Task<List<PropertyDto>> GetAllAsync();
        Task<PropertyDto?> GetByIdAsync(int id);
        Task<PropertyDto> CreateAsync(CreatePropertyDto dto);
        Task<bool> UpdateAsync(int id,UpdatePropertyDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
