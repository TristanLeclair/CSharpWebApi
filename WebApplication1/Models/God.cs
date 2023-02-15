namespace WebApplication1.Models;

public class God
{
    public string Name { get; private set; }
    public IEnumerable<Domain> Domains { get; private set; }
    public Alignment Alignment { get; private set; }
    public IEnumerable<string> Titles { get; private set; }

    public God(string name, IEnumerable<Domain> domains, Alignment alignment, IEnumerable<string> titles)
    {
        Name = name;
        Domains = domains;
        Alignment = alignment;
        Titles = titles;
    }
}