namespace ServerAnalysisAPI.Context;

public class DataContext : IdentityDbContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{
	}

	public DbSet<Source> Sources => Set<Source>();
	public DbSet<Benefit> Benefits => Set<Benefit>();
	public DbSet<Topic> Topics => Set<Topic>();
	public DbSet<ServerBasedApplication> ServerBasedApplications => Set<ServerBasedApplication>();
	public DbSet<ServerlessFunction> ServerlessFunctions => Set<ServerlessFunction>();
	public DbSet<ServerBasedApplicationSource> ServerBasedApplicationSources => Set<ServerBasedApplicationSource>();
	public DbSet<ServerlessFunctionSource> ServerlessFunctionSources => Set<ServerlessFunctionSource>();
	public DbSet<BenefitSource> BenefitSources => Set<BenefitSource>();

	// Configures the relationships between the entities
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Benefit>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.HasOne(d => d.Topic)
				.WithOne()
				.HasForeignKey<Benefit>(d => d.TopicId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<Topic>(entity =>
		{
			entity.HasKey(e => e.Id);

			entity.HasMany<Benefit>() // No navigation property, so specify the type directly
				.WithOne(p => p.Topic)
				.HasForeignKey(p => p.TopicId)
				.OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<Source>(entity =>
		{
			entity.HasKey(e => e.Id);
		});

		modelBuilder.Entity<ServerBasedApplication>(entity =>
		{
			entity.HasKey(e => e.Id);

			entity.HasOne(d => d.Benefit)
				.WithMany(p => p.ServerBasedApplications)
				.HasForeignKey(d => d.BenefitId)
				.OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<ServerlessFunction>(entity =>
		{
			entity.HasKey(e => e.Id);

			entity.HasOne(d => d.Benefit)
				.WithMany(p => p.ServerlessFunctions)
				.HasForeignKey(d => d.BenefitId)
				.OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<BenefitSource>(entity =>
		{
			entity.HasKey(e => new { e.BenefitId, e.SourceId });

			entity.HasOne(d => d.Benefit)
				.WithMany(p => p.BenefitSources)
				.HasForeignKey(d => d.BenefitId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(d => d.Source)
				.WithMany(p => p.BenefitSources)
				.HasForeignKey(d => d.SourceId)
				.OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<ServerBasedApplicationSource>(entity =>
		{
			entity.HasKey(e => new { e.ServerBasedApplicationId, e.SourceId });

			entity.HasOne(d => d.ServerBasedApplication)
				.WithMany(p => p.ServerBasedApplicationSources)
				.HasForeignKey(d => d.ServerBasedApplicationId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(d => d.Source)
				.WithMany(p => p.ServerBasedApplicationSources)
				.HasForeignKey(d => d.SourceId)
				.OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<ServerlessFunctionSource>(entity =>
		{
			entity.HasKey(e => new { e.ServerlessFunctionId, e.SourceId });

			entity.HasOne(d => d.ServerlessFunction)
				.WithMany(p => p.ServerlessFunctionSources)
				.HasForeignKey(d => d.ServerlessFunctionId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(d => d.Source)
				.WithMany(p => p.ServerlessFunctionSources)
				.HasForeignKey(d => d.SourceId)
				.OnDelete(DeleteBehavior.Cascade);
		});
	}
}
