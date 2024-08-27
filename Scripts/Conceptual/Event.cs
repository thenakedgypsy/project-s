using Godot;
using System;
using System.Collections.Generic;

public partial class Event : Node
{

	public string Type;	
	public string Title;
	public string Text;
	public string Choice1;
	public string Choice2;
	public string Choice3;

	public Event(string type, string title, string text, string choice1, string choice2, string choice3)
	{
		Type = type;
		Title = title;
		Text = text;
		Choice1 = choice1;
		Choice2 = choice2;
		Choice3 = choice3;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
