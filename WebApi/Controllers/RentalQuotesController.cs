using Application.Features.RentalQuotes.Pricing.Commands;
using Application.Features.RentalQuotes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RentWise.API.Controllers;

[ApiController]
[Route("api/rental-quotes")]
public class RentalQuotesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RentalQuotesController(IMediator mediator) => _mediator = mediator;

    [HttpPost("calculate")]
    public async Task<ActionResult<CalculateRentalQuoteVm>> Calculate([FromBody] CalculateRentalQuoteQuery query)
        => Ok(await _mediator.Send(query));

    [HttpPut("pricing")]
    public async Task<IActionResult> UpdatePricing([FromBody] UpdatePricingRulesCommand cmd)
    {
        await _mediator.Send(cmd);
        return NoContent();
    }
}