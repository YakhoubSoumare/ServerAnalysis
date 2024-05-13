namespace ServerAnalysisAPI.DTOs
{
    public class TopicDto
    {
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Introduction { get; set; } = string.Empty;
		public string Approach { get; set; } = string.Empty;
		public string UseCases { get; set; } = string.Empty;
		public string Limitations { get; set; } = string.Empty;
		public string Advantages { get; set; } = string.Empty;
		public string Comparison { get; set; } = string.Empty;
		public string IndustryInsights { get; set; } = string.Empty;
		public string Beneficiaries { get; set; } = string.Empty;
	}
}