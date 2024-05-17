namespace ServerAnalysisAPI.Tests;

[TestClass]
public class AboutsControllerTests
{
    private Mock<IDbService> _mockDbService = null!;
    private AboutsController _controller = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDbService = new Mock<IDbService>();
        _controller = new AboutsController(_mockDbService.Object);
    }
    
    [TestMethod]
    public async Task Get_ReturnsOkResult_WhenAboutsExist()
    {
        // Arrange
        var abouts = new List<AboutDto> { new AboutDto(), new AboutDto() };
        _mockDbService.Setup(db => db.GetAsync<About, AboutDto>()).ReturnsAsync(abouts);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var responseAbouts = okResult.Value as List<AboutDto>;
        Assert.IsNotNull(responseAbouts);
        Assert.AreEqual(abouts.Count, responseAbouts.Count);
    }

    [TestMethod]
    public async Task Get_ReturnsNotFoundResult_WhenAboutsDoNotExist()
    {
        // Arrange
        _mockDbService.Setup(db => db.GetAsync<About, AboutDto>()).ReturnsAsync(new List<AboutDto>());

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var responseAbouts = okResult.Value as List<AboutDto>;
        Assert.IsNotNull(responseAbouts);
        Assert.AreEqual(0, responseAbouts.Count);
    }

    [TestMethod]
    public async Task Get_ReturnsBadRequestResult_WhenExceptionIsThrown()
    {
        // Arrange
        _mockDbService.Setup(db => db.GetAsync<About, AboutDto>()).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        Assert.AreEqual(StatusCodes.Status400BadRequest, ((BadRequestResult)result).StatusCode);
    }

    [TestMethod]
    public async Task GetById_ReturnsOkResult_WhenAboutExists()
    {
        // Arrange
        var AboutId = 1;
        var about = new AboutDto { Id = AboutId };
        _mockDbService.Setup(db => db.SingleAsync<About, AboutDto>(i => i.Id == AboutId)).ReturnsAsync(about);

        // Act
        var result = await _controller.Get(AboutId);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var responseAbout = okResult.Value as AboutDto;
        Assert.IsNotNull(responseAbout);
        Assert.AreEqual(AboutId, responseAbout.Id);
    }

    [TestMethod]
    public async Task Post_ReturnsCreatedResult_WhenAboutIsValid()
    {
        // Arrange
        var aboutDto = new AboutDto();
        var about = new About();
        _mockDbService.Setup(db => db.AddAsync<About, AboutDto>(aboutDto)).ReturnsAsync(about);
        _mockDbService.Setup(db => db.SaveChangesAsync()).ReturnsAsync(true);
        _mockDbService.Setup(db => db.GetURI<About>(about)).Returns("http://localhost/api/abouts/1");

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
        var result = await _controller.Post(aboutDto);

        // Assert
        var createdResult = result as CreatedResult;
        Assert.IsNotNull(createdResult);
        Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);
        Assert.AreEqual("http://localhost/api/abouts/1", createdResult.Location);
        var responseAbout = createdResult.Value as About;
        Assert.IsNotNull(responseAbout);
    }

    [TestMethod]
    public async Task Put_ReturnsNoContentResult_WhenAboutIsValid()
    {
        // Arrange
        var aboutId = 1;
        var aboutDto = new AboutDto { Id = aboutId };
        var about = new About();

        _mockDbService.Setup(db => db.AnyAsync<About>(e => e.Id == aboutId)).ReturnsAsync(true);
        _mockDbService.Setup(db => db.UpdateAsync<About, AboutDto>(aboutId, aboutDto)); // No return value
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
        var result = await _controller.Put(aboutId, aboutDto);

        // Assert
        var noContentResult = result as NoContentResult;
        Assert.IsNotNull(noContentResult);
        Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode);
    }

    [TestMethod]
    public async Task Delete_ReturnsNoContentResult_WhenAboutExists()
    {
        // Arrange
        var aboutId = 1;
        _mockDbService.Setup(db => db.AnyAsync<About>(e => e.Id == aboutId)).ReturnsAsync(true);
        _mockDbService.Setup(db => db.DeleteAsync<About>(aboutId)).ReturnsAsync(true);
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
        var result = await _controller.Delete(aboutId);

        // Assert
        var noContentResult = result as NoContentResult;
        Assert.IsNotNull(noContentResult);
        Assert.AreEqual(StatusCodes.Status204NoContent, noContentResult.StatusCode);
    }
}