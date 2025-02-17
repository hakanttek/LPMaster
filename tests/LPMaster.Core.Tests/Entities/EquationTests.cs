using LPMaster.Domain.Entities;

namespace LPMaster.Core.Tests.Entities;

public class EquationTests
{
    Model _model;
    [SetUp]
    public void Setup()
    {
        _model = Fake.Model;
    }

    [Test]
    [TestCase(false, false, true, TestName = "CreateAllFromOneModel_ShoulNotdBeVerified")]
    [TestCase(true, false, false, TestName = "CreateLeftExpressionByAnotherModel_ShoulNotdBeVerified")]
    [TestCase(false, true, false, TestName = "CreateRightExpressionByAnotherModel_ShoulNotdBeVerified")]
    public void Verified_ShouldReturnExpected(bool leftByAnotherModel, bool rightByAnotherModel, bool expectedVerified)
    {
        var leftExpression = (leftByAnotherModel ? Fake.Model : _model).CreateExpression();
        var rightExpression = (rightByAnotherModel ? Fake.Model : _model).CreateExpression();
        var equation = new Equation()
        {
            LeftExpressionId = leftExpression.Id,
            LeftExpression = leftExpression,
            RightExpressionId = rightExpression.Id,
            RightExpression = rightExpression,
            Model = _model
        };

        var verified = equation.Verified;

        Assert.That(verified, Is.EqualTo(expectedVerified));
    }

    [Test]
    [TestCase(true, true, false, TestName = "CreateAllAsConstant_ShoulNotdBeVerified")]
    [TestCase(true, false, true, TestName = "CreateOnlyLeftExpressionAsConstant_ShoulNotdBeVerified")]
    [TestCase(false, true, true, TestName = "CreateOnlyRightExpressionAsConstant_ShoulNotdBeVerified")]
    public void Verified_WhenUsingConstantExpressions_ShouldReturnExpected(bool constantLeftExpression, bool constantRightExpression, bool expectedVerified)
    {
        var leftExpression = constantLeftExpression ? Fake.ConstantExpression : _model.CreateExpression();
        var rightExpression = constantRightExpression ? Fake.ConstantExpression : _model.CreateExpression();
        var equation = new Equation()
        {
            LeftExpressionId = leftExpression.Id,
            LeftExpression = leftExpression,
            RightExpressionId = rightExpression.Id,
            RightExpression = rightExpression,
            Model = _model
        };

        var verified = equation.Verified;

        Assert.That(verified, Is.EqualTo(expectedVerified));
    }
}
