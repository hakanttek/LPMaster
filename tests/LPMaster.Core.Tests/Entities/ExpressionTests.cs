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
        var multis = Fake.CreateMulti(Random.Shared.Next(2, 5)).ToList();    //create default multies
        var models = Fake.CreateModel(nOfModel);
        foreach (var model in models)
            multis.AddRange(model.CreateMulti(Random.Shared.Next(2, 5)));

        var expression = Fake.CreateExpression(multis, 1).First();
        
        // Act
        var verified = expression.Verified;

        // Assert
        Assert.That(verified, Is.EqualTo(expectedVerified));
    }
}
