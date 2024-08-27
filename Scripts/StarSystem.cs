using Godot;
using System;

public partial class StarSystem : Area2D
{

	private bool _mouseOver;
	public string Type {get;set;}

	[Signal]
	public delegate void SystemSelectedEventHandler(Vector2 systemPosition);

	public StarSystem()
	{
		this.Type = "Dead_Star";
	}

	public StarSystem(string type)
	{
		Type = type;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_mouseOver = false;
		SetSprite(Type);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CheckClick();
	}


	public void MouseOver()  //called when the mouse is over the star system. (tooltip later?)
	{		
		_mouseOver = true;
	}

	public void MouseExit()
	{
		_mouseOver = false;
	}

	public void CheckClick()		//called when the mouse is clicking on the star system
	{
		if(Input.IsActionJustPressed("click") && _mouseOver)
		{
			Clicked();
		}
	}

	public void Clicked()
	{
		if(StateManager.Instance.State == "SystemSelection")
		{
			GD.Print($"System Clicked! Emitting Signal to Travel button with Position {this.GlobalPosition}");
			EmitSignal(nameof(SystemSelected), this.GlobalPosition);
		}
		else
		{
			GD.Print("Unable to select system due to gamestate.");
			GD.Print($"Current: {StateManager.Instance.State}");
			GD.Print("Wanted: SystemSelection");
		}	
	}

	public void SetSprite(string type)
	{

	}



}
