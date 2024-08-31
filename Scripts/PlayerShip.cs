using Godot;
using System;


public partial class PlayerShip : Node2D
{

	private float _moveSpeed = 100f;
	private Vector2 _destination;
	private bool _atDestination;
	private StarSystem _currentSystem;
	private StarSystem _destinationSystem;
	private double _updateFrequency;
	private double _lastUpdateTime;
	private double _currentTime;
	private Vector2 _selectionZoom;
	private Vector2 _eventZoom;
	private Camera2D _camera;
	private PointLight2D _engineLight;
	private AnimatedSprite2D _sprite;
	private AudioStreamPlayer _engineSound;
	// Called when the node enters the scene tree for the first time.
	
	public override void _Ready()
	{
		_engineSound = GetNode<AudioStreamPlayer>("AnimatedSprite2D/AudioStreamPlayer");
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_engineLight = GetNode<PointLight2D>("AnimatedSprite2D/PointLight2D");
		_selectionZoom = new Vector2(0.9f, 0.9f);
		_eventZoom = new Vector2(1.8f,1.8f);
		_camera = GetNode<Camera2D>("Camera2D");
		_currentTime = 0d;
		_updateFrequency = 0.5d;
		_lastUpdateTime = 0d;
		_destinationSystem = null;
		_currentSystem = null;
		_atDestination = false;
		SetDestination(this.GlobalPosition,null);
		
	}


	public void UpdateCameraZoom(double delta)
	{
		
		string state = StateManager.Instance.State;
		if(state == "SystemSelection")
		{
			_camera.Zoom = _camera.Zoom.MoveToward(_selectionZoom, 1.2f * (float)delta);
		}
		if(state == "EventWindow")
		{
			_camera.Zoom = _camera.Zoom.MoveToward(_eventZoom, 1.2f * (float)delta);
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CheckPosition();
		_currentTime += delta;
		UpdateCameraZoom(delta);
				
	}

	public void MoveToSystem()		//move the ship to the destination system 
	{
		if(ResourceManager.Instance.Fuel > 0)
		{

			Vector2 currentPosition = GlobalPosition;
			Vector2 direction = (_destination - currentPosition).Normalized();
			Vector2 movement = direction * _moveSpeed * (float)GetProcessDeltaTime();
			GlobalPosition = GlobalPosition.MoveToward(_destination, movement.Length());
			if (movement != Vector2.Zero) //still in motion
    		{
				_sprite.Animation = "moving";
				_engineLight.Enabled = true;
				
    		    Rotation = Mathf.Atan2(movement.Y, movement.X);
				if(_currentTime - _lastUpdateTime >= _updateFrequency)
				{
					_lastUpdateTime = _currentTime;
					ResourceManager.Instance.Fuel -= 1;
				}
    		}
			else if(_destinationSystem != null)	//has arrived at destination
			{
				_engineSound.Stop();
				_sprite.Animation = "waiting";
				_engineLight.Enabled = false;
				GD.Print($"Destination {_destination} Reached! New Position: {GlobalPosition}");
				_atDestination = true;
				_currentSystem = _destinationSystem;
				_destinationSystem = null;
				GD.Print($"Now orbiting system: {_currentSystem}");
				_currentSystem.PlayerArrivedAtSystem();
			}
		}
	}

	public void SetDestination(Vector2 destination, StarSystem system)
	{
		
		_destination = destination;
		if(_destination != GlobalPosition)
		{
			_engineSound.Play();
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
