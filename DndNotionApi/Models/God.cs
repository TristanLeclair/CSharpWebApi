namespace WebApplication1.Models;

/// <summary>
///     Model version of a God.
/// </summary>
/// <param name="Name">Name of the god</param>
/// <param name="Domains">In game domains of the god</param>
/// <param name="Alignment">Alignment presented by the god</param>
/// <param name="Titles">Different titles the god holds</param>
public record God(string Name, IEnumerable<Domain> Domains,
    Alignment Alignment, IEnumerable<string> Titles);