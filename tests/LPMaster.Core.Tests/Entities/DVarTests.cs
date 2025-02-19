using LPMaster.Domain.Entities;
using LPMaster.Domain.Enums;

namespace LPMaster.Core.Tests.Entities;

public class DVarTests
{
    private Model _model;

    [SetUp]
    public void Setup()
    {
        var oFunc = new Expression() { Multis = [] };
        _model = new Model() { Id = -1, Name = "TestModel",  Objective = Objective.Minimization, Constraints = [], ObjectiveFunction = oFunc };
    }

    [Test]
    [TestCase(true, false, TestName = "ModelIdIsChanged_ShoulNotdBeVerified")]
    [TestCase(false, true, TestName = "ModelIdIsNotChanged_ShouldBeVerified")]
    public void Verified_ReturnsExpectedValue(bool changeModelId, bool expectedVerified)
    {
        // Arrange
        var dvar = new DVar()
        { 
            ColIndex = 0,
            Name = "x",
            ModelId = changeModelId ? _model.Id + 1 : _model.Id,
            Model = _model
        };

        // Act
        var verified = dvar.Verified;

        // Assert
        Assert.That(verified, Is.EqualTo(expectedVerified));
    }
}
