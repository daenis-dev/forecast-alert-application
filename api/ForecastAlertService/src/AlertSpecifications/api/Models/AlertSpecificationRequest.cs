namespace ForecastAlertService.Models
{
    public class AlertSpecificationRequest
    {
        public int Id { get; set; }
        public int AlertId { get; set; }
        public int SpecificationId { get; set; }
        public int OperatorId { get; set; }
        public int ThresholdValue { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDateTimeUtc { get; set; } = DateTime.UtcNow;
    }
}