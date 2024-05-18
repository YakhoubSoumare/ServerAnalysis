namespace ServerAnalysisAPI.Models;

public class AboutSource : IReferenceEntity
{
	public int AboutId { get; set; }
	public int SourceId { get; set; }
	public About? About { get; set; }
	public Source? Source { get; set; }
}
