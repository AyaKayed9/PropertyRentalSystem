using Microsoft.EntityFrameworkCore;
using PropertyRentalManagementSystem.Application.Features.Properties.DTOs;
using PropertyRentalManagementSystem.Application.Features.Properties.Interfaces;
using PropertyRentalManagementSystem.Domain.Entities;
using PropertyRentalManagementSystem.Infrastructure.Data;


namespace PropertyRentalManagementSystem.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PropertyRentalManagementDBContext _context;

        public PropertyRepository(PropertyRentalManagementDBContext context)
        {
            _context = context;
        }
        public async Task<List<PropertyDto>> GetAllAsync()
        {
            return await _context.Properties.Select(p => new PropertyDto
                        {
                            PropertyId = p.PropertyId,
                            PropertyTypeId = p.PropertyTypeId,
                            Title = p.Title,
                            Description = p.Description,
                            PropertyStatus = p.PropertyStatus,
                            City = p.City,
                            StreetAddress = p.StreetAddress,
                            AreaSize = p.AreaSize,
                            YearBuilt = p.YearBuilt
                        }).AsNoTracking()
                          .ToListAsync();
        }
        public async Task<PropertyDto?> GetByIdAsync(int id)
        {
            return await _context.Properties
                        .Where(p => p.PropertyId == id)
                        .Select(p => new PropertyDto
                        {
                            PropertyId = p.PropertyId,
                            PropertyTypeId = p.PropertyTypeId,
                            Title = p.Title,
                            Description = p.Description,
                            PropertyStatus = p.PropertyStatus,
                            City = p.City,
                            StreetAddress = p.StreetAddress,
                            AreaSize = p.AreaSize,
                            YearBuilt = p.YearBuilt
                        }).AsNoTracking()
                          .FirstOrDefaultAsync();
        }
        public async Task<bool> PropertyTypeExistsAsync(int id)
        {
            return await _context.PropertyTypes.AnyAsync(pt => pt.PropertyTypeId == id);
        }
        public async Task<Property> AddAsync(Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            return property;
        }
        public async Task<Property?> GetEntityByIdAsync(int id)
        {
            return await _context.Properties
                .FirstOrDefaultAsync(p => p.PropertyId == id);

        }
        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();           
        }

        public async Task DeleteAsync(Property property)
        {
            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
        }
    }
}
