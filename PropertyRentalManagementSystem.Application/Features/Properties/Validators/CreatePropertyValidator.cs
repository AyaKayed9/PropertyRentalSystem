using FluentValidation;
using PropertyRentalManagementSystem.Application.Features.Properties.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyRentalManagementSystem.Application.Features.Properties.Validators
{
    public class CreatePropertyValidator : AbstractValidator<CreatePropertyDto>
    {
        public CreatePropertyValidator()
        {
            RuleFor(property => property.Title)
                   .NotEmpty()
                   .WithMessage("Property title is required.")
                   .MaximumLength(150)
                   .WithMessage("Property title cannot exceed 150 characters.");


            RuleFor(property => property.AreaSize)
                   .GreaterThan(0)
                   .WithMessage("Area size must be greater than zero.");

            RuleFor(property=> property.YearBuilt)
                   .LessThanOrEqualTo((short)DateTime.Now.Year)
                   .When(property => property.YearBuilt.HasValue)
                   .WithMessage("Year built cannot be in the future.");
        }

    }
}
