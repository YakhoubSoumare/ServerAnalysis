namespace ServerAnalysisAPI.DTOs;

public class ImageDto
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Url { get; set; } = string.Empty;
	public int TopicId { get; set; }
}