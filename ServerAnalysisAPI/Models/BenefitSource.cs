namespace ServerAnalysisAPI.Models
{
    public class BenefitSource: IReferenceEntity
    {
        public int BenefitId { get; set; }
        public int SourceId { get; set; }
        public Benefit? Benefit { get; set; }
        public Source? Source { get; set; }
    }
}