using LPMaster.Application.Exceptions;
using LPMaster.Application.Models.Commands;
using LPMaster.Application.Models.Queries;
using LPMaster.Domain.Enums;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LPMaster.Core.Tests.Application;

public class ModelTests
{
    private IMediator _mediator;

    [SetUp]
    public void Setup()
    {
        _mediator = Fake.Provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task CreateCommand_ShouldReturnValidResponse()
    {
        // Arrange
        var createCommand = new CreateModelCommand()
        {
            Objective = Objective.Minimization,
            Name = "TestModel"
        };

        // Act
        var res = await _mediator.Send(createCommand);
        var model = await _mediator.Send(new ReadModelQuery(res.Id));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Id, Is.EqualTo(res.Id));
            Assert.That(model.Name, Is.EqualTo(res.Name));
        });        
    }

    [Test]
    [TestCase(1)]
    [TestCase(2, typeof(BadRequestException))]
    public async Task CreateModelCommand_ShouldThrowExpectedException(int numberOfCreation, Type? expectedExceptionType = null)
    {
        Type? exceptionType = null;
        try
        {
            // Arrange
            var modelName = Guid.NewGuid().ToString();
            var createCommand = new CreateModelCommand()
            {
                Objective = Objective.Minimization,
                Name = modelName
            };

            // Act
            for (int i = 0; i < numberOfCreation; i++)
                await _mediator.Send(createCommand);
        }
        catch (Exception ex)
        {
            exceptionType = ex.GetType();
        }

        Assert.That(exceptionType, Is.EqualTo(expectedExceptionType));
    }
}
