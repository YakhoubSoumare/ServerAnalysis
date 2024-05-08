namespace ServerAnalysisAPI.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Source, SourceDto>();
        CreateMap<Benefit, BenefitDto>();
        CreateMap<Topic, TopicDto>();
        CreateMap<ServerBasedApplication, ServerBasedApplicationDto>();
        CreateMap<ServerlessFunction, ServerlessFunctionDto>();
        CreateMap<ServerBasedApplicationSource, ServerBasedApplicationSourceDto>();
        CreateMap<ServerlessFunctionSource, ServerlessFunctionSourceDto>();
        CreateMap<BenefitSource, BenefitSourceDto>();
    }
}