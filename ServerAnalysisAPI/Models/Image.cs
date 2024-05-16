namespace ServerAnalysisAPI.Models;

public class Image : IEntity
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Url { get; set; } = string.Empty;
	public int TopicId { get; set;} 
	public Topic? Topic { get; set; }
}

