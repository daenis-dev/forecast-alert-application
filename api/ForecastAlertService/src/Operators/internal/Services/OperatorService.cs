using Microsoft.EntityFrameworkCore;
using ForecastAlertService.Data;
using ForecastAlertService.Models;

namespace ForecastAlertService.Services
{
    internal class OperatorService: IGetAllOperators
    {
        private readonly AppDbContext _context;

        public OperatorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OperatorDto>> GetAllOperatorsAsync()
        {
            return await _context.Operators
                .Select(o => new OperatorDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Symbol = o.Symbol
                })
                .ToListAsync();
        }
    }
}
