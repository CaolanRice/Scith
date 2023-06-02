using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Scith.API.Controllers;
using Scith.API.Entities;

namespace Scith.UnitTests;

public class ItemsControllerTests
{

    private readonly Mock<InterfaceItemsRepository> repositoryStub = new();
    private readonly Mock<ILogger<ItemsController>> loggerStub = new();
    private readonly Random rand = new Random();

    [Fact]
    //Test naming convention. UnitOfWork_StateUnderTest_ExpectedBehaviour()
    public async void GetItemAsync_WithNullItem_ReturnsNotFound()
    {
        // Arrange
        repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Item)null);

        // var loggerStub = new Mock<ILogger<ItemsController>>();

        var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

        // Act
        var result = await controller.GetItemAsync(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);

    }

    [Fact]
    //Test naming convention. UnitOfWork_StateUnderTest_ExpectedBehaviour()
    public async void GetItemAsync_WithExistingItem_ReturnsExpectedItem()
    {


    }

    // private Item CreateItem()
    // {
    //     return new()
    //     {
    //         Id = Guid.NewGuid(),
    //         Name = Guid.NewGuid().ToString();
            
    //     }
    // }
}
