using AutoMapper;
using Botaniqa.Api.Domain;

public class ProductProfile : Profile  // наследуемся от Profile
{
    public ProductProfile()
    {
        CreateMap<User, CreateUserRequest>();
    }
}