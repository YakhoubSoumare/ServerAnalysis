namespace ServerAnalysisAPI.Models;

public class TopicSource : IReferenceEntity
{
	public int TopicId { get; set; }
	public int SourceId { get; set; }
	public Topic? Topic { get; set; }
	public Source? Source { get; set; }
}
