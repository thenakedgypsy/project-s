using Godot;
using System;

public partial class EventWindow : CanvasLayer
{

	public RichTextLabel TitleLabel;
	public RichTextLabel BodyLabel;
	public Button ButtonA;
	public Button ButtonB;
	public Choice ChoiceA;
	public Choice ChoiceB;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TitleLabel = GetNode<RichTextLabel>("Window/Title");
		BodyLabel = GetNode<RichTextLabel>("Window/Body");
		ButtonA = GetNode<Button>("ChoiceBackA/ChoiceButtonA");
		ButtonB = GetNode<Button>("ChoiceBackB/ChoiceButtonB");
		Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void LoadEvent(Event eventToLoad)	//event comes in from system (generatetype)
	{
		if(eventToLoad == null)
		{
			GD.Print("Null event receieved by Window");
			return;
		}
		GD.Print("Loading event to Window");
		TitleLabel.Text = eventToLoad.Title;
		BodyLabel.Text = eventToLoad.Body;
		ShowEventWindow();
		ChoiceA = eventToLoad.ChoiceA;
		ChoiceB = eventToLoad.ChoiceB;
		PopulateChoiceButtons();
	}

	public void ShowEventWindow()
	{
		StateManager.Instance.EventWindow();
		Visible = true;
	}

	public void HideEventWindow()
	{
		StateManager.Instance.SystemSelection();
		Visible = false;
	}

	public void PopulateChoiceButtons()
	{
		ButtonA.Text = ChoiceA.Text;
		ButtonB.Text = ChoiceB.Text;
	}

	public void ButtonAPressed()
	{
		ChoiceA.EnactChoice();
		HideEventWindow();
	}

	public void ButtonBPressed()
	{
		ChoiceB.EnactChoice();
		HideEventWindow();
	}
}
