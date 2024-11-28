using AutoMapper;
using TodoAPI.Entities;

namespace TodoAPI.Dto.Mapper
{
    public class TodoMapProfile: Profile
    {
        public TodoMapProfile()
        {
            CreateMap<Todo, TodoCreateDto>()
                .ForMember(dest => dest.Percentage, opt => opt.MapFrom(src => src.PercentComplete));

        }
    }
}
