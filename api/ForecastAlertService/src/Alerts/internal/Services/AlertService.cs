using Microsoft.EntityFrameworkCore;
using ForecastAlertService.Data;
using ForecastAlertService.Models;
using ForecastAlertService.Entities;

namespace ForecastAlertService.Services
{
    internal class AlertService: IGetAllAlerts, ICreateAlert
    {
        private readonly AppDbContext _context;

        public AlertService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AlertDto>> GetAllAlertsAsync()
        {
            return await _context.Alerts
                .Select(o => new AlertDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    IsUrgent = o.IsUrgent,
                    CreatedDateTimeUtc = o.CreatedDateTimeUtc,
                    ModifiedDateTimeUtc = o.ModifiedDateTimeUtc
                })
                .ToListAsync();
        }

        public async Task<AlertDto> CreateAlertAsync(AlertRequest alertRequest)
        {
            var alert = new Alert
            {
                Name = alertRequest.Name,
                IsUrgent = alertRequest.IsUrgent,
                CreatedDateTimeUtc = DateTime.UtcNow,
                ModifiedDateTimeUtc = DateTime.UtcNow
            };

            _context.Alerts.Add(alert);
            await _context.SaveChangesAsync();

            return new AlertDto
            {
                Id = alert.Id,
                Name = alert.Name,
                IsUrgent = alert.IsUrgent,
                CreatedDateTimeUtc = alert.CreatedDateTimeUtc,
                ModifiedDateTimeUtc = alert.ModifiedDateTimeUtc
            };
        }
    }
}