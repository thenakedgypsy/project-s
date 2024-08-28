using System.Collections.Generic;

public class JsonEvent
{
    public string Type { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string Choice1Text { get; set; }
    public Dictionary<string, int> Choice1Effects { get; set; }
    public string Choice2Text { get; set; }
    public Dictionary<string, int> Choice2Effects { get; set; }
}