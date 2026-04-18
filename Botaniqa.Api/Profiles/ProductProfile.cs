// Botaniqa.BusinessLogic/Profiles/MappingProfile.cs
using AutoMapper;
using Botaniqa.Api.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, CreateUserRequest>();
        CreateMap<CreateUserRequest, User>();
    }
}