using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Constants;

namespace Domain.Entities
{
    public sealed class RentalPricingRules
    {
        public Money BasePricePerDay { get; set; } = new(200_000m);
        public Money WeekendSurchargePerDay { get; set; } = new(50_000m);
        public Money InsurancePerDay { get; set; } = new(30_000m);
        public Percentage Discount { get; set; } = new(0m);
    }
}