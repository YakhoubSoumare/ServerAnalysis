namespace ServerAnalysisAPI.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Source, SourceDto>();
        CreateMap<Topic, TopicDto>();
        CreateMap<TopicSource, TopicSourceDto>();
        CreateMap<About, AboutDto>();
        CreateMap<AboutSource, AboutSourceDto>();
        CreateMap<Image, ImageDto>();
	}
}