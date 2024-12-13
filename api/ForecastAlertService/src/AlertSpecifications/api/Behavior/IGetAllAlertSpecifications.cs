using ForecastAlertService.Models;

public interface IGetAllAlertSpecifications {

    Task<List<AlertSpecificationDto>> GetAllAlertSpecificationsAsync();
}