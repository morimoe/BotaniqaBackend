// Botaniqa.BusinessLogic/Profiles/MappingProfile.cs
using AutoMapper;
using Botaniqa.BL.UserDTO;
using Botaniqa.Domain.Entities.User;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, CreateUserRequest>();
        CreateMap<CreateUserRequest, User>();
        CreateMap<CreateUserRequest, UserData>();
    }
}