using LPMaster.Domain.Entities;

namespace LPMaster.Core.Tests;

public class ModelTest
{
    [Test]
    public void Verified_ShouldReturnTrue()
    {
        // Assert
        var model = Fake.Model;

        // Act
        var allVerified = model.Verified;

        // Assert
        Assert.That(allVerified, Is.True);
    }
}
