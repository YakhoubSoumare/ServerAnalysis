namespace ServerAnalysisAPI.Services;

public class DataSeeder : IDataSeeder
{
	private readonly UserManager<IdentityUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IHostEnvironment _env;

	public DataSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IHostEnvironment env)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_env = env;
	}

	public async Task SeedData()
	{
		// Creates Admin role
		if (!await _roleManager.RoleExistsAsync("Admin"))
		{
			await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
		}

		// If in development environment, loads environment variables from .env file
		if (_env.IsDevelopment())
		{
			DotNetEnv.Env.Load("../.env");
		}
		
		// Get admin credentials from environment variables
		var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
		var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

		// Check if any of the environment variables are null
		if (adminEmail == null)
		{
			throw new Exception("Admin email not set in environment variables");
		}

		if (adminPassword == null)
		{
			throw new Exception("Admin password not set in environment variables");
		}

		// Create Admin user
		var user = new IdentityUser { UserName = adminEmail };
		var result = await _userManager.CreateAsync(user, adminPassword);

		if (result.Succeeded)
		{
			await _userManager.AddToRoleAsync(user, "Admin");
		}
		else
		{
			// Log the errors
			foreach (var error in result.Errors)
			{
				Console.WriteLine(error.Description);
			}
		}
	}
}