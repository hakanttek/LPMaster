using LPMaster.Domain.Entities;

namespace LPMaster.Core.Tests.Entities;

public class DVarTests
{
    private LinModel _model;

    [SetUp]
    public void Setup()
    {
        _model = Fake.Model;
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
