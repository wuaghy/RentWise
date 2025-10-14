using Domain.Dtos.RentalQuote.Request;
using Domain.Dtos.RentalQuote.Response;

namespace Application.Abstractions
{
    public interface IRentalQuoteService
    {
        RentalQuoteRes Calculate(RentalQuoteReq request);
    }
}