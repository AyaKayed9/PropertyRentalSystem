using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PropertyRentalManagementSystem.API.Middleware;
using PropertyRentalManagementSystem.Application.Features.Properties.Interfaces;
using PropertyRentalManagementSystem.Application.Features.Properties.Services;
using PropertyRentalManagementSystem.Application.Features.Properties.Validators;
using PropertyRentalManagementSystem.Infrastructure.Data;
using PropertyRentalManagementSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddValidatorsFromAssemblyContaining<CreatePropertyValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PropertyRentalManagementDBContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();

builder.Services.AddScoped<IPropertyService, PropertyService>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
