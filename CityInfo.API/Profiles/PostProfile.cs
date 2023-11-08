using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Entities.Post, Models.PostDto>();
            CreateMap<Models.PostDto, Entities.Post>();
        }
    }
}
