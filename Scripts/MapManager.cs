using Godot;
using System;
using MapGenerator;
public class MapManager : Node
{
    [Export]
    public int MapWidth, MapHeight;
    [Export]
    public bool DebugMode;
    [Export]
    public int Seed;
    public int[,] Map;

    [Signal]
    delegate void UpdateMapCells(int MapBorderX, int MapBorderY);

    TileMap CaveWalls;
    TileMap CaveFloor;
    MapGenerator.MapGenerator Generator;
    public override void _Ready()
    {
       Map = new int[MapWidth,MapHeight];

       CaveWalls = (TileMap)GetNode("CaveWalls");
       CaveFloor = (TileMap)GetNode("CaveFloor");
       Generator = new MapGenerator.MapGenerator(MapWidth,MapHeight,Seed);
        if (!DebugMode)
        {
            ResetMap();
        }

  
        GD.Print("MapManagerReady");
    }

    public void GenerateMap(){
    
     ResetMap();
     Seed = ((TextEdit)GetNode("../LightUI/Seed")).Text.GetHashCode();
     GD.Print("Generating Map with seed: ", Seed, "...");
     
     
     Map = Generator.GenerateMap(Seed);

        for (int y = 0; y < MapHeight; y++)
        {
            for (int x = 0; x < MapWidth; x++)
            {
                if (Map[x,y] == 1)
                {
                   CaveWalls.SetCell(x,y,6); 
                }else if (Map[x,y] == 0)
                {
                    CaveWalls.SetCell(x,y,-1); 
                }
                
                
            }
        }
     GD.Print("Map generated!");
    }

    public void UpdateMap(){

        EmitSignal(nameof(UpdateMapCells), MapWidth, MapHeight);
    }


    public void ResetMap(){

        

        for (int y = 0; y < MapHeight; y++)
        {
            for (int x = 0; x < MapWidth; x++)
            {
                CaveWalls.SetCell(x,y,-1);
                CaveFloor.SetCell(x,y,0);

            }
        }
    }
//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
