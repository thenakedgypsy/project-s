using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class EventBank : Node
{

	public static EventBank Instance;

    public List<Event> BeltEvents;
    public List<Event> DeadStarEvents;

	
	// Called when the node enters the scene tree for the first time.

    public void AddEvent(string type, string title, string text, string choice1 = "", string choice2 = "", string choice3 = "")
    {
        Event newEvent = new Event(type, title, text, choice1, choice2, choice3);
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
        if(type == "Belt")
        {
            return BeltEvents[0];            
        }
        if(type == "DeadStar")
        {
            return DeadStarEvents[0];
        }
        return null;
    }

    	public override void _Ready()
	{
        BeltEvents = new List<Event>();
        DeadStarEvents = new List<Event>();
		Instance = this;
        AddEvent("DeadStar","Dead Star Example Event",
        "This is the event text, in here we will explain what is happening", "A", "B");
        
        AddEvent("Belt","Asteroid Belt Example Event",
        "This is the event text, in here we will explain what is happening", "A", "B");
		
	}
}