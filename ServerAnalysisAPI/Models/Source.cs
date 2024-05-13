namespace ServerAnalysisAPI.Models
{
    public class Source: IEntity
    {
        public int Id { get; set; }
        [Required]
		public string Title { get; set; } = string.Empty;
		public string Link { get; set; } = string.Empty;
		public int? ReferenceNumber { get; set; }
		public virtual ICollection<TopicSource>? TopicSources { get; set; }
    }
}