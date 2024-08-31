using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class ResourceManager : Node
{

	public static ResourceManager Instance;
	public int Fuel;
	public int MaxFuel;
	public int Morale;
	public int Supplies;
	public int MaxSupplies;
	public string GameOverMessage;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameOverMessage = "";
		Fuel = 100;
		Morale = 100;
		Supplies = 50;
		MaxSupplies = 100;
		Instance = this;		
	}

	public override void _Process(double delta)
	{
		CheckAll();
	}

	public void AdjustFuel(int value)
	{
		Fuel += value;
		if(Fuel > 100)
		{
			Fuel = 100;
		}
		if(Fuel < 0)
		{
			Fuel = 0;
		}
	}

	public void AdjustMorale(int value)
	{
		Morale += value;
		if(Morale > 100)
		{
			Morale = 100;
		}
		if(Morale < 0)
		{
			Morale = 0;
		}
	}

	public void AdjustSupplies(int value)
	{
		Supplies += value;
		if(Supplies > MaxSupplies)
		{
			Supplies = MaxSupplies;
		}
		if(Supplies < 0)
		{
			Supplies = 0;
		}
	}

	public void CheckAll()
	{
		if(Morale == 0)
		{
			GameOverMessage = "Mutiny! The endless void of space and mounting failures have taken their toll. Whispers of discontent turn to shouts of rebellion. Before you can react, the crew storms the bridge. Your command ends not with a bang, but with the cold click of restraints around your wrists.";
			reset();
			Error error = GetTree().ChangeSceneToFile("res://Scenes/GameOver.tscn");
       		if (error != Error.Ok)
       		{
       		    GD.PrintErr($"Failed to change scene. Error code: {error}");
       		}
		}
		if(Fuel == 0)
		{
			GameOverMessage = "The engines sputter and die. Silence engulfs the ship as you drift aimlessly through the cosmos. Your distress beacon blinks feebly, but in the vastness of space, hope fades with each passing day. The stars, once a guide, now mock your eternal, helpless journey.";
			reset();
			Error error = GetTree().ChangeSceneToFile("res://Scenes/GameOver.tscn");
       		if (error != Error.Ok)
       		{
       		    GD.PrintErr($"Failed to change scene. Error code: {error}");
       		}
		}
		if(Supplies == 0)
		{
			GameOverMessage = "The food stores dwindle to nothing. Rationing turns to desperation as hollow-eyed crew members eye each other warily. In the end, it's the gnawing emptiness in your stomach that defeats you, not the perils of space. Your mission ends in quiet whimpers of hunger.";
			reset();
			Error error = GetTree().ChangeSceneToFile("res://Scenes/GameOver.tscn");
       		if (error != Error.Ok)
       		{
       		    GD.PrintErr($"Failed to change scene. Error code: {error}");
       		}
		}

	}

	public void reset()
	{
		Fuel = 100;
		Morale = 100;
		Supplies = 50;
		MaxSupplies = 100;
	}
}
