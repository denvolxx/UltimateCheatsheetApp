using ApplicationDTO.MSSQL.Users;
using AutoMapper;
using DBModels;

namespace DBService.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<AddUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<RegisterUserDTO, User>();
        }
    }
}
