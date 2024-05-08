namespace ServerAnalysisAPI.Context;

public class DataContext : IdentityDbContext
{
	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{
	}
}
