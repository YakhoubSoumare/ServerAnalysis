namespace ServerAnalysisAPI.Tests;

[TestClass]
public class AutoMapperMappingTest
{
    private IMapper _mapper = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Source, SourceDto>().ReverseMap();
            cfg.CreateMap<Topic, TopicDto>().ReverseMap();
            cfg.CreateMap<TopicSource, TopicSourceDto>().ReverseMap();
            cfg.CreateMap<About, AboutDto>().ReverseMap();
            cfg.CreateMap<AboutSource, AboutSourceDto>().ReverseMap();
            cfg.CreateMap<Image, ImageDto>().ReverseMap();
        });

        _mapper = config.CreateMapper();
    }

    [TestMethod]
    public void Source_To_SourceDto_Mapping_IsCorrect()
    {
        // Arrange
        var source = new Source
        {
            Id = 1,
            Title = "Test Title",
            Link = "Test Link",
            ReferenceNumber = 123
        };

        // Act
        var sourceDto = _mapper.Map<SourceDto>(source);

        // Assert
        Assert.AreEqual(source.Id, sourceDto.Id);
        Assert.AreEqual(source.Title, sourceDto.Title);
        Assert.AreEqual(source.Link, sourceDto.Link);
        Assert.AreEqual(source.ReferenceNumber, sourceDto.ReferenceNumber);

    }

    [TestMethod]
    public void Topic_To_TopicDto_Mapping_IsCorrect()
    {
        // Arrange
        var topic = new Topic
        {
            Id = 1,
            Title = "Test Title",
            Introductions = "Test Introductions",
            Approaches = "Test Approaches",
            UseCases = "Test UseCases",
            Limitations = "Test Limitations",
            Advantages = "Test Advantages",
            Comparisons = "Test Comparisons",
            IndustryInsights = "Test IndustryInsights",
            Beneficiaries = "Test Beneficiaries"
        };

        
        // Act
        var topicDto = _mapper.Map<TopicDto>(topic);

        // Assert
        Assert.AreEqual(topic.Id, topicDto.Id);
        Assert.AreEqual(topic.Title, topicDto.Title);
        Assert.AreEqual(topic.Introductions, topicDto.Introductions);
        Assert.AreEqual(topic.Approaches, topicDto.Approaches);
        Assert.AreEqual(topic.UseCases, topicDto.UseCases);
        Assert.AreEqual(topic.Limitations, topicDto.Limitations);
        Assert.AreEqual(topic.Advantages, topicDto.Advantages);
        Assert.AreEqual(topic.Comparisons, topicDto.Comparisons);
        Assert.AreEqual(topic.IndustryInsights, topicDto.IndustryInsights);
        Assert.AreEqual(topic.Beneficiaries, topicDto.Beneficiaries);
    }

    [TestMethod]
    public void Image_To_ImageDto_Mapping_IsCorrect()
    {
        // Arrange
        var image = new Image
        {
            Id = 1,
            Title = "Test Title",
            Url = "Test Url",
            TopicId = 2
        };

        // Act
        var imageDto = _mapper.Map<ImageDto>(image);

        // Assert
        Assert.AreEqual(image.Id, imageDto.Id);
        Assert.AreEqual(image.Title, imageDto.Title);
        Assert.AreEqual(image.Url, imageDto.Url);
        Assert.AreEqual(image.TopicId, imageDto.TopicId);
    }

    [TestMethod]
    public void About_To_AboutDto_Mapping_IsCorrect()
    {
        // Arrange
        var about = new About
        {
            Id = 1,
            Title = "Test Title",
            Overview = "Test Overview",
            Language = "Test Language",
            Framework = "Test Framework",
            API = "Test API",
            Database = "Test Database",
            Security = "Test Security",
            FrontEnd = "Test FrontEnd",
            Test = "Test Test",
            VersionControl = "Test VersionControl"
        };

        // Act
        var aboutDto = _mapper.Map<AboutDto>(about);

        // Assert
        Assert.AreEqual(about.Id, aboutDto.Id);
        Assert.AreEqual(about.Title, aboutDto.Title);
        Assert.AreEqual(about.Overview, aboutDto.Overview);
        Assert.AreEqual(about.Language, aboutDto.Language);
        Assert.AreEqual(about.Framework, aboutDto.Framework);
        Assert.AreEqual(about.API, aboutDto.API);
        Assert.AreEqual(about.Database, aboutDto.Database);
        Assert.AreEqual(about.Security, aboutDto.Security);
        Assert.AreEqual(about.FrontEnd, aboutDto.FrontEnd);
        Assert.AreEqual(about.Test, aboutDto.Test);
        Assert.AreEqual(about.VersionControl, aboutDto.VersionControl);
    }
}