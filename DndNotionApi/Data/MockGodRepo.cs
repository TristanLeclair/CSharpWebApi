using WebApplication1.Models;

namespace WebApplication1.Data;

public class MockGodRepo : IGodRepo
{
    private readonly IEnumerable<God> _gods = new[]
    {
        new God("Yi The Creator God", Enumerable.Empty<Domain>(),
            Alignment.TN,
            new[] { "The First", "One", "Yi" }),
        new God("Udar Archgod of Earth", new[] { Domain.Forge, Domain.Life },
            Alignment.LG,
            new[] { "The StoneGod", "The Second" }),
        new God("Aeros Archgod of the Storm and Sea", new[] { Domain.Tempest },
            Alignment.CG,
            new[] { "The Everwind", "Master of the Tides" })
    };

    /// <inheritdoc />
    public Task<IEnumerable<God>> GetAllGods()
    {
        return Task.FromResult(_gods);
    }

    /// <inheritdoc/>
    public Task<God?> GetGodByName(string name)
    {
        return Task.FromResult(_gods.FirstOrDefault(
            god => god != null && god.Name.Equals(name),
            defaultValue: null));
    }
}