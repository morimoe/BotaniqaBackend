// Botaniqa.BusinessLogic/Profiles/MappingProfile.cs
using AutoMapper;
using Botaniqa.BL.ProductDTO;
using Botaniqa.BL.UserDTO;
using Botaniqa.Domain.Entities.Product;
using Botaniqa.Domain.Entities.User;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, CreateUserRequest>();

        CreateMap<CreateUserRequest, User>();

        CreateMap<CreateUserRequest, UserData>();

        CreateMap<ProductData, Product>();

        CreateMap<Product, ProductData>();

        CreateMap<CreateProductRequest, ProductData>();


    }
}