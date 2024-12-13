namespace ForecastAlertService.Models
{
    public class AlertDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsUrgent { get; set; } = false;
        public DateTime CreatedDateTimeUtc { get; set; }
        public DateTime ModifiedDateTimeUtc { get; set; }
    }
}
