using Godot;
using System;
using System.Collections.Generic;

public partial class Choice : Node
{
	public string Text;
	public Dictionary<string, int> ResourceChange;

	public Choice(string text, Dictionary<string,int> change)
	{
		Text = text;
		ResourceChange = change;
		
	}

	public void EnactChoice()
	{
		
	}


}
