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
    public async Task CreateModel_ShouldReturnValidModelId()
    {
        // Arrange
        var createCommand = new CreateModelCommand()
        {
            Objective = Objective.Minimization,
        };

        // Act
        var id = await _mediator.Send(createCommand);
        var model = await _mediator.Send(new ReadModelQuery(id));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(model, Is.Not.Null);
            Assert.That(model.Id, Is.EqualTo(id));
        });        
    }

    [Test]
    public async Task CreateModel_ShouldBeRetrievableByName()
    {
        // Arrange
        var modelName = "TestModel";
        var createCommand = new CreateModelCommand()
        {
            Name = modelName,
            Objective = Objective.Minimization
        };

        // Act
        await _mediator.Send(createCommand);
        var modelByName = await _mediator.Send(new ReadModelQuery(Name: modelName));

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modelByName, Is.Not.Null);
            Assert.That(modelByName.Name, Is.EqualTo(modelName));
        });
    }
}
