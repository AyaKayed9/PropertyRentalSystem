using PropertyRentalManagementSystem.Application.Features.Properties.DTOs;
using PropertyRentalManagementSystem.Domain.Entities;

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

        public static void UpdateEntity(this UpdatePropertyDto dto, Property property)
        {
            property.PropertyTypeId = dto.PropertyTypeId;
            property.Title = dto.Title;
            property.Description = dto.Description;
            property.City = dto.City;
            property.StreetAddress = dto.StreetAddress;
            property.AreaSize = dto.AreaSize;
            property.YearBuilt = dto.YearBuilt;
        }
    }
}
