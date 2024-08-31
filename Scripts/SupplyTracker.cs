using Godot;
using System;

public partial class SupplyTracker : CanvasLayer
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
		Bar.Value = ResourceManager.Instance.Supplies;		
	}

	public void UpdateLabel()
	{
		int currentSupplies = ResourceManager.Instance.Supplies;
		int storage = ResourceManager.Instance.MaxSupplies;
		Label.Text = $"Supplies: {currentSupplies} / {storage}";
	}
}
