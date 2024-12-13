using Microsoft.AspNetCore.Mvc;
using ForecastAlertService.Services;
using ForecastAlertService.Models;

namespace ForecastAlertService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperatorsController : ControllerBase
    {
        private readonly IGetAllOperators _getAllOperators;

        public OperatorsController(IGetAllOperators getAllOperators)
        {
            _getAllOperators = getAllOperators;
        }

        [HttpGet]
        public async Task<ActionResult<List<OperatorDto>>> GetOperators()
        {
            var operators = await _getAllOperators.GetAllOperatorsAsync();
            return Ok(operators);
        }
    }
}