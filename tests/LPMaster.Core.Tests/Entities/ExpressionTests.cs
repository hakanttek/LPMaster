using LPMaster.Domain.Entities;

namespace LPMaster.Core.Tests.Entities;

public class ExpressionTests
{
    private Model _model;

    [SetUp]
    public void Setup()
    {
        _model = Fake.Model;
    }

    [Test]
    public void Model_ShouldReturnExpectedModel()
    {        
        // Arrange
        var expression = _model.CreateExpression();

        // Act
        var model = expression.Model;
        var expectedModel = _model;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(model, Is.EqualTo(expectedModel));
            Assert.That(model.Id, Is.EqualTo(expectedModel.Id));
        });
    }
}
