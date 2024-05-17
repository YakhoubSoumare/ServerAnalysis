namespace ServerAnalysisAPI.Context;

public class DataContext : IdentityDbContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{
	}

	public DbSet<Source> Sources => Set<Source>();
	public DbSet<Topic> Topics => Set<Topic>();
	public DbSet<About> Abouts => Set<About>();
	public DbSet<TopicSource> TopicSources => Set<TopicSource>();
	public DbSet<AboutSource> AboutSources => Set<AboutSource>();
	public DbSet<Image> Images => Set<Image>();

	// Configures the relationships between the entities
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Image>(entity =>
		{
			entity.HasKey(e => e.Id);

			entity.HasOne(d => d.Topic)
			.WithMany(p => p.Images)
			.HasForeignKey(d => d.TopicId)
			.OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<Topic>(entity =>
		{
			entity.HasKey(e => e.Id);
		});

		modelBuilder.Entity<About>(entity =>
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

		modelBuilder.Entity<AboutSource>(entity =>
		{
			entity.HasKey(e => new { e.AboutId, e.SourceId });

			entity.HasOne(d => d.About)
				.WithMany(p => p.AboutSources)
				.HasForeignKey(d => d.AboutId)
				.OnDelete(DeleteBehavior.Cascade);

			entity.HasOne(d => d.Source)
				.WithMany(p => p.AboutSources)
				.HasForeignKey(d => d.SourceId)
				.OnDelete(DeleteBehavior.Cascade);
		});

	}
}
