using LPMaster.Domain.Entities;
using LPMaster.Domain.Enums;
using System.Linq.Expressions;

namespace LPMaster.Core.Tests.Entities;

public class MultiTests
{
    private DVar _dvar;
    private LinModel _model;

    [SetUp]
    public void Setup()
    {
        _model = Fake.Model;
        _dvar = _model.CreateDVar();
    }

    [Test]
    [TestCase(true, false, TestName = "DVarExists_IsConstantShouldBeFalse")]
    [TestCase(false, true, TestName = "DVarDoesNotExist_IsConstantShouldBeTrue")]
    public void IsConstant_ReturnsExpectedValue(bool hasDVar, bool expectedIsConstant)
    {
        // Arrange
        var expression = _model.CreateExpression();
        var multi = new Multi()
        {
            Coef = 1,
            Expression = expression,
            ExpressionId = expression.Id,
            ColIndex = hasDVar ? _dvar.ColIndex : null,
            DVar = hasDVar ? _dvar : null
        };

        // Act
        var isConstant = multi.IsConstant;

        // Assert
        Assert.That(isConstant, Is.EqualTo(expectedIsConstant));
    }
    
    [Test]
    [TestCase(true, false, TestName = "DvarIdIsChanged_ShouldNotBeVerified")]
    [TestCase(false, true, TestName = "DvarIdIsNotChanged_ShouldBeVerified")]
    public void Verified_ReturnsExpectedValue(bool changeDVarId, bool expectedVerified)
    {
        // Arrange
        var expression = _model.CreateExpression();
        var multi = new Multi()
        {
            Coef = 1,
            ColIndex = changeDVarId ? _dvar.ColIndex + 1 : _dvar.ColIndex,
            DVar = _dvar,
            Expression = expression,
            ExpressionId = expression.Id
        };

        // Act
        var verified = multi.Verified;

        // Assert
        Assert.That(verified, Is.EqualTo(expectedVerified));
    }
}
