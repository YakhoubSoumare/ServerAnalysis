namespace ServerAnalysisAPI.Models
{
    public class ServerlessFunctionSource: IReferenceEntity
    {
        public int ServerlessFunctionId { get; set; }
        public int SourceId { get; set; }
        public ServerlessFunction? ServerlessFunction { get; set; }
        public Source? Source { get; set; }
    }
}