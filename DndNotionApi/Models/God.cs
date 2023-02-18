namespace WebApplication1.Models;

public record God(string Name, IEnumerable<Domain> Domains,
    Alignment Alignment, IEnumerable<string> Titles);