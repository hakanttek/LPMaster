using AutoMapper;
using LPMaster.Application.Common.Models.Update;
using LPMaster.Application.Common.Exceptions;
using LPMaster.Application.Models.Commands;
using LPMaster.Application.Models.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LPMaster.Core.Tests.Application;

public class ModelTests
{
    private IServiceProvider _provider;
    private IMediator _mediator;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        _provider = Fake.CreateProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
        _mapper = _provider.GetRequiredService<IMapper>();
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
        var createCommand = new CreateModelCommand("TestModel");

        // Act
        var res = await _mediator.Send(createCommand);
        var model = await _mediator.Send(new ReadModelQuery() { Id = res.Id });

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
        var modelName = Guid.NewGuid().ToString();
        var createCommand = new CreateModelCommand(modelName);
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
        var readQuery = new ReadModelQuery() { Id = id, Name = name };
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

    [Test]
    public async Task UpdateCommand_ShouldUpdateCorrectly()
    {
        // Arrange
        var createCommand = new CreateModelCommand(Guid.NewGuid().ToString());
        var createResult = await _mediator.Send(createCommand);
        var model = await _mediator.Send(new ReadModelQuery() { Id = createResult.Id });
        var modelUpdateDto = new ModelUpdateDto()
        {
            Objective = model.Objective,
            Description = model.Description,
            Name = model.Name
        };
        modelUpdateDto.Name = Guid.NewGuid().ToString();
        modelUpdateDto.Description = "UpdatedDescription";
        var updateCommand = new UpdateModelCommand(modelUpdateDto, createResult.Id);

        // Act
        await _mediator.Send(updateCommand);
        var updatedModel = await _mediator.Send(new ReadModelQuery() { Id = createResult.Id });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modelUpdateDto.Name, Is.EqualTo(updatedModel.Name));
            Assert.That(modelUpdateDto.Description, Is.EqualTo(updatedModel.Description));
            Assert.That(modelUpdateDto.Objective, Is.EqualTo(updatedModel.Objective));
        });
    }

    [Test]
    [TestCase(true, true, false, null, TestName = "ShouldNotThrow_WhenDeleteById")]
    [TestCase(true, false, true, null, TestName = "ShouldNotThrow_WhenDeleteByName")]
    [TestCase(true, false, false, typeof(BadRequestException), TestName = "ShouldThrowBadRequest_WhenNeitherIdNorNameProvided")]
    [TestCase(true, true, true, typeof(BadRequestException), TestName = "ShouldThrowBadRequest_WhenBothIdAndNameProvided")]
    [TestCase(false, true, false, typeof(NotFoundException), TestName = "ShouldThrowNotFound_WhenIdProvidedButNoData")]
    public async Task DeleteCommand_ShouldDeleteCorrectly(bool createNewModel, bool deleteById, bool deleteByName, Type? expectedExceptionType)
    {
        // Arrange
        var createResult = createNewModel
            ? await _mediator.Send(new CreateModelCommand(Guid.NewGuid().ToString()))
            : new(-1, Guid.NewGuid().ToString());
        var deleteCommand = new DeleteModelCommand(deleteById ? createResult.Id : null, deleteByName ? createResult.Name : null);

        // Act        
        Type? exceptionType = null;        
        try
        {
            await _mediator.Send(deleteCommand);
        }
        catch (Exception ex)
        {
            exceptionType = ex.GetType();
        }

        // Assert
        Assert.That(exceptionType, Is.EqualTo(expectedExceptionType));
    }
}
