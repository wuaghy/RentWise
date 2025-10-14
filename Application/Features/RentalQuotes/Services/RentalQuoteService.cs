using Application.Abstractions;
using Domain.Constants;
using Domain.Dtos.RentalQuote.Request;
using Domain.Dtos.RentalQuote.Response;
using Domain.Entities;

namespace Application.Features.RentalQuotes.Services
{
    public sealed class RentalQuoteService : IRentalQuoteService
    {
        private readonly RentalPricingRules _rules;

        public RentalQuoteService(RentalPricingRules rules) => _rules = rules;

        public RentalQuoteRes Calculate(RentalQuoteReq req)
        {
            if (req.Days <= 0)
                throw new ArgumentOutOfRangeException(nameof(req.Days), "Days must be positive");

            var basePerDay = req.Category switch
            {
                VehicleCategory.Standard => _rules.BasePricePerDay,
                VehicleCategory.Premium => new Money(_rules.BasePricePerDay.Value + 120_000m),
                VehicleCategory.Suv => new Money(_rules.BasePricePerDay.Value + 200_000m),
                _ => _rules.BasePricePerDay
            };

            Money subtotal = new(0m);
            Money weekend = new(0m);
            Money insurance = new(0m);

            for (int i = 0; i < req.Days; i++)
            {
                var date = req.StartDate.AddDays(i);
                subtotal += basePerDay;

                if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
                    weekend += _rules.WeekendSurchargePerDay;

                if (req.IncludeInsurance)
                    insurance += _rules.InsurancePerDay;
            }

            var preDiscount = subtotal + weekend + insurance;
            var discountRate = req.Discount ?? _rules.Discount;
            var discountAmount = discountRate.of(preDiscount);
            var total = new Money(preDiscount.Value - discountAmount.Value);

            return new RentalQuoteRes
            {
                Subtotal = subtotal,
                WeekendSurcharge = weekend,
                Insurance = insurance,
                DiscountAmount = discountAmount,
                Total = total
            };
        }
    }
}