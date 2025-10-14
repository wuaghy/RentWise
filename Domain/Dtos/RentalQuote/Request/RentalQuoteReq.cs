using Domain.Constants;
using Domain.Entities;

namespace Domain.Dtos.RentalQuote.Request
{
    public class RentalQuoteReq
    {
        public DateOnly StartDate { get; init; }
        public int Days { get; init; }
        public VehicleCategory Category { get; init; }
        public bool IncludeInsurance { get; init; }
        public Percentage? Discount { get; init; }
    }
}