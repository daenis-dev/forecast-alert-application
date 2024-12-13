using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ForecastAlertService.Controllers;
using ForecastAlertService.Models;
using ForecastAlertService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OperatorsControllerTests
{
    private readonly Mock<IGetAllOperators> _getAllOperatorsMock;
    private readonly OperatorsController _controller;

    public OperatorsControllerTests()
    {
        _getAllOperatorsMock = new Mock<IGetAllOperators>();
        _controller = new OperatorsController(_getAllOperatorsMock.Object);
    }

    [Fact]
    public async Task GetOperators_ReturnsList()
    {
        var operatorList = new List<OperatorDto>
        {
            new OperatorDto { Id = 1, Name = "Equals", Symbol = "=" },
            new OperatorDto { Id = 2, Name = "Not Equals", Symbol = "!=" }
        };

        _getAllOperatorsMock.Setup(service => service.GetAllOperatorsAsync())
            .ReturnsAsync(operatorList);

        var result = await _controller.GetOperators();

        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult); 

        var returnValue = okResult?.Value as List<OperatorDto>;
        Assert.NotNull(returnValue);
        Assert.Equal(2, returnValue.Count);
    }
}