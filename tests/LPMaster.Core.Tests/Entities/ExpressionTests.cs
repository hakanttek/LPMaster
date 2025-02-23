using LPMaster.Domain.Entities;

namespace LPMaster.Core.Tests.Entities;

public class ExpressionTests
{
    private LinModel _model;

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
            Assert.That(model?.Id, Is.EqualTo(expectedModel?.Id));
        });
    }

    [Test]
    [TestCase(0, false, TestName = "ExpressionHasNoModel_ShouldNotBeVerified")]
    [TestCase(1, true, TestName = "ExpressionHasOneModel_ShouldBeVerified")]
    [TestCase(2, false, TestName = "ExpressionHasMoreThanOneModel_ShouldNotBeVerified")]
    public void Verified_ShouldReturnExpected(int nOfModel, bool expectedVerified)
    {
        // Arrange
        List<Multi> multis = new();

        var models = Fake.CreateModel(Math.Max(0, nOfModel - 1));
               
        foreach (var model in models)
            multis.AddRange(model.CreateMulti(Random.Shared.Next(2, 5), model.CreateExpression()));

        var expression = Fake.CreateExpression(_model, multis, numberOfMulti: nOfModel == 1 ? 5 : 0, count: 1).First();
        
        // Act
        var verified = expression.Verified;

        // Assert
        Assert.That(verified, Is.EqualTo(expectedVerified));
    }
}
