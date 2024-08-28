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

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Fuel = 100;
		Morale = 100;
		Supplies = 100;
		MaxSupplies = 100;
		Instance = this;		
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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
