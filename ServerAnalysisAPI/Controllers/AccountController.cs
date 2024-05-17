// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using ServerAnalysisAPI.Services;

namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
	private readonly IAccountService _accountService;

	public AccountController(IAccountService accountService)
	{
		_accountService = accountService;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterInput model)
	{
		var result = await _accountService.RegisterUserAsync(model);

		if (result.IsSuccess)
		{
			return Ok();
		}

		return BadRequest(result.Errors);
	}
}
