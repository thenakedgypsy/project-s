using Godot;
using System;

public partial class StarSystem : Area2D
{

	private bool _mouseOver;
	public string ID;
	public string Type;
	private AnimatedSprite2D _systemSprite;
	public bool IsScanned;

	[Signal]
	public delegate void SystemSelectedEventHandler(Vector2 systemPosition, StarSystem self);
	[Signal]
	public delegate void ArrivedInSystemEventHandler(StarSystem system);

	public StarSystem()
	{
		this.Type = "";
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		IsScanned = false;
		_systemSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_mouseOver = false;		
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
			EmitSignal(nameof(SystemSelected), this.GlobalPosition, this);
		}
		else
		{
			GD.Print("Unable to select system due to gamestate.");
			GD.Print($"Current: {StateManager.Instance.State}");
			GD.Print("Wanted: SystemSelection");
		}	
	}

	public void PlayerArrivedAtSystem()
	{
		GD.Print($"From System: Player Arrived at System {this} at {GlobalPosition}");
		ShowScanButton();
	}

	public void ShowScanButton()
	{
		EmitSignal(nameof(ArrivedInSystem), this);
	}

	public void SystemScanned()
	{

		GenerateType();
	
	}

	public void GenerateType()
	{
		GD.Print("Generating a type for this star system...");
		Random random = new Random();
		int typeInt = random.Next(2); //0 - 1
		GD.Print($"Random int is: {typeInt}");
		if(typeInt == 0)
		{
			SetType("Belt");
			GD.Print("Setting type to belt");
		}
		if(typeInt == 1)
		{
			SetType("DeadStar");
			GD.Print("Setting type to Dead");
		}
	}

	public void SetType(string type)
	{
		Type = type;
		SetSprite();
	}

	public void SetSprite()
	{
		_systemSprite.Animation = Type;
	}



}
