namespace ForecastAlertService.Models
{
    public class AlertRequest
    {
        public string Name { get; set; }
        public bool IsUrgent { get; set; } = false;
        public DateTime CreatedDateTimeUtc { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDateTimeUtc { get; set; } = DateTime.UtcNow;
    }
}
