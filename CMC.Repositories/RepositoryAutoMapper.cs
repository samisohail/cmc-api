using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CMC.Domain.Cart;
using CMC.Repositories.DbEntities;

namespace CMC.Repositories
{
    public class RepositoryAutoMapper : Profile
    {
        public RepositoryAutoMapper()
        {
            CreateMap<DbCurrency, Currency>();
            CreateMap<DbProduct, Product>();
        }
    }
}
