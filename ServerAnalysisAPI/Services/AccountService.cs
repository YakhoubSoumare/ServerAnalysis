namespace ServerAnalysisAPI.Services;

public class AccountService
{
	private readonly UserManager<IdentityUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;

	public AccountService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
	{
		_userManager = userManager;
		_roleManager = roleManager;
	}

	public async Task CreateRolesAsync()
	{
		if (!await _roleManager.RoleExistsAsync("User"))
		{
			await _roleManager.CreateAsync(new IdentityRole("User"));
		}
	}

	public async Task<ServiceResult> RegisterUserAsync(RegisterInput input)
	{
		var user = new IdentityUser { UserName = input.UserName };
		var result = await _userManager.CreateAsync(user, input.Password);

		if (result.Succeeded)
		{
			await _userManager.AddToRoleAsync(user, "User");
			return new ServiceResult { IsSuccess = true };
		}

		return new ServiceResult { IsSuccess = false, Errors = result.Errors.Select(e => e.Description) };
	}
}

public class ServiceResult
{
	public bool IsSuccess { get; set; }
	public IEnumerable<string>? Errors { get; set; }
}
