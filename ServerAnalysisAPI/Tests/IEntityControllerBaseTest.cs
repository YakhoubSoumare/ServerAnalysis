namespace ServerAnalysisAPI.Tests;
public abstract class ControllerTestBase<TController, TEntity, TDto>
    where TController : ControllerBase
    where TEntity : class, IEntity
    where TDto : class, IDto
{
    protected Mock<IDbService> _mockDbService;
    protected TController _controller;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockDbService = new Mock<IDbService>();
        _controller = Activator.CreateInstance<TController>(_mockDbService.Object);
    }

    [TestMethod]
    public async Task Get_ReturnsOkResult_WhenEntitiesExist()
    {
        // Arrange
        var entities = new List<TDto> { Activator.CreateInstance<TDto>(), Activator.CreateInstance<TDto>() };
        _mockDbService.Setup(db => db.GetAsync<TEntity, TDto>()).ReturnsAsync(entities);

        // Act
        var result = await _controller.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        var responseEntities = okResult.Value as List<TDto>;
        Assert.IsNotNull(responseEntities);
        Assert.AreEqual(entities.Count, responseEntities.Count);
    }

}