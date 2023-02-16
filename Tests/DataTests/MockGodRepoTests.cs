using WebApplication1.Data;

namespace Tests.DataTests;

public class MockGodRepoTests
{
    private readonly MockGodRepo _godRepo = new MockGodRepo();

    [Fact]
    public void GetGods_ReturnsList_Always()
    {
        // Arrange

        // Act
        var gods = _godRepo.GetGods();

        //Assert
        Assert.NotNull(gods);
    }

    [Fact]
    public void GetGodByName_ReturnsGod_WhenGodExists()
    {
        // Arrange
        const string godName = "Yi the Creator God";

        // Act
        var god = _godRepo.GetGodByName(godName);

        //Assert   
        Assert.NotNull(god);
        Assert.Equal(godName, god.Name);
    }

    [Fact]
    public void GetGodByName_ReturnsNull_WhenGodDoesNotExist()
    {
        // Arrange
        const string godName = "Yi the Non Creator God";

        // Act
        var god = _godRepo.GetGodByName(godName);

        //Assert
        Assert.Null(god);
    }
}