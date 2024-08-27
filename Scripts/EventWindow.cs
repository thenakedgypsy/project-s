using Godot;
using System;

public partial class EventWindow : CanvasLayer
{

	public RichTextLabel TitleLabel;
	public RichTextLabel BodyLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TitleLabel = GetNode<RichTextLabel>("Window/Title");
		BodyLabel = GetNode<RichTextLabel>("Window/Body");
		Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void LoadEvent(Event eventToLoad)
	{
		if(eventToLoad == null)
		{
			GD.Print("Null event receieved by Window");
			return;
		}
		GD.Print("Loading event to Window");
		TitleLabel.Text = eventToLoad.Title;
		BodyLabel.Text = eventToLoad.Body;
		Visible = true;
	}

	public void ShowEventWindow()
	{
		Visible = true;
	}

	public void HideEventWindow()
	{
		Visible = false;
	}
}
