using FluentValidation;
using PropertyRentalManagementSystem.Application.Features.Properties.DTOs;
using PropertyRentalManagementSystem.Application.Features.Properties.Interfaces;
using PropertyRentalManagementSystem.Application.Features.Properties.Mappings;

namespace PropertyRentalManagementSystem.Application.Features.Properties.Services
{
    public class PropertyService : IPropertyService
    {     
        private readonly IPropertyRepository _propertyRepository;
        private readonly IValidator<CreatePropertyDto> _createValidator;
        private readonly IValidator<UpdatePropertyDto> _updateValidator;

        public PropertyService(IPropertyRepository propertyRepository,
                                IValidator<CreatePropertyDto> createValidator,
                                IValidator<UpdatePropertyDto> updateValidator)
        {
            _propertyRepository = propertyRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
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
           
            var validationResult = await _createValidator.ValidateAsync(dto);

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
        public async Task<bool> UpdateAsync(int id, UpdatePropertyDto dto)
        {
            var property = await _propertyRepository.GetEntityByIdAsync(id);

            if (property == null)
            {
                return false;
            }

            var validationResult = await _updateValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            if (!await _propertyRepository.PropertyTypeExistsAsync(dto.PropertyTypeId))
            {
                throw new ArgumentException("Invalid Property Type.");
            }

            dto.UpdateEntity(property);

            await _propertyRepository.UpdateAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var property = await _propertyRepository.GetEntityByIdAsync(id);

            if(property == null)
            {
                return false;
            }

            await _propertyRepository.DeleteAsync(property);

            return true;
        }
    }
}
