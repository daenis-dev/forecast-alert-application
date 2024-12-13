using Microsoft.EntityFrameworkCore;
using ForecastAlertService.Data;
using ForecastAlertService.Models;
using ForecastAlertService.Entities;

namespace ForecastAlertService.Services
{
    internal class AlertSpecificationService: IGetAllAlertSpecifications, ICreateAlertSpecification
    {
        private readonly AppDbContext _context;

        public AlertSpecificationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AlertSpecificationDto>> GetAllAlertSpecificationsAsync()
        {
            return await _context.AlertSpecifications
                .Include(aspec => aspec.Alert)
                .Include(aspec => aspec.Specification)
                .Include(aspec => aspec.Operator)
                .Select(aspec => new AlertSpecificationDto
                {
                    Id = aspec.Id,
                    AlertId = aspec.AlertId,
                    AlertName = aspec.Alert.Name,
                    SpecificationId = aspec.SpecificationId,
                    SpecificationName = aspec.Specification.Name,
                    OperatorId = aspec.OperatorId,
                    OperatorSymbol = aspec.Operator.Symbol,
                    ThresholdValue = aspec.ThresholdValue,
                    CreatedDateTimeUtc = aspec.CreatedDateTimeUtc,
                    ModifiedDateTimeUtc = aspec.ModifiedDateTimeUtc
                })
                .ToListAsync();
        }

        public async Task<AlertSpecificationDto> CreateAlertSpecificationAsync(AlertSpecificationRequest alertSpecificationRequest)
        {
            var alertExists = await _context.Alerts.AnyAsync(a => a.Id == alertSpecificationRequest.AlertId);
            if (!alertExists)
            {
                throw new ArgumentException($"Alert with ID {alertSpecificationRequest.AlertId} does not exist.");
            }

            var specificationExists = await _context.Specifications.AnyAsync(s => s.Id == alertSpecificationRequest.SpecificationId);
            if (!specificationExists)
            {
                throw new ArgumentException($"Specification with ID {alertSpecificationRequest.SpecificationId} does not exist.");
            }

            var operatorExists = await _context.Operators.AnyAsync(o => o.Id == alertSpecificationRequest.OperatorId);
            if (!operatorExists)
            {
                throw new ArgumentException($"Operator with ID {alertSpecificationRequest.OperatorId} does not exist.");
            }

            var alertSpecification = new AlertSpecification
            {
                AlertId = alertSpecificationRequest.AlertId,
                SpecificationId = alertSpecificationRequest.SpecificationId,
                OperatorId = alertSpecificationRequest.OperatorId,
                ThresholdValue = alertSpecificationRequest.ThresholdValue,
                CreatedDateTimeUtc = DateTime.UtcNow,
                ModifiedDateTimeUtc = DateTime.UtcNow
            };

            _context.AlertSpecifications.Add(alertSpecification);
            await _context.SaveChangesAsync();

            var alert = await _context.Alerts.FindAsync(alertSpecification.AlertId);
            var specification = await _context.Specifications.FindAsync(alertSpecification.SpecificationId);
            var operatorEntity = await _context.Operators.FindAsync(alertSpecification.OperatorId);

            var createdAlertSpecification = new AlertSpecificationDto
            {
                AlertId = alertSpecification.AlertId,
                AlertName = alert?.Name,
                SpecificationId = alertSpecification.SpecificationId,
                SpecificationName = specification?.Name,
                OperatorId = alertSpecification.OperatorId,
                OperatorSymbol = operatorEntity?.Symbol,
                ThresholdValue = alertSpecification.ThresholdValue,
                CreatedDateTimeUtc = alertSpecification.CreatedDateTimeUtc,
                ModifiedDateTimeUtc = alertSpecification.ModifiedDateTimeUtc
            };

            return createdAlertSpecification;
        }

    }
}