namespace ServerAnalysisAPI.DTOs;
public class AboutDto
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Overview { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Framework { get; set; } = string.Empty;
    public string API { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public string Security { get; set; } = string.Empty;
    public string FrontEnd { get; set; } = string.Empty;
    public string Test { get; set; } = string.Empty;
    public string VersionControl { get; set; } = string.Empty;
    public string Challenges { get; set; } = string.Empty;
	public string Improvements { get; set; } = string.Empty;
}