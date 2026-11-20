namespace PropertyRentalManagementSystem.Application.Features.Properties.DTOs
{
    public class PropertyDto
    {
        public int PropertyId { get; set; }
        public int PropertyTypeId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string PropertyStatus { get; set; } = null!;
        public string City { get; set; } = null!;
        public string StreetAddress { get; set; } = null!;
        public decimal AreaSize { get; set; }
        public short? YearBuilt { get; set; }

    }
}
