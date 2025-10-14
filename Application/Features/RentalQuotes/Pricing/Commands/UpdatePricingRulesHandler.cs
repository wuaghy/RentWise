using Domain.Constants;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RentalQuotes.Pricing.Commands
{
    public sealed class UpdatePricingRulesHandler : IRequestHandler<UpdatePricingRulesCommand, Unit>
    {
        private readonly RentalPricingRules _rules;

        public UpdatePricingRulesHandler(RentalPricingRules rules) => _rules = rules;

        public Task<Unit> Handle(UpdatePricingRulesCommand cmd, CancellationToken ct)
        {
            if (cmd.BasePricePerDay.HasValue) _rules.BasePricePerDay = new Money(cmd.BasePricePerDay.Value);
            if (cmd.WeekendSurchargePerDay.HasValue) _rules.WeekendSurchargePerDay = new Money(cmd.WeekendSurchargePerDay.Value);
            if (cmd.InsurancePerDay.HasValue) _rules.InsurancePerDay = new Money(cmd.InsurancePerDay.Value);
            if (cmd.DefaultDiscount.HasValue) _rules.Discount = new Percentage(cmd.DefaultDiscount.Value);

            return Task.FromResult(Unit.Value);
        }
    }
}