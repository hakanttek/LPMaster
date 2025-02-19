using LPMaster.Domain.Entities;
using LPMaster.Domain.Enums;

namespace LPMaster.Core.Tests.Entities;

public class MultiTests
{
    private DVar _dvar;

    [SetUp]
    public void Setup()
    {
        var oFunc = new Expression() { Multis = [] };
        var model = new Model() { Id = 0, Name = "TestModel", Objective = Objective.Minimization, Constraints = [], ObjectiveFunction = oFunc };
        _dvar = new DVar() { ColIndex = 1, Name = "x", Model = model };
    }

    [Test]
    [TestCase(true, false, TestName = "DVarExists_IsConstantShouldBeFalse")]
    [TestCase(false, true, TestName = "DVarDoesNotExist_IsConstantShouldBeTrue")]
    public void IsConstant_ReturnsExpectedValue(bool hasDVar, bool expectedIsConstant)
    {
        // Arrange
        var multi = new Multi()
        {
            ColIndex = hasDVar ? _dvar.ColIndex : null,
            DVar = hasDVar ? _dvar : null
        };

        // Act
        var isConstant = multi.IsConstant;

        // Assert
        Assert.That(isConstant, Is.EqualTo(expectedIsConstant));
    }

    [Test]
    [TestCase(true, TestName = "DVarExists_ShouldMatchModelIdAndModel")]
    [TestCase(false, TestName = "DVarDoesNotExist_ShouldModelIdAndModelBeNull")]
    public void ModelIdAndModel_ShouldBehaveAsExpectedBasedOnDVar(bool hasDVar)
    {
        // Arrange
        var multi = new Multi()
        {
            ColIndex = hasDVar ? _dvar.ColIndex : null,
            DVar = hasDVar ? _dvar : null
        };

        // Act
        var modelId = multi.ModelId;
        var model = multi.Model;

        // Assert
        Assert.Multiple(() =>
        {
            if (hasDVar)
            {
                Assert.That(modelId, Is.EqualTo(_dvar.ModelId));
                Assert.That(model, Is.EqualTo(_dvar.Model));
            }
            else
            {
                Assert.That(modelId, Is.Null);
                Assert.That(model, Is.Null);
            }
        });
    }

    [Test]
    [TestCase(true, false, TestName = "DvarIdIsChanged_ShouldNotBeVerified")]
    [TestCase(false, true, TestName = "DvarIdIsNotChanged_ShouldBeVerified")]
    public void Verified_ReturnsExpectedValue(bool changeDVarId, bool expectedVerified)
    {
        // Arrange
        var multi = new Multi()
        {
            ColIndex = changeDVarId ? _dvar.ColIndex + 1 : _dvar.ColIndex,
            DVar = _dvar
        };

        // Act
        var verified = multi.Verified;

        // Assert
        Assert.That(verified, Is.EqualTo(expectedVerified));
    }
}
