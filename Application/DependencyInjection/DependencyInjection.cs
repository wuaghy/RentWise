using Microsoft.Extensions.DependencyInjection;
using Application.Abstractions;
using Application.Features.RentalQuotes.Services;
using Domain.Entities;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton(new RentalPricingRules());
        services.AddScoped<IRentalQuoteService, RentalQuoteService>();
        return services;
    }
}