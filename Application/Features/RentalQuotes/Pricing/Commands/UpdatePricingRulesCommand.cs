using MediatR;

namespace Application.Features.RentalQuotes.Pricing.Commands
{
    public sealed record UpdatePricingRulesCommand(
    decimal? BasePricePerDay,
    decimal? WeekendSurchargePerDay,
    decimal? InsurancePerDay,
    decimal? DefaultDiscount
    ) : IRequest<Unit>;
}