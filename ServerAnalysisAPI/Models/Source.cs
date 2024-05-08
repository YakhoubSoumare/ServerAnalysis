namespace ServerAnalysisAPI.Models
{
    public class Source: IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Link { get; set; }
        public string? Purpose { get; set; }
        public int? ReferenceNumber { get; set; }
        public virtual ICollection<ServerBasedApplicationSource>? ServerBasedApplicationSources { get; set; }
        public virtual ICollection<ServerlessFunctionSource>? ServerlessFunctionSources { get; set; }
        public virtual ICollection<BenefitSource>? BenefitSources { get; set; }
    }
}