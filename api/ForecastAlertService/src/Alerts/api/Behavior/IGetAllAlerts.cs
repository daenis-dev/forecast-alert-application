using ForecastAlertService.Models;

public interface IGetAllAlerts {

    Task<List<AlertDto>> GetAllAlertsAsync();
}