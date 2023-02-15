namespace WebApplication1.Models;

public readonly record struct God(string Name, IEnumerable<Domain> Domains,
    Alignment Alignment, IEnumerable<string> Titles);