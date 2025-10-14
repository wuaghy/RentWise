using Application.Features.RentalQuotes.Queries;
using AutoMapper;
using Domain.Dtos.RentalQuote.Response;

namespace Application.Mapping
{
    public sealed class RentalQuoteProfile : Profile
    {
        public RentalQuoteProfile()
        {
            CreateMap<RentalQuoteRes, CalculateRentalQuoteVm>()
            .ForMember(d => d.Subtotal, m => m.MapFrom(s => s.Subtotal.Value))
            .ForMember(d => d.WeekendSurcharge, m => m.MapFrom(s => s.WeekendSurcharge.Value))
            .ForMember(d => d.Insurance, m => m.MapFrom(s => s.Insurance.Value))
            .ForMember(d => d.DiscountAmount, m => m.MapFrom(s => s.DiscountAmount.Value))
            .ForMember(d => d.Total, m => m.MapFrom(s => s.Total.Value));
        }
    }
}