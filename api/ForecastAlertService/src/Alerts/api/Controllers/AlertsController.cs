using Microsoft.AspNetCore.Mvc;
using ForecastAlertService.Services;
using ForecastAlertService.Models;

namespace ForecastAlertService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlertsController : ControllerBase
    {
        private readonly IGetAllAlerts _getAllAlerts;
        private readonly ICreateAlert _createAlert;

        public AlertsController(IGetAllAlerts getAllAlerts, ICreateAlert createAlert)
        {
            _getAllAlerts = getAllAlerts;
            _createAlert = createAlert;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlertDto>>> GetAlerts()
        {
            var alerts = await _getAllAlerts.GetAllAlertsAsync();
            return Ok(alerts);
        }

        [HttpPost]
        public async Task<ActionResult<AlertDto>> CreateAlert([FromBody] AlertRequest alertRequest)
        {
            if (alertRequest == null)
            {
                return BadRequest("Alert data is null.");
            }

            var createdAlert = await _createAlert.CreateAlertAsync(alertRequest);
            return CreatedAtAction(nameof(GetAlerts), new { id = createdAlert.Id }, createdAlert);
        }
    }
}