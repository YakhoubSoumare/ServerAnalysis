namespace ServerAnalysisAPI.Models
{
    public class Topic: IEntity
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}