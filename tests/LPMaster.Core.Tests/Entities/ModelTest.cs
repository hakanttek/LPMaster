using LPMaster.Domain.Entities;

namespace LPMaster.Core.Tests;

public class ModelTest
{
    [Test]
    public void VerifyAll_ShouldReturnTrue()
    {
        // Assert
        var model = Fake.Model;

        // Act
        var allVerified = model.VerifyAll();

        // Assert
        Assert.That(allVerified, Is.True);
    }
}
