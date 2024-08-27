using Godot;
using System;


public partial class PlayerShip : Node2D
{

	private float _moveSpeed = 100f;
	private Vector2 _destination;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetDestination(this.GlobalPosition);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MoveToSystem();
	}

	public void MoveToSystem()
	{
		Vector2 currentPosition = GlobalPosition;
		Vector2 direction = (_destination - currentPosition).Normalized();
		Vector2 movement = direction * _moveSpeed * (float)GetProcessDeltaTime();
		GlobalPosition = GlobalPosition.MoveToward(_destination, movement.Length());
		if (movement != Vector2.Zero)
    	{
    	    Rotation = Mathf.Atan2(movement.Y, movement.X);
    	}
	}

	public void SetDestination(Vector2 destination)
	{
		_destination = destination;
	}

	
	
}
