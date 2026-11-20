
using FluentValidation;
using PropertyRentalManagementSystem.Application.Features.Properties.DTOs;

namespace PropertyRentalManagementSystem.Application.Features.Properties.Validators
{
    public class UpdatePropertyValidator : AbstractValidator<UpdatePropertyDto>
    {
        public UpdatePropertyValidator()
        {
            RuleFor(property => property.PropertyTypeId)
                .GreaterThan(0)
                .WithMessage("Invalid property type ID.");

            RuleFor(property => property.Title)
                .NotEmpty()
                .WithMessage("Property title is required.")
                .MaximumLength(150)
                .WithMessage("Property title cannot exceed 150 characters.");

            RuleFor(property => property.Description)
                .MaximumLength(1000)
                .WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(property => property.City)
                .NotEmpty()
                .WithMessage("City is required.")
                .MaximumLength(50)
                .WithMessage("City cannot exceed 100 characters.");

            RuleFor(property => property.StreetAddress)
                .NotEmpty()
                .WithMessage("Street address is required.")
                .MaximumLength(200)
                .WithMessage("Street address cannot exceed 250 characters.");

            RuleFor(property => property.AreaSize)
                .GreaterThan(0)
                .WithMessage("Area size must be greater than zero.");

            RuleFor(property => property.YearBuilt)
                .GreaterThan((short)0)
                .WithMessage("Year built must be greater than zero.")
                .LessThanOrEqualTo((short)DateTime.Now.Year)
                .WithMessage("Year built cannot be in the future.")
                .When(property => property.YearBuilt.HasValue);
        }
    }
}
