using ForecastAlertService.Models;

public interface IGetAllSpecifications {

    Task<List<SpecificationDto>> GetAllSpecificationsAsync();
}