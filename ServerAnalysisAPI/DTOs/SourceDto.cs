namespace ServerAnalysisAPI.DTOs
{
    public class SourceDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public string? Purpose { get; set; }
        public int? ReferenceNumber { get; set; }
    }
}