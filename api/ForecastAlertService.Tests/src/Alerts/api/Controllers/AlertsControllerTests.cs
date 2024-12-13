using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ForecastAlertService.Controllers;
using ForecastAlertService.Models;
using ForecastAlertService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AlertsControllerTests
{
    private readonly Mock<IGetAllAlerts> _getAllAlertsMock;
    private readonly Mock<ICreateAlert> _createAlertMock;
    private readonly AlertsController _controller;

    public AlertsControllerTests()
    {
        _getAllAlertsMock = new Mock<IGetAllAlerts>();
        _createAlertMock = new Mock<ICreateAlert>();
        _controller = new AlertsController(_getAllAlertsMock.Object, _createAlertMock.Object);
    }

    [Fact]
    public async Task GetAlerts_ReturnsList()
    {
        var alertList = new List<AlertDto>
        {
            new AlertDto { Id = 1, Name = "Test Alert 1" },
            new AlertDto { Id = 2, Name = "Test Alert 2" }
        };

        _getAllAlertsMock.Setup(service => service.GetAllAlertsAsync())
            .ReturnsAsync(alertList);

        var result = await _controller.GetAlerts();

        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult); 

        var returnValue = okResult?.Value as List<AlertDto>;
        Assert.NotNull(returnValue);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task CreateAlert_ReturnsCreatedAtActionResult_WhenAlertIsCreated()
    {
        var alertRequest = new AlertRequest { Name = "Bring a Big Umbrella" };
        var createdAlert = new AlertDto { Id = 1, Name = "Bring a Big Umbrella" };
        
        _createAlertMock.Setup(service => service.CreateAlertAsync(alertRequest))
            .ReturnsAsync(createdAlert);

        var result = await _controller.CreateAlert(alertRequest);

        var createdResult = result.Result as CreatedAtActionResult;
        Assert.NotNull(createdResult);

        var returnValue = createdResult?.Value as AlertDto;
        Assert.NotNull(returnValue);
        Assert.Equal(createdAlert.Id, returnValue?.Id);
        Assert.Equal(createdAlert.Name, returnValue?.Name);
    }

    [Fact]
    public async Task CreateAlert_ReturnsBadRequest_WhenAlertDataIsNull()
    {
        var result = await _controller.CreateAlert(null);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.NotNull(badRequestResult);
        Assert.Equal("Alert data is null.", badRequestResult?.Value);
    }
}
