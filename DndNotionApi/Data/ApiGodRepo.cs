using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Utils.Api;

namespace WebApplication1.Data;

/// <summary>
///     God Repo connected to the Notion API.
/// </summary>
public class ApiGodRepo : IGodRepo
{
    private const string CreatorGodsDbId = "78ba72de20954d91b387dd76aec36fd6";
    private const string DarkGodsDbId = "a9cef774528744c38de73073fa5f9e3f";

    private const string GreaterDeitiesDbId =
        "517731b942bd4a6ab188dd0bf3241ede";

    private const string LesserGodsDbId = "58efda37eac4443985d9f0b7bc445e97";
    private const string QuasiDeitiesDbId = "fc398385b7be401ba31de3acff8a09de";

    private static readonly Dictionary<GodType, string> DbIds =
        new()
        {
            { GodType.CreatorGods, CreatorGodsDbId },
            { GodType.DarkGods, DarkGodsDbId },
            { GodType.GreaterDeities, GreaterDeitiesDbId },
            { GodType.LesserGods, LesserGodsDbId },
            { GodType.QuasiDeities, QuasiDeitiesDbId }
        };

    /// <inheritdoc />
    public async Task<IEnumerable<God>> GetAllGods()
    {
        return await GetGodsHttp();
    }

    /// <inheritdoc />
    public async Task<God?> GetGodByName(string name)
    {
        var gods = await GetGodsHttp();
        var god = gods.Where(god => god.Name.Equals(name));
        var enumerable = god as God[] ?? god.ToArray();
        if (enumerable.Length > 1 || !enumerable.Any())
            return await Task.FromResult<God?>(null);
        return enumerable.First();
    }

    private static async Task<IEnumerable<God>> GetGodsHttp()
    {
        // var archGodsJson =
        //     await NotionApiCaller.GetFromDatabase<GodsJson>(CreatorGodsDbId);
        // var darkGodsJson =
        //     await NotionApiCaller.GetFromDatabase<GodsJson>(DarkGodsDbId);

        IEnumerable<God> results = Array.Empty<God>();
        try
        {
            foreach (var dbIdsValue in DbIds.Values)
            {
                var temp =
                    await NotionApiCaller
                        .GetFromDatabase<GodsJson, List<God>>(dbIdsValue);
                IEnumerable<God> enumerable =
                    results as God[] ?? results.ToArray();
                results = !enumerable.Any()
                    ? temp.Process()
                    : enumerable.Concat(temp.Process());
            }
        }
        // TODO: figure out what to return here
        catch (NullReferenceException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            throw;
        }

        return results;
    }
}