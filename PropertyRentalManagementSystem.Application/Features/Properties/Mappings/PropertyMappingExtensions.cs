using PropertyRentalManagementSystem.Application.Features.Properties.DTOs;
using PropertyRentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyRentalManagementSystem.Application.Features.Properties.Mappings
{
    public static class PropertyMappingExtensions
    {
        public static Property ToEntity(this CreatePropertyDto dto)
        {
            return new Property
            {
                PropertyTypeId = dto.PropertyTypeId,
                Title = dto.Title,
                Description = dto.Description,
                PropertyStatus = "Available",
                City = dto.City,
                StreetAddress = dto.StreetAddress,
                AreaSize = dto.AreaSize,
                YearBuilt = dto.YearBuilt
            };
        }

        public static PropertyDto ToDto(this Property property)
        {
            return new PropertyDto
            {
                PropertyId = property.PropertyId,
                PropertyTypeId = property.PropertyTypeId,
                Title = property.Title,
                Description = property.Description,
                PropertyStatus = property.PropertyStatus,
                City = property.City,
                StreetAddress = property.StreetAddress,
                AreaSize = property.AreaSize,
                YearBuilt = property.YearBuilt
            };
        }
    }
}
