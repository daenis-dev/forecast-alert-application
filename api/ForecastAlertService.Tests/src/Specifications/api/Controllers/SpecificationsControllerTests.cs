using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ForecastAlertService.Controllers;
using ForecastAlertService.Models;
using ForecastAlertService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SpecificationsControllerTests
{
    private readonly Mock<IGetAllSpecifications> _getAllSpecificationsMock;
    private readonly SpecificationsController _controller;

    public SpecificationsControllerTests()
    {
        _getAllSpecificationsMock = new Mock<IGetAllSpecifications>();
        _controller = new SpecificationsController(_getAllSpecificationsMock.Object);
    }

    [Fact]
    public async Task GetSpecifications_ReturnsList()
    {
        var specificationList = new List<SpecificationDto>
        {
            new SpecificationDto { Id = 1, Name = "wind in miles per hour" },
            new SpecificationDto { Id = 2, Name = "chance of precipitation" }
        };

        _getAllSpecificationsMock.Setup(service => service.GetAllSpecificationsAsync())
            .ReturnsAsync(specificationList);

        var result = await _controller.GetSpecifications();

        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult); 

        var returnValue = okResult?.Value as List<SpecificationDto>;
        Assert.NotNull(returnValue);
        Assert.Equal(2, returnValue.Count);
    }
}