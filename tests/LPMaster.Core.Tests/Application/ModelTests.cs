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
}
