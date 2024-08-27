using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class ResourceManager : Node
{

	public static ResourceManager Instance;
	public int Fuel;
	public int Morale;
	public int Supplies;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Fuel = 100;
		Morale = 100;
		Supplies = 100;
		Instance = this;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
