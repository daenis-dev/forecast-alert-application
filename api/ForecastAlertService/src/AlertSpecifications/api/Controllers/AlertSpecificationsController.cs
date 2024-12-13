using Microsoft.AspNetCore.Mvc;
using ForecastAlertService.Services;
using ForecastAlertService.Models;

namespace ForecastAlertService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlertSpecificationsController : ControllerBase
    {
        private readonly IGetAllAlertSpecifications _getAllAlertSpecifications;
        private readonly ICreateAlertSpecification _createAlertSpecification;

        public AlertSpecificationsController(IGetAllAlertSpecifications getAllAlertSpecifications, ICreateAlertSpecification createAlertSpecification)
        {
            _getAllAlertSpecifications = getAllAlertSpecifications;
            _createAlertSpecification = createAlertSpecification;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlertSpecificationDto>>> GetAlertSpecifications()
        {
            var alertSpecifications = await _getAllAlertSpecifications.GetAllAlertSpecificationsAsync();
            return Ok(alertSpecifications);
        }

        [HttpPost]
        public async Task<ActionResult<AlertSpecificationDto>> CreateAlertSpecification([FromBody] AlertSpecificationRequest alertSpecificationRequest)
        {
            if (alertSpecificationRequest == null)
            {
                return BadRequest("AlertSpecificationRequest cannot be null.");
            }

            try
            {
                var createdAlertSpecification = await _createAlertSpecification.CreateAlertSpecificationAsync(alertSpecificationRequest);
                return CreatedAtAction(nameof(GetAlertSpecifications), new { id = createdAlertSpecification.Id }, createdAlertSpecification);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the alert specification.");
            }
        }
    }
}