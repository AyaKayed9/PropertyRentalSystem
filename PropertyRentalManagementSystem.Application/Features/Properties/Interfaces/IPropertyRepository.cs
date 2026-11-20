
using PropertyRentalManagementSystem.Application.Features.Properties.DTOs;
using PropertyRentalManagementSystem.Domain.Entities;


namespace PropertyRentalManagementSystem.Application.Features.Properties.Interfaces
{
    public interface IPropertyRepository
    {
        Task<List<PropertyDto>> GetAllAsync();
        Task<PropertyDto?> GetByIdAsync(int id);
        Task<Property> AddAsync(Property property);
        Task<bool> PropertyTypeExistsAsync(int id);
        Task<Property?> GetEntityByIdAsync(int id);
        Task UpdateAsync();
        Task DeleteAsync(Property property);
    }
}
