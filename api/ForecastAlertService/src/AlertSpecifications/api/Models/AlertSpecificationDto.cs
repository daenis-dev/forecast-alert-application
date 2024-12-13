namespace ForecastAlertService.Models
{
    public class AlertSpecificationDto
    {
        public int Id { get; set; }
        public int AlertId { get; set; }
        public string AlertName { get; set; }
        public int SpecificationId { get; set; }
        public string SpecificationName { get; set; }
        public int OperatorId { get; set; }
        public string OperatorSymbol { get; set; }
        public int ThresholdValue { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDateTimeUtc { get; set; } = DateTime.UtcNow;
    }
}