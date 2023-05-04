using TesteApi.Models;
using AutoMapper;

namespace TesteApi.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Manga, MangaDTO>().ReverseMap();
        CreateMap<Chapter, ChapterDTO>().ReverseMap();
    }
}


