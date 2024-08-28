using Godot;
using System;
using System.Collections.Generic;

public partial class Event : Node
{

	public string Type;	
	public string Title;
	public string Body;
	public Choice ChoiceA;
	public Choice ChoiceB;
	public bool Used; // for not repeating? 

	public Event(string type, string title, string body, Choice choice1, Choice choice2)
	{
		Type = type;
		Title = title;
		Body = body;
		ChoiceA = choice1;
		ChoiceB = choice2;
	}

	

	
}
