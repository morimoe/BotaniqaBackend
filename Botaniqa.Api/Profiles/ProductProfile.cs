using AutoMapper;
using Botaniqa.Api.Domain;
//маппер настроить
public class ProductProfile : Profile  // наследуемся от Profile
{
    public ProductProfile()
    {
        CreateMap<User, CreateUserRequest>();
    }
}