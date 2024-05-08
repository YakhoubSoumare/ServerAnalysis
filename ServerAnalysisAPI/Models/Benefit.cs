namespace ServerAnalysisAPI.Models
{
    public class Benefit: IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Advantages { get; set; }
        public string? Comparison { get; set; }
        public string? IndustryInsights { get; set; }
        public string? Beneficiaries { get; set; }
        public string? Purpose { get; set; }
        public int TopicId { get; set; }

        // Navigation properties
        public virtual ICollection<ServerBasedApplication>? ServerBasedApplications { get; set; }
        public ICollection<ServerlessFunction>? ServerlessFunctions { get; set; }
        public ICollection<BenefitSource>? BenefitSources { get; set; }
        public Topic Topic { get; set; }
    }
}