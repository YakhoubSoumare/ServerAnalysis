namespace ServerAnalysisAPI.Models
{
    public class ServerBasedApplicationSource: IReferenceEntity
    {
        // Foreign keys
        public int ServerBasedApplicationId { get; set; }
        public int SourceId { get; set; }

        // Navigation properties
        public ServerBasedApplication? ServerBasedApplication  { get; set; }
        public Source? Source { get; set; }
    }
}