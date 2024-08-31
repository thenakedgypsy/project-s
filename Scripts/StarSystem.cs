using Godot;
using System;

public partial class StarSystem : Area2D
{

	private bool _mouseOver;
	public string ID;
	public string Type;
	private AnimatedSprite2D _systemSprite;
	public bool IsScanned;
	public PointLight2D Light;
	public PointLight2D InitialLight;

	[Signal]
	public delegate void SystemSelectedEventHandler(Vector2 systemPosition, StarSystem self);
	[Signal]
	public delegate void ArrivedInSystemEventHandler(StarSystem system);
	[Signal]
	public delegate void PostEventToWindowEventHandler(Event eventToPost);

	public StarSystem()
	{
		this.Type = "";
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InitialLight = GetNode<PointLight2D>("GreenLight");
		IsScanned = false;
		_systemSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_mouseOver = false;	
		Light = GetNode<PointLight2D>("PointLight2D");
		Light.Color = Color.FromHtml("a8e2ec");
		//Light.Enabled = false;
		ConnectToEventViewer();	
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

	public void MouseExit()	//called when mouse leaves the star system
	{
		_mouseOver = false;
	}

	public void CheckClick()		//called when the mouse is clicking on the star system
	{
		if(Input.IsActionJustPressed("click") && _mouseOver && StateManager.Instance.State == "SystemSelection")
		{
			Clicked();
		}
	}

	public void Clicked()		//when clicked shows the travel button
	{
		if(StateManager.Instance.State == "SystemSelection")
		{
			GD.Print($"Gamestate: {StateManager.Instance.State}");
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

	public void PlayerArrivedAtSystem() //called when the player ship arrives at the system
	{
		GD.Print($"From System: Player Arrived at System {this} at {GlobalPosition}");
		ShowScanButton();
	}

	public void ShowScanButton()//display the scan button
	{
		EmitSignal(nameof(ArrivedInSystem), this);
	}

	public void SystemScanned()	
	{
		GenerateType();
	}

	public void GenerateType()			//called when the system in scanned. generates a type
	{									//which will dictate which events it can roll
		GD.Print("Generating a type for this star system...");
		Random random = new Random();
		int typeInt = random.Next(1,101); //0 - 10
		GD.Print($"Random int for system type generation is: {typeInt}");
		if(typeInt < 24)
		{
			SetType("Belt");
			GD.Print("Setting type to belt");
		}
		if(typeInt >= 24 && typeInt <= 49)
		{
			SetType("DeadStar");
			GD.Print("Setting type to Dead Star");
		}
		if(typeInt > 49 && typeInt <= 69)
		{
			SetType("MStar");
			GD.Print("Setting type to MStar");
		}
		if(typeInt > 69 && typeInt < 95)
		{
			SetType("StarWithPlanets");
		}
		if(typeInt > 95)
		{
			SetType("Earthlike");
		}
		SetLightingByType();
		ShowEvent();
	}

	public void SetLightingByType()
	{
		InitialLight.Enabled = false;
		Light.Enabled = true;
		if(Type == "Belt")
		{
			Light.Color = Color.FromHtml("723a0e");
			Light.Energy = 3;
		}
		if(Type == "DeadStar")
		{
			Light.Color = Color.FromHtml("500900");
			Light.Energy = 3;
		}
		if(Type == "MStar")
		{
			Light.Color = Color.FromHtml("d22800");
			Light.Energy = 3;
		}
		if(Type == "StarWithPlanets")
		{
			Light.Color = Color.FromHtml("d22800");
			Light.Energy = 2;
		}
		if(Type == "Earthlike")
		{
			Light.Color = Color.FromHtml("d22800");
			Light.Energy = 0.2f;
		}
		

	}

	public void SetType(string type)		//sets the type and the sprite
	{
		Type = type;
		SetSprite();
	}

	public void SetSprite()		//sets the sprite 
	{
		_systemSprite.Animation = Type;
	}

	public void ShowEvent()		//shows the event window
	{
		Event eventToPost = EventBank.Instance.RollEvent(Type);
		EmitSignal(nameof(PostEventToWindow), eventToPost);
	}

	public void ConnectToEventViewer()	//connection to event window
	{
		var eventWindowGroup = GetTree().GetNodesInGroup("EventWindow");
		if(eventWindowGroup.Count > 0)
		{
			var eventWindow = eventWindowGroup[0] as EventWindow;
			if(eventWindow != null)
			{
				PostEventToWindow += eventWindow.LoadEvent;
				//GD.Print($"Connected System {this} to EventWindow");
			}
			else
			{
				GD.Print("Event window group item found but is not type EventWindow");
			}
		}
		else
		{
			GD.Print("EventWindow not found");
		}
	}



}
