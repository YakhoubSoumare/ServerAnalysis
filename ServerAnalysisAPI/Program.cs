using Microsoft.OpenApi.Models;
using ServerAnalysisAPI.Profiles;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
string? connectionString;

//Add services to the container.
builder.Services.AddDbContext<DataContext>((serviceProvider, options) =>
{
	if (env.IsDevelopment())
	{
		DotNetEnv.Env.Load("../.env"); // local environment variable
		//connectionString = Environment.GetEnvironmentVariable("LOCAL_CONNECTION");
		connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
	}
	else
	{
		connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
	}

	if (connectionString is null)
	{
		throw new InvalidOperationException("Connection string is not set.");
	}

	//options.UseSqlServer(connectionString);
	options.UseNpgsql(connectionString);
	
});

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

var app = builder.Build();

app.UseCors("AllowAll");

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

using (var scope = app.Services.CreateScope())
{
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var accountService = new AccountService(userManager, roleManager);
	accountService.CreateRolesAsync().GetAwaiter().GetResult();

	//Seeding admin if none exists
	var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
	dataSeeder.SeedData().Wait();
}

app.Run();

#region Functions
void ConfigureAutomapper(IServiceCollection services)
{
	var config = new MapperConfiguration(cfg =>
	{
		cfg.CreateMap<Source, SourceDto>().ReverseMap();
		cfg.CreateMap<Topic, TopicDto>().ReverseMap();
		cfg.CreateMap<TopicSource, TopicSourceDto>().ReverseMap();
	});
	var mapper = config.CreateMapper();

	builder.Services.AddSingleton(mapper);
}
#endregion
