using Godot;
using System;


public partial class PlayerShip : Node2D
{

	private float _moveSpeed = 100f;
	private Vector2 _destination;
	private bool _atDestination;
	private StarSystem _currentSystem;
	private StarSystem _destinationSystem;
	// Called when the node enters the scene tree for the first time.
	
	public override void _Ready()
	{
		_destinationSystem = null;
		_currentSystem = null;
		_atDestination = false;
		SetDestination(this.GlobalPosition,null);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CheckPosition();		
	}

	public void MoveToSystem()
	{
		Vector2 currentPosition = GlobalPosition;
		Vector2 direction = (_destination - currentPosition).Normalized();
		Vector2 movement = direction * _moveSpeed * (float)GetProcessDeltaTime();
		GlobalPosition = GlobalPosition.MoveToward(_destination, movement.Length());
		if (movement != Vector2.Zero) //still in motion
    	{
    	    Rotation = Mathf.Atan2(movement.Y, movement.X);
    	}
		else if(_destinationSystem != null)	//has arrived at destination
		{
			GD.Print($"Destination {_destination} Reached! New Position: {GlobalPosition}");
			_atDestination = true;
			_currentSystem = _destinationSystem;
			_destinationSystem = null;
			GD.Print($"Now orbiting system: {_currentSystem}");
			_currentSystem.PlayerArrivedAtSystem();
		}
	}

	public void SetDestination(Vector2 destination, StarSystem system)
	{
		
		_destination = destination;
		if(_destination != GlobalPosition)
		{
			GD.Print($"Destination: {_destination} different to current Position: {GlobalPosition}");
			_destinationSystem = system;
			_atDestination = false;
		}
	}

	public void CheckPosition()
	{
		if(!_atDestination)
		{
			MoveToSystem();
		}
	}

	
	
}
