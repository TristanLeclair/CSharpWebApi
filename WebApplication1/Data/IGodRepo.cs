using WebApplication1.Models;

namespace WebApplication1.Data;

public interface IGodRepo
{
    /// <summary>
    /// Returns an IEnumerable of all the gods
    /// </summary>
    /// <returns>IEnumerable of all gods</returns>
    IEnumerable<God> GetGods();
    /// <summary>
    /// Returns the god with the passed in name
    /// </summary>
    /// <param name="name">Name of the god to get</param>
    /// <returns>God or null if no god with that name exists</returns>
    God? GetGodByName(string name);
}