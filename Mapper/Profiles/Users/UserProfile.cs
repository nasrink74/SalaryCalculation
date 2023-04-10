using AutoMapper;
using Dto.Incoms.Query;
using Dto.Users.Command;
using Entities;

namespace Mapper.Profiles.Users
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetUserAndIncomeDto>().ReverseMap();
            CreateMap<User, AddUserDto>().ReverseMap();
            CreateMap<User, EditUserDto>().ReverseMap();
        }
    }
}
