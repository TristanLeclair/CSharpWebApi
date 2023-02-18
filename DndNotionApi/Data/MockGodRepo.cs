using WebApplication1.Models;

namespace WebApplication1.Data;

public class MockGodRepo : IGodRepo
{
    private readonly IEnumerable<God> _gods = new[]
    {
        new God("Yi the Creator God", Enumerable.Empty<Domain>(),
            Alignment.TrueNeutral,
            new[] { "The First", "One", "Yi" }),
        new God("Udar Archgod of Earth", new[] { Domain.Forge, Domain.Life },
            Alignment.LawfulGood,
            new[] { "The StoneGod", "The Second" }),
        new God("Aeros Archgod of the Storm and Sea", new[] { Domain.Tempest },
            Alignment.ChaoticGood,
            new[] { "The Everwind", "Master of the Tides" })
    };

    /// <inheritdoc />
    public IEnumerable<God> GetGods()
    {
        return _gods;
    }

    /// <inheritdoc/>
    public God? GetGodByName(string name)
    {
        return _gods.FirstOrDefault(god => god != null && god.Name.Equals(name),
            defaultValue: null);
    }
}