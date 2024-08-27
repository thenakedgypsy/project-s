using Godot;
using System;


public partial class TravelButton : Area2D
{

	private bool _mouseOver;
	private Vector2 _systemPosition;
	private TextureRect _textureHolder;
	private Texture2D _selectedTexture;
	private Texture2D _unSelectedTexture;
	private bool _justAppeared;
	private StarSystem _attachedSystem;

	[Signal]
	public delegate void TravelClickedEventHandler(Vector2 destination, StarSystem attached);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_justAppeared = false;
		_mouseOver = false;
		Visible = false;
		_textureHolder = GetNode<TextureRect>("TextureHolder");
		_selectedTexture = ResourceLoader.Load<Texture2D>("res://Assets/Sprites/UI/Buttons/Travel_Selected.png");
		_unSelectedTexture = ResourceLoader.Load<Texture2D>("res://Assets/Sprites/UI/Buttons/Travel_UnSelected.png");
		ConnectToPlayer();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CheckClick();
	}

	public void SystemClicked(Vector2 systemPos, StarSystem attached)
	{
		_attachedSystem = attached;
		_systemPosition = systemPos;
		Visible = true;	
		_justAppeared = true;	
	}

	public void MouseOver()
	{
		_textureHolder.Texture = _selectedTexture;
		_mouseOver = true;			
	}

	public void MouseExit()
	{
		_textureHolder.Texture = _unSelectedTexture;
		_mouseOver = false;
	}

		public void CheckClick()		//called when the mouse is clicking on the travel button
	{
		if(Input.IsActionJustPressed("click"))
		{
			if(_mouseOver)
			{
				Clicked();
			}
			else if(_justAppeared)
			{
				_justAppeared = false;
				return;
			}
			else
			{
				Visible = false;
			}
		}
		
	}

	public void Clicked()
	{
		GD.Print("Travel clicked emitting signal to player");
		EmitSignal(nameof(TravelClicked), _systemPosition, _attachedSystem);
		Visible = false;
	}

		public void ConnectToPlayer()
	{
		var playerGroup = GetTree().GetNodesInGroup("Player");
		if(playerGroup.Count > 0)
		{
			var player = playerGroup[0] as PlayerShip;
			if(player != null)
			{
				TravelClicked += player.SetDestination;
				GD.Print($"Connected System {this} to Player");
			}
			else
			{
				GD.Print("Player group item found but is not type PlayerShip");
			}
		}
		else
		{
			GD.Print("Player not found");
		}
	}
}
