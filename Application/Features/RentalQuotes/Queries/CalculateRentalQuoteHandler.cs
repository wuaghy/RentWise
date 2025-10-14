// Application/Features/RentalQuotes/Queries/CalculateRentalQuoteHandler_NoMapper.cs
using Application.Abstractions;
using Domain.Constants;
using Domain.Dtos.RentalQuote.Request;
using Domain.Entities;
using MediatR;

namespace Application.Features.RentalQuotes.Queries;

public sealed class CalculateRentalQuoteHandler : IRequestHandler<CalculateRentalQuoteQuery, CalculateRentalQuoteVm>
{
    private readonly IRentalQuoteService _service;

    public CalculateRentalQuoteHandler(IRentalQuoteService service)
        => _service = service;

    public Task<CalculateRentalQuoteVm> Handle(CalculateRentalQuoteQuery query, CancellationToken ct)
    {
        if (!Enum.TryParse<VehicleCategory>(query.Category, true, out var category))
            throw new ArgumentException($"Invalid category '{query.Category}'");

        var req = new RentalQuoteReq
        {
            StartDate = query.StartDate,
            Days = query.Days,
            Category = category,
            IncludeInsurance = query.IncludeInsurance,
            Discount = query.Discount.HasValue ? new Percentage(query.Discount.Value) : null
        };

        var res = _service.Calculate(req);

        var vm = new CalculateRentalQuoteVm
        {
            Subtotal = res.Subtotal.Value,
            WeekendSurcharge = res.WeekendSurcharge.Value,
            Insurance = res.Insurance.Value,
            DiscountAmount = res.DiscountAmount.Value,
            Total = res.Total.Value
        };

        return Task.FromResult(vm);
    }
}