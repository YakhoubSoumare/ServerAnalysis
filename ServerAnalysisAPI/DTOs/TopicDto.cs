namespace ServerAnalysisAPI.DTOs;

public class TopicDto
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Introductions { get; set; } = string.Empty;
	public string Approaches { get; set; } = string.Empty;
	public string UseCases { get; set; } = string.Empty;
	public string Limitations { get; set; } = string.Empty;
	public string Advantages { get; set; } = string.Empty;
	public string Comparisons { get; set; } = string.Empty;
	public string IndustryInsights { get; set; } = string.Empty;
	public string Beneficiaries { get; set; } = string.Empty;
}