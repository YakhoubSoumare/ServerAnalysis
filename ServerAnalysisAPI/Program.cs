using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
string? connectionString;

// Add services to the container.
//builder.Services.AddDbContext<DataContext>((serviceProvider, options) =>
//{
//	if (env.IsDevelopment())
//	{
//		DotNetEnv.Env.Load("../.env"); // local environment variable
//		connectionString = Environment.GetEnvironmentVariable("LOCAL_CONNECTION");
//	}
//	else
//	{
//		connectionString = Environment.GetEnvironmentVariable("PROD_CONNECTION_STRING");
//	}

//	if (connectionString is null)
//	{
//		throw new InvalidOperationException("Connection string is not set.");
//	}

//	options.UseSqlServer(connectionString);
//});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Name = "Autorization",
		Type = SecuritySchemeType.ApiKey
	});

	options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<DataContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
	.AddEntityFrameworkStores<DataContext>();

var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
if (!await roleManager.RoleExistsAsync("User"))
{
	await roleManager.CreateAsync(new IdentityRole("User"));
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
