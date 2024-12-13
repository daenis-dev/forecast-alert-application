using Microsoft.EntityFrameworkCore;
using ForecastAlertService.Data;
using ForecastAlertService.Models;

namespace ForecastAlertService.Services
{
    internal class SpecificationService: IGetAllSpecifications
    {
        private readonly AppDbContext _context;

        public SpecificationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SpecificationDto>> GetAllSpecificationsAsync()
        {
            return await _context.Specifications
                .Select(s => new SpecificationDto
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToListAsync();
        }
    }
}
