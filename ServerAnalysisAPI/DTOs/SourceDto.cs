namespace ServerAnalysisAPI.DTOs;

public class SourceDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public int? ReferenceNumber { get; set; }
}