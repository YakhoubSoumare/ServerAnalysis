namespace ServerAnalysisAPI.DTOs
{
    public class ServerBasedApplicationDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Introduction { get; set; }
        public string? Approach { get; set; }
        public string? UseCases { get; set; }
        public string? Limitations { get; set; }
        public string? Purpose { get; set; }
        public int BenefitId { get; set; }
    }
}