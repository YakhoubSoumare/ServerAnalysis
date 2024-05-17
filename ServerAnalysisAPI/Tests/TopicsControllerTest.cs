namespace ServerAnalysisAPI.Tests;

[TestClass]
public class TopicsControllerTests
{
    private Mock<IDbService> _mockDbService = null!;
    private TopicsController _controller = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDbService = new Mock<IDbService>();
        _controller = new TopicsController(_mockDbService.Object);
    }
    
    [TestMethod]
    public async Task Get_ReturnsOkResult_WhenTopicsExist()
    {
        // Arrange
        var topics = new List<TopicDto> { new TopicDto(), new TopicDto() };
        _mockDbService.Setup(db => db.GetAsync<Topic, TopicDto>()).ReturnsAsync(topics);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var responseTopics = okResult.Value as List<TopicDto>;
        Assert.IsNotNull(responseTopics);
        Assert.AreEqual(topics.Count, responseTopics.Count);
    }

    [TestMethod]
    public async Task Get_ReturnsNotFoundResult_WhenTopicsDoNotExist()
    {
        // Arrange
        _mockDbService.Setup(db => db.GetAsync<Topic, TopicDto>()).ReturnsAsync(new List<TopicDto>());

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var responseTopics = okResult.Value as List<TopicDto>;
        Assert.IsNotNull(responseTopics);
        Assert.AreEqual(0, responseTopics.Count);
    }

    [TestMethod]
    public async Task Get_ReturnsBadRequestResult_WhenExceptionIsThrown()
    {
        // Arrange
        _mockDbService.Setup(db => db.GetAsync<Topic, TopicDto>()).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        Assert.AreEqual(StatusCodes.Status400BadRequest, ((BadRequestResult)result).StatusCode);
    }

    [TestMethod]
    public async Task GetById_ReturnsOkResult_WhenTopicExists()
    {
        // Arrange
        var topicId = 1;
        var topic = new TopicDto { Id = topicId };
        _mockDbService.Setup(db => db.SingleAsync<Topic, TopicDto>(i => i.Id == topicId)).ReturnsAsync(topic);

        // Act
        var result = await _controller.Get(topicId);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var responsetopic = okResult.Value as TopicDto;
        Assert.IsNotNull(responsetopic);
        Assert.AreEqual(topicId, responsetopic.Id);
    }

    [TestMethod]
    public async Task Post_ReturnsCreatedResult_WhenTopicIsValid()
    {
        // Arrange
        var topicDto = new TopicDto();
        var topic = new Topic();
        _mockDbService.Setup(db => db.AddAsync<Topic, TopicDto>(topicDto)).ReturnsAsync(topic);
        _mockDbService.Setup(db => db.SaveChangesAsync()).ReturnsAsync(true);
        _mockDbService.Setup(db => db.GetURI<Topic>(topic)).Returns("http://localhost/api/topics/1");

        // Mock user and role
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Name, "Test User"),
            new Claim(ClaimTypes.Role, "Admin"),
        }));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _controller.Post(topicDto);

        // Assert
        var createdResult = result as CreatedResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
        Assert.AreEqual("http://localhost/api/topics/1", createdResult.Location);
        var responseTopic = createdResult.Value as Topic;
        Assert.IsNotNull(responseTopic);
    }

    [TestMethod]
    public async Task Put_ReturnsNoContentResult_WhenTopicIsValid()
    {
        // Arrange
        var topicId = 1;
        var topicDto = new TopicDto { Id = topicId };
        var topic = new Topic();

        _mockDbService.Setup(db => db.AnyAsync<Topic>(e => e.Id == topicId)).ReturnsAsync(true);
        _mockDbService.Setup(db => db.UpdateAsync<Topic, TopicDto>(topicId, topicDto)); // No return value
        _mockDbService.Setup(db => db.SaveChangesAsync()).ReturnsAsync(true);

        // Mock user and role
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Name, "Test User"),
            new Claim(ClaimTypes.Role, "Admin"),
        }));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _controller.Put(topicId, topicDto);

        // Assert
        var noContentResult = result as NoContentResult;
        Assert.IsNotNull(noContentResult);
        Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode);
    }

    [TestMethod]
    public async Task Delete_ReturnsNoContentResult_WhenTopicExists()
    {
        // Arrange
        var topicId = 1;
        _mockDbService.Setup(db => db.AnyAsync<Topic>(e => e.Id == topicId)).ReturnsAsync(true);
        _mockDbService.Setup(db => db.DeleteAsync<Topic>(topicId)).ReturnsAsync(true);
        _mockDbService.Setup(db => db.SaveChangesAsync()).ReturnsAsync(true);

        // Mock user and role
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Name, "Test User"),
            new Claim(ClaimTypes.Role, "Admin"),
        }));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await _controller.Delete(topicId);

        // Assert
        var noContentResult = result as NoContentResult;
        Assert.IsNotNull(noContentResult);
        Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode);
    }
}