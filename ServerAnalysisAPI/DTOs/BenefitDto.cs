namespace ServerAnalysisAPI.DTOs
{
    public class BenefitDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Advantages { get; set; }
        public string? Comparison { get; set; }
        public string? IndustryInsights { get; set; }
        public string? Beneficiaries { get; set; }
        public string? Purpose { get; set; }
        public int TopicId { get; set; }
    }
}