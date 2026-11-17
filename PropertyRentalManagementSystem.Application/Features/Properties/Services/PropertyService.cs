using FluentValidation;
using PropertyRentalManagementSystem.Application.Features.Properties.DTOs;
using PropertyRentalManagementSystem.Application.Features.Properties.Interfaces;
using PropertyRentalManagementSystem.Application.Features.Properties.Mappings;
using PropertyRentalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PropertyRentalManagementSystem.Application.Features.Properties.Services
{
    public class PropertyService : IPropertyService
    {     
        private readonly IPropertyRepository _propertyRepository;
        private readonly IValidator<CreatePropertyDto> _validator;

        public PropertyService(IPropertyRepository propertyRepository, IValidator<CreatePropertyDto> validator)
        {
            _propertyRepository = propertyRepository;
            _validator = validator;
        }     

        public Task<List<PropertyDto>> GetAllAsync()
        {
            return _propertyRepository.GetAllAsync();
        }
        public Task<PropertyDto?> GetByIdAsync(int id)
        {
            return _propertyRepository.GetByIdAsync(id);
        }        
        public async Task<PropertyDto> CreateAsync(CreatePropertyDto dto)
        {
           
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            if (!await _propertyRepository.PropertyTypeExistsAsync(dto.PropertyTypeId))
            {
                throw new ArgumentException("Invalid Property Type.");
            }

            var property = dto.ToEntity();


            await _propertyRepository.AddAsync(property);

            return property.ToDto();

        }       
    }
}
