namespace ServerAnalysisAPI.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Source, SourceDto>();
        CreateMap<Topic, TopicDto>();
        CreateMap<TopicSource, TopicSourceDto>();
        CreateMap<Image, ImageDto>();
    }
}