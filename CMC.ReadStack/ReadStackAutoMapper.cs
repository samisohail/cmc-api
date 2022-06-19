using AutoMapper;
using CMC.Domain.Cart;

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
