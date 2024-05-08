namespace ServerAnalysisAPI.Models
{
    public class ServerlessFunction: IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Introduction { get; set; }
        public string? Approach { get; set; }
        public string? UseCases { get; set; }
        public string? Limitations { get; set; }
        public string? Purpose { get; set; }
        public int BenefitId { get; set; }
        public Benefit? Benefit { get; set; }
        public virtual ICollection<ServerlessFunctionSource>? ServerlessFunctionSources { get; set; }
    }
}