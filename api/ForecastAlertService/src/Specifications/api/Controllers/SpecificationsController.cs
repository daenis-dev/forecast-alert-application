using Microsoft.AspNetCore.Mvc;
using ForecastAlertService.Services;
using ForecastAlertService.Models;

namespace ForecastAlertService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecificationsController : ControllerBase
    {
        private readonly IGetAllSpecifications _getAllSpecifications;

        public SpecificationsController(IGetAllSpecifications getAllSpecifications)
        {
            _getAllSpecifications = getAllSpecifications;
        }

        [HttpGet]
        public async Task<ActionResult<List<SpecificationDto>>> GetSpecifications()
        {
            var specifications = await _getAllSpecifications.GetAllSpecificationsAsync();
            return Ok(specifications);
        }
    }
}