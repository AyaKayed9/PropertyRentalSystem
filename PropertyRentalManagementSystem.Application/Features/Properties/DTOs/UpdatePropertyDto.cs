namespace PropertyRentalManagementSystem.Application.Features.Properties.DTOs
{
    public class UpdatePropertyDto
    {       
        public int PropertyTypeId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }       
        public string City { get; set; } = null!;
        public string StreetAddress { get; set; } = null!;
        public decimal AreaSize { get; set; }
        public short? YearBuilt { get; set; }
    }
}
