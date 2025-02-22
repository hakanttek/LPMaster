using LPMaster.Domain.Entities;
using LPMaster.Domain.Enums;

namespace LPMaster.Core.Tests.Entities;

public class EquationTests
{
    LinModel _model;
    [SetUp]
    public void Setup()
    {
        _model = Fake.Model;
    }

    [Test]
    [TestCase(false, false, true, TestName = "CreateAllFromOneModel_ShouldBeVerified")]
    [TestCase(true, false, false, TestName = "CreateLeftExpressionByAnotherModel_ShoulNotdBeVerified")]
    [TestCase(false, true, false, TestName = "CreateRightExpressionByAnotherModel_ShoulNotdBeVerified")]
    public void Verified_ShouldReturnExpected(bool leftByAnotherModel, bool rightByAnotherModel, bool expectedVerified)
    {
        var leftExpression = (leftByAnotherModel ? Fake.Model : _model).CreateExpression();
        var rightExpression = (rightByAnotherModel ? Fake.Model : _model).CreateExpression();
        var equation = new LinEquation()
        {
            LeftExpressionId = leftExpression.Id,
            LeftExpression = leftExpression,
            RightExpressionId = rightExpression.Id,
            RightExpression = rightExpression,
            Model = _model,
            ModelId = _model.Id,
            Relation = Relation.Eq
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
        var leftExpression = constantLeftExpression ? Fake.CreateConstantExpression(_model) : _model.CreateExpression();
        var rightExpression = constantRightExpression ? Fake.CreateConstantExpression(_model) : _model.CreateExpression();
        var equation = new LinEquation()
        {
            LeftExpressionId = leftExpression.Id,
            LeftExpression = leftExpression,
            RightExpressionId = rightExpression.Id,
            RightExpression = rightExpression,
            Model = _model,
            ModelId = _model.Id,
            Relation = Relation.Eq
        };

        var verified = equation.Verified;

        Assert.That(verified, Is.EqualTo(expectedVerified));
    }
}
