namespace ServerAnalysisAPI.Services;

public class DataSeeder : IDataSeeder
{
	private readonly UserManager<IdentityUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IHostEnvironment _env;
	private readonly DataContext _context;


	public DataSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IHostEnvironment env, DataContext context)
	{
		_userManager = userManager;
		_roleManager = roleManager;
		_env = env;
		_context = context;
	}

	public async Task SeedData()
	{
		// Load .env file in development
		if (_env.IsDevelopment())
		{
			DotNetEnv.Env.Load("../.env");
		}
		
		// Read credentials from environment variables
		var adminEmail = Environment.GetEnvironmentVariable("SEEDED_ADMIN_EMAIL");
		var adminPassword = Environment.GetEnvironmentVariable("SEEDED_ADMIN_PASSWORD");

		// Handle missing config
		if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
		{
			var message = "SEEDED_ADMIN_EMAIL or SEEDED_ADMIN_PASSWORD is missing.";
			if (_env.IsDevelopment())
			{
				// Fail fast in development
				throw new Exception(message);
			}
			else
			{
				// Skip in production
				Console.WriteLine("⚠️ " + message + " Admin user will not be seeded.");
				return; 
			}
		}

		// Ensure the Admin role exists
		if (!await _roleManager.RoleExistsAsync("Admin"))
		{
			var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
			if (!roleResult.Succeeded)
			{
				Console.WriteLine("⚠️ Failed to create Admin role: " +
					string.Join("; ", roleResult.Errors.Select(e => e.Description)));
				return;
			}
		}

		// Check if admin user exists
    	var existingUser = await _userManager.FindByEmailAsync(adminEmail);
		if (existingUser == null)
		{
			var newUser = new IdentityUser
			{
				UserName = adminEmail,
				Email = adminEmail,
				EmailConfirmed = true
			};

			var userResult = await _userManager.CreateAsync(newUser, adminPassword);
			if (!userResult.Succeeded)
			{
				Console.WriteLine("⚠️ Failed to create admin user: " +
					string.Join("; ", userResult.Errors.Select(e => e.Description)));
				return;
			}

			existingUser = newUser;
			Console.WriteLine("✅ Admin user created.");
		}
		else
		{
			Console.WriteLine("ℹ️ Admin user already exists.");
		}

		// Ensure user is in Admin role
		var roles = await _userManager.GetRolesAsync(existingUser);
		if (!roles.Contains("Admin"))
		{
			await _userManager.AddToRoleAsync(existingUser, "Admin");
			Console.WriteLine("✅ Admin user added to Admin role.");
		}

		// Seed tables separately
		await SeedSourcesAsync();
		await SeedTopicsAsync();
		await SeedTopicSourcesAsync();
		await SeedAboutsAsync();
		await SeedAboutSourcesAsync();
		await SeedImagesAsync();
	}

	private async Task SeedSourcesAsync()
	{
		if (!_context.Sources.Any())
		{
			var strategy = _context.Database.CreateExecutionStrategy();

			await strategy.ExecuteAsync(async () =>
			{
				using var transaction = await _context.Database.BeginTransactionAsync();

				await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Sources ON");

				var sources = new List<Source>
				{
					new Source { Id = 1, Title = "Dedicated Server", Link = "https://www.inmotionhosting.com/blog/what-is-a-dedicated-server/", ReferenceNumber = 1 },
					new Source { Id = 2, Title = "Difference Between Cloud Based and Server Based | Difference Between", Link = "http://www.differencebetween.net/technology/difference-between-cloud-based-and-server-based/", ReferenceNumber = 2 },
					new Source { Id = 3, Title = "What is Serverless Computing? - Serverless Computing Explained - AWS (amazon.com)", Link = "https://aws.amazon.com/what-is/serverless-computing/", ReferenceNumber = 3 },
					new Source { Id = 4, Title = "What is serverless computing  |  Google Cloud", Link = "https://cloud.google.com/discover/what-is-serverless-computing", ReferenceNumber = 4 },
					new Source { Id = 5, Title = "What is serverless computing? | Serverless definition | Cloudflare", Link = "https://www.cloudflare.com/en-gb/learning/serverless/what-is-serverless/", ReferenceNumber = 5 },
					new Source { Id = 6, Title = "Why use serverless computing? | Pros and cons of serverless | Cloudflare", Link = "https://www.cloudflare.com/en-gb/learning/serverless/why-use-serverless/", ReferenceNumber = 6 },
					new Source { Id = 7, Title = "Serverless Architecture: Key Benefits and Limitations | New Relic", Link = "https://newrelic.com/blog/best-practices/what-is-serverless-architecture", ReferenceNumber = 7 },
					new Source { Id = 8, Title = "Why consider moving to the cloud? (metabytes.se)", Link = "https://www.metabytes.se/en/metacademy/why-consider-moving-to-the-cloud?trk=public_post_comment-text", ReferenceNumber = 8 },
					new Source { Id = 9, Title = "Choosing the Right Cloud Architecture: Server-Based or Server-less? | by Vishv | Medium", Link = "https://medium.com/@vishv_2332/choosing-the-right-cloud-architecture-server-based-or-server-less-54544c425792", ReferenceNumber = 9 },
					new Source { Id = 10, Title = "Serverless Architecture vs. Traditional Server-Based Development - Eastern Enterprise", Link = "https://www.easternenterprise.com/thought-leadership/serverless-architecture-vs-traditional-server-based-development/", ReferenceNumber = 10 },
					new Source { Id = 11, Title = "Top-benefits-and-disadvantages-of-serverless-computing", Link = "https://www.techtarget.com/searchcloudcomputing/tip/Top-benefits-and-disadvantages-of-serverless-computing", ReferenceNumber = 11 },
					new Source { Id = 12, Title = "Benefits of Server-based", Link = "https://www.temok.com/blog/benefits-of-a-server/", ReferenceNumber = 12 },
					new Source { Id = 13, Title = "Microsoft Docs: Introduction to Identity on ASP.NET Core", Link = "https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio", ReferenceNumber = 13 },
					new Source { Id = 14, Title = "Exploring Identity Endpoints in .NET 8", Link = "https://dev.to/grontis/exploring-identity-endpoints-in-net-8-3lid", ReferenceNumber = 14 },
					new Source { Id = 15, Title = "Docker on Render", Link = "https://docs.render.com/docker", ReferenceNumber = 15 },
					new Source { Id = 16, Title = "Render Pricing and Features", Link = "https://render.com/pricing", ReferenceNumber = 16 },
					new Source { Id = 17, Title = "Supabase Pricing and Features", Link = "https://supabase.com/pricing", ReferenceNumber = 17 },
					new Source { Id = 18, Title = "Microsoft Docs: Tour of C#", Link = "https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/", ReferenceNumber = 18 },
					new Source { Id = 19, Title = "Microsoft Docs: Tour of .NET", Link = "https://learn.microsoft.com/en-us/dotnet/core/introduction", ReferenceNumber = 19 },
					new Source { Id = 20, Title = "Microsoft Docs: Choose between .NET Core and .NET Framework for server apps", Link = "https://learn.microsoft.com/en-us/dotnet/standard/choosing-core-framework-server", ReferenceNumber = 20 },
					new Source { Id = 21, Title = "Stack Overflow: 2021 Developer Survey", Link = "https://survey.stackoverflow.co/2021#technology-most-loved-dreaded-and-wanted-web-frameworks-loved2", ReferenceNumber = 21 },
					new Source { Id = 22, Title = "Tagline Info Tech: Blazor vs. React: Best option for web development", Link = "https://taglineinfotech.com/blazor-vs-react/", ReferenceNumber = 22 },
					new Source { Id = 23, Title = "Unit testing C# with MSTest and .NET", Link = "https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest", ReferenceNumber = 23 }
				};

				_context.Sources.AddRange(sources);
				await _context.SaveChangesAsync();

				await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Sources OFF");

				await transaction.CommitAsync();
			});
		}		
	}

	private async Task SeedTopicsAsync()
	{
		if (!_context.Topics.Any())
		{
			var strategy = _context.Database.CreateExecutionStrategy();

			await strategy.ExecuteAsync(async () =>
			{
				using var transaction = await _context.Database.BeginTransactionAsync();
				await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Topics ON");

				var topics = new List<Topic>
				{
					new Topic
					{
						Id = 1,
						Title = "Server-Based Applications",
						Introductions = "A server-based application is software that runs on servers, allowing end users to access it remotely over a network. These applications rely on server infrastructure to store, process and deliver data and features to clients. Server-based applications usually run on a centralized server, which handles network resources and responds to client requests. Source links: [1][2]",
						Approaches = "Server-based applications operate on central servers, handling client requests. Clients engage through interfaces or APIs, submitting requests for processing. The server executes tasks and returns results to clients. Server-based applications can be stored on a remote server and accessed through a browser interface, with the server offering services like resource sharing and data access. Source links: [1][2]",
						UseCases = "Server-based applications are commonly implemented across diverse industries and situations. Within enterprises, they drive critical operations such as customer relationship management (CRM), enterprise resource planning (ERP) and document management systems. Additionally, web-based services depend heavily on server-based applications to handle user requests, handle data processing and provide dynamic content. Moreover, server-based applications are crucial in cloud computing, facilitating the delivery of computing resources over the internet via services like Software as a Service (SaaS), Platform as a Service (PaaS) and Infrastructure as a Service (IaaS). Source links: [1][2][12]",
						Limitations = "Server-based applications offer numerous benefits but come with limitations. They depend on network connectivity, which can be unreliable, affecting functionality. Performance may suffer due to high network traffic or server load, leading to slowdowns for users. Security becomes a concern with centralized infrastructure, exposing servers to potential breaches, making servers vulnerable to breaches. Moreover, the setup and maintenance costs of server infrastructure can be high for small businesses or start-ups. Source links: [2][12]",
						Advantages = "Server-based applications offer numerous benefits. Firstly, they provide centralized data management, storing data on servers for easy access and ensuring consistency across clients. Secondly, these applications are scalable, able to accommodate increasing users or data volumes by adding server resources. Thirdly, their remote accessibility allows users to connect from anywhere with an internet connection, offering flexibility. Moreover, centralized server infrastructure enables robust security measures, including access controls and encryption, enhancing data protection. Source links: [2][12]",
						Comparisons = "Server-based architecture offers control over infrastructure but may lead to underutilization, while serverless architecture provides automatic scaling and cost efficiency but may suffer from latency issues. Traditional server-based development requires manual provisioning, fixed scaling and complex infrastructure management for continuous availability. In contrast, serverless architecture automates scaling and simplifies deployment, reducing operational overhead and potentially lowering costs. Source links: [9][10]",
						IndustryInsights = "Server-Based Architecture is favored by industries with stable workloads and strict regulatory requirements, such as finance, healthcare and manufacturing, due to its reliability and stability. Source links: [9]",
						Beneficiaries = "Developers aiming for full control of infrastructure and organizations seeking stable costs and processes benefit from Server-Based Architecture, while Developers looking to reduce operational burden, speed up development and Organizations aiming for cost efficiency and agility for variable workloads find advantages in Serverless Architecture. Source links: [9]"
					},
					new Topic
					{
						Id = 2,
						Title = "Serverless Functions",
						Introductions = "Serverless computing, revolutionizes cloud computing by offering developers a seamless way to deploy backend code without the burden of directly managing servers. This allows developers to focus solely on designing their applications, while the cloud service provider takes care of essential tasks like provisioning, scaling and maintenance of the underlying infrastructure. Resources are allocated on an as-used basis, ensuring developers only pay for what they consume. This approach not only simplifies development but also provides a cost-effective and scalable solution for businesses. Despite the term \"serverless\", physical servers are still used behind the scenes, but developers are shielded from the complexities of managing them. Source links: [3][4][5]",
						Approaches = "In serverless architecture, developers work with event-triggered functions, dynamically allocating resources for execution. This approach eliminates server management tasks, allowing focus solely on coding, resulting in faster provisioning times and reduced operational overhead. Additionally, serverless computing boosts developer productivity, provides efficient scalability and lowers costs through pay-as-you-go pricing. Source links: [3][4][5]",
						UseCases = "Serverless computing excels in stateless application development, batch processing, real-time data analytics and business process automation. Additionally, it is well-suited for integrating with third-party services, running scheduled tasks, automating IT processes, handling real-time processing, managing CI/CD pipelines, serving as REST API backends and notifications. Moreover, it is suitable for event-driven applications, modular architectures and scenarios requiring rapid scaling. Source links: [3][4][5]",
						Limitations = "While serverless computing offers versatility, it also comes with notable limitations. It is not designed for executing code over extended periods and may struggle with applications requiring strict low-latency, such as financial services applications. Moreover, migrating to a new cloud provider can pose challenges and there's limited visibility and control over service operations, scaling and disaster recovery. Additionally, cold starts, where dormant serverless functions need to be initialized, have been a significant drawback. However, advancements like Cloudflare Workers aim to address this issue by eliminating cold starts entirely. Security can be a concern for Serverless applications with an increased risk of cyberattacks, as each function can serve as a potential attack entry point. While cloud providers implement extensive security measures, organizations are responsible for securing application code and data per the shared responsibility model. Source links: [4][5][11]",
						Advantages = "Serverless computing revolutionizes application development with numerous benefits Developers are freed from server management tasks, reducing expenses and allowing a focus on coding. Its cost-effective pricing model eliminates the need for capacity estimation, ensuring affordable resource usage. Scalability is built-in, with resources automatically adjusting to demand, enabling seamless and simplified handling of workloads. Deployment and updates are quick and easy, thanks to simplified backend configurations. Reduced latency improves user experience, while the event-driven model ensures efficient resource utilization. Overall, serverless computing promotes modernization, flexibility and cost-efficiency in application development. Source links: [6][7][8]",
						Comparisons = "Serverless computing presents a dynamic alternative to traditional server-based architectures, offering advantages in resource management, cost efficiency, scalability and deployment flexibility. Unlike server-based setups, serverless architectures automate resource allocation and scaling, reducing operational surplus and complexity. Scalability is built-in, ensuring optimal performance even during peak usage, while organizations only pay for the resources consumed. Serverless architecture enables rapid development and deployment, allowing quick adaptation to changing business needs. By dynamically allocating and releasing resources based on demand, serverless functions optimize resource utilization, contrasting with static server-based setups. Overall, while server-based applications provide control over infrastructure, serverless computing offers flexibility and efficiency without the maintenance burden. Source links: [6][7][8]",
						IndustryInsights = "Serverless Architecture is gaining popularity for its suitability with event-driven and variable workloads, as well as microservices setups. It's preferred by industries prioritizing flexibility, cost efficiency and quick time-to-market. Moreover, businesses seeking simplified operations and faster development cycles often opt for Serverless Architecture, thanks to its reduced operational burden and rapid deployment capabilities. Source links: [9]",
						Beneficiaries = "Serverless computing offers significant advantages to developers, operations teams and businesses. Developers benefit from increased productivity and faster time-to-market, while operations teams experience reduced surplus through automated allocations and maintenance. For businesses, serverless computing translates into cost savings, scalability and agility, encouraging creativity and rapid responses to market demands. Additionally, serverless computing benefits various industries and business sizes, providing scalability and efficiency for large enterprises and enabling development acceleration for small and medium-sized businesses. Source links: [6][7][8]"
					}
				};

				_context.Topics.AddRange(topics);
				await _context.SaveChangesAsync();

				await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Topics OFF");
				await transaction.CommitAsync();
			});

		}
	}

	private async Task SeedAboutsAsync()
	{
		if (!_context.Abouts.Any())
		{
			var strategy = _context.Database.CreateExecutionStrategy();

			await strategy.ExecuteAsync(async () =>
			{
				using var transaction = await _context.Database.BeginTransactionAsync();
				await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Abouts ON");

				var abouts = new List<About>
				{
					new About
					{
						Id = 1,
						Title = "Thesis",
						Overview = "This study explores modern cloud architectures' implementation. It compares server-based applications with serverless functions, analyzing different topics of discussion. It provides insights for developers and companies considering cloud architectures, aiming for a balanced view.\nThe practical component involves developing a secure full-stack system for production use with a database, an API, a Front-End and Deployment to Cloud providers.\nIn this section, tools used to develop the system will be covered.",
						Language = "The system is developed using the programming language C# primarily due to the education's focus on .NET full stack development with C#.\nC# offers robustness, type safety and extensive support for object-oriented programming. Its design is tailored for .NET, ensuring smooth integration with .NET libraries and tools. The language is also a popular choice for web development with a wide user network and ample support resources.\nSource-links: [18]",
						Framework = ".NET 8.0 is chosen as the framework for its advanced features, performance enhancements and support for modern development methodologies. It integrates support for building web APIs with features like model binding, validation and a built-in dependency injection container.\nSource-links: [19][20]",
						API = "The API is deployed on Render, which offers a free tier suitable for small projects or personal use. Render provides automatic HTTPS. It was chosen over other hosting services for its simplicity, cost-effectiveness and comprehensive features, even in the free tier.\nTo ensure the API can run on the Render platform, a Dockerfile specifying the application's environment and settings is required.\n\nUpdate: The API is now deployed on Microsoft Azure using App Service and Docker. Azure provides better scalability, integration with infrastructure provisioning (Terraform), and enterprise-level reliability.\nSource-links: [15][16]",
						Database = "The database used is PostgreSQL, deployed on Supabase.\nSupabase offers a free tier and is a powerful open-source alternative to Firebase. It seamlessly interacts with PostgreSQL and offers a user-friendly web interface for database management. PostgreSQL offers search capabilities with full text and support for advanced query operations.\n\nUpdate: Supabase has been replaced with Azure SQL (SQL Server). The database is provisioned and managed using Terraform. This aligns with the .NET ecosystem and offers tighter integration with other Azure services.\nSource-links: [17]",
						Security = "Authentication and authorization for user management, Entity Framework Identity was chosen, providing a framework for managing user accounts, roles and permissions. It supports features like account activation, password reset and other management functions.\nAdditionally, with the use of .NET 8.0, built-in authentication and authorization features are provided, eliminating the need for external libraries like JWT.\nSource-links: [13][14]",
						FrontEnd = "React is chosen as the front-end development tool instead of Blazor because of its longer tenure and broader user network compared to Blazor. This means there is a wider range of resources, instructions and libraries available to facilitate the development process.\nIn terms of performance, studies have shown that React is generally faster than Blazor, especially for complex and large applications.\nIn the job market, React is more attractive; meaning knowledge of React can open up more career opportunities in the future.\nLastly, React's foundation in JavaScript makes it easy to integrate with other JavaScript libraries and frameworks, providing flexibility and extended functionality in the project.\nSource-links: [21][22]",
						Test = "For testing, unit tests were conducted using MSTest. The choice was based on its stable integration with Entity Framework and its ability to effectively test APIs.\nMSTest provides good tools for isolating and testing individual components, which is important to ensure the API works correctly and reliably.\nSource-links: [23]",
						VersionControl = "Git was used for version control, while GitHub was used as the code repository.\nGitHub Packages are also offered along with tokens to send Docker images to a registry and then deploy the API on Render's platform. This helps maintain sustainability and flexibility.",
						Challenges = "CI/CD with GitHub Actions is implemented to improve deployment. The CI part for Render succeeds, but more research is needed to resolve issues with the CD part. The solution in the project is to handle deployment manually with GitHub Packages containing the Docker Image for the API sent by the CI part.\nMicrosoft Azure CI/CD succeeds, however the Azure portal does not recognize the port exposed in the Dockerfile. Ongoing efforts include adding port descriptions in both CI/CD and Azure portal's resources.\n\nUpdate: The port recognition issue in Azure has been resolved by explicitly configuring port exposure in the Dockerfile and Azure App Service. The application is now fully deployed using GitHub Actions and Azure.",
						Improvements = "An area for improvement is to introduce integration tests in addition to unit tests to ensure that different components work together and identify issues that unit tests might not detect. This would increase system reliability and identify potential faults at an early stage.\nThe project can also limit and control various policies for CORS configuration."
					}
				};

				_context.Abouts.AddRange(abouts);
				await _context.SaveChangesAsync();

				await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Abouts OFF");
				await transaction.CommitAsync();
			});
		}
	}

	private async Task SeedTopicSourcesAsync()
	{
		if (!_context.TopicSources.Any())
		{
			var topicSources = new List<TopicSource>
			{
				new TopicSource { TopicId = 1, SourceId = 1 },
				new TopicSource { TopicId = 1, SourceId = 2 },
				new TopicSource { TopicId = 1, SourceId = 9 },
				new TopicSource { TopicId = 1, SourceId = 10 },
				new TopicSource { TopicId = 1, SourceId = 12 },
				new TopicSource { TopicId = 2, SourceId = 3 },
				new TopicSource { TopicId = 2, SourceId = 4 },
				new TopicSource { TopicId = 2, SourceId = 5 },
				new TopicSource { TopicId = 2, SourceId = 6 },
				new TopicSource { TopicId = 2, SourceId = 7 },
				new TopicSource { TopicId = 2, SourceId = 8 },
				new TopicSource { TopicId = 2, SourceId = 9 },
				new TopicSource { TopicId = 2, SourceId = 11 }
			};

			_context.TopicSources.AddRange(topicSources);
			await _context.SaveChangesAsync();
		}
	}

	private async Task SeedAboutSourcesAsync()
	{
		if (!_context.AboutSources.Any())
		{
			var aboutSources = new List<AboutSource>
			{
				new AboutSource { AboutId = 1, SourceId = 13 },
				new AboutSource { AboutId = 1, SourceId = 14 },
				new AboutSource { AboutId = 1, SourceId = 15 },
				new AboutSource { AboutId = 1, SourceId = 16 },
				new AboutSource { AboutId = 1, SourceId = 17 },
				new AboutSource { AboutId = 1, SourceId = 18 },
				new AboutSource { AboutId = 1, SourceId = 19 },
				new AboutSource { AboutId = 1, SourceId = 20 },
				new AboutSource { AboutId = 1, SourceId = 21 },
				new AboutSource { AboutId = 1, SourceId = 22 },
				new AboutSource { AboutId = 1, SourceId = 23 }
			};

			_context.AboutSources.AddRange(aboutSources);
			await _context.SaveChangesAsync();
		}
	}

	private async Task SeedImagesAsync()
	{
		if (!_context.Images.Any())
		{
			var strategy = _context.Database.CreateExecutionStrategy();

			await strategy.ExecuteAsync(async () =>
			{
				using var transaction = await _context.Database.BeginTransactionAsync();
				await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Images ON");

				var images = new List<Image>
				{
					new Image { Id = 1, Title = "Introductions", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverbased-introduction.webp", TopicId = 1 },
					new Image { Id = 2, Title = "Advantages", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverbased-advantages.webp", TopicId = 1 },
					new Image { Id = 3, Title = "Approaches", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverbased-approach.webp", TopicId = 1 },
					new Image { Id = 5, Title = "Comparisons", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverbased-comparison.webp", TopicId = 1 },
					new Image { Id = 6, Title = "IndustryInsights", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverbased-industryInsights.webp", TopicId = 1 },
					new Image { Id = 7, Title = "UseCases", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverbased-useCases.webp", TopicId = 1 },
					new Image { Id = 8, Title = "Limitations", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverbased-limitations.webp", TopicId = 1 },
					new Image { Id = 9, Title = "Introductions", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverless-introduction.webp", TopicId = 2 },
					new Image { Id = 10, Title = "Advantages", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverless-advantages.webp", TopicId = 2 },
					new Image { Id = 11, Title = "Approaches", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverless-approach.webp", TopicId = 2 },
					new Image { Id = 13, Title = "Comparisons", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverless-comparison.webp", TopicId = 2 },
					new Image { Id = 14, Title = "IndustryInsights", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverless-industryInsights.webp", TopicId = 2 },
					new Image { Id = 15, Title = "UseCases", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverless-useCases.webp", TopicId = 2 },
					new Image { Id = 16, Title = "Limitations", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverless-limitations.webp", TopicId = 2 },
					new Image { Id = 4, Title = "Beneficiaries", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverbased-beneficiaries.webp", TopicId = 1 },
					new Image { Id = 12, Title = "Beneficiaries", Url = "https://serveranalysisimages.blob.core.windows.net/images/serverless-beneficiaries.webp", TopicId = 2 }
				};

				_context.Images.AddRange(images);
				await _context.SaveChangesAsync();

				await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Images OFF");
				await transaction.CommitAsync();
			});
		}
	}

}
