namespace ServerAnalysisAPI.Context;

public class DataContext : IdentityDbContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{
	}

	public DbSet<Source> Sources => Set<Source>();
	public DbSet<Topic> Topics => Set<Topic>();
	public DbSet<TopicSource> TopicSources => Set<TopicSource>();

	// Configures the relationships between the entities
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Topic>(entity =>
		{
			entity.HasKey(e => e.Id);
		});

		modelBuilder.Entity<Source>(entity =>
		{
			entity.HasKey(e => e.Id);
		});

		modelBuilder.Entity<TopicSource>(entity =>
		{
			entity.HasKey(e => new { e.TopicId, e.SourceId });

			entity.HasOne(d => d.Topic)
				.WithMany(p => p.TopicSources)
				.HasForeignKey(d => d.TopicId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(d => d.Source)
				.WithMany(p => p.TopicSources)
				.HasForeignKey(d => d.SourceId)
				.OnDelete(DeleteBehavior.Cascade);
		});

	}
}
