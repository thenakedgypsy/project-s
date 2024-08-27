using Godot;
using System;

public partial class FuelTracker : CanvasLayer
{

	public TextureProgressBar Bar;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		Bar = GetNode<TextureProgressBar>("TextureProgressBar");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
			Bar.Value = ResourceManager.Instance.Fuel;		
	}
}
