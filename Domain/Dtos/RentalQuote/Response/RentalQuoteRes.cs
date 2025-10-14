using Domain.Constants;

namespace Domain.Dtos.RentalQuote.Response
{
    public class RentalQuoteRes
    {
        public Money Subtotal { get; init; }
        public Money WeekendSurcharge { get; init; }
        public Money Insurance { get; init; }
        public Money DiscountAmount { get; init; }
        public Money Total { get; init; }
    }
}