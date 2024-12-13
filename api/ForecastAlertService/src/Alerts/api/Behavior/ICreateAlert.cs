using ForecastAlertService.Models;

public interface ICreateAlert {

    Task<AlertDto> CreateAlertAsync(AlertRequest alertRequest);
}