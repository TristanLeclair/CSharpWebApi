using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.DTO;

/// <summary>
/// A json containing information on a notion database containing various gods.
/// </summary>
public class GodsJson : IProcessableJson<List<God>>
{
    public string @object { get; set; }
    public List<Result> results { get; set; }
    public object next_cursor { get; set; }
    public bool has_more { get; set; }
    public string type { get; set; }
    public Page page { get; set; }

    public class Alias
    {
        public string id { get; set; }
        public string type { get; set; }
        public List<object> rich_text { get; set; }
    }

    public class Alignment
    {
        public string id { get; set; }
        public string type { get; set; }
        public Select select { get; set; }
    }

    public class Annotations
    {
        public bool bold { get; set; }
        public bool italic { get; set; }
        public bool strikethrough { get; set; }
        public bool underline { get; set; }
        public bool code { get; set; }
        public string color { get; set; }
    }

    public class CreatedBy
    {
        public string @object { get; set; }
        public string id { get; set; }
    }

    public class Domains
    {
        public string id { get; set; }
        public string type { get; set; }
        public List<MultiSelect> multi_select { get; set; }
    }

    public class File
    {
        public string name { get; set; }
        public string type { get; set; }
        public File file { get; set; }
    }

    public class File2
    {
        public string url { get; set; }
        public DateTime expiry_time { get; set; }
    }

    public class LastEditedBy
    {
        public string @object { get; set; }
        public string id { get; set; }
    }

    public class MultiSelect
    {
        public string id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
    }

    public class Name
    {
        public string id { get; set; }
        public string type { get; set; }
        public List<Title> title { get; set; }
    }

    public class OutOfGameDomains
    {
        public string id { get; set; }
        public string type { get; set; }
        public List<MultiSelect> multi_select { get; set; }
    }

    public class Page
    {
    }

    public class Parent
    {
        public string type { get; set; }
        public string database_id { get; set; }
    }

    public class Picture
    {
        public string id { get; set; }
        public string type { get; set; }
        public List<File> files { get; set; }
    }

    public class Properties
    {
        public Alias Alias { get; set; }
        public Titles Titles { get; set; }

        [JsonProperty("Worshipped by")]
        public WorshippedBy Worshippedby { get; set; }

        public Picture Picture { get; set; }
        public Alignment Alignment { get; set; }
        public Domains Domains { get; set; }

        [JsonProperty("Out of game domains")]
        public OutOfGameDomains Outofgamedomains { get; set; }

        public Name Name { get; set; }
    }

    public class Result
    {
        public string @object { get; set; }
        public string id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime last_edited_time { get; set; }
        public CreatedBy created_by { get; set; }
        public LastEditedBy last_edited_by { get; set; }
        public object cover { get; set; }
        public object icon { get; set; }
        public Parent parent { get; set; }
        public bool archived { get; set; }
        public Properties properties { get; set; }
        public string url { get; set; }
    }

    public class RichText
    {
        public string type { get; set; }
        public Text text { get; set; }
        public Annotations annotations { get; set; }
        public string plain_text { get; set; }
        public object href { get; set; }
    }

    public class Select
    {
        public string id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
    }

    public class Text
    {
        public string content { get; set; }
        public object link { get; set; }
    }

    public class Title
    {
        public string type { get; set; }
        public Text text { get; set; }
        public Annotations annotations { get; set; }
        public string plain_text { get; set; }
        public object href { get; set; }
    }

    public class Titles
    {
        public string id { get; set; }
        public string type { get; set; }
        public List<RichText> rich_text { get; set; }
    }

    public class WorshippedBy
    {
        public string id { get; set; }
        public string type { get; set; }
        public List<object> rich_text { get; set; }
    }

    /// <inheritdoc />
    /// <summary>
    /// Process God json into a list of gods to be used in our actual model
    /// </summary>
    /// <returns>The list of gods in this notion page</returns>
    /// <exception cref="JsonException">throws if the alignment or domain
    /// in the json don't conform
    /// to our model</exception>
    public List<God> Process()
    {
        var gods = new List<God>();
        results.ForEach(god =>
        {
            var props = god.properties;
            var titles = new List<string>();
            props.Titles.rich_text.ForEach(text =>
            {
                var strings = text.plain_text.Split(separator: '\n');
                titles.AddRange(strings);
            });
            if (!Enum.TryParse<Models.Alignment>(props.Alignment.select.name,
                    out var alignment))
            {
                throw new JsonException(
                    $"Alignment name {god.properties.Alignment.select.name} is not valid");
            }

            var name = props.Name.title.First().plain_text;
            var domains = new List<Domain>();
            props.Domains.multi_select.ForEach(select =>
            {
                if (!Enum.TryParse(select.name, out Domain domain))
                {
                    throw new JsonException(
                        $"Domain name {select.name} is not valid");
                }

                domains.Add(domain);
            });
            gods.Add(new God(name, domains, alignment, titles));
        });
        return gods;
    }
}