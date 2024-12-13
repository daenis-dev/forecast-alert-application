using ForecastAlertService.Models;

public interface ICreateAlertSpecification {

    Task<AlertSpecificationDto> CreateAlertSpecificationAsync(AlertSpecificationRequest alertSpecificationRequest);
}