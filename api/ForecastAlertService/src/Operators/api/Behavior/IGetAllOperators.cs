using ForecastAlertService.Models;

public interface IGetAllOperators {

    Task<List<OperatorDto>> GetAllOperatorsAsync();
}