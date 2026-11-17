using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyRentalManagementSystem.Application.Features.Properties.DTOs;
using PropertyRentalManagementSystem.Application.Features.Properties.Interfaces;


namespace PropertyRentalManagementSystem.API.Controllers
{
    [Route("api/Properties")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertiesController(IPropertyService propertyService)                                    
        {
            _propertyService = propertyService;
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PropertyDto>>> GetAllProperties()
        {
            var properties = await _propertyService.GetAllAsync();

            return Ok(properties);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PropertyDto>> GetPropertyById(int id)
        {
            var property = await _propertyService.GetByIdAsync(id);

            if (property == null) 
            {
                return NotFound();
            }

            return Ok(property);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateProperty(CreatePropertyDto dto)
        {
            
            var property = await _propertyService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetPropertyById),
                                   new { id = property.PropertyId },
                                   property);
          
        }

    }
}
