using Godot;
using System;


public partial class ScanButton : Area2D
{

	private bool _mouseOver;
	private TextureRect _textureHolder;
	private Texture2D _selectedTexture;
	private Texture2D _unSelectedTexture;
	private bool _justAppeared;
	private StarSystem _attachedSystem;
	private AudioStreamPlayer _clickSound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_clickSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		_justAppeared = false;
		_mouseOver = false;
		Visible = false;
		_textureHolder = GetNode<TextureRect>("TextureHolder");
		_selectedTexture = ResourceLoader.Load<Texture2D>("res://Assets/Sprites/UI/Buttons/Scan_Selected.png");
		_unSelectedTexture = ResourceLoader.Load<Texture2D>("res://Assets/Sprites/UI/Buttons/Scan_UnSelected.png");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CheckClick();
	}

	public void PlayerArrivedInSystem(StarSystem attached)
	{
		GD.Print("Scan Button Displaying due to player arrival");
		_attachedSystem = attached;
		if(!_attachedSystem.IsScanned)
		{
			Visible = true;	
			_justAppeared = true;
		}	
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
		_clickSound.Play();
		GD.Print("Scan clicked!");
		Visible = false;
		_attachedSystem.IsScanned = true;
		_attachedSystem.GenerateType();
	}

}
