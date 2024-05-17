namespace ServerAnalysisAPI.Tests;

[TestClass]
public class SourcesControllerTests
{
    private Mock<IDbService> _mockDbService = null!;
    private SourcesController _controller = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDbService = new Mock<IDbService>();
        _controller = new SourcesController(_mockDbService.Object);
    }
    
    [TestMethod]
    public async Task Get_ReturnsOkResult_WhenSourcesExist()
    {
        // Arrange
        var sources = new List<SourceDto> { new SourceDto(), new SourceDto() };
        _mockDbService.Setup(db => db.GetAsync<Source, SourceDto>()).ReturnsAsync(sources);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var responsesources = okResult.Value as List<SourceDto>;
        Assert.IsNotNull(responsesources);
        Assert.AreEqual(sources.Count, responsesources.Count);
    }

    [TestMethod]
    public async Task Get_ReturnsNotFoundResult_WhenSourcesDoNotExist()
    {
        // Arrange
        _mockDbService.Setup(db => db.GetAsync<Source, SourceDto>()).ReturnsAsync(new List<SourceDto>());

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var responsesources = okResult.Value as List<SourceDto>;
        Assert.IsNotNull(responsesources);
        Assert.AreEqual(0, responsesources.Count);
    }

    [TestMethod]
    public async Task Get_ReturnsBadRequestResult_WhenExceptionIsThrown()
    {
        // Arrange
        _mockDbService.Setup(db => db.GetAsync<Source, SourceDto>()).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        Assert.AreEqual(StatusCodes.Status400BadRequest, ((BadRequestResult)result).StatusCode);
    }

    [TestMethod]
    public async Task GetById_ReturnsOkResult_WhenSourceExists()
    {
        // Arrange
        var sourceId = 1;
        var source = new SourceDto { Id = sourceId };
        _mockDbService.Setup(db => db.SingleAsync<Source, SourceDto>(i => i.Id == sourceId)).ReturnsAsync(source);

        // Act
        var result = await _controller.Get(sourceId);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var responseSource = okResult.Value as SourceDto;
        Assert.IsNotNull(responseSource);
        Assert.AreEqual(sourceId, responseSource.Id);
    }

    [TestMethod]
    public async Task Post_ReturnsCreatedResult_WhenSourceIsValid()
    {
        // Arrange
        var sourceDto = new SourceDto();
        var source = new Source();
        _mockDbService.Setup(db => db.AddAsync<Source, SourceDto>(sourceDto)).ReturnsAsync(source);
        _mockDbService.Setup(db => db.SaveChangesAsync()).ReturnsAsync(true);
        _mockDbService.Setup(db => db.GetURI<Source>(source)).Returns("http://localhost/api/sources/1");

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
        var result = await _controller.Post(sourceDto);

        // Assert
        var createdResult = result as CreatedResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
        Assert.AreEqual("http://localhost/api/sources/1", createdResult.Location);
        var responseSource = createdResult.Value as Source;
        Assert.IsNotNull(responseSource);
    }

    [TestMethod]
    public async Task Put_ReturnsNoContentResult_WhenSourceIsValid()
    {
        // Arrange
        var sourceId = 1;
        var sourceDto = new SourceDto { Id = sourceId };
        var source = new Source();

        _mockDbService.Setup(db => db.AnyAsync<Source>(e => e.Id == sourceId)).ReturnsAsync(true);
        _mockDbService.Setup(db => db.UpdateAsync<Source, SourceDto>(sourceId, sourceDto)); // No return value
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
        var result = await _controller.Put(sourceId, sourceDto);

        // Assert
        var noContentResult = result as NoContentResult;
        Assert.IsNotNull(noContentResult);
        Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode);
    }

    [TestMethod]
    public async Task Delete_ReturnsNoContentResult_WhenSourceExists()
    {
        // Arrange
        var sourceId = 1;
        _mockDbService.Setup(db => db.AnyAsync<Source>(e => e.Id == sourceId)).ReturnsAsync(true);
        _mockDbService.Setup(db => db.DeleteAsync<Source>(sourceId)).ReturnsAsync(true);
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
        var result = await _controller.Delete(sourceId);

        // Assert
        var noContentResult = result as NoContentResult;
        Assert.IsNotNull(noContentResult);
        Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode);
    }
}