using Godot;
using System;

public partial class MoraleTracker : CanvasLayer
{

	public TextureProgressBar Bar;
	public RichTextLabel Label;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		Bar = GetNode<TextureProgressBar>("TextureProgressBar");
		Label = GetNode<RichTextLabel>("RichTextLabel");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateLabel();
		Bar.Value = ResourceManager.Instance.Morale;		
	}

	public void UpdateLabel()
	{
		int currentMorale = ResourceManager.Instance.Morale;
		Label.Text = $"Morale: {currentMorale}%";
	}
}
