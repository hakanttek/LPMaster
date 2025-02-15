using LPMaster.Domain.Entities;
using System.Reflection;

namespace LPMaster.Core.Tests.Entities;

public class DVarTests
{
    private Model _model;

    [SetUp]
    public void Setup()
    {
        var oFunc = new Expression() { Multis = [] };
        _model = new Model() { Constraints = [], ObjectiveFunction = oFunc };
    }

    [Test]
    [TestCase(true, false, TestName = "DVarExists_IsConstantShouldBeFalse")]
    [TestCase(false, true, TestName = "DVarDoesNotExist_IsConstantShouldBeTrue")]
    public void Verified_ReturnsExpectedValue(bool changeModelId, bool expectedVerified)
    {
        // Arrange
        var dvar = new DVar()
        { 
            ModelId = changeModelId ? _model.Id + 1 : _model.Id,
            Model = _model
        };

        // Act
        var verified = dvar.Verified;

        // Assert
        Assert.That(verified, Is.EqualTo(expectedVerified));
    }
}
