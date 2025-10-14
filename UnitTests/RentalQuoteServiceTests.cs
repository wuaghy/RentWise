using Application.Features.RentalQuotes.Services;
using Domain.Constants;
using Domain.Dtos.RentalQuote.Request;
using Domain.Entities;
using FluentAssertions;

namespace UnitTests;

public sealed class RentalQuoteServiceTests
{
    private static RentalQuoteService CreateService(RentalPricingRules? rules = null)
        => new(rules ?? new RentalPricingRules());

    [Fact]
    public void Should_Throw_When_Days_Invalid()
    {
        var svc = CreateService();
        var req = new RentalQuoteReq
        {
            StartDate = new DateOnly(2025, 10, 14),
            Days = 0,
            Category = VehicleCategory.Standard,
            IncludeInsurance = false
        };

        Action act = () => svc.Calculate(req);
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(1, false, 0.00, 200_000)]
    [InlineData(2, true, 0.00, 460_000)]
    public void Should_Calculate_Total_DataDriven(int days, bool ins, double disc, double expected)
    {
        var svc = CreateService(new RentalPricingRules
        {
            BasePricePerDay = new Money(200_000),
            InsurancePerDay = new Money(30_000),
            Discount = new Percentage((decimal)disc)
        });

        var req = new RentalQuoteReq
        {
            StartDate = new DateOnly(2025, 10, 14),
            Days = days,
            Category = VehicleCategory.Standard,
            IncludeInsurance = ins,
            Discount = new Percentage((decimal)disc)
        };

        var res = svc.Calculate(req);
        res.Total.Value.Should().Be((decimal)expected);
    }

    [Fact]
    public void Should_Add_Weekend_Surcharge()
    {
        var svc = CreateService(new RentalPricingRules
        {
            BasePricePerDay = new Money(100_000),
            WeekendSurchargePerDay = new Money(10_000),
            InsurancePerDay = new Money(0)
        });

        var req = new RentalQuoteReq
        {
            StartDate = new DateOnly(2025, 10, 17), // Friday
            Days = 3, // Fri, Sat(+10k), Sun(+10k)
            Category = VehicleCategory.Standard,
            IncludeInsurance = false
        };

        var res = svc.Calculate(req);
        res.Subtotal.Value.Should().Be(300_000m);
        res.WeekendSurcharge.Value.Should().Be(20_000m);
        res.Total.Value.Should().Be(320_000m);
    }
}