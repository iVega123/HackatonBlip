using AutoMapper;
using PocGPT.Core.Dtos;
using PocGPT.Core.Model;

namespace PocGPT.Core.mapper
{
    public class WebApiMapperProfile : Profile
    {
        public WebApiMapperProfile()
        {
            CreateMap<Item, Messages>()
            .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.Direction))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

        }
    }
}
