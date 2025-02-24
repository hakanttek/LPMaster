using LPMaster.Application.Exceptions;
using LPMaster.Application.Models.Commands;
using LPMaster.Application.Models.Queries;
using LPMaster.Domain.Enums;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LPMaster.Core.Tests.Application;

public class ModelTests
{
    private IServiceProvider _provider;
    private IMediator _mediator;

    [SetUp]
    public void Setup()
    {
        _provider = Fake.CreateProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [TearDown]
    public void TearDown()
    {
        if (_provider is IDisposable disposable)
            disposable.Dispose();
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
    [TestCase(1, TestName = "ShouldNotThrow_WhenCreateOne")]
    [TestCase(2, typeof(BadRequestException), TestName = "ShouldThrowBadRequestException_WhenCreateMoreThanOne")]
    public async Task CreateCommand_ShouldThrowExpectedException(int numberOfCreation, Type? expectedExceptionType = null)
    {
        // Arrange
        var modelName = "TestModel";
        var createCommand = new CreateModelCommand()
        {
            Objective = Objective.Minimization,
            Name = modelName
        };
        Type? exceptionType = null;

        // Act
        try
        {
            for (int i = 0; i < numberOfCreation; i++)
                await _mediator.Send(createCommand);
        }
        catch (Exception ex)
        {
            exceptionType = ex.GetType();
        }

        // Assert
        Assert.That(exceptionType, Is.EqualTo(expectedExceptionType));
    }

    [Test]
    [TestCase(-1, "Test Model", typeof(BadRequestException), TestName = "ShouldThrowBadRequest_WhenBothIdAndNameProvided")]
    [TestCase(null, null, typeof(BadRequestException), TestName = "ShouldThrowBadRequest_WhenNeitherIdNorNameProvided")]
    [TestCase(-1, null, typeof(NotFoundException), TestName = "ShouldThrowNotFound_WhenIdProvidedButNoData")]
    [TestCase(null, "Test Model", typeof(NotFoundException), TestName = "ShouldThrowNotFound_WhenNameProvidedButNoData")]
    public async Task ReadCommand_ShouldThrowExpectedException(int? id, string? name, Type? expectedExceptionType)
    {
        // Arrange
        var readQuery = new ReadModelQuery(id, name);
        Type? exceptionType = null;

        // Act
        try
        {
            await _mediator.Send(readQuery);
        }
        catch (Exception ex)
        {
            exceptionType = ex.GetType();
        }

        // Assert
        Assert.That(exceptionType, Is.EqualTo(expectedExceptionType));
    }
}
