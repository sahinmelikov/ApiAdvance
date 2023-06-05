using AutoMapper;
using WebApiAdvance.Entities;
using WebApiAdvance.Entities.Dtos.Products;

namespace WebApiAdvance.Profiles;

public class ProductProfile:Profile
{
    public ProductProfile()
    {
        CreateMap<Product, GetProductDto>()
            .ForMember(p=>p.ProductName,opt=>opt.MapFrom(p=>p.Name));
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
}
