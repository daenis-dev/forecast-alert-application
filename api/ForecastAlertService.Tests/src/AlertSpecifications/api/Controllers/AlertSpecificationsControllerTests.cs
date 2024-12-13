using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ForecastAlertService.Controllers;
using ForecastAlertService.Models;
using ForecastAlertService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AlertSpecificationsControllerTests
{
    private readonly Mock<IGetAllAlertSpecifications> _getAllAlertSpecificationsMock;
    private readonly Mock<ICreateAlertSpecification> _createAlertSpecificationMock;
    private readonly AlertSpecificationsController _controller;

    public AlertSpecificationsControllerTests()
    {
        _getAllAlertSpecificationsMock = new Mock<IGetAllAlertSpecifications>();
        _createAlertSpecificationMock = new Mock<ICreateAlertSpecification>();
        _controller = new AlertSpecificationsController(_getAllAlertSpecificationsMock.Object, _createAlertSpecificationMock.Object);
    }

    [Fact]
    public async Task GetAlertSpecifications_ReturnsList()
    {
        var alertSpecificationList = new List<AlertSpecificationDto>
        {
            new AlertSpecificationDto 
            {
                Id = 1, 
                AlertId = 101, 
                AlertName = "Temperature Alert", 
                SpecificationId = 1, 
                SpecificationName = "Max Temperature", 
                OperatorId = 1, 
                OperatorSymbol = ">", 
                ThresholdValue = 75, 
                CreatedDateTimeUtc = DateTime.UtcNow.AddDays(-1),
                ModifiedDateTimeUtc = DateTime.UtcNow.AddHours(-2)
            },
            new AlertSpecificationDto 
            {
                Id = 2, 
                AlertId = 102, 
                AlertName = "Humidity Alert", 
                SpecificationId = 2, 
                SpecificationName = "Max Humidity", 
                OperatorId = 2, 
                OperatorSymbol = "<", 
                ThresholdValue = 50, 
                CreatedDateTimeUtc = DateTime.UtcNow.AddDays(-2),
                ModifiedDateTimeUtc = DateTime.UtcNow.AddHours(-4)
            }
        };

        _getAllAlertSpecificationsMock.Setup(service => service.GetAllAlertSpecificationsAsync())
            .ReturnsAsync(alertSpecificationList);

        var result = await _controller.GetAlertSpecifications();

        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult); 

        var returnValue = okResult?.Value as List<AlertSpecificationDto>;
        Assert.NotNull(returnValue);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task CreateAlertSpecification_ReturnsCreatedAtActionResult_WhenAlertSpecificationIsCreated()
    {
        var alertSpecificationRequest = new AlertSpecificationRequest 
        {
            Id = 1,
            AlertId = 101,
            SpecificationId = 1,
            OperatorId = 1,
            ThresholdValue = 75,
            CreatedDateTimeUtc = DateTime.UtcNow.AddDays(-1),
            ModifiedDateTimeUtc = DateTime.UtcNow.AddHours(-2)
        };

        var createdAlertSpecification = new AlertSpecificationDto
        {
            Id = 1,
            AlertId = 101,
            AlertName = "Bring a Big Umbrella",
            SpecificationId = 1,
            SpecificationName = "Max Rain",
            OperatorId = 1,
            OperatorSymbol = ">",
            ThresholdValue = 75,
            CreatedDateTimeUtc = DateTime.UtcNow.AddDays(-1),
            ModifiedDateTimeUtc = DateTime.UtcNow.AddHours(-2)
        };
        
        _createAlertSpecificationMock.Setup(service => service.CreateAlertSpecificationAsync(alertSpecificationRequest))
            .ReturnsAsync(createdAlertSpecification);

        var result = await _controller.CreateAlertSpecification(alertSpecificationRequest);

        var createdResult = result.Result as CreatedAtActionResult;
        Assert.NotNull(createdResult);

        var returnValue = createdResult?.Value as AlertSpecificationDto;
        Assert.NotNull(returnValue);
        Assert.Equal(createdAlertSpecification.Id, returnValue?.Id);
        Assert.Equal(createdAlertSpecification.AlertName, returnValue?.AlertName);
    }

    [Fact]
    public async Task CreateAlertSpecification_ReturnsBadRequest_WhenAlertSpecificationDataIsNull()
    {
        var result = await _controller.CreateAlertSpecification(null);

        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.NotNull(badRequestResult);
        Assert.Equal("AlertSpecificationRequest cannot be null.", badRequestResult?.Value);
    }
}