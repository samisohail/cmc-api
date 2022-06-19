using AutoMapper;
using CMC.Domain.Cart;
using CMC.Models.DTO;

namespace CMC.ReadStack
{
    public class ReadStackAutoMapper : Profile
    {
        public ReadStackAutoMapper()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
