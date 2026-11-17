using PropertyRentalManagementSystem.Application.Features.Properties.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyRentalManagementSystem.Application.Features.Properties.Interfaces
{
    public interface IPropertyService
    {
        Task<List<PropertyDto>> GetAllAsync();
        Task<PropertyDto?> GetByIdAsync(int id);
        Task<PropertyDto> CreateAsync(CreatePropertyDto dto);        
    }
}
