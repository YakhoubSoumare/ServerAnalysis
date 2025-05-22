using Microsoft.OpenApi.Models;
using ServerAnalysisAPI.Profiles;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Bind ASP.NET Core to port 8080 for Azure
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

var env = builder.Environment;


builder.Services.AddDbContext<DataContext>((serviceProvider, options) =>
{
	string? connectionString;

	if (env.IsDevelopment())
	{ // Development
		// options.UseInMemoryDatabase("TestDb");
		connectionString = builder.Configuration.GetConnectionString("AzureConnection");
	}
	else
	{ // Production
		connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
	}

	if (string.IsNullOrWhiteSpace(connectionString))
	{
		throw new InvalidOperationException("Connection string is not set.");
	}

	options.UseSqlServer(connectionString, sqlOptions => { sqlOptions.EnableRetryOnFailure(); });
	
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey
	});

	options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
ConfigureAutomapper(builder.Services);

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<DataContext>();

builder.Services.AddScoped<AccountService>();

builder.Services.AddScoped<IDataSeeder, DataSeeder>();

builder.Services.AddScoped<IDbService, DbService>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});

builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

app.UseCors("AllowAll");


if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

// Apply migrations and seed database on app startup
using (var scope = app.Services.CreateAsyncScope())
{
	var environment = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();
	var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

	if (environment.IsDevelopment())
	{
		// dbContext.Database.EnsureCreated(); # only for in-memory database
		dbContext.Database.Migrate();
	}
	else
	{
		dbContext.Database.Migrate();
	}

	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
	await accountService.CreateRolesAsync();

	var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
	await dataSeeder.SeedData();
}

// Simple root endpoint to verify the API is running and avoid 404 on accessing '/'
app.MapGet("/", () => "ServerAnalysisAPI is running.");

await app.RunAsync();

#region Functions
void ConfigureAutomapper(IServiceCollection services)
{
	var config = new MapperConfiguration(cfg =>
	{
		cfg.CreateMap<Source, SourceDto>().ReverseMap();
		cfg.CreateMap<Topic, TopicDto>().ReverseMap();
		cfg.CreateMap<TopicSource, TopicSourceDto>().ReverseMap();
		cfg.CreateMap<About, AboutDto>().ReverseMap();
		cfg.CreateMap<AboutSource, AboutSourceDto>().ReverseMap();
		cfg.CreateMap<Image, ImageDto>().ReverseMap();
	});
	var mapper = config.CreateMapper();

	builder.Services.AddSingleton(mapper);
}
#endregion
