using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class StateManager : Node
{

	public static StateManager Instance;

	public string State;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		State = "SystemSelection";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
