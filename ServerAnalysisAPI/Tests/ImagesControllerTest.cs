namespace ServerAnalysisAPI.Tests;

[TestClass]
public class ImagesControllerTests
{
    private Mock<IDbService> _mockDbService = null!;
    private ImagesController _controller = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDbService = new Mock<IDbService>();
        _controller = new ImagesController(_mockDbService.Object);
    }
    
    [TestMethod]
    public async Task Get_ReturnsOkResult_WhenImagesExist()
    {
        // Arrange
        var images = new List<ImageDto> { new ImageDto(), new ImageDto() };
        _mockDbService.Setup(db => db.GetAsync<Image, ImageDto>()).ReturnsAsync(images);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var responseImages = okResult.Value as List<ImageDto>;
        Assert.IsNotNull(responseImages);
        Assert.AreEqual(images.Count, responseImages.Count);
    }

    [TestMethod]
    public async Task Get_ReturnsNotFoundResult_WhenImagesDoNotExist()
    {
        // Arrange
        _mockDbService.Setup(db => db.GetAsync<Image, ImageDto>()).ReturnsAsync(new List<ImageDto>());

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var responseImages = okResult.Value as List<ImageDto>;
        Assert.IsNotNull(responseImages);
        Assert.AreEqual(0, responseImages.Count);
    }

    [TestMethod]
    public async Task Get_ReturnsBadRequestResult_WhenExceptionIsThrown()
    {
        // Arrange
        _mockDbService.Setup(db => db.GetAsync<Image, ImageDto>()).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        Assert.AreEqual(StatusCodes.Status400BadRequest, ((BadRequestResult)result).StatusCode);
    }

    [TestMethod]
    public async Task GetById_ReturnsOkResult_WhenImageExists()
    {
        // Arrange
        var imageId = 1;
        var image = new ImageDto { Id = imageId };
        _mockDbService.Setup(db => db.SingleAsync<Image, ImageDto>(i => i.Id == imageId)).ReturnsAsync(image);

        // Act
        var result = await _controller.Get(imageId);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var responseImage = okResult.Value as ImageDto;
        Assert.IsNotNull(responseImage);
        Assert.AreEqual(imageId, responseImage.Id);
    }
}