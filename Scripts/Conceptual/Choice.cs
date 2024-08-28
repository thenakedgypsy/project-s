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
		foreach(string resource in ResourceChange.Keys)
		{
			int change = ResourceChange[resource];
			if(resource == "Fuel")
			{				
				ResourceManager.Instance.AdjustFuel(change);
				GD.Print($"Resource {resource} adjusted by a choice by {change}.");
			}
			else if(resource == "Morale")
			{
				ResourceManager.Instance.AdjustMorale(change);
				GD.Print($"Resource {resource} adjusted by a choice by {change}.");
			}
			else
			{
				GD.Print($"Resource {resource} not found to make change.");
			}
		}
	}
}
