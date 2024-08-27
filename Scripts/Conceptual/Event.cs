using Godot;
using System;
using System.Collections.Generic;

public partial class Event : Node
{

	public string Type;	
	public string Title;
	public string Body;
	public string Choice1;
	public string Choice2;
	public string Choice3;

	public Event(string type, string title, string body, string choice1, string choice2, string choice3)
	{
		Type = type;
		Title = title;
		Body = body;
		Choice1 = choice1;
		Choice2 = choice2;
		Choice3 = choice3;
	}

	

	
}
