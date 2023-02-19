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
        var gods = _godRepo.GetAllGods();

        //Assert
        Assert.NotNull(gods);
    }

    [Fact]
    public async void GetGodByName_ReturnsGod_WhenGodExists()
    {
        // Arrange
        const string godName = "Yi The Creator God";

        // Act
        var god = await _godRepo.GetGodByName(godName);

        //Assert   
        Assert.NotNull(god);
        Assert.Equal(godName, god.Name);
    }

    [Fact]
    public async void GetGodByName_ReturnsNull_WhenGodDoesNotExist()
    {
        // Arrange
        const string godName = "Yi The Non Creator God";

        // Act
        var god = await _godRepo.GetGodByName(godName);

        //Assert
        Assert.Null(god);
    }
}