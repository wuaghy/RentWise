using MediatR;

namespace Application.Features.RentalQuotes.Queries
{
    public sealed record CalculateRentalQuoteQuery(
    DateOnly StartDate,
    int Days,
    string Category,
    bool IncludeInsurance,
    decimal? Discount
) : IRequest<CalculateRentalQuoteVm>;

    public sealed class CalculateRentalQuoteVm
    {
        public decimal Subtotal { get; init; }
        public decimal WeekendSurcharge { get; init; }
        public decimal Insurance { get; init; }
        public decimal DiscountAmount { get; init; }
        public decimal Total { get; init; }
    }
}