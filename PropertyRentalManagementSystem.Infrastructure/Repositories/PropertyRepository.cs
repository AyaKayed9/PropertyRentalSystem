using Microsoft.EntityFrameworkCore;
using PropertyRentalManagementSystem.Application.Features.Properties.DTOs;
using PropertyRentalManagementSystem.Application.Features.Properties.Interfaces;
using PropertyRentalManagementSystem.Domain.Entities;
using PropertyRentalManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
                        }).ToListAsync();
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
                        }).FirstOrDefaultAsync();
        }
        public async Task<bool> PropertyTypeExistsAsync(int id)
        {
            return await _context.PropertyTypes.AnyAsync(pt => pt.PropertyTypeId == id);
        }
        public async Task<Property> AddAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();

            return property;
        }

    }
}
