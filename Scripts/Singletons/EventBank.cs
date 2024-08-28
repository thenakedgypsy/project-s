using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;

public partial class EventBank : Node
{

	public static EventBank Instance;

    public List<Event> BeltEvents;
    public List<Event> DeadStarEvents;
    public List<Event> MStarEvents;

	
	// Called when the node enters the scene tree for the first time.

    public void AddEvent(string type, string title, string text, Choice choice1, Choice choice2)
    {
        Event newEvent = new Event(type, title, text, choice1, choice2);
        if(type == "Belt")
        {
            BeltEvents.Add(newEvent);            
        }
        if(type == "DeadStar")
        {
            DeadStarEvents.Add(newEvent);
        }
    }

    public Event RollEvent(string type)
    {
        Random random = new Random();
        int rand = 0;
        if(type == "Belt")
        {
            rand = random.Next(BeltEvents.Count);
            return BeltEvents[rand];            
        }
        if(type == "DeadStar")
        {
            rand = random.Next(DeadStarEvents.Count);
            return DeadStarEvents[rand];
        }
        if(type == "MStar")
        {
            rand = random.Next(MStarEvents.Count);
            return MStarEvents[rand];
        }
        return null;
    }

    	public override void _Ready()
	{
        BeltEvents = new List<Event>();
        DeadStarEvents = new List<Event>();
        MStarEvents = new List<Event>();
		Instance = this;
        LoadEventsFromJson("res://JSON/events.json");
	}

    private void LoadEventsFromJson(string path)
    {

        var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        
        if(file==null)
        {
            GD.Print($"Error reading file at {path}");
            return; //exit so that we dont attempt to continue
        }

        string JSONContent = file.GetAsText();
        file.Close();
         
        GD.Print($"JSON content length: {JSONContent.Length}");

        if (string.IsNullOrWhiteSpace(JSONContent))
        {
            GD.PrintErr("JSON content is empty or whitespace");
            return;
        }

        var eventsFromJSON = JsonSerializer.Deserialize<List<JsonEvent>>(JSONContent);

        foreach(var currentEvent in eventsFromJSON)
        {
            var choiceA = new Choice(currentEvent.Choice1Text, currentEvent.Choice1Effects);
            var choiceB = new Choice(currentEvent.Choice2Text, currentEvent.Choice2Effects);

            string title = "[center]" + currentEvent.Title;

            var newEvent = new Event(currentEvent.Type, title, currentEvent.Body, choiceA, choiceB);

            
            //after parse add it to the appropriate bank
            if(currentEvent.Type == "DeadStar")
            {
                DeadStarEvents.Add(newEvent);
            }
            else if(currentEvent.Type == "Belt")
            {
                BeltEvents.Add(newEvent);
            }
            else if(currentEvent.Type == "MStar")
            {
                MStarEvents.Add(newEvent);
            }
            else
            {
                GD.Print($"Event Type: {currentEvent.Type} does not exist. Unable to add event.");
            }
        }
        GD.Print($"Loaded {DeadStarEvents.Count} deadstar events");
        GD.Print($"Loaded {BeltEvents.Count} belt events");
    }
}