using Application.Abstractions;
using Application.Features.RentalQuotes.Queries;
using Domain.Constants;
using Domain.Dtos.RentalQuote.Request;
using Domain.Dtos.RentalQuote.Response;
using FluentAssertions;
using Moq;

namespace UnitTests;

public sealed class CalculateRentalQuoteHandlerTests
{
    [Fact]
    public async Task Handle_Should_Map_And_Return_Result()
    {
        // Arrange
        var mockSvc = new Mock<IRentalQuoteService>();
        mockSvc.Setup(s => s.Calculate(It.IsAny<RentalQuoteReq>()))
               .Returns(new RentalQuoteRes
               {
                   Subtotal = new Money(200_000),
                   WeekendSurcharge = new Money(50_000),
                   Insurance = new Money(30_000),
                   DiscountAmount = new Money(28_000),
                   Total = new Money(252_000)
               });

        var handler = new CalculateRentalQuoteHandler(mockSvc.Object);

        var query = new CalculateRentalQuoteQuery(
            StartDate: new DateOnly(2025, 10, 14),
            Days: 2,
            Category: "Standard",
            IncludeInsurance: true,
            Discount: 0.1m
        );

        // Act
        var vm = await handler.Handle(query, default);

        // Assert
        vm.Total.Should().Be(252_000m);
        mockSvc.Verify(s => s.Calculate(It.IsAny<RentalQuoteReq>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_When_Category_Invalid()
    {
        var mockSvc = new Mock<IRentalQuoteService>();
        var handler = new CalculateRentalQuoteHandler(mockSvc.Object);

        var bad = new CalculateRentalQuoteQuery(
            new DateOnly(2025, 10, 14), 1, "Spaceship", false, null);

        var act = async () => await handler.Handle(bad, default);
        await act.Should().ThrowAsync<ArgumentException>();
    }
}