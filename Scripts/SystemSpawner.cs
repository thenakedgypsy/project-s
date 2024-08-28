using Godot;
using System;


public partial class SystemSpawner : Node2D
{
	
	public PackedScene System;
	[Export]
	public int NumberTospawn = 5;
	[Export]
	public Vector2 SpawnAreaSize = new Vector2(1920,1080);
	

	public override void _Ready()
	{
		System = ResourceLoader.Load("res://Prefabs/StarSystem.tscn") as PackedScene;
		if(System == null)
		{
			GD.Print("res://Prefabs/StarSystem.tscn failed to load");
		}
		SpawnSystems();
	}

	private void SpawnSystems()
	{
		Random random = new Random();

		for(int i = 0; i < NumberTospawn; i++)
		{

			StarSystem system = System.Instantiate() as StarSystem;

			if(system != null)
			{
				float x = (float)random.NextDouble() * SpawnAreaSize.X;
				float y = (float)random.NextDouble() * SpawnAreaSize.Y;

				system.Position = new Vector2(x,y);

				AddChild(system);
			}
			else
			{
				GD.Print("Null System spawned");
			}

		}
	}

}
