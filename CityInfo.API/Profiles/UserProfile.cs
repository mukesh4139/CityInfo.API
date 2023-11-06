using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.User, Models.UserDto>();
            CreateMap<Models.UserDto, Entities.User>();
        }
    }
}
