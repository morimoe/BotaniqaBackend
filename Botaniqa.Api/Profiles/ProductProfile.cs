// Botaniqa.BusinessLogic/Profiles/MappingProfile.cs
using AutoMapper;
using Botaniqa.BL.UserDTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, CreateUserRequest>();
        CreateMap<CreateUserRequest, User>();
    }
}