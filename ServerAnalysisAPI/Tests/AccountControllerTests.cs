namespace ServerAnalysisAPI.Tests;

[TestClass]
public class AccountControllerTests
{
    private Mock<IAccountService> _mockAccountService = null!;
    private AccountController _controller = null!;


    [TestInitialize]
    public void TestInitialize()
    {
        _mockAccountService = new Mock<IAccountService>();
        _controller = new AccountController(_mockAccountService.Object);
    }

    [TestMethod]
    public async Task Register_ReturnsOkResult_WhenRegistrationIsSuccessful()
    {
        // Arrange
        var registerInput = new RegisterInput 
        { 
            UserName = "testuser@example.com",
            Password = "TestPassword123"
        };
        var serviceResult = new ServiceResult { IsSuccess = true };
        _mockAccountService.Setup(service => 
            service.RegisterUserAsync(registerInput)).ReturnsAsync(serviceResult);

        // Act
        var result = await _controller.Register(registerInput);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task Register_ReturnsBadRequestResult_WhenRegistrationFails()
    {
        // Arrange
        var registerInput = new RegisterInput 
        { 
            UserName = "testuser@example.com",
            Password = "TestPassword123"
        };
        var serviceResult = new ServiceResult { IsSuccess = false, Errors = new List<string> { "Error" } };
        _mockAccountService.Setup(service => service.RegisterUserAsync(registerInput)).ReturnsAsync(serviceResult);

        // Act
        var result = await _controller.Register(registerInput);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
}