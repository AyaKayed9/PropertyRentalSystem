using Microsoft.AspNetCore.Mvc;
using PropertyRentalManagementSystem.Application.Features.Properties.DTOs;
using PropertyRentalManagementSystem.Application.Features.Properties.Interfaces;


namespace PropertyRentalManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PropertyDto>> GetPropertyById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Property ID.");
            }

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
        public async Task<ActionResult> CreateProperty([FromBody] CreatePropertyDto dto)
        {

            var property = await _propertyService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetPropertyById),
                                   new { id = property.PropertyId },
                                   property);

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody] UpdatePropertyDto dto)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid Property ID.");
            }

            var isUpdated = await _propertyService.UpdateAsync(id, dto);

            if (!isUpdated)
                return NotFound("Property not found.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            if (id <= 0) 
            {
                return BadRequest("Invalid Property ID.");
            }

            var isDeleted = await _propertyService.DeleteAsync(id);

            if(!isDeleted)
                return NotFound("Property not found.");

            return NoContent();
        }

    }
}
