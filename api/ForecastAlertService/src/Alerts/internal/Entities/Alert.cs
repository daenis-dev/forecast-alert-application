using System;

namespace ForecastAlertService.Entities
{
    internal class Alert
    {
        internal int Id { get; set; }
        internal string Name { get; set; }
        internal bool IsUrgent { get; set; } = false;
        internal DateTime CreatedDateTimeUtc { get; set; } = DateTime.UtcNow;
        internal DateTime ModifiedDateTimeUtc { get; set; } = DateTime.UtcNow;
        internal ICollection<AlertSpecification> AlertSpecifications { get; set; }
    }
}