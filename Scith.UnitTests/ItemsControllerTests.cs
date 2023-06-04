namespace Scith.UnitTests;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Scith.API.Controllers;
using Scith.API.Entities;

public class ItemsControllerTests
{
    [Fact]
    //Test naming convention. UnitOfWork_StateUnderTest_ExpectedBehaviour()
    public async void GetItemAsync_WithNullItem_ReturnsNotFound()
    {
        // Arrange 
        //naming convention = stubs do not verify anything on the object itself, mocks will verify somethng that happened to the mock during the test 
        var repositoryStub = new Mock<InterfaceItemsRepository>();
        repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Item)null);


        var loggerStub = new Mock<ILogger<ItemsController>>();

        var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

        // Act
        var result = await controller.GetItemAsync(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}