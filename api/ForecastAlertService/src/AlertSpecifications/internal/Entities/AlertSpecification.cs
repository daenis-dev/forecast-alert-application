using System;

namespace ForecastAlertService.Entities
{
    internal class AlertSpecification
    {
        internal int Id { get; set; }
        internal int AlertId { get; set; }
        internal int SpecificationId { get; set; }
        internal int OperatorId { get; set; }
        internal int ThresholdValue { get; set; }
        internal DateTime CreatedDateTimeUtc { get; set; } = DateTime.UtcNow;
        internal DateTime ModifiedDateTimeUtc { get; set; } = DateTime.UtcNow;

        // Navigation properties
        internal Alert Alert { get; set; }
        internal Specification Specification { get; set; }
        internal Operator Operator { get; set; }

    }
}